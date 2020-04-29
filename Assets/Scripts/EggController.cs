using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggController : MonoBehaviour
{
    [SerializeField]
    private Text distanceText;

    public Transform player;

    [SerializeField]
    private bool pickupAllowed;
    public bool isInHand;

    private Animator anim;
    private bool cracked;

    void Start()
    {
        cracked = false;
        pickupAllowed = false;
        anim = GetComponent<Animator>();
    }

    void DisplayDistanceFromEgg()
    {
        float distance = Mathf.Abs(transform.position.x - player.position.x);
        distanceText.text = String.Format("{0:0.00}", distance) + " meters";

        if (distance >= 20)
        {
            distanceText.color = Color.red;
            //if (distance >= 25) ;
            // you lost
        }
        else
            distanceText.color = Color.white;
    }

    void Update()
    {

        DisplayDistanceFromEgg();
        if (Input.GetKeyDown(KeyCode.E) && !cracked)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.pickEgg);
            if (pickupAllowed && isInHand == false)
            {
                PickUp();
            }
            else if(isInHand == true)
            {
                PlaceDown();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if (!pickupAllowed && !cracked)
            {
                GameManager.Instance.UpdateInstructionsText("Press 'E' to pick up");
                pickupAllowed = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            GameManager.Instance.UpdateInstructionsText("");
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

    public void EggCrack()
    {
        SaveSystem.SaveStatsData(1, 0, 0, 0);

        cracked = true;
        isInHand = false;
        transform.parent = null;
        IEnumerator coroutine = EggFall(new Vector3(transform.position.x, player.position.y - 0.48f, 0f), 1.0f);
        StartCoroutine(coroutine);
        SoundManager.Instance.PlaySound(SoundManager.Instance.eggCrack);
    }

    private IEnumerator EggFall(Vector3 groundPosition, float speed)
    {
        float step = (speed / (transform.position - groundPosition).magnitude) * Time.fixedDeltaTime;
        float t = 0f;
        while (Vector3.Distance(transform.position, groundPosition) > 0.001f)
        {
            t += step;
            transform.position = Vector3.Lerp(transform.position, groundPosition, t);
            yield return new WaitForFixedUpdate();
        }
        anim.SetBool("isCracked", true);
    }
    
    public void Explosion()
    {
        SaveSystem.SaveStatsData(1, 0, 0, 0);

        // instantiate the particles prefab from Resources
        Instantiate(Resources.Load<GameObject>("EggCrackParticles") as GameObject, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

        SoundManager.Instance.PlaySound(SoundManager.Instance.eggCrack);
        player.GetComponent<PlayerContoller>().Invoke("KillPlayer", 2.0f); 
    }
}
