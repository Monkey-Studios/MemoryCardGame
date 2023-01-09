using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("The Game has shutdown");
    }
}
