using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookEnemyScript : MonoBehaviour
{
    public Transform self;
    public Transform player;

    private float distance;
    private float hookDelay = 3f;
    private bool waiting = false;
    private bool first = false;

    public float hookDistance = 6;
    public float hookOffset = 0.5f;

    public GameObject hookObject;

    void Update()
    {
        distance = Vector3.Distance(self.position, player.position);
    
        if (distance <= hookDistance)
        {   
            //Debug.Log("in range");

            if (!waiting)
            {
                if (first)
                {   
                    first = false;
                    Instantiate(hookObject, new Vector3(self.position.x, self.position.y + hookOffset, 0), Quaternion.identity);
                }
                waiting = true;
                StartCoroutine(shootTimer());
            }
        }
        else
        {
            first = true;
        }
    }

    IEnumerator shootTimer() 
    {
        yield return new WaitForSeconds (hookDelay);
        waiting = false;
        if (distance <= hookDistance)
        {
            Instantiate(hookObject, new Vector3(self.position.x, self.position.y + hookOffset, 0), Quaternion.identity);
        }
    }
}
