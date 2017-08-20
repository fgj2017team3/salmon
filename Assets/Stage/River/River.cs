// River.cs 
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
	Transform _t;		// 高速化 
	Transform transCam;	// メインカメラ取得 

	[SerializeField]FX prefLightFX;		//@@ 川のキラキラしたエフェクト 
	int loopCount = 0;					// ループ回数 

	[SerializeField]GameObject[] width = new GameObject[3];	//@@ 段階ごとの川幅 



	//--------------------------------------------------------------------------------
	// コンストラクタ 
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		_t = this.gameObject.transform;
	}



	//--------------------------------------------------------------------------------
	// セットアップ 
	//--------------------------------------------------------------------------------
	public void Setup (Transform cam, float y)
	{
		transCam = cam;;

		_t.position = new Vector3(_t.position.x, y, _t.position.z);
		width[0].SetActive(true);
	}



	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	void Update ()
	{
		if(transCam.position.y > _t.position.y+240+32){
			_t.position = new Vector3(0, _t.position.y+480+64, 0);

			// キラキラを0～2つ作る 
			// 乱数にマイナスを含めることで0個になる確率をあげている 
			int num = Random.Range(-5, 3);
			for(int i=0; i<num; i++){
				ShowFx();
			}

			// 川幅の変更チェック 
			if(loopCount == 4){
				width[0].SetActive(false);
				width[1].SetActive(true);
			}
			else if(loopCount == 10){
				width[1].SetActive(false);
				width[2].SetActive(true);
			}

			// ループ回数を増やす 
			loopCount++;
		}
	}



	//--------------------------------------------------------------------------------
	// キラキラエフェクトの生成(左右の位置はランダム) 
	//--------------------------------------------------------------------------------
	void ShowFx ()
	{
		FX light = Instantiate<FX>(prefLightFX);
		light.Setup(transCam, Random.Range(-200, 200), _t.position.y);
	}



	//--------------------------------------------------------------------------------
	// 川幅の取得 
	//--------------------------------------------------------------------------------
	public float GetWidth ()
	{
		if(width[0].activeSelf){
			return 200-32;
		}
		else if(width[1].activeSelf){
			return 200;
		}
		else if(width[2].activeSelf){
			return 200+32;
		}

		return -1;
	}
}
