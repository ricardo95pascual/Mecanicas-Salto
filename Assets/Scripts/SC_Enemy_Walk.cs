﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Enemy_Walk : MonoBehaviour {

    bool _grounded = false;
    public bool _movingRight = true;

    public float _movementSpeed;

    public float _distanceToChangeDirection = 0.5f;
    public float _distanceToDetectTheFloor = 1.2f;

    Rigidbody _rb;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        CheckDirectionChange();

        if (_movingRight)
        {
            Vector3 rbVel = _rb.velocity;
            rbVel.x = _movementSpeed;

            _rb.velocity = rbVel;
        }
        else
        {
            Vector3 rbVel = _rb.velocity;
            rbVel.x = -_movementSpeed;

            _rb.velocity = rbVel;
        }
	}

    void CheckDirectionChange()
    {

        if (_movingRight)
        {
            Ray RayFrontR = new Ray(transform.position + Vector3.down * 0.9f, Vector3.right);
            RaycastHit RayHit;
            if (Physics.Raycast(RayFrontR, out RayHit, _distanceToChangeDirection + 0.5f))
            {
                _movingRight = !_movingRight;
            }
            else
            {
                Ray RayDownR = new Ray(transform.position + Vector3.right * (0.5f + _distanceToChangeDirection), Vector3.down * _distanceToDetectTheFloor);
                if (!Physics.Raycast(RayDownR, out RayHit, _distanceToDetectTheFloor))
                {
                    _movingRight = !_movingRight;
                }
            }
        }
        else
        {
            Ray RayFrontL = new Ray(transform.position + Vector3.down * 0.9f, Vector3.left);
            RaycastHit RayHit;
            if (Physics.Raycast(RayFrontL, out RayHit, _distanceToChangeDirection + 0.5f))
            {
                _movingRight = !_movingRight;
            }
            else
            {
                Ray RayDownL = new Ray(transform.position + Vector3.left * (0.5f + _distanceToChangeDirection), Vector3.down * _distanceToDetectTheFloor);
                if (!Physics.Raycast(RayDownL, out RayHit, _distanceToDetectTheFloor))
                {
                    _movingRight = !_movingRight;
                }
            }
        }
    
    }
}
