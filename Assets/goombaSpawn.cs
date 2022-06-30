using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goombaSpawn : MonoBehaviour
{
    public GameObject goomba1;
    //public GameObject goomba2;
    //public GameObject goomba3;

    //private int goombaIndex = 1;

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
            Instantiate(goomba1, new Vector3(tf.position.x, tf.position.y, 0), Quaternion.identity);

            /*
            if (goombaIndex == 1)
            {
                Instantiate(goomba1, new Vector3(tf.position.x, tf.position.y, 0), Quaternion.identity);
            }
            else if (goombaIndex == 2)
            {
                Instantiate(goomba2, new Vector3(tf.position.x, tf.position.y, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(goomba3, new Vector3(tf.position.x, tf.position.y, 0), Quaternion.identity);
            }
            if(goombaIndex == 3){
                goombaIndex = 1;
            }else{
                goombaIndex++;
            }
            */
        }
    }
    IEnumerator delay() 
    {
        yield return new WaitForSeconds (spawnDelay);
        ready = true;
    }
}
