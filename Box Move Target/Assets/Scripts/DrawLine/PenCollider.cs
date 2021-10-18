using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenCollider : MonoBehaviour
{
    public DrawLine pen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tilemap")
        {
            Debug.Log("Bi chan");
            pen.penMoveSpeed = 0.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tilemap")
        {
            Debug.Log("Thoat chan");
            pen.penMoveSpeed = 5f;
        }
    }
}
