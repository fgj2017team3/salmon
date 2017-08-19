//------------------------------------------------------------------------------------------------------------------------
// FCEI.cs
//
// @idev Unity5.1.4f1 / MonoDevelop4.0.1
// @auth FCEI.No-Va
// @date 2017/02/02
//
// Copyright (C)2017 FlyteCatEmotion Inc.
// All Rights Reserved. 
//------------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System;

//----------------------------------------------------------------------------------------------------
// FCEI製の便利機能をまとめたもの 
//----------------------------------------------------------------------------------------------------
namespace FCEI
{
	//--------------------------------------------------------------------------------
	// 便利メソッド集 
	//--------------------------------------------------------------------------------
	public class Util
	{
		//----------------------------------------
		// @description
		//		特定の条件になるまで待って処理を実行する 
		// @param
		//		System.Func<bool> IsOK : 待ちが終了する条件 
		//		System.Action Callback : 待ちが終了後に実行する処理 
		public static void WaitFunc (System.Func<bool> IsOK, System.Action Callback, string name="", bool isDontDestroyOnLoad=true)
		{
			string objName;
			if(string.IsNullOrEmpty(name)){
				objName = "WaitFunc[" + IsOK.ToString() + "=>" + Callback.ToString() + "]";
			}
			else{
				objName = name;
			}
			GameObject obj = new GameObject(objName);
			if(isDontDestroyOnLoad){ GameObject.DontDestroyOnLoad(obj); }
			MonoCotourine mono = obj.AddComponent<MonoCotourine>();
			Callback += () => {
				GameObject.Destroy(obj);
			};
			mono.StartCoroutine(WaitFuncCore(IsOK, Callback));
		}
		static IEnumerator WaitFuncCore (System.Func<bool> IsOK, System.Action Callback)
		{
			while(IsOK() == false){
				yield return null;
			}
			Callback();
		}



		//----------------------------------------
		// @description
		//		一定時間待って処理を実行する 
		// @param
		//		float time : 待ち時間 
		//		System.Action Callback : 待ちが終了後に実行する処理 
		public static void WaitTimer (float time, System.Action Callback, string name="", bool isDontDestroyOnLoad=true)
		{
			string objName;
			if(string.IsNullOrEmpty(name)){
				objName = "WaitTimer["+time.ToString()+"=>"+Callback.ToString()+"]";
			}
			else{
				objName = name;
			}
			GameObject obj = new GameObject(objName);
			if(isDontDestroyOnLoad){ GameObject.DontDestroyOnLoad(obj); }
			MonoCotourine mono = obj.AddComponent<MonoCotourine>();
			Callback += () => {
				GameObject.Destroy(obj);
			};
			mono.StartCoroutine(WaitTimerCore(time, Callback));
		}
		static IEnumerator WaitTimerCore (float time, System.Action Callback)
		{
			yield return new WaitForSeconds(time);
			Callback();
		}



		//----------------------------------------
		// @description
		//		二点間の距離を返す  
		// @param
		//		float x1, float y1, float x2, float y2 : それぞれの位置 
		//		Vector2 pos1, Vector2 pos2 : それぞれの位置(Vector2で指定も可能) 
		public static float GetRange (Vector2 pos1, Vector2 pos2)
		{
			return GetRange(pos1.x, pos1.y, pos2.x, pos2.y);
		}
		public static float GetRange (float x1, float y1, float x2, float y2)
		{
			return Mathf.Sqrt((x1-x2)*(x1-x2) + (y1-y2)*(y1-y2));
		}



		//----------------------------------------
		// @description
		//		ボタンの入力(マウス/タッチ)を取得 
		public static bool GetPointerInput (bool isTrigger=false)
		{
			if(isTrigger){
				return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0);
			}
			else{
				return Input.touchCount > 0 || Input.GetMouseButton(0);
			}
		}
		public static Vector3 GetPointerPosition ()
		{
			if(Input.touchCount > 0){
				return new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
			}
			else{
				return Input.mousePosition;
			}
		}
		public static Vector3 GetPointerPositionOnScreen (Camera cam=null)
		{
			if(cam == null){
				cam = Camera.main;
			}

			return cam.ScreenToWorldPoint(GetPointerPosition());
		}



		//----------------------------------------
		// @description
		//		指定したTransformのHierarchy上のディレクトリを文字列で返す 
		public static string GetDirectryInHierarchy(Transform transform)
		{
			if(transform.parent != null){
				return GetDirectryInHierarchy(transform.parent) + " > " + transform.gameObject.name;
			}
			else{
				return transform.gameObject.name;
			}
		}



		//----------------------------------------
		// @description
		//		指定したTransformの子のリストを文字列で返す  
		public static string GetAllChildInHierarchy(Transform transform, int deep=0, string tab="\t")
		{
			string ret = "";

			// 深さ 
			for(int i=0; i<deep; i++){
				ret += tab;
			}

			if(transform.childCount > 0){
				ret += transform.gameObject.name;

				for(int i=0; i<transform.childCount; i++){
					ret += "\n"+GetAllChildInHierarchy(transform.GetChild(i), deep+1);
				}
			}
			else{
				ret += transform.gameObject.name;
			}

			return ret;
		}
	}



	//----------------------------------------------------------------------------------------------------
	// MonoBehaviour
	//----------------------------------------------------------------------------------------------------
	public class MonoCotourine : MonoBehaviour
	{

	}



	//----------------------------------------------------------------------------------------------------
	// Enumのパラメータを取得する便利クラス(現在長さのみ対応) 
	//----------------------------------------------------------------------------------------------------
	public class Enums<T> where T : struct, IConvertible
	{
		public static readonly int Length = Enum.GetValues(typeof(T)).Length;
	}
}

