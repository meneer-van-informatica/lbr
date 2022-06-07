using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{   
    private Transform hook;
    public Transform tf;

    void Start()
    {
        hook = GameObject.Find("Hook(Clone)").GetComponent<Transform>();
        tf.up = hook.up;
    }

    void Update()
    {
        if (GameObject.Find("Hook(Clone)") == null)
        {
            Destroy(gameObject);
        }
    }
}
