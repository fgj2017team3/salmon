using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 住民のスクリプト
 * 
 * @author ogino
 */
public class ResidentsScript : MonoBehaviour {

	/** 当たり判定の半径 */
	public float radius = 10.0f;

	/** 活力回復値 */
	public int resilientRecoverNum = 10;

	/** あたり済みフラグ */
	protected bool isHit = false;

	Transform transCam;		// カメラ位置 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!isHit) {
			// 何かコリジョンが当たっていないか、球状にレイを飛ばして判定
			Collider2D hitColliders = Physics2D.OverlapCircle(transform.localPosition, radius);
			// 何かコリジョンをもつコンポーネントがヒットした場合
			if (hitColliders) {
				// サーモンのスクリプトを持っているか？
				if (hitColliders.gameObject.GetComponent<Salmon>()) {
					Salmon salmon = hitColliders.gameObject.GetComponent<Salmon>();
					salmon.resilient += resilientRecoverNum;
					SoundManager.PlaySE(SoundManager.SE.RECOVERY);
					isHit = true;
				}

				if (hitColliders.gameObject.GetComponent<Stone>()) {
					Destroy(hitColliders.gameObject);
				}
			}
		}

		autoRemove();
	}

	public void autoRemove() {
		// 画面外に出たら消す処理 
		if(transCam.position.y > gameObject.transform.position.y + 240 + 32){
			GameObject.Destroy(this.gameObject);
		}
	}

	public void Setup (Transform cam, float x, float y)
	{
		// カメラ保持 
		transCam = cam;

		// パラメータを設定 
		gameObject.transform.localPosition = new Vector3(x, y, 10);

		// サイズの種類を選択 

	}
}
