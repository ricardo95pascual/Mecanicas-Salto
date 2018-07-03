using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Logic_StartingPoint : MonoBehaviour {

    public GameObject _playerPrefab;

    GameObject _player;

    CS_Logic_Checkpoint _checkpoint;

	// Use this for initialization
	void Start () {
        _player = GameObject.Instantiate(_playerPrefab, transform.position, Quaternion.identity);

        _checkpoint = gameObject.GetComponent<CS_Logic_Checkpoint>();

        _player.GetComponent<CS_PlayerLife>()._CheckPoint = _checkpoint;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
