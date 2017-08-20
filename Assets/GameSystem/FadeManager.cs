// FadeManager.cs 
//
// @idev Unity2017.1.0f3 / MonoDevelop5.9.6
// @auth FCEI.No-Va
// @date 2017/08/20
//
// Copyright (C) 2017 FlyteCatEmotion Inc.
// All Rights Reserved.
//------------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------------------------------------------
// フェード管理 黒フェードのみ対応 
//----------------------------------------------------------------------------------------------------
public class FadeManager : MonoBehaviour
{
	static FadeManager instance;				// シングルトン用インスタンス 

	[SerializeField]SpriteRenderer Sprite;		//@@ フェード板 

	public static bool isPlaying{ get; private set; }	// 使用中のロックフラグ 



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

		// 変数の初期化 
		isPlaying = false;
	}
		
	//--------------------------------------------------------------------------------
	// 画面暗転開始 
	//--------------------------------------------------------------------------------
	public static void FadeOut(float time, System.Action Callback=null, Color? nColor=null)
	{
		if(instance == null || isPlaying){ return; }
		Color color = nColor ?? Color.black;
		instance.StartCoroutine(instance.FadeCore(false, time, Callback, color));
	}

	//--------------------------------------------------------------------------------
	// 画面明るくする 
	//--------------------------------------------------------------------------------
	public static void FadeIn(float time, System.Action Callback=null, Color? nColor=null)
	{
		if(instance == null || isPlaying){ return; }
		Color color = nColor ?? Color.black;
		instance.StartCoroutine(instance.FadeCore(true, time, Callback, color));
	}

	//--------------------------------------------------------------------------------
	// フェード共通処理  
	//--------------------------------------------------------------------------------
	IEnumerator FadeCore(bool isFadeIn, float max_time, System.Action Callback, Color color)
	{
		// 多重フェードのロック 
		isPlaying = true;

		// パネルの透明度を変化させる 
		float timer = 0;
		while(timer < max_time){
			float alpha;

			if(isFadeIn){
				alpha = 1 - timer / max_time;
			}
			else{
				alpha = timer / max_time;
			}

			Sprite.color = new Color(color.r, color.g, color.b, alpha);

			yield return null;
			timer += Time.deltaTime;
		}
			
		// 最後の帳尻合わせ 
		if(isFadeIn){
			Sprite.color = new Color(color.r, color.g, color.b, 0);
		}
		else{
			Sprite.color = new Color(color.r, color.g, color.b, 1);
		}

		// 終了コールバックの実行 
		if(Callback != null){ Callback(); }

		// ロック解除 
		isPlaying = false;
	}
}
