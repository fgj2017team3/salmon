// Stone.cs 
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
// 石ころの管理  
//----------------------------------------------------------------------------------------------------
public class Stone : MonoBehaviour
{
	//--------------------------------------------------------------------------------
	// サイズの種類  
	//--------------------------------------------------------------------------------
	public enum SIZE{
		NORMAL,
		SMALL,
		LARGE,
	}

	//--------------------------------------------------------------------------------
	// メンバ変数  
	//--------------------------------------------------------------------------------
	static List<Stone> stones = new List<Stone>();		// 石リスト 

	Transform _t;			// 高速化 
	Transform transCam;		// カメラ位置 

	[SerializeField]BoxCollider2D collid;				// 当たり判定 
	[SerializeField]SpriteRenderer sprite;				// 画像 

	bool isReadyRemove = false;							// 壊れ中かどうか 



	//--------------------------------------------------------------------------------
	// コンストラクタ 
	//--------------------------------------------------------------------------------
	void Awake ()
	{ 
		stones.Add(this);
		_t = this.gameObject.transform;
	}

	//--------------------------------------------------------------------------------
	// デストラクタ 
	//--------------------------------------------------------------------------------
	void OnDestroy ()
	{
		stones.Remove(this);
	}



	//--------------------------------------------------------------------------------
	// 初期化 
	//--------------------------------------------------------------------------------
	public void Setup (Transform cam, float x, float y, SIZE size)
	{
		// カメラ保持 
		transCam = cam;

		// パラメータを設定 
		_t.localPosition = new Vector3(x, y, 0);

		// サイズの種類を選択 

	}



	//--------------------------------------------------------------------------------
	// 指定したポジションが石なのかどうか調べる 
	//--------------------------------------------------------------------------------
	public static bool CheckStones (float x, float y)
	{
		foreach(Stone s in stones){
			if(s.IsStone(x,y)){ return true; }
		}

		return false;
	}



	//--------------------------------------------------------------------------------
	// 指定したポジションが石なのかどうか調べる(個別) 
	//--------------------------------------------------------------------------------
	bool IsStone (float x, float y)
	{
		return collid.OverlapPoint(new Vector2(x,y));
	}



	//--------------------------------------------------------------------------------
	// 指定したポジションの石を飛ばす 
	// isRight は自分が左にいる場合(右に飛ぶ) を意味している 
	//--------------------------------------------------------------------------------
	public static bool Attack (float x, float y, bool isRght, bool isUp)
	{
		foreach(Stone s in stones){
			if(s.isReadyRemove){ continue; }
			if(s.IsStone(x,y)){
				FollowCamera.Shake();
				s.StartCoroutine(s.Break(isRght, isUp));
				s.isReadyRemove = true;;
				return true;
			}
		}

		return false;
	}



	//--------------------------------------------------------------------------------
	// 石を飛ばす 
	//--------------------------------------------------------------------------------
	IEnumerator Break(bool isRight, bool isUp)
	{
		float time=0;
		float dirX = Random.Range(0, 320);
		float rotSpeed = Random.Range(15, 45) * (Random.Range(0,2)==0 ? -1:1);
		while(time < 2.5f){

			// 消える 
			float alpha = (1 - time / 2.5f);
			sprite.color = new Color(1,1,1,alpha);

			// 回る 
			_t.Rotate(0, 0, rotSpeed);

			// 飛ぶ 
			_t.position = new Vector3(
				_t.position.x + dirX/10 * (isRight ? 1:-1),
				_t.position.y + 4 * (isUp ? 1:-1),
				0
			);

			yield return null;
			time += Time.deltaTime;
		}

		GameObject.Destroy(this.gameObject);
	}



	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	void Update ()
	{
		// 画面外に出たら消す処理 
		if(transCam.position.y > _t.position.y + 240 + 32){
			GameObject.Destroy(this.gameObject);
		}
	}
}
