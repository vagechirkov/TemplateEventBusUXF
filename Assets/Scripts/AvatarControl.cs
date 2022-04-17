using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarControl : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 10f;

    float _pitch, _yaw, _roll;
    Transform _trans;
    GameObject _birdInFlock, _birdAlone;
    

    void Start()
    {
        _birdInFlock = GameObject.Find("Bird Pref B(Clone)");
        _birdAlone = GameObject.Find("Bird Pref B");
        gameObject.transform.SetParent(_birdInFlock.transform, false);
        _trans = gameObject.GetComponentInParent<Transform>().parent;
    }

    void Update()
    {
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");
        // var boost = Input.GetButton("Jump") ? speed : 0f;
        var boost = Input.GetButton("Jump");

        if (hor != 0 || ver != 0)
        {
            // (_pitch, _yaw, _roll) = _trans.rotation;
            // _pitch = ver * controlPitchFactor + _trans.position.y;
            // _yaw = hor * controlYawFactor + _trans.position.x;
            // _roll = hor * controlRollFactor;
            // Quaternion.Euler(_pitch, _yaw, _roll);
            var tg = new Vector3(-ver, hor, 0) * 100f + _trans.forward;
            var target = Quaternion.Euler(tg);

            // Dampen towards the target rotation
            _trans.rotation = Quaternion.Slerp(_trans.rotation, target,  Time.deltaTime * rotationSpeed);
            
            // var fwd = _trans.forward;
            // var tg = fwd + new Vector3( hor, ver, 0f) * (Time.deltaTime * rotationSpeed);
            // _trans.rotation = Quaternion.LookRotation(Vector3.RotateTowards(fwd, tg , 50, 0));
            // Debug.Log(hor);
            // Debug.Log(_trans.rotation);
        }


        if (boost) _trans.position += _trans.forward * (Time.deltaTime * speed);
        // _trans.position += _trans.forward * (Time.deltaTime * (speed + boost));
    }
}
