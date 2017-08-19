// Salmon.cs 
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
// しゃけの管理 
//----------------------------------------------------------------------------------------------------
public class Salmon : MonoBehaviour
{
	//--------------------------------------------------------------------------------
	// メンバ変数 
	//--------------------------------------------------------------------------------
	Transform _t; 	// 高速化用Transform 

	//-------- パラメータ --------//
	//public int resilient{ get; private set; }	// 活力 
	public int resilient;						// 活力 

	//-------- アタッチ --------//
	[SerializeField]Gauge gauge;				// UI 



	//--------------------------------------------------------------------------------
	// コンストラクタ 
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		// Transfomr保持 
		_t = this.gameObject.transform;

		Initialize();
	}



	//--------------------------------------------------------------------------------
	// 初期化  
	//--------------------------------------------------------------------------------
	void Initialize ()
	{
		resilient = MAX_RESILIENT;
		_t.position = Vector3.zero;
	}



	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	void Update ()
	{
		// 入力 
		if(Input.GetKey(KeyCode.UpArrow)){
			_t.position = new Vector3(_t.position.x, _t.position.y+GetSpeed(), _t.position.z);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			_t.position = new Vector3(_t.position.x, _t.position.y-GetSpeed(), _t.position.z);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			_t.position = new Vector3(_t.position.x-GetSpeed(), _t.position.y, _t.position.z);
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			_t.position = new Vector3(_t.position.x+GetSpeed(), _t.position.y, _t.position.z);
		}

		// 表示の更新 
		gauge.val = resilient;
	}



	//--------------------------------------------------------------------------------
	// 徐々に弱る 
	//--------------------------------------------------------------------------------
	public void Decline (int val)
	{
		resilient -= val;

		if(resilient <= 0){
			Debug.Log("GameOver");
		}
	}



	//--------------------------------------------------------------------------------
	// 活力による速度の算出 
	//--------------------------------------------------------------------------------
	float GetSpeed ()
	{
		return Mathf.Sqrt(resilient)+1;
	}



	//--------------------------------------------------------------------------------
	// 定数 
	//--------------------------------------------------------------------------------
	const int MAX_RESILIENT = 100;
}
