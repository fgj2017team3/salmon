﻿// River.cs 
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
// 川の流れ早いところを制御  
//----------------------------------------------------------------------------------------------------
public class River : MonoBehaviour
{
	//--------------------------------------------------------------------------------
	// メンバ変数  
	//--------------------------------------------------------------------------------
	Transform _t;	// 高速化 



	//--------------------------------------------------------------------------------
	// コンストラクタ 
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		_t = this.gameObject.transform;
	}


	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	void Update ()
	{
		
	}
}
