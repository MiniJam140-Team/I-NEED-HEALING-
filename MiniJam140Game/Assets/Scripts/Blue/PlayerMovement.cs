using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public UnityEngine.Vector2 moveDir;
    [HideInInspector]
    public UnityEngine.Vector2 lastMovedVector;

    //References
    Rigidbody2D rb;
    public CharacterScriptableObject characterData;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new UnityEngine.Vector2(1, 0f);  //If we don't do this and game starts, the player doesn't move and the projectile will have no movement
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new UnityEngine.Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new UnityEngine.Vector2(lastHorizontalVector, 0f);  //Last moved X
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new UnityEngine.Vector2(0f, lastVerticalVector);  //Last moved Y
        }

        if(moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new UnityEngine.Vector2(lastHorizontalVector, lastVerticalVector);  //While moving
        }

    }

    void Move()
    {
        rb.velocity = new UnityEngine.Vector2 (moveDir.x * characterData.MoveSpeed, moveDir.y * characterData.MoveSpeed);
    }
}
