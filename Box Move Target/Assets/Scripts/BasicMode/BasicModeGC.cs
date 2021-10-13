using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BasicModeGC : MonoBehaviour
{
    public Target target;
    public GameObject winGame;
    public TMP_Text countTime;

    public float threeSecond = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WinGame();
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
        if (target.beginCountTime == true)
        {
            countTime.enabled = true;

            if (Time.time > target.timeLine + threeSecond + 1)
            {
                threeSecond++;
                countTime.text = threeSecond.ToString();
            }
        }

        if (target.beginCountTime == false)
        {
            countTime.text = 0.ToString();
            threeSecond = 0;
            countTime.enabled = false;
        }
    }

    IEnumerator DisabledCountTime()
    {
        yield return new WaitForSeconds(0.5f);

        winGame.SetActive(true);
        target.beginCountTime = false;
    }
}
