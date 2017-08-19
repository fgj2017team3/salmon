using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 住民のスクリプト
 */
public class ResidentsScript : MonoBehaviour {

	/** 当たり判定の半径 */
	public float radius = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D hitColliders = Physics2D.OverlapCircle(transform.localPosition, radius);
		print(transform.localPosition);
		if (hitColliders) {
			if (hitColliders.gameObject.GetComponent<Salmon>()) {
				Salmon salmon = hitColliders.gameObject.GetComponent<Salmon>();
				print("活力アップ");
			}
		}
	}
}
