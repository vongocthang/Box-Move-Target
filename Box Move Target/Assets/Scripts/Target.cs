using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public bool beginCountTime;
    public float countTime;
    public bool winGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
            Debug.Log("Vao vi tri");
            beginCountTime = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
            beginCountTime = false;
        }
    }

    public void CountTime()
    {
        if (beginCountTime == true)
        {
            
        }
    }
}
