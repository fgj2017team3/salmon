// Brink.cs 
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
// コンポーネントとしてくっつけるとオブジェクトが点滅する 
//----------------------------------------------------------------------------------------------------
public class Brink : MonoBehaviour
{
	//--------------------------------------------------------------------------------
	// メンバ変数 
	//--------------------------------------------------------------------------------
	[SerializeField]float onTime = 1.0f;	//@@ 点灯中の時間 
	[SerializeField]float offTime = 1.0f;	//@@ 消えている時間 
	[SerializeField]GameObject target;	//@@ 対象 

	//--------------------------------------------------------------------------------
	// メイン処理 
	//--------------------------------------------------------------------------------
	IEnumerator Start ()
	{
		while(true){
			target.SetActive(!target.activeSelf);
			yield return new WaitForSeconds(target.activeSelf ? onTime : offTime);
		}
	}
}
