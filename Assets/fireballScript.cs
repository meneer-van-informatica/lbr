using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public int runSpeed = 15;

    void Update()
    {
         if (!GetComponent<Renderer>().isVisible)
            {
                Destroy(gameObject);
            }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(-1 * runSpeed, 0);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log("hit");
            Destroy(gameObject);
        }
    }
}
