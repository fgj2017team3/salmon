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
	[SerializeField]Gauge gauge;				// 活力ゲージ 



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
		// 入力 
		bool isInput = false;
		float X=_t.position.x;
		float Y=_t.position.y;

		if(Input.GetKey(KeyCode.UpArrow)){
			isInput = true;
			X = _t.position.x;
			Y = _t.position.y + GetSpeed();

			if(Stone.CheckStones(X-32, Y+64) || Stone.CheckStones(X+32, Y+64)){
				//Y--;
			}
			else{
				_t.position = new Vector3(X, Y, _t.position.z);
			}
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			isInput = true;
			X = _t.position.x;
			Y = _t.position.y - GetSpeed();

			if(Stone.CheckStones(X-32, Y-64) || Stone.CheckStones(X+32, Y-64)){
				//Y++;
			}
			else{
				_t.position = new Vector3(X, Y, _t.position.z);
			}
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			isInput = true;
			X = _t.position.x - GetSpeed();
			Y = _t.position.y;
	
			if(Stone.CheckStones(X-32, Y+64)
			   || Stone.CheckStones(X-32, Y+32)
			   || Stone.CheckStones(X-32, Y   )
			   || Stone.CheckStones(X-32, Y-32)
			   || Stone.CheckStones(X-32, Y-64)
			   || X < -240){
				//X++;
			}
			else{
				_t.position = new Vector3(X, Y, _t.position.z);
			}
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			isInput = true;
			X = _t.position.x + GetSpeed();
			Y = _t.position.y;

			if(Stone.CheckStones(X+32, Y+64)
			   || Stone.CheckStones(X+32, Y+32)
			   || Stone.CheckStones(X+32, Y   )
			   || Stone.CheckStones(X+32, Y-32)
			   || Stone.CheckStones(X+32, Y-64)
			   || X > 240){
				//X--;
			}
			else{
				_t.position = new Vector3(X, Y, _t.position.z);
			}
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			SoundManager.PlaySE(SoundManager.SE.ATTACK);
			Stone.Attack(_t.position.x   , _t.position.y+72);
			Stone.Attack(_t.position.x-40, _t.position.y+72);	
			Stone.Attack(_t.position.x+40, _t.position.y+72);	
			Stone.Attack(_t.position.x-40, _t.position.y+64);	
			Stone.Attack(_t.position.x+40, _t.position.y+64);	
			Stone.Attack(_t.position.x-40, _t.position.y+48);	
			Stone.Attack(_t.position.x+40, _t.position.y+48);	
			Stone.Attack(_t.position.x-40, _t.position.y+32);	
			Stone.Attack(_t.position.x+40, _t.position.y+32);	
		}



		// 減衰 
		if(time > 0.5f){
			Decline(1);
			time -= 0.5f;
		}



		// debug //
		if(Input.GetKeyDown(KeyCode.KeypadEnter)){
			resilient = MAX_RESILIENT;
		}
		// debug //



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



		// UI表示の更新 
		gauge.val = (float)resilient / 100;

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
