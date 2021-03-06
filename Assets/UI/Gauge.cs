﻿// Gauge.cs 
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
// ゲージ管理 
//----------------------------------------------------------------------------------------------------
public class Gauge : MonoBehaviour
{
	//--------------------------------------------------------------------------------
	// メンバ変数 
	//--------------------------------------------------------------------------------
	Transform _t; 	// 高速化用Transform 

	[SerializeField]SpriteRenderer gaugeSprite;	// メインゲージ 
	float maxScale;								// 最大時のスケール 

	[HideInInspector]public float val;			// ゲージが参照する値(0.0f-1.0f) 



	//--------------------------------------------------------------------------------
	// コンストラクタ 
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		// Transfomr保持 
		_t = gaugeSprite.gameObject.transform;

		// デフォルト値の設定 
		maxScale = _t.localScale.x;
	}



	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	void Update ()
	{
		val = Mathf.Clamp(val, 0.0f, 1.0f);

		_t.localScale    = new Vector3(maxScale * val, 16, 16);
		_t.localPosition = new Vector3(-maxScale * (1-val) / 2, 0, 0);
	}
}
