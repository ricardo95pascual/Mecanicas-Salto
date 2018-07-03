using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerLife : MonoBehaviour {

    public float _spawmDelay = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SC_IsEnemy>())
        {
            //Morir
        }
    }

    void Death()
    {
        //Cómo morir
    }
}
