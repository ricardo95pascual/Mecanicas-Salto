using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Logic_SpawnPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 fixPos = transform.position;
        fixPos.z = 0f;
        transform.position = fixPos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
