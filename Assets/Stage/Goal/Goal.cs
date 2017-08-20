// Goal.cs 
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
// ゴールの管理・演出処理  
//----------------------------------------------------------------------------------------------------
public class Goal : MonoBehaviour
{
	//--------------------------------------------------------------------------------
	// メンバ変数 
	//--------------------------------------------------------------------------------
	Transform _t;			// 高速化 
	Transform transSalmon;	// しゃけの位置 

	[SerializeField]SpriteRenderer mask;	// マスク 

	SpriteRendererIndexer spriteSalmon;	// しゃけの表示 



	//--------------------------------------------------------------------------------
	// コンストラクタ 
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		// 自身のTransformを保持 
		_t = this.gameObject.transform;

		// しゃけを取得 
		Salmon salmon = GameObject.FindObjectOfType<Salmon>();
		salmon.enabled = false;	// 操作できないようにenableを切る 

		// コンポーネント・Transform取得 
		spriteSalmon = salmon.gameObject.transform.GetComponentInChildren<SpriteRendererIndexer>();
		transSalmon  = salmon.gameObject.transform;
	}



	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	bool isLockUpdate = false;		// アップデートしないフラグ 
	bool once = true;				// 1度だけ実行 
	void Update ()
	{
		if(isLockUpdate){ return; }

		if(once){
			once = false;
			StartCoroutine(MaskFade());
			StartCoroutine(SalmonMove());
		}

		// しゃけが中央に集まる(横) 
		if(transSalmon.position.x - _t.position.x >= 1){
			transSalmon.position = new Vector3(transSalmon.position.x-1, transSalmon.position.y, transSalmon.position.z);
		}
		else if(transSalmon.position.x - _t.position.x <= -1){
			transSalmon.position = new Vector3(transSalmon.position.x+1, transSalmon.position.y, transSalmon.position.z);
		}
		else{
			transSalmon.position = new Vector3(_t.position.x, transSalmon.position.y, transSalmon.position.z);
		}

		// しゃけが中央に集まる(縦) 
		if(transSalmon.position.y - _t.position.y >= 1){
			transSalmon.position = new Vector3(transSalmon.position.x, transSalmon.position.y-1, transSalmon.position.z);
		}
		else if(transSalmon.position.y - _t.position.y <= -1){
			transSalmon.position = new Vector3(transSalmon.position.x, transSalmon.position.y+1, transSalmon.position.z);
		}
		else{
			transSalmon.position = new Vector3(transSalmon.position.x, _t.position.y, transSalmon.position.z);
		}

		// ゴール演出へ 
		if(transSalmon.position.x == _t.position.x && transSalmon.position.y == _t.position.y){
			StartCoroutine(GoalEffect());
			isLockUpdate = true;
		}
	}



	//--------------------------------------------------------------------------------
	// マスクのフェード  
	//--------------------------------------------------------------------------------
	IEnumerator MaskFade ()
	{
		for(int i=0; i<192; i++){
			mask.color = new Color(0, 0, 0, (float)i/255);
			yield return null;
		}
	}
	//--------------------------------------------------------------------------------
	// しゃけの動き 
	//--------------------------------------------------------------------------------
	IEnumerator SalmonMove ()
	{
		spriteSalmon.index = 0;
		float time = 0;
		while(isLockUpdate == false){
			spriteSalmon.isFlipX = (int)(time*5) % 2 == 0;
			yield return null;
			time += Time.deltaTime;
		}
	}



	//--------------------------------------------------------------------------------
	// ゴール演出 
	//--------------------------------------------------------------------------------
	[SerializeField]Ikura ikuraOriginal;		// ゴール時にまき散らすイクラ 
	IEnumerator GoalEffect ()
	{
		for(int j=0; j<5; j++){
			for(int i=0; i<100; i++){
				Ikura ikura = Instantiate<Ikura>(ikuraOriginal); 
				ikura.Setup(_t.position.x, _t.position.y, Random.Range(-50, 50), Random.Range(-50, 50));
			}

			yield return new WaitForSeconds(1);
		}

		SoundManager.StopBGM();
		FadeManager.FadeOut(1.5f, ()=>{
			UnityEngine.SceneManagement.SceneManager.LoadScene("GAMECLEAR");
		});
	}
}
