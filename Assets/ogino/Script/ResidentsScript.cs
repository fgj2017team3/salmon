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
	bool isHit = false;

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
					isHit = true;
				}
			}
		}
	}
}
