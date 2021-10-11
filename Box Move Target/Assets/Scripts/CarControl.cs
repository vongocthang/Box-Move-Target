using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Vector3 forceLeft;
    public Vector3 forceRight;
    public float speed;

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
        NotMove();
    }

    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void Move()
    {
        if (moveLeft == true)
        {
            rb2D.velocity =new Vector2(-speed * Time.deltaTime, 0f);
        }
        if (moveRight == true)
        {
            
            rb2D.velocity = new Vector2(speed * Time.deltaTime, 0f);
        }
    }

    public void NotMove()
    {
        if (moveLeft == true || moveRight == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                moveLeft = false;
                moveRight = false;
            }
        }
    }
}
