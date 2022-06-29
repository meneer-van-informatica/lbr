using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsLoad : MonoBehaviour
{   
    public Animator credits;

    public void LoadAnimation()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        credits.SetTrigger("Start");

        yield return new WaitForSeconds(10);

        SceneManager.LoadScene("MainMenu");
    }

}
