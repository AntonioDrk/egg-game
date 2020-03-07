using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContoller : MonoBehaviour
{
    public GameObject egg;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 movement;
    private SpriteRenderer spriteRenderer;

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;

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

    void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        if (movement.x != 0)
        {
            if (movement.x > 0)
            {
                transform.localScale = new Vector3(2, 2, 2);
            }
            else
            {
                transform.localScale = new Vector3(-2, 2, 2);
            }

            anim.SetBool("moving", true);

            Vector3 targetVelocity = new Vector2(speed * Time.fixedDeltaTime * movement.x * 10f, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("moving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce); 
        }
        
    }

    void CheckRaycastHit()
    {
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
                var eggController = egg.GetComponent<EggController>();
                if (eggController.isInHand)
                {
                    var eggAnimator = egg.GetComponent<Animator>();
                    eggAnimator.SetBool("isCracked", true);
                    eggController.PlaceDown();
                }
            }
            else
            {
                _grounded = true;
                anim.SetBool("jump", false);
            }
        }
    }

    void FixedUpdate()
    {
        Movement();
        CheckRaycastHit();
    }

}
