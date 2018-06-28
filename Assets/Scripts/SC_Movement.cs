using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Movement : MonoBehaviour {

    bool _grounded = true;
    bool _canSecondJump = true;
    
    public float _speedGroundAcceleration;
    public float _speedGroundBrakeTo0 = 0.5f;

    
    public float _speedAirAcceleration;
    public float _speedAirBrakeTo0 = 0.1f;


    public float _speedMovement;
    float _speedAcceleration;
    float _speedBrakeTo0Force = 0.5f;

    Rigidbody _rb;

    public float _jumpSpeed0 = 8f;
    public float _jumpSecondSpeed0 = 6f;
    public float _jumpBrakeSpeed = 3f;


	// Use this for initialization
	void Start () {
        _rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        CheckGrounded();
       
        //Apply the movement
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            DoAcceleration();
        }
        else DoAcceleration0();

        

        //Jump
        if (Input.GetKeyDown("space"))
        {
            Debug.Log(Input.GetAxis("Jump"));
            if (_grounded)
            {
                Jump(_jumpSpeed0);
            }
            else if (_canSecondJump)
            {
                Jump(_jumpSecondSpeed0);
                _canSecondJump = false;
            }
        }

        CheckBrakeJump();

    }

    void CheckGrounded()
    {
        RaycastHit hit;
        Ray groundRay = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(groundRay, out hit, 1.01f))
        {
            _grounded = true;
        }
        else _grounded = false;

        //Debug.Log(_grounded);
        //Set ground depending stats
        if (_grounded)
        {
            _speedAcceleration = _speedGroundAcceleration;
            _speedBrakeTo0Force = _speedGroundBrakeTo0;
            _canSecondJump = true;
        }
        else
        {
            _speedAcceleration = _speedAirAcceleration;
            _speedBrakeTo0Force = _speedAirBrakeTo0;
        }
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

    void Jump(float speed0)
    {
        Vector3 rbVel = _rb.velocity;
        rbVel.y = speed0;
        _rb.velocity = rbVel;
    }

    void CheckBrakeJump()
    {
        if (Input.GetKeyUp("space"))
        {
            if (_rb.velocity.y > _jumpBrakeSpeed)
            {
                Vector3 rbVel = _rb.velocity;
                rbVel.y = _jumpBrakeSpeed;
                _rb.velocity = rbVel;
            }
        }
    }
}
