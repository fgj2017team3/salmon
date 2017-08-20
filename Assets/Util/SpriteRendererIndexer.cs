//------------------------------------------------------------------------------------------------------------------------
// SpriteRendererIndexer.cs
//
// @idev Unity5.1.3f1 / MonoDevelop4.0.1
// @auth FCEI.No-Va
// @date 2015/09/09
//
// Copyright (C) 2015 FlyteCatEmotion Inc.
// All Rights Reserved.
//------------------------------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

//----------------------------------------------------------------------------------------------------
// PpriteRendererの画像を入れ替える 
//----------------------------------------------------------------------------------------------------
public class SpriteRendererIndexer : MonoBehaviour
{
	[SerializeField]SpriteRenderer spriteRenderer;
	[SerializeField]Sprite[] sprites;

	private int _index;
	public  int index{
		set{
			if(value>=0){
				spriteRenderer.sprite = sprites[value % sprites.Length];
			}
			else{
				spriteRenderer.sprite = sprites[0];
			}
			_index = value;
		}
		get{
			return _index;
		}
	}

	public float alpha{
		set{
			float a = Mathf.Clamp(value, 0.0f, 1.0f);
			spriteRenderer.color = new Color(
				spriteRenderer.color.r,
				spriteRenderer.color.g,
				spriteRenderer.color.b,
				a
			);
		}
	}

	public Vector3 rgb{
		set{
			spriteRenderer.color = new Color(
				value.x,
				value.y,
				value.z,
				spriteRenderer.color.a
			);
		}
	}

	public Color color{
		set{
			spriteRenderer.color = new Color(
				value.r,
				value.g,
				value.b,
				value.a
			);
		}
	}

	public Bounds bounds{
		get{
			return spriteRenderer.bounds;
		}
	}

	public bool isFlipX{
		get{ return spriteRenderer.flipX; }
		set{ spriteRenderer.flipX = value; }
	}
	public bool isFlipY{
		get{ return spriteRenderer.flipY; }
		set{ spriteRenderer.flipY = value; }
	}



	//--------------------------------------------------------------------------------
	// レンダラー自動検出 
	//--------------------------------------------------------------------------------
	[ContextMenu("EXEC_AutoDetector")]
	void AutoDetector ()
	{
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		if(spriteRenderer==null){
			Debug.LogError("SpriteRendererIndexer : spriteRenderer is null.");
			return;
		}
		if(sprites.Length==0){
			sprites = new Sprite[1];
		}
		if(sprites.Length==1 && sprites[0]==null){
			sprites[0] = spriteRenderer.sprite;
		}
	}

	//--------------------------------------------------------------------------------
	// デバッグ用の設定忘れ検出 
	//--------------------------------------------------------------------------------
	public void DebugAssert ()
	{
		if(sprites==null){
			Debug.LogError("SpriteRendererIndexer : sprites is null.");
			sprites = new Sprite[1];
			sprites[0] = spriteRenderer.sprite;
			return;
		}
		if(sprites.Length==0){
			Debug.LogError("SpriteRendererIndexer : sprites length is Zero.");
			sprites = new Sprite[1];
			sprites[0] = spriteRenderer.sprite;
			return;
		}
		for(int i=0; i<sprites.Length; i++){
			if(sprites[i] == null){
				Debug.LogError("SpriteRendererIndexer : sprites["+i.ToString()+"] is null.");
				return;
			}
		}
	}
}
