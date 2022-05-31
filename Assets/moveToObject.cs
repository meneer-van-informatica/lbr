using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToObject : MonoBehaviour
{

    public Transform tf;
    public Rigidbody2D rb;
    public float runSpeed = 2f;

    public bool inRange = false;
    public bool inTrigger = false;

    public SpriteRenderer renderer;

    public Color standard;
    public Color orange;

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
            //Debug.Log ("Player entered walking range");
            inTrigger = true;
            inRange = true;
            renderer.color = orange;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            //Debug.Log ("Player left walking range");
            renderer.color = standard;
            inTrigger = false;
            inRange = false;
        }
    }
}
