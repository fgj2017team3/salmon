﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameClear : MonoBehaviour {

    // kuny@ccraftsmen.jp タイトルまでの自動遷移タイマー（5秒）
    float time = 600.0f;

    // Use this for initialization
    void Start () {
		FadeManager.FadeIn(1.5f);
		SoundManager.PlayBGM(SoundManager.BGM.ENDING);
	}
	
	// Update is called once per frame
	void Update () {

        // kuny@craftsmen.jp タイトルまでの自動遷移タイマー処理
        if (time < 0.0f)
        {
            Scene();
        }
        else
        {
            time -= 1.0f;
        }
    }



    public void Scene () {
		SoundManager.StopBGM();
		FadeManager.FadeOut(1.5f, ()=>{
			SceneManager.LoadScene("title");
		}, Color.white);
	}
}
