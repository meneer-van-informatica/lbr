using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    
    public void test()
    {
        SceneManager.LoadScene("scheikundeMain");
    }

    void loadMainMenu()
    {
        
    }

    void loadLevelselect()
    {
        SceneManager.LoadScene("levelselect");
    }

    void loadLevel1()
    {
        
    }

    void loadLevel2()
    {
        
    }

    void loadSettings()
    {
        SceneManager.LoadScene("settings");
    }

    void loadCredits()
    {
        SceneManager.LoadScene("credits");
    }

    public void quitGame()
    {
        Application.Quit();
    }


}
