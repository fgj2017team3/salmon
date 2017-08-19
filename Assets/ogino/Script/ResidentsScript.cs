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

	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);
		print("test1");
	}
}
