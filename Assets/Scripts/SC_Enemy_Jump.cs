using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Enemy_Jump : MonoBehaviour {

    public float _JumpDelay = 1.5f;
    float _CurrentJumpDelay = 0f;

    public float _JumpSpeed0 = 6f;

    Rigidbody _rb;
    CapsuleCollider _col;

    bool _isGrounded = false;

    // Use this for initialization
    void Start ()
    {
        _CurrentJumpDelay = _JumpDelay;

        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        IsGrounded();

        if (_CurrentJumpDelay > 0f && _isGrounded)
        {
            _CurrentJumpDelay = _CurrentJumpDelay - Time.fixedDeltaTime;
        }
        else if (_isGrounded)
        {
            JumpMadafaka();
        }
	}

    void JumpMadafaka ()
    {
        _rb.velocity = Vector3.up * _JumpSpeed0;
        _CurrentJumpDelay = _JumpDelay;
    }

    void IsGrounded()
    {
        if (Physics.Raycast(_rb.transform.position, Vector3.down, _col.height / 2f + 0.01f))
        {
            _isGrounded = true;
        }
        else _isGrounded = false;
    }
}
