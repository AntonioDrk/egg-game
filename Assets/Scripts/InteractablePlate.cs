using System;
using  UnityEngine;


public class InteractablePlate:Interactable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player is on top of the plate, run the animaiton
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            RunAnimation();
            _interactCallback.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            RunAnimation(false);
            _interactCallback.Invoke(false);
        }
            
    }
}