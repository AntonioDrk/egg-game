using System;
using  UnityEngine;


public class InteractablePlate:Interactable
{

    [SerializeField]
    private Sprite OnSprite, OffSprite;

    private SpriteRenderer spriteRenderer;

    private int collidersNr;

    private void Start()
    {
        collidersNr = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void ChangeSprite(bool changeSprite)
    {
        if (changeSprite)
            spriteRenderer.sprite = OffSprite;
        else
            spriteRenderer.sprite = OnSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player is on top of the plate, run the animaiton
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            collidersNr++;
            if (collidersNr == 1)
            {
                ChangeSprite(true);
                _interactCallback.Invoke(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            collidersNr--;
            if(collidersNr == 0)
            {
                ChangeSprite(false);
                _interactCallback.Invoke(false);
            }
        }
            
    }
}