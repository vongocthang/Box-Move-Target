using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIComtroller : MonoBehaviour
{
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadBasicMode()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadCollectWood()
    {
        SceneManager.LoadScene(1);
    }
}
