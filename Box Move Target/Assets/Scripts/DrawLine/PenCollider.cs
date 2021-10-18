using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenCollider : MonoBehaviour
{
    public DrawLine pen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tilemap" || collision.tag == "Line" || collision.tag == "Car" 
            || collision.tag == "Barrier" || collision.tag == "Wheel" || collision.tag == "Box"
            || collision.tag == "Wood")
        {
            Debug.Log("Bi chan");
            pen.penMoveSpeed = 0.5f;
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.tag == "Tilemap" || collision.tag == "Line")
    //    {
    //        pen.penMoveSpeed = 0.5f;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tilemap" || collision.tag == "Line" || collision.tag == "Car"
            || collision.tag == "Barrier" || collision.tag == "Wheel" || collision.tag == "Box"
            || collision.tag == "Wood")
        {
            Debug.Log("Thoat chan");
            pen.penMoveSpeed = 10f;
        }
    }
}
