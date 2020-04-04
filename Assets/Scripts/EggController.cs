using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggController : MonoBehaviour
{
    [SerializeField]
    private Text pickupText;
    [SerializeField]
    private Text distanceText;

    public Transform player;

    [SerializeField]
    private bool pickupAllowed;
    public bool isInHand;

    void Start()
    {
        pickupAllowed = false;
        pickupText.gameObject.SetActive(false);
    }

    void DisplayDistanceFromEgg()
    {
        float distance = Mathf.Abs(transform.position.x - player.position.x);
        distanceText.text = String.Format("{0:0.00}", distance) + " meters";

        if (distance >= 20)
        {
            distanceText.color = Color.red;
            if (distance >= 25) ;
            // you lost
        }
        else
            distanceText.color = Color.white;
    }

    void Update()
    {

        DisplayDistanceFromEgg();
        if (Input.GetKeyDown(KeyCode.E))
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
            if (!pickupAllowed)
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
        transform.SetParent(player);
        transform.localPosition = new Vector3(0.35f, -0.071f, 0f);
        GetComponent<Renderer>().sortingOrder = 1;
        isInHand = true;
    }

    public void PlaceDown()
    {
        transform.parent = null;
        transform.position = new Vector3(player.position.x, player.position.y - 0.48f, 0f);
        isInHand = false;
    }
}
