using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dùng để tăng giảm tốc độ của Pen khi tiếp xúc với các đôi tượng không thể vẽ xuyên qua
//Giảm tốc để không vẽ xuyên qua + tính năng vẽ men theo cạnh của đối tượng
//Khi hết tiếp xúc thì tăng tốc để Pen không bị delay khi follow Con trỏ
public class PenCollider : MonoBehaviour
{
    public DrawLine pen;
    //Đếm số đối tượng đang tiếp xúc với Pen
    //Tránh khi Pen thoát khỏi đối tượng 1 mà đối tượng 2 quá gần
    //Tăng tốc phát 1 dẫn tới xuyên qua đối tượng 2
    public int countCollision = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tilemap" || collision.tag == "Car" || collision.tag == "Line"
            || collision.tag == "Barrier" || collision.tag == "Wheel" || collision.tag == "Box"
            || collision.tag == "Wood")
        {
            Debug.Log("Bị chặn bởi "+collision.gameObject.name);
            countCollision++;

            pen.penMoveSpeed = 1f;
            //Làm đối tượng không bị tác dụng lực bởi Pen khi tiếp xúc
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Tilemap" || collision.tag == "Car" || collision.tag == "Line"
            || collision.tag == "Barrier" || collision.tag == "Wheel" || collision.tag == "Box"
            || collision.tag == "Wood")
        {
            //Debug.Log("Bị chặn bởi " + collision.gameObject.name);

            //if (pen.penMoveSpeed >= 1)
            //{
            //    pen.penMoveSpeed = 1f / pen.distance;
            //}
            
            //Làm đối tượng không bị tác dụng lực bởi Pen khi tiếp xúc
            //collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tilemap" || collision.tag == "Car" || collision.tag == "Line"
            || collision.tag == "Barrier" || collision.tag == "Wheel" || collision.tag == "Box"
            || collision.tag == "Wood")
        {
            Debug.Log("Thoat chan");
            if (countCollision > 1)
            {
                countCollision--;
                //Mở tác dụng lực trở lại
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                countCollision--;

                while (pen.penMoveSpeed < 20f)
                {
                    pen.penMoveSpeed += 0.1f;
                }
                //pen.penMoveSpeed = 20f;
                //Mở tác dụng lực trở lại
                collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
