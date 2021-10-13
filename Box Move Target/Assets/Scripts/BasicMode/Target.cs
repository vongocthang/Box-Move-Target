using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool beginCountTime;
    public float timeLine;
    public bool winGame;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CountTimeWin();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
            Debug.Log("Vao dung vi tri");
            beginCountTime = true;
            timeLine = Time.time;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
            beginCountTime = false;
        }
    }

    

    public void CountTimeWin()
    {
        if (beginCountTime == true)
        {
            if(Time.time > timeLine + 3)
            {
                winGame = true;
            }
            
        }
    }

}
