using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToObject : MonoBehaviour
{

    public Transform tf;
    public Rigidbody2D rb;
    public float runSpeed = 5f;

    private bool inRange = false;

    void FixedUpdate()
    {
        if (inRange){

            rb.velocity = new Vector2(-1 * runSpeed, 0);

        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log ("Player entered walking range");
            inRange = true;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log ("Player left walking range");
            inRange = false;
        }
    }
}
