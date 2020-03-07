using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 movement;
    private SpriteRenderer spriteRenderer;

    public float speed;
    public float _jumpForce;

    private bool _grounded;
    private float _groundedDistValue = 3.2f;


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
       
    }

    void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        if (movement.x != 0)
        {
            if (movement.x > 0)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;

            anim.SetBool("moving", true);
            rb.velocity = new Vector2(rb.velocity.x + speed * Time.fixedDeltaTime * movement.x, rb.velocity.y);
        }
        else
            anim.SetBool("moving", false);
        
        if(Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce); 
        }
        
    }

    void FixedUpdate()
    {
        Movement();

        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 100, LayerMask.GetMask("Platform"));
        
        // If it hits something...
        if (hit.collider != null)
        {
            float distToGround = Mathf.Abs(hit.transform.position.y - transform.position.y);
            //Debug.Log(distToGround);
            if (distToGround > _groundedDistValue)
            {
                _grounded = false;
                anim.SetBool("jump", true);
            }
            else
            {
                _grounded = true;
                anim.SetBool("jump", false);
            }
        }
    }

}
