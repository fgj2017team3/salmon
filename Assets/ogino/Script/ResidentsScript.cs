using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 住民スクリプト
 */
public class ResidentsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.GetComponent<Salmon>()) {
			Salmon salmon = other.gameObject.GetComponent<Salmon>();
			print("活力アップ");
		}
	}
}
