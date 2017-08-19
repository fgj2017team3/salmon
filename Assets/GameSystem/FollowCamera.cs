﻿// FollowCamera.cs 
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
	Transform _t; 						// 高速化用Transform 

	[SerializeField]Transform target;	// 追いかける対象 



	//--------------------------------------------------------------------------------
	// コンストラクタ 
	//--------------------------------------------------------------------------------
	void Awake ()
	{
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
}