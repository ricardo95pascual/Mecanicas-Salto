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

    public float _dashSpeed = 20f;
    public float _dashDistance = 5f;
    float _dashRemainingDistance = 0f;
    public float _dashCooldwon = 1.5f;
    float _dashRemainingCooldwon = 0f;
    bool _dashDoingNow = false;
    int _dashDirection = 0;
    public bool _dashUseGravity = false;


    Rigidbody _rb;

    public float _jumpSpeed0 = 8f;
    public float _jumpSecondSpeed0 = 6f;
    public float _jumpBrakeSpeed = 3f;
    public float _maxFallSpeed = 20f;


    public float _wallJumpFallSpeed = 2f;
    bool _isOnWall = false;
    bool _isOnWallRight = true;

	// Use this for initialization
	void Start () {
        _rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {


        CheckGrounded();
       
        //Apply the movement
        if (!_dashDoingNow)
        {
            if (Input.GetAxisRaw("Horizontal") != 0f)
            {
                DoAcceleration();
            }
            else DoAcceleration0();
        }

        CheckWallJump();
        //Debug.Log(_isOnWall);

        //Jump
        if (Input.GetKeyDown("space"))
        {
            Debug.Log(Input.GetAxis("Jump"));
            if (_grounded)
            {
                Jump(_jumpSpeed0);
            }
            else if (_isOnWall)
            {
                if (_isOnWallRight)
                {
                    _rb.velocity = new Vector3(-_speedMovement, _rb.velocity.y);
                }
                else _rb.velocity = new Vector3(_speedMovement, _rb.velocity.y);

                Jump(_jumpSpeed0);
                //_isOnWall = false;
            }
            else if (_canSecondJump)
            {
                Jump(_jumpSecondSpeed0);
                _canSecondJump = false;
            }
        }

        CheckBrakeJump();

        ManageDash();



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

        if (_grounded)
        {
            _isOnWall = false;
        }

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

    void LimitFallSpeed()
    {
        if (_rb.velocity.y < -_maxFallSpeed)
        {
            Vector3 rbVel = _rb.velocity;
            rbVel.y = -_maxFallSpeed;
            _rb.velocity = rbVel;
        }
    }

    void ManageDash()
    {
        
        if (_dashDoingNow)
        {
            Vector3 rbVel = _rb.velocity;
            rbVel.x = _dashSpeed *_dashDirection;
            if (!_dashUseGravity)
            {
                rbVel.y = 0f;
            }
            _rb.velocity = rbVel;

            _dashRemainingDistance = _dashRemainingDistance - (_dashSpeed * Time.fixedDeltaTime);
            
            if (_dashRemainingDistance <= 0f)
            {
                EndDash();
            }
        }
        else
        {
            //Debug.Log(_dashRemainingCooldwon);
            if (_dashRemainingCooldwon > 0f) //Dash Cooldown
            {
                _dashRemainingCooldwon = _dashRemainingCooldwon - Time.fixedDeltaTime;
            }
            else
            {
                
                if (Input.GetKeyDown("left shift") == true && Input.GetAxisRaw("Horizontal") != 0f) //Dash Start
                {
                    
                    _dashDoingNow = true;
                    _dashRemainingDistance = _dashDistance;
                    if (Input.GetAxisRaw("Horizontal") > 0f)
                    {
                        _dashDirection = 1;
                    }
                    else _dashDirection = -1;
                }
            }
        }
    }
    
    void EndDash()
    {
        _dashDoingNow = false;
        _dashRemainingCooldwon = _dashCooldwon;
    }

    void CheckWallJump()
    {
        /*if (!_isOnWall)
        {
            RaycastHit hitR;
            Ray rayR = new Ray(gameObject.transform.position + (Vector3.up * 0.5f), Vector3.right);

            RaycastHit hitL;
            Ray rayL = new Ray(gameObject.transform.position, Vector3.left);

            if (Physics.Raycast(rayR, out hitR, 0.51f))
            {
                _isOnWall = true;
                _isOnWallRight = true;
                EndDash();
            }
            else if (Physics.Raycast(rayL, out hitL, 0.51f))
            {
                _isOnWall = true;
                _isOnWallRight = false;
                EndDash();
            }
        }*/

        RaycastHit hitR;
        Ray rayR = new Ray(gameObject.transform.position + (Vector3.up * 0.5f), Vector3.right);

        RaycastHit hitL;
        Ray rayL = new Ray(gameObject.transform.position, Vector3.left);

        if (Physics.Raycast(rayR, out hitR, 0.51f))
        {
            _isOnWall = true;
            _isOnWallRight = true;
            EndDash();
        }
        else if (Physics.Raycast(rayL, out hitL, 0.51f))
        {
            _isOnWall = true;
            _isOnWallRight = false;
            EndDash();
        }
        else _isOnWall = false;
        //



        if (_rb.velocity.y < -_wallJumpFallSpeed && _isOnWall)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, -_wallJumpFallSpeed);
        }

    }

    void StartWallJump (bool R)
    {
        _isOnWall = true;
        EndDash();

        if (R)
        {
            _isOnWallRight = true;
        }
        else _isOnWallRight = false;
    }
}
