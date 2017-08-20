// FollowCamera.cs 
//
// @idev Unity2017.1.0f3 / MonoDevelop5.9.6
// @auth FCEI.No-Va
// @date 2017/08/19
//
// Copyright (C) 2017 FlyteCatEmotion Inc.
// All Rights Reserved.
//------------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------------------------------------------
// カメラが対象を追いかける(Y座標のみ) 
//----------------------------------------------------------------------------------------------------
public class FollowCamera : MonoBehaviour
{
	//--------------------------------------------------------------------------------
	// メンバ変数 
	//--------------------------------------------------------------------------------
	static FollowCamera instance;		// シングルトンインスタンス 

	Transform _t; 						// 高速化用Transform 

	[SerializeField]Transform target;	// 追いかける対象 



	//--------------------------------------------------------------------------------
	// コンストラクタ 
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		// シングルトン作成 
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else{
			GameObject.Destroy(this.gameObject);
		}

		// Transfomr保持 
		_t = this.gameObject.transform;
	}



	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	void Update ()
	{
		if(target.localPosition.y > _t.localPosition.y){
			_t.localPosition = new Vector3(
				_t.localPosition.x,
				_t.localPosition.y + 1,
				_t.localPosition.z
			);
		}
	}



	//--------------------------------------------------------------------------------
	// 画面揺らす 
	//--------------------------------------------------------------------------------
	public static void Shake ()
	{
		instance.StartCoroutine(instance.ShakeCore());	
	}
	IEnumerator ShakeCore ()
	{
		Transform parent = _t.parent;
		for(int i=8; i>=0; i--){
			float val = (float)i * (i%2==0 ? -1 : 1);
			parent.position = new Vector3(0, val, 0);
			yield return new WaitForSeconds(0.02f);
		}
	}
}
