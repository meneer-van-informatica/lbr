using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{   
    public LoaderScriptMM scriptLoad;

    public void loadMainMenu()
    {
        scriptLoad.LoadAnimation("MainMenu");
    }

    public void loadSettings()
    {
        scriptLoad.LoadAnimation("Settings");
    }

    public void loadLevelselect()
    {
        scriptLoad.LoadAnimation("levelselect");
    }

    public void loadScheikunde1()
    {
        scriptLoad.LoadAnimation("Scheikundeklaar");
    }

    public void loadScheikundeBoss()
    {
        scriptLoad.LoadAnimation("bossScheikunde");
    }

    public void loadWiskunde1()
    {
        scriptLoad.LoadAnimation("Wiskundeklaar");
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
