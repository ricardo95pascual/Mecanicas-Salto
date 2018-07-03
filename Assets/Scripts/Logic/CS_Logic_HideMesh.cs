using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Logic_HideMesh : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
