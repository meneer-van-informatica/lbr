using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goombaSpawn : MonoBehaviour
{
    public GameObject goomba;
    public Transform tf;

    private bool ready = false;
    public int spawnDelay = 7;

    void Start()
    {
        StartCoroutine(delay());
    }
    void Update()
    {
        if (ready)
        {
            ready = false;
            StartCoroutine(delay());

            Instantiate(goomba, new Vector3(tf.position.x, tf.position.y, 0), Quaternion.identity);
        }
    }
    IEnumerator delay() 
    {
        yield return new WaitForSeconds (spawnDelay);
        ready = true;
    }
}
