using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float moveSpeed;
    public float tempMoveSpeed;

    public bool moveLeft;
    public bool moveRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void NotMove()
    {
        moveLeft = false;
        moveRight = false;
        tempMoveSpeed = 0;
    }

    public void Move()
    {
        if (moveLeft == true)
        {
            //Tốc độ tăng dần
            if (Mathf.Abs(tempMoveSpeed) < moveSpeed)
            {
                tempMoveSpeed -= 2;
            }
            rb2D.velocity = new Vector2(tempMoveSpeed * Time.deltaTime, 0f);
        }
        if (moveRight == true)
        {
            //Tốc độ tăng dần
            if (Mathf.Abs(tempMoveSpeed) < moveSpeed)
            {
                tempMoveSpeed += 2;
            }
            rb2D.velocity = new Vector2(tempMoveSpeed * Time.deltaTime, 0f);
        }
    }
}
