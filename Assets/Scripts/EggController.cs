using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggController : MonoBehaviour
{
    [SerializeField]
    private Text pickupText;
    public Transform player;

    [SerializeField]
    private bool pickupAllowed;

    void Start()
    {
        pickupAllowed = false;
        pickupText.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(pickupAllowed)
            {
                PickUp();
            }
            else if (this.transform.parent != null)
            {
                PlaceDown();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if(!pickupAllowed)
            {
                pickupText.gameObject.SetActive(true);
                pickupAllowed = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickupText.gameObject.SetActive(false);
            pickupAllowed = false;
        }
    }

    void PickUp()
    {
        this.transform.SetParent(player);
        this.transform.localPosition = new Vector3(0.35f, -0.75f, 0f);
        this.GetComponent<Renderer>().sortingOrder = 1;
    }

    void PlaceDown()
    {
        this.transform.parent = null;
        this.transform.position = new Vector3(player.position.x, 0.95f, 0f);
    }
}
