using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Win win;
    public TMP_Text winGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinGame()
    {
        if (win.winGame == true)
        {
            winGame.enabled = true;
        }
    }
}
