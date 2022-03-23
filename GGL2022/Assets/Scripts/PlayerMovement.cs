using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

using aburron.Input;

public class PlayerMovement : MonoBehaviour
{

    private string direction;

    private AbuInput input = new AbuInput();

    public bool dead = false;

    Rigidbody2D body;
    SpriteRenderer sr;
    Animator animator;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    private void Awake()
    {
        input.Enable();

        input.onLeftStick += onLeftStick;
    }

    private void onLeftStick(Vector2 input)
    {
        horizontal = input.x;
        vertical = input.y;
    }

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!dead)
        {
            if (horizontal != 0)
            {
                sr.flipX = horizontal >= 0 ? false : true;
            }
            
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
            animator.SetFloat("Speed", new Vector2(horizontal, vertical).sqrMagnitude);

        }
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }

    private void OnDestroy()
    {
        input.Disable();
        input.onLeftStick -= onLeftStick;
    }
}
