using System.Collections;
using UnityEngine;

public class AvatarControl : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 1f;
    public GameObject flockPref;

    float _pitch, _yaw, _roll;
    Transform _trans;
    GameObject _birdInFlock, _birdAlone;
    bool _avatarControlInFlock;
    

    void OnEnable() 
    {
        ExpEventBus.Subscribe(ExpEvents.ActionBegin, () => _avatarControlInFlock = true);
        ExpEventBus.Subscribe(ExpEvents.ActionBegin, () => StartCoroutine(Flocking()));
        ExpEventBus.Subscribe(ExpEvents.ActionEnd, () => _avatarControlInFlock = false);
    }
    
    IEnumerator Flocking()
    {
        var go = Instantiate(flockPref, new Vector3(0, 100, 0), Quaternion.identity);
        _birdInFlock = GameObject.Find("Bird Pref B(Clone)");
        gameObject.transform.SetParent(_birdInFlock.transform, false);
        _trans = gameObject.GetComponentInParent<Transform>().parent;
        
        while (_avatarControlInFlock)
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
        _trans.SetParent(null);
        Destroy(go);
    }
    
    void Update()
    {
        if (false) // practice
        {
            var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");
            var boost = Input.GetButton("Jump");
            
            // (_pitch, _yaw, _roll) = _trans.rotation;
            // _pitch = ver * controlPitchFactor + _trans.position.y;
            // _yaw = hor * controlYawFactor + _trans.position.x;
            // _roll = hor * controlRollFactor;
            // Quaternion.Euler(_pitch, _yaw, _roll);
            // var fwd = _trans.forward;
            // var tg = fwd + new Vector3( hor, ver, 0f) * (Time.deltaTime * rotationSpeed);
            // _trans.rotation = Quaternion.LookRotation(Vector3.RotateTowards(fwd, tg , 50, 0));
            // Debug.Log(hor);
            // Debug.Log(_trans.rotation);
            // _trans.position += _trans.forward * (Time.deltaTime * (speed + boost));
        }
    }
}
