using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 応援する人々、クマ、釣り人の配置を管理するマネージャースクリプト
 * 
 * @author ogino
 */
public class ResidentsManagerScript : MonoBehaviour {

	Transform _t;

	[SerializeField]GameObject prefResidents;	// 応援する人々のプレハブ 
	[SerializeField]GameObject prefBear;	// クマのプレハブ 
	[SerializeField]GameObject prefFisher;	// 釣り人のプレハブ 
	[SerializeField]Transform transCam;		// カメラの位置を補足しておく


	//--------------------------------------------------------------------------------
	// コンストラクタ 
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		_t = this.gameObject.transform;
	}

	//--------------------------------------------------------------------------------
	// 最初に実行 
	//--------------------------------------------------------------------------------
	void Start ()
	{
		//SoundManager.PlayBGM(SoundManager.BGM.STAGE1);
	}



	//--------------------------------------------------------------------------------
	// 更新 
	//--------------------------------------------------------------------------------
	void Update ()
	{
		if(transCam.position.y > _t.position.y){

			SpawnResidents();
			if(Random.Range(0,2) == 1) {
				SpawnBear();
			} else {
				SpawnFisher();
			}

			_t.localPosition = new Vector3(
				_t.localPosition.x,
				_t.localPosition.y + SCREEN_HEIGHT,
				_t.localPosition.z
			);
		}
	}



	//--------------------------------------------------------------------------------
	// 応援する人々のランダム生成  
	//--------------------------------------------------------------------------------
	void SpawnResidents ()
	{
		GameObject residents = Instantiate<GameObject>(prefResidents);
		float randfX = RIVER_WIDTH/2;
		float randfY = Random.Range(0, SCREEN_HEIGHT);

		// 左右に分ける 
		randfX = randfX * (Random.Range(0,2)==0 ? (1):(-1));

		residents.transform.GetComponent<ResidentsScript>().Setup(transCam, randfX, transCam.localPosition.y + SCREEN_HEIGHT + randfY);
	}

	//--------------------------------------------------------------------------------
	// クマのランダム生成  
	//--------------------------------------------------------------------------------
	void SpawnBear ()
	{
		GameObject bear = Instantiate<GameObject>(prefBear);
		float randfX = Random.Range(0, RIVER_WIDTH/2);
		float randfY = Random.Range(0, SCREEN_HEIGHT);

		// 両端に寄せる処理 
		{
			float threshold = Random.Range(0, RIVER_WIDTH/2); 
			if(threshold < randfX){ randfX = threshold; }
		}
		{
			float threshold = Random.Range(0, RIVER_WIDTH/2); 
			if(threshold < randfX){ randfX = threshold; }
		}

		// 左右に分ける 
		randfX = randfX * (Random.Range(0,2)==0 ? (1):(-1));

		bear.transform.GetComponent<BearScript>().Setup(transCam, randfX, transCam.localPosition.y + SCREEN_HEIGHT + randfY);
	}

	//--------------------------------------------------------------------------------
	// 釣り人のランダム生成  
	//--------------------------------------------------------------------------------
	void SpawnFisher ()
	{
		GameObject fisher = Instantiate<GameObject>(prefFisher);
		float randfX = Random.Range(0, RIVER_WIDTH/2);
		float randfY = Random.Range(0, SCREEN_HEIGHT);

		// 両端に寄せる処理 
		{
			float threshold = Random.Range(0, RIVER_WIDTH/2); 
			if(threshold > randfX){ randfX = threshold; }
		}
		{
			float threshold = Random.Range(0, RIVER_WIDTH/2); 
			if(threshold > randfX){ randfX = threshold; }
		}

		// 左右に分ける 
		randfX = randfX * (Random.Range(0,2)==0 ? (1):(-1));

		fisher.transform.GetComponent<ResidentsScript>().Setup(transCam, randfX, transCam.localPosition.y + SCREEN_HEIGHT + randfY);
	}

	//--------------------------------------------------------------------------------
	// 定数 
	//--------------------------------------------------------------------------------
	const float SCREEN_HEIGHT = 480;
	const float RIVER_WIDTH   = 480;
}
