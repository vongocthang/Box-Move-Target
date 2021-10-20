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

    //Để xác định con dốc nằm ở bên nào
    //Từ đó xác định di chuyển trái/phải là xuống dốc
    public float zRotation;
    //public float yVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //Debug.Log(this.transform.rotation.z);
        
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
        zRotation = this.transform.rotation.z;

        if (moveLeft == true)
        {
            //Tốc độ tăng dần
            if (Mathf.Abs(tempMoveSpeed) < moveSpeed)
            {
                tempMoveSpeed -= 2;
            }
            if (zRotation > 0.02)
            {
                Debug.Log("Xuống dốc phải");
                //yVelocity = -10;
                //yVelocity *= zRotation;
            }
            else
            {
                Debug.Log("Tác dụng lực Veloctiy");
                rb2D.velocity = new Vector2(tempMoveSpeed * Time.deltaTime, 0f);
            }
            //rb2D.velocity = new Vector2(tempMoveSpeed * Time.deltaTime, yVelocity);
        }
        if (moveRight == true)
        {
            //Tốc độ tăng dần
            if (Mathf.Abs(tempMoveSpeed) < moveSpeed)
            {
                tempMoveSpeed += 2;
            }
            if (zRotation < -0.02)
            {
                Debug.Log("Xuống dốc trái");
                //yVelocity = -10;
                //yVelocity *= (-zRotation);
            }
            else
            {
                Debug.Log("Tác dụng lực Veloctiy");
                rb2D.velocity = new Vector2(tempMoveSpeed * Time.deltaTime, 0f);
            }
            //rb2D.velocity = new Vector2(tempMoveSpeed * Time.deltaTime, yVelocity);
        }
    }
}
