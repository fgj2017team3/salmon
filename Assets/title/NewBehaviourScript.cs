using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour {

	static bool once = true;	// 一度だけ実行 

	// Use this for initialization
	void Start () {
		if(once){
			once = false;
		}
		else{
			FadeManager.FadeIn(0.5f);
		}
		SoundManager.PlayBGM(SoundManager.BGM.TITLE);
	}

    // Update is called once per frame
    void Update()
    {
        // kuny@ccraftsmen.jp ゲームパッドボタン対応
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            Scene();
        }
    }

    public void Scene () {
		SoundManager.PlaySE(SoundManager.SE.PUSH);
		SoundManager.StopBGM();
		FadeManager.FadeOut(0.5f, ()=>{
			SceneManager.LoadScene("Main");
		});
	}
}