using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScriptMM : MonoBehaviour
{

    public Animator transition;

    public void LoadAnimation(string level)
    {
        Debug.Log(level);
        StartCoroutine(LoadLevel(level));
    }

    IEnumerator LoadLevel(string level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(level);
    }
}