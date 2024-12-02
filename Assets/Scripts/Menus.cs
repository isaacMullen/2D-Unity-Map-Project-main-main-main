using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("2DRPG");
    }
   
    public void BackToMenu()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
