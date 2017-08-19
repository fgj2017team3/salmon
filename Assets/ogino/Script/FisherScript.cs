using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 釣り師のスクリプト
 * 
 * @override ResidentsScript
 * @author ogino
 */
public class FisherScript : ResidentsScript {
	
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
					salmon.resilient = 0;
					isHit = true;
				}
			}
		}
	}
}
