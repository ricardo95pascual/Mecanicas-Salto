using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Movement : MonoBehaviour {

    bool _grounded = true;
    
    public float _speedGroundAcceleration;
    public float _speedGroundBrakeTo0 = 0.5f;

    
    public float _speedAirAcceleration;
    public float _speedAirBrakeTo0 = 0.1f;


    public float _speedMovement;
    float _speedAcceleration;
    float _speedBrakeTo0Force = 0.5f;

    Rigidbody _rb;
    

	// Use this for initialization
	void Start () {
        _rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        CheckGrounded();
        
        
        //Check if grounded
        if (_grounded)
        {
            _speedAcceleration = _speedGroundAcceleration;
            _speedBrakeTo0Force = _speedGroundBrakeTo0;
        }
        else
        {
            _speedAcceleration = _speedAirAcceleration;
            _speedBrakeTo0Force = _speedAirBrakeTo0;
        }
        
        //Apply the movement
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            DoAcceleration();
        }
        else DoAcceleration0();
    }

    void CheckGrounded()
    {

    }

    void DoAcceleration()
    {
        Vector3 rbVel = _rb.velocity;
        rbVel.x = rbVel.x + (Input.GetAxisRaw("Horizontal") * _speedAcceleration * Time.fixedDeltaTime);
        if (rbVel.x > _speedMovement)
        {
            rbVel.x = _speedMovement;
        }
        else if (rbVel.x < -_speedMovement)
        {
            rbVel.x = -_speedMovement;
        }
        _rb.velocity = rbVel;
    } 

    void DoAcceleration0()
    {
        Vector3 rbVel = _rb.velocity;
        rbVel.x = Mathf.Lerp(rbVel.x, 0, _speedBrakeTo0Force);
    }
}
