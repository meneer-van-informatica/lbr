using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionScript : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
     {
         if (other.gameObject.tag == "Player") 
         {
             Destroy(gameObject);
         }
     }
}