using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
		SoundManager.StopBGM();
		SceneManager.LoadScene("Main");
	}
}