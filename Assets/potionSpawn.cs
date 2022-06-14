using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionSpawn : MonoBehaviour
{
    public GameObject potion;
    public Transform tf;

    public bool potionUsed = false;
    public int spawnDelay = 7;
    public float offset = 2f;

    void Start()
    {   
        StartCoroutine(delay());
    }

    void Update()
    {
        if (potionUsed)
        {
            potionUsed = false;
            StartCoroutine(delay());
        }
    }

    IEnumerator delay() 
    {
        yield return new WaitForSeconds (spawnDelay);
        Instantiate(potion, new Vector3(tf.position.x, tf.position.y + offset, 0), Quaternion.identity);
    }
        
}
