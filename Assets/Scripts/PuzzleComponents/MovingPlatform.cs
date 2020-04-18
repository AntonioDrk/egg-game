using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class MovingPlatform : MonoBehaviour
{
    // ReSharper disable twice InconsistentNaming
    [SerializeField] private bool _horizontal = true;
    // Distance required for the platform to travel, this can be also negative resulting in a reverse motion
    [SerializeField] private float _distance;
    [SerializeField] private float _timeToTarget = 1f;
    [SerializeField] private float _smoothTime = 1f;

    private Vector2 _startPos;
    private Vector2 _endPos;
    private Vector2 _target;
    
    private float _t = 0;
    private void Start()
    {
        _startPos = new Vector2(transform.position.x, transform.position.y);
        // Determines the target location in retrospect to the _horizontal boolean
        if(_horizontal)
            _endPos = new Vector2(_startPos.x + _distance, _startPos.y);
        else
            _endPos = new Vector2(_startPos.x, _startPos.y + _distance);
            
        
        _target = _endPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.transform.SetParent(null);
    }


    private void FixedUpdate()
    {
        Vector2 myPos2d = new Vector2(transform.position.x, transform.position.y);
        
        Debug.DrawLine(_startPos, _endPos, Color.blue);
        
        
        if (Vector2.Distance(myPos2d, _endPos) < 0.1f && _target == _endPos)
        {
            _target = _startPos;
            _t = 0;
        }
        
        if (Vector2.Distance(myPos2d, _startPos) < 0.1f && _target == _startPos)
        {
            _target = _endPos;
            _t = 0;
        }
        Vector2 points = Vector2.Lerp(myPos2d, _target, _t);
        transform.position = new Vector3(points.x, points.y, transform.position.z);
        _t += (Time.fixedDeltaTime / _timeToTarget);
    }
}
