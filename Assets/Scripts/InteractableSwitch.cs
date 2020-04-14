using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSwitch : Interactable
{
    [SerializeField]
    private bool interactionAllowed = false;
    [SerializeField]
    private bool activated = false;


    [SerializeField]
    private Sprite OnSprite, OffSprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void ChangeSprite(bool activated)
    {
        if (!activated)
            spriteRenderer.sprite = OffSprite;
        else
            spriteRenderer.sprite = OnSprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && interactionAllowed)
        {
            ChangeSprite(activated);
            _interactCallback.Invoke(activated);
            activated = !activated;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player is near the switch, the interaction is allowed
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.UpdateInstructionsText("Press 'F' to switch");
            interactionAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.UpdateInstructionsText("");
            interactionAllowed = false;
        }
    }
}
