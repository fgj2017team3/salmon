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
					Destroy(salmon.gameObject.GetComponent<BoxCollider2D>());
					Destroy(Instantiate(hitEffect, transform.localPosition, transform.localRotation, gameObject.transform), 1);
					isHit = true;
					StartCoroutine(Break(salmon, true, true));

				}
			}
		}

		autoRemove();
	}

	//--------------------------------------------------------------------------------
	// 石を飛ばす 
	//--------------------------------------------------------------------------------
	IEnumerator Break(Salmon salmon, bool isRight, bool isUp)
	{
		float time=0;
		float rotSpeed = Random.Range(15, 45) * (Random.Range(0,2)==0 ? -1:1);
		while(time < 0.5f){

			Transform t = salmon.transform;
			// 回る 
			t.Rotate(0, 0, rotSpeed);

			// 飛ぶ 
			t.position = new Vector3(
				t.position.x,
				t.position.y + 14 * (isUp ? 1:-1),
				0
			);
			t.localScale = new Vector3(t.localScale.x + 0.25f, t.localScale.y + 0.25f, t.localScale.z + 0.25f);

			yield return null;
			time += Time.deltaTime;
		}

		salmon.resilient = 0;
	}
}
