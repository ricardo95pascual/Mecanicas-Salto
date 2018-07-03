using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Logic_Checkpoint : MonoBehaviour {

    public GameObject _playerPrefab;

    GameObject _player;
    bool _isOnRespawn;
    float _timeToRespawn;
    Vector3 _SpawnPosition;

    // Use this for initialization
    void Start () {
        _isOnRespawn = false;
        
        _SpawnPosition = GetComponentInChildren<CS_Logic_SpawnPos>().gameObject.transform.position;
        Debug.Log(GetComponentInChildren<CS_Logic_SpawnPos>().gameObject.name);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if (_isOnRespawn)
        {
            _timeToRespawn = _timeToRespawn - Time.fixedDeltaTime;

            if (_timeToRespawn <= 0f)
            {
                SpawmPlayer();
            }
        }

	}

    public void Respawn(float t)
    {
        _isOnRespawn = true;
        _timeToRespawn = t;
    }

    void SpawmPlayer()
    {
        _player = GameObject.Instantiate(_playerPrefab, _SpawnPosition, Quaternion.identity);

        _player.GetComponent<CS_PlayerLife>()._CheckPoint = this;

        _isOnRespawn = false;
    }
}