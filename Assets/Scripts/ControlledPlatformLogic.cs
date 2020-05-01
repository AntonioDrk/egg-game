using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledPlatformLogic : MonoBehaviour
{
    private Transform _transform;
    private Transform _bridge;
    private Transform _ropes;
    private AudioSource audio;

    private bool _moveUp = false;
    private bool _moveDown = false;
    private bool _playMusic = false;

    [SerializeField] private float _heightRange = 1f;
    private float _currentHeight = 0;
    [SerializeField] private float _movementSpeed = 0.01f;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _ropes = _transform.Find("Ropes").gameObject.transform;
        _bridge = _transform.Find("Bridge").gameObject.transform;
        audio = GetComponent<AudioSource>();
        audio.Pause();

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
                if (audio.isPlaying == false && _playMusic)
                {
                    audio.volume = 0.75f;
                    audio.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
                    audio.Play();
                }
                _currentHeight += _movementSpeed;
                _bridge.position += new Vector3(0, _movementSpeed, 0);
                _ropes.localScale -= new Vector3(0, _movementSpeed, 0);
                _ropes.position += new Vector3(0, _movementSpeed/2, 0);
            }
            else if (audio.isPlaying && _currentHeight >= _heightRange)
            {
                _playMusic = false;
                audio.Pause();
            }
        }
        if (_moveDown)
        {
            if (_currentHeight > -_heightRange)
            {
                if (audio.isPlaying == false && _playMusic)
                {
                    audio.volume = 0.75f;
                    audio.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
                    audio.Play();
                }
                _currentHeight -= _movementSpeed;
                _bridge.position -= new Vector3(0, _movementSpeed, 0);
                _ropes.localScale += new Vector3(0, _movementSpeed, 0);
                _ropes.position -= new Vector3(0, _movementSpeed/2, 0);
            }
            else if (audio.isPlaying && _currentHeight <= -_heightRange)
            {
                _playMusic = false;
                audio.Pause();
            }
        }
    }

    public void MoveUp(bool isInteracted)
    {
        _playMusic = isInteracted;
        _moveUp = isInteracted;

        if(!isInteracted)
        {
            _playMusic = false;
            audio.Pause();
        }
    }

    public void MoveDown(bool isInteracted)
    {
        _playMusic = isInteracted;
        _moveDown = isInteracted;

        if (!isInteracted)
        {
            _playMusic = false;
            audio.Pause();
        }
    }
}
