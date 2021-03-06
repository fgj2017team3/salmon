﻿// Stage.cs 
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
// ステージの配置 
//----------------------------------------------------------------------------------------------------
public class Stage : MonoBehaviour
{
	//--------------------------------------------------------------------------------
	// メンバ変数 
	//--------------------------------------------------------------------------------
	Transform _t;

	[SerializeField]Stone      prefStone;	// 石のプレハブ 
	[SerializeField]River      prefRiver;	// 川のプレハブ(弱い順に) 
	[SerializeField]Transform  transCam;	// カメラの位置を補足しておく 
	[SerializeField]GameObject prefGoal;	// ゴール 



	//--------------------------------------------------------------------------------
	// コンストラクタ 
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		_t = this.gameObject.transform;
	}



	//--------------------------------------------------------------------------------
	// 最初に実行 
	//--------------------------------------------------------------------------------
	void Start ()
	{
		// BGMはランダム 
		SoundManager.BGM bgmID = (SoundManager.BGM)Random.Range((int)SoundManager.BGM.STAGE1, (int)SoundManager.BGM.STAGE3+1); 
		SoundManager.PlayBGM(bgmID);

		FadeManager.FadeIn(0.5f);

		// 背景設置 
		for(int i=0; i<9; i++){
			River river = Instantiate<River>(prefRiver);
			river.Setup(transCam, (i-4)*64);
		}
	}



	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	void Update ()
	{
		// ゴールを生成 
		if(transCam.position.y >= 10000-64*2+128){
			GameObject obj = Instantiate(prefGoal);
			obj.transform.localPosition = new Vector3(0, 10000+64*4, 0);
			obj.transform.localRotation = Quaternion.identity;
			obj.transform.localScale = Vector3.one;
			this.gameObject.SetActive(false);
			return;
		}
		// この辺から何も生成しない 
		else if(transCam.position.y >= 10000-64*8){
			return;
		}

		if(transCam.position.y > _t.position.y){
			for(int i=0; i<60; i++){
				SpawnStone();
			}
			_t.localPosition = new Vector3(
				_t.localPosition.x,
				_t.localPosition.y + SCREEN_HEIGHT,
				_t.localPosition.z
			);
		}
	}



	//--------------------------------------------------------------------------------
	// 石のランダム生成  
	//--------------------------------------------------------------------------------
	void SpawnStone ()
	{
		Stone stone = Instantiate<Stone>(prefStone);
		float randfX = Random.Range(0, RIVER_WIDTH/2);
		float randfY = Random.Range(0, SCREEN_HEIGHT);

		// 両端に寄せる処理 
		{
			float threshold = Random.Range(0, RIVER_WIDTH/2); 
			if(threshold > randfX){ randfX = threshold; }
		}
		{
			float threshold = Random.Range(0, RIVER_WIDTH/2); 
			if(threshold > randfX){ randfX = threshold; }
		}

		// 左右に分ける 
		randfX = randfX * (Random.Range(0,2)==0 ? (1):(-1));

		// 石の出意をランダムに 
		Stone.SIZE size = (Stone.SIZE)Random.Range(0,3);
		stone.Setup(transCam, randfX, transCam.localPosition.y + SCREEN_HEIGHT + randfY, size);
	}

	//--------------------------------------------------------------------------------
	// 川のランダム生成  
	//--------------------------------------------------------------------------------
	void SpawnRiver ()
	{

	}



	//--------------------------------------------------------------------------------
	// 定数 
	//--------------------------------------------------------------------------------
	public const float SCREEN_HEIGHT = 480;	// スクリーンの縦幅 
	public const float RIVER_WIDTH   = 480;	// 川の横幅 
	public const int   DISTANCE      = 999;	// ゴールまでの距離 
}
