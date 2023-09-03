using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    Rigidbody2D rb;
    [HideInInspector]
    Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    Animator animator;

    //References
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        InputManagement();

        if(moveDir.x != 0 || moveDir.y != 0)
        {
            animator.SetBool(AnimationStrings.isMoving, true);
            SpriteDirectionChecker();
        }
        else
        {
            animator.SetBool (AnimationStrings.isMoving, false);
        }
    }

    void SpriteDirectionChecker()
    {
        if(moveDir.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if(moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;

        }
        if(moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
