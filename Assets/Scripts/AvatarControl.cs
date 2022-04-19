using System;
using System.Collections;
using UnityEngine;

public class AvatarControl : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] GameObject flockPref;
    [SerializeField] GameObject birdPref;
    [SerializeField] Transform flockParent;

    float _pitch, _yaw, _roll;
    Transform _trans;
    GameObject _birdInFlock, _birdAlone;
    bool _isAvatarControl;
    

    void OnEnable() 
    {
        ExpEventBus.Subscribe(ExpEvents.ActionBegin, () => _isAvatarControl = true);
        ExpEventBus.Subscribe(ExpEvents.PracticeBegin, () => _isAvatarControl = true);
        
        ExpEventBus.Subscribe(ExpEvents.ActionBegin, () => StartCoroutine(Flocking()));
        ExpEventBus.Subscribe(ExpEvents.PracticeBegin, () => StartCoroutine(PracticeFlocking()));
        
        ExpEventBus.Subscribe(ExpEvents.ActionEnd, () => _isAvatarControl = false);
        ExpEventBus.Subscribe(ExpEvents.PracticeEnd, () => _isAvatarControl = false);
    }
    
    IEnumerator Flocking()
    {
        Instantiate(flockPref, new Vector3(0, 100, 0), Quaternion.identity, flockParent);
        _birdInFlock = GameObject.Find("Bird Pref B(Clone)");
        _trans = _birdInFlock.transform;
        
        transform.SetParent(_trans, false);

        while (_isAvatarControl)
        {
            var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");
            var boost = Input.GetButton("Jump");

            if (hor != 0 || ver != 0)
            {
                var tg = new Vector3(-ver, hor, 0) * 100f + _trans.forward;
                var target = Quaternion.Euler(tg);

                // Dampen towards the target rotation
                _trans.rotation = Quaternion.Slerp(_trans.rotation, target, Time.deltaTime * rotationSpeed);
            }
            if (boost) _trans.position += _trans.forward * (Time.deltaTime * speed);
            yield return null;
        }
        transform.SetParent(flockParent, false);
        Destroy(_trans.parent.gameObject);
    }
    
    IEnumerator PracticeFlocking()
    {
        Instantiate(birdPref, new Vector3(0, 100, 0) + Vector3.forward, Quaternion.identity, flockParent);
        _birdInFlock = GameObject.Find("Bird Pref B(Clone)");
        _trans = _birdInFlock.transform;
        
        transform.SetParent(_trans, false);
        
        while (_isAvatarControl)
        {
            var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");
            var boost = Input.GetAxis("Jump");
            
            _pitch += ver * rotationSpeed;
            _yaw += hor * rotationSpeed;
            _roll = 0;
            _trans.rotation = Quaternion.Euler(-_pitch, _yaw, _roll);
            _trans.position += _trans.forward * (Time.deltaTime * (speed + boost));
            yield return null;
        }
        
        transform.SetParent(flockParent, false);
        
        Destroy(_trans.gameObject);
    }
}
