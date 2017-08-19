// Stage.cs 
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

	[SerializeField]Stone     prefStone;	// 石のプレハブ 
	[SerializeField]River[]   prefRiver;	// 川のプレハブ(弱い順に) 
	[SerializeField]Transform transCam;		// カメラの位置を補足しておく 



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
		SoundManager.PlayBGM(SoundManager.BGM.STAGE1);
	}



	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	void Update ()
	{
		if(transCam.position.y > _t.position.y + 240){
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

		stone.Setup(transCam, randfX, transCam.localPosition.y + SCREEN_HEIGHT + randfY, Stone.SIZE.NORMAL);
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
	const float SCREEN_HEIGHT = 480;
	const float RIVER_WIDTH   = 480;
}
