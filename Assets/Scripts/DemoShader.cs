using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoShader : MonoBehaviour
{
    private bool isDissolving = false;
    // Duration to fully dissolve
    private float fadeDuration = 1;
    
    [SerializeField] private KeyCode _keyCode;
    
    private Material _material;

    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            isDissolving = true;
        }

        if (isDissolving)
        {
            fadeDuration -= Time.deltaTime;

            if (fadeDuration <= 0f)
            {
                fadeDuration = 1f;
                isDissolving = false;
            }
            
            _material.SetFloat("_Fade", fadeDuration);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.name != "PlatformSection")
            return; 

        // If on the platform the egg entered and is in hand, don't modify it's hierarchy
        if (other.CompareTag("Egg") && other.gameObject.GetComponent<EggController>().isInHand)
            return;

        other.gameObject.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (this.gameObject.name != "PlatformSection")
            return; 

        // If on the platform the egg entered and is in hand, don't modify it's hierarchy
        if (other.CompareTag("Egg") && other.gameObject.GetComponent<EggController>().isInHand)
            return;

        other.gameObject.transform.SetParent(null);
    }
}
