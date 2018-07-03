using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_PlayerLife : MonoBehaviour {

    public float _spawmDelay = 1f;

    public CS_Logic_Checkpoint _CheckPoint;

    public GameObject _camera;
    GameObject _activeCamera;
    public float _cameraDistanceZ;

	// Use this for initialization
	void Start () {
        _activeCamera = GameObject.Instantiate(_camera, transform.position + Vector3.back * _cameraDistanceZ, Quaternion.identity);
        _activeCamera.GetComponent<SC_Camera>()._Player = gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CS_Logic_Checkpoint>())
        {
            _CheckPoint = other.GetComponent<CS_Logic_Checkpoint>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SC_IsEnemy>())
        {
            Death();
        }
    }

    void Death()
    {
        _CheckPoint.GetComponent<CS_Logic_Checkpoint>().Respawn(_spawmDelay);
        Destroy(gameObject);
    }
}
