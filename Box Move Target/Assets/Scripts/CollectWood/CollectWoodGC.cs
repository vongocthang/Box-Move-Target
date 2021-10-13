using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CollectWoodGC : MonoBehaviour
{
    public GameObject winGame;
    public TMP_Text countTime;

    public int countWood;
    public bool beginCountTime;


    public float timeLine;
    public float threeSecond = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WinGame();
        //BeginCountTime();
        CountTimeWin();
    }

    public void WinGame()
    {
        if (threeSecond >= 3)
        {
            StartCoroutine(DisabledCountTime());
        }
    }


    //Đếm thời gian
    public void CountTimeWin()
    {
        //Điều kiện thỏa mãn để bắt đầu đếm
        if (beginCountTime == true)
        {
            countTime.enabled = true;
            if (Time.time > timeLine + threeSecond + 1)
            {
                threeSecond++;
                countTime.text = threeSecond.ToString();
            }
        }

        //Nếu đang đếm mà điều kiện không thỏa mãn nữa thì
        if (threeSecond > 0 && beginCountTime == false)
        {
            countTime.enabled = false;
            threeSecond = 0;
            countTime.text = 0.ToString();
        }
    }

    IEnumerator DisabledCountTime()
    {
        yield return new WaitForSeconds(0.5f);

        winGame.SetActive(true);
        beginCountTime = false;
    }
}
