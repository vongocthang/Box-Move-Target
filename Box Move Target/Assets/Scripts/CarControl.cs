using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float moveSpeed;
    public float rotateSpeed;

    public GameObject wheel1;
    public GameObject wheel2;

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
    }

    public void Move()
    {
        if (moveLeft == true)
        {
            
            rb2D.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0f);
            wheel1.transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
            wheel2.transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
        }
        if (moveRight == true)
        {
            
            rb2D.velocity = new Vector2(moveSpeed * Time.deltaTime, 0f);
            wheel1.transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
            wheel2.transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
        }
    }
}
