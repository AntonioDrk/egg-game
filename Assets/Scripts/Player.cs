using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 movement;
    private SpriteRenderer spriteRenderer;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        if (movement.x != 0)
        {
            if (movement.x > 0)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;

            anim.SetBool("moving", true);
            rb.MovePosition(transform.position + speed * Time.deltaTime * movement);
        }
        else
            anim.SetBool("moving", false);

    }
}
