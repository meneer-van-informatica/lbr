using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{   
    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void loadLevelselect()
    {
        SceneManager.LoadScene("levelselect");
    }

    public void loadScheikunde1()
    {
        SceneManager.LoadScene("scheikundeMain");
    }

    public void loadScheikundeBoss()
    {
        SceneManager.LoadScene("bossScheikunde");
    }

    public void loadWiskunde1()
    {
        
    }

    public void loadBio1()
    {
    }

    public void loadCredits()
    {
    }

    public void quitGame()
    {
        Application.Quit();
    }


}
