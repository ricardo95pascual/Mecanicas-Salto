using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KillFloor : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CS_PlayerLife>())
        {
            collision.gameObject.GetComponent<CS_PlayerLife>().Death();
        }
    }

}
