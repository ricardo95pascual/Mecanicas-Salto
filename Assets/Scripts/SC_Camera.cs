using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Camera : MonoBehaviour {

    public GameObject _Player;

    public float _CameraFollowSpeed = 0.5f;

    public float _cameraMaxDistanceX;

    public float _cameraMaxDistanceY;

    float _cameraDistanceZ;

    Vector3 _doubleLerpPoint;
    

	// Use this for initialization
	void Start () {
        _cameraDistanceZ = _Player.GetComponent<CS_PlayerLife>()._cameraDistanceZ;

        _doubleLerpPoint = _Player.transform.position + Vector3.back * _cameraDistanceZ;
        _doubleLerpPoint.z = _cameraDistanceZ;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (_Player != null)
        {
            Follow();
        }
        

	}

    void Follow()
    {
        _doubleLerpPoint = Vector3.Lerp(_doubleLerpPoint, _Player.transform.position + Vector3.back * _cameraDistanceZ, _CameraFollowSpeed);

        transform.position = Vector3.Lerp(transform.position, _doubleLerpPoint, _CameraFollowSpeed);
    }
}
