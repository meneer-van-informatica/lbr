using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionScript : MonoBehaviour
{   
    public GameObject spawner;
    public potionSpawn script;

    void Start()
    {
        spawner = GameObject.Find("PotionSpawner");
        script = spawner.GetComponent<potionSpawn>();
    }

    void OnTriggerEnter2D (Collider2D other)
     {
         if (other.gameObject.tag == "Player") 
         {
            script.potionUsed = true;
            Destroy(gameObject);
         }
     }
}