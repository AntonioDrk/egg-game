using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledPlatformLogic : MonoBehaviour
{
    private Transform _transform;
    private Transform _bridge;
    private Transform _ropes;

    private bool _moveUp = false;
    private bool _moveDown = false;

    [SerializeField] private float _heightRange = 1f;
    private float _currentHeight = 0;
    private float _movementSpeed = 0.01f;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _ropes = _transform.Find("Ropes").gameObject.transform;
        _bridge = _transform.Find("Bridge").gameObject.transform;

        _bridge.position -= new Vector3(0, _heightRange, 0);
        _ropes.localScale += new Vector3(0, _heightRange, 0);
        _ropes.position -= new Vector3(0, _heightRange/2, 0);
    }

    private void Update()
    {
        if (_moveUp)
        {
            if (_currentHeight < _heightRange)
            {
                _currentHeight += _movementSpeed;
                _bridge.position += new Vector3(0, _movementSpeed, 0);
                _ropes.localScale -= new Vector3(0, _movementSpeed, 0);
                _ropes.position += new Vector3(0, _movementSpeed/2, 0);
            }
        }
        if (_moveDown)
        {
            if (_currentHeight > -_heightRange)
            {
                _currentHeight -= _movementSpeed;
                _bridge.position -= new Vector3(0, _movementSpeed, 0);
                _ropes.localScale += new Vector3(0, _movementSpeed, 0);
                _ropes.position -= new Vector3(0, _movementSpeed/2, 0);
            }
        }
    }

    public void MoveUp(bool isInteracted)
    {
        _moveUp = isInteracted;
    }

    public void MoveDown(bool isInteracted)
    {
        _moveDown = isInteracted;
    }
}
