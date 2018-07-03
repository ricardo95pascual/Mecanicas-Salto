using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_Logic_EndLevel : MonoBehaviour {

    //public string _nextScene;

    public bool _goToMenu = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CS_PlayerLife>())
        {
            if (_goToMenu)
            {
                SceneManager.LoadScene(0);
            }
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
            //Application.LoadLevel(0);
        }
    }


}
