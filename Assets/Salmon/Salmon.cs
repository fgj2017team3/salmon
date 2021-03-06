﻿// Salmon.cs 
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
using UnityEngine.SceneManagement;
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

	//-------- 内部パラメータ --------//
	public int resilient;						// 活力 
	float time;									// 経過時間 

	//-------- 表示 --------//
	[SerializeField]SpriteRendererIndexer sprite;	// スプライト 

	//-------- UI制御 --------//
	[SerializeField]Gauge    gauge;				// 活力ゲージ 
	[SerializeField]Distance distance;			// ゴールまでの距離 



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
		time = 0;

		_t.position = Vector3.zero;
	}



	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	void Update ()
	{
		// フェード中は入力できない 
		if(FadeManager.isPlaying){ return; }

		// 入力 
		bool isInput = false;
		float X=_t.position.x;
		float Y=_t.position.y;

        // kuny@ccraftsmen.jp ゲームパッドアナログスティック対応
		if(Input.GetKey(KeyCode.UpArrow) || (Input.GetAxis("Vertical") >= 0.5))
        {
			isInput = true;
			X = _t.position.x;
			Y = _t.position.y + GetSpeed();

			if(Stone.CheckStones(X-32, Y+64) || Stone.CheckStones(X, Y+64) || Stone.CheckStones(X+32, Y+64)){
				//Y--;
			}
			else{
				_t.position = new Vector3(X, Y, _t.position.z);
			}
		}
		if(Input.GetKey(KeyCode.DownArrow) || (Input.GetAxis("Vertical") <= -0.5))
        {
			isInput = true;
			X = _t.position.x;
			Y = _t.position.y - GetSpeed();

			if(Stone.CheckStones(X-32, Y-64) || Stone.CheckStones(X, Y-64) || Stone.CheckStones(X+32, Y-64)){
				//Y++;
			}
			else{
				_t.position = new Vector3(X, Y, _t.position.z);
			}
		}
		if(Input.GetKey(KeyCode.LeftArrow) || (Input.GetAxis("Horizontal") <= -0.5))
        {
			isInput = true;
			X = _t.position.x - GetSpeed();
			Y = _t.position.y;
	
			if(Stone.CheckStones(X-32, Y+64)
			   || Stone.CheckStones(X-32, Y+32)
			   || Stone.CheckStones(X-32, Y   )
			   || Stone.CheckStones(X-32, Y-32)
			   || Stone.CheckStones(X-32, Y-64)
			   || X < -200){
				//X++;
			}
			else{
				_t.position = new Vector3(X, Y, _t.position.z);
			}
		}
		if(Input.GetKey(KeyCode.RightArrow) || (Input.GetAxis("Horizontal") >= 0.5))
        {
			isInput = true;
			X = _t.position.x + GetSpeed();
			Y = _t.position.y;

			if(Stone.CheckStones(X+32, Y+64)
			   || Stone.CheckStones(X+32, Y+32)
			   || Stone.CheckStones(X+32, Y   )
			   || Stone.CheckStones(X+32, Y-32)
			   || Stone.CheckStones(X+32, Y-64)
			   || X > 200){
				//X--;
			}
			else{
				_t.position = new Vector3(X, Y, _t.position.z);
			}
		}

        // kuny@ccraftsmen.jp ゲームパッドボタン対応
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")){
			SoundManager.PlaySE(SoundManager.SE.ATTACK);
			Stone.Attack(_t.position.x   , _t.position.y+72, true , true);
			Stone.Attack(_t.position.x-40, _t.position.y+72, false, true);	
			Stone.Attack(_t.position.x+40, _t.position.y+72, true , true);	
			Stone.Attack(_t.position.x-40, _t.position.y+64, false, true);	
			Stone.Attack(_t.position.x+40, _t.position.y+64, true , true);	
			Stone.Attack(_t.position.x-40, _t.position.y+48, false, true);	
			Stone.Attack(_t.position.x+40, _t.position.y+48, true , true);	
			Stone.Attack(_t.position.x-40, _t.position.y+32, false, true);	
			Stone.Attack(_t.position.x+40, _t.position.y+32, true , true);	
		}



		// 減衰 
		if(time > 0.5f){
			Decline((int)_t.position.y/3000+1);
			time -= 0.5f;
		}

		// ゴール判定 
		//if(Stage.DISTANCE - (int)_t.position.y/10 < 0){
		//	Debug.Log("CLEAR");
		//}

		// しゃけの表示切り替え 
		{
			if(isInput){
				sprite.index = ((int)(time*15) % 2 == 0 ? 2 : 1);
			}
			else{
				sprite.index = 0;
				sprite.isFlipX = (int)(time*5) % 2 == 0;
			}
		}

		// debug //
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Fire2")){
			SoundManager.PlaySE(SoundManager.SE.RECOVERY);
			resilient = MAX_RESILIENT;
		}
		// debug end //

		// UI表示の更新 
		gauge.val = (float)resilient / 100;
		distance.num = Stage.DISTANCE - (int)_t.position.y/10;

		// 時間経過を取得 
		time += Time.deltaTime;
	}



	//--------------------------------------------------------------------------------
	// 徐々に弱る 
	//--------------------------------------------------------------------------------
	public void Decline (int val)
	{
		resilient -= val;

		if(resilient <= 0){
			SoundManager.PlaySE(SoundManager.SE.DAMAGE);
			SoundManager.StopBGM();
			SceneManager.LoadScene("gameover");
			Debug.Log("GameOver");
		}
	}



	//--------------------------------------------------------------------------------
	// 活力による速度の算出 
	//--------------------------------------------------------------------------------
	float GetSpeed ()
	{
		if(resilient <= 0){ return 0; }

		return Mathf.Sqrt(resilient)/4+0.25f;
	}



	//--------------------------------------------------------------------------------
	// 定数 
	//--------------------------------------------------------------------------------
	const int MAX_RESILIENT = 100;
}
