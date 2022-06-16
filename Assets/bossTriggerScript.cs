using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossTriggerScript : MonoBehaviour
{
    public Rigidbody2D rb;

    void OnTriggerEnter2D (Collider2D other)
    {   
        if (other.gameObject.tag == "Player")
        {
            rb.isKinematic = true;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {   
        if (other.gameObject.tag == "Player")
        {
            rb.isKinematic = false;
        }
    }
}
