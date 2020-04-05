﻿using System;
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
    private Vector2 m_Velocity = Vector2.zero;

    [SerializeField] private float _xSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _grounded = true;

    /// Used in the checking of the collision with the ground
    /// Needed because of the inconsistency of collider positions at really small values
    private float _groundCheckingBias = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        Movement();
    }
    
    void Movement()
    {
        movement.x = Input.GetAxis("Horizontal");

        if (movement.x != 0)
        {
            // Flipping of the sprite
            if (movement.x > 0)
            {
                transform.localScale = new Vector3(2, 2, 2);
            }
            else
            {
                transform.localScale = new Vector3(-2, 2, 2);
            }
            
            // Start the moving animation
            anim.SetBool("moving", true);
            
            Vector3 targetVelocity = new Vector2(_xSpeed * Time.fixedDeltaTime * movement.x, rb.velocity.y);
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("moving", false);
        }

        if (Input.GetKey(KeyCode.Space) && _grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
            OnPlayerJump();
        }
        
    }

    /// <summary>
    /// Function callback that gets called when the player jumps
    /// </summary>
    private void OnPlayerJump()
    {
        EggController eggController = egg.GetComponent<EggController>();
        if (eggController.isInHand)
        {
            var eggAnimator = egg.GetComponent<Animator>();
            eggAnimator.SetBool("isCracked", true);
            eggController.PlaceDown();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        // Check if the collision was made with an object under the player
        if (CollisionIsWithGround(other))
            _grounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // If it exited from a collision with the ground it isn't grounded anymore
        if (!CollisionIsWithGround(other))
            _grounded = false;
    }

    /// <summary>
    /// Checks to see if the object collided with is under the player
    /// </summary>
    /// <param name="collision">The collision 2D</param>
    /// <returns></returns>
    bool CollisionIsWithGround(Collision2D collision)
    {
        bool isWithGround = false;
        
        // foreach contact point
        foreach (ContactPoint2D c in collision.contacts)
        {
            // If the point is lower, then the player is on the ground
            if (c.point.y < spriteRenderer.bounds.min.y + _groundCheckingBias)
            {
                isWithGround = true;
            }
        }

        return isWithGround;
    }
}
