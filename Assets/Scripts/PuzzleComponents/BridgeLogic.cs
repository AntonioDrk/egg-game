using System;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Component usually used on tilemaps that represent bridges
/// </summary>
public class BridgeLogic : MonoBehaviour
{
    private Material _material;
    private float _fadeIndex = 0;
    private bool _FadeOut = true;
    private float t = 0f;
    [SerializeField] private float _fadeSpeed = 1f;
    [SerializeField] private bool _startFaded = true;
    private TilemapCollider2D _tilemapCollider;

    private void Start()
    {
        _tilemapCollider = GetComponent<TilemapCollider2D>();
        _material = GetComponent<TilemapRenderer>().material;
        
        if(_tilemapCollider == null)
            Debug.LogError("TileMapCollider2D missing from the object!!");

        // If chosen to start faded
        if (_startFaded)
        {
            // 
            _FadeOut = true;
            _material.SetFloat("_Fade", _fadeIndex);
            _tilemapCollider.enabled = false;
        }
        else
        {
            _FadeOut = false;
            _fadeIndex = 1;
            _tilemapCollider.enabled = true;
        }
    }

    private void Update()
    {
        if (_FadeOut)
        {
            if (t < 1)
            {
                _fadeIndex = Mathf.Lerp(_fadeIndex, 0, t);
                t += _fadeSpeed * Time.deltaTime;
                _material.SetFloat("_Fade", _fadeIndex);
                if (_fadeIndex >= 0.5f)
                    _tilemapCollider.enabled = false;
            }
        }
        else
        {
            if (t < 1)
            {
                _fadeIndex = Mathf.Lerp(_fadeIndex, 1, t);
                t += _fadeSpeed * Time.deltaTime;
                _material.SetFloat("_Fade", _fadeIndex);
                if (_fadeIndex >= 0.5f)
                    _tilemapCollider.enabled = true;
            }
            
        }

    }

    public void OnInteract(bool isInteracted)
    {
        _FadeOut = _startFaded ? !isInteracted : isInteracted;
        t = 0f;
    }
}
