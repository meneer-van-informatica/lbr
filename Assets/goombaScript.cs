using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goombaScript : MonoBehaviour
{

    private int direction = -1;
    public float runSpeed = 5f;
    public float fallSpeed = -6f;
    public Rigidbody2D rb;
    public Transform tf;
    public BoxCollider2D box;
    private float startDelay = 0.5f;
    private bool start = false;
    private bool dead = false;
    private bool grounded = true;
    private float deadDelay = 1f;


    //Pas op: DEZE HELE SCRIPT IS KUT

    // Start is called before the first frame update
    void Start()
    {   
        //pick direction
        if(Random.Range(1, 3) == 1)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        StartCoroutine(Wait());
    }

    IEnumerator Wait() 
    {
        yield return new WaitForSeconds (startDelay);
        start = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            dead = true;
            rb.mass = 50;
            tf.eulerAngles = new Vector3(0, 0, 270); //Lay on side
            box.isTrigger = true;
            StartCoroutine(deadWait());
        }
    }

    void FixedUpdate()
    {
        if (start && !dead && grounded)
        {
            rb.velocity = new Vector2(direction * runSpeed, 0);
            //rb.AddForce(tf.right * runSpeed);
        }
        else if (start && !dead && !grounded)
        {
            rb.velocity = new Vector2(direction * (runSpeed/2), fallSpeed);
        }
    }

    IEnumerator deadWait() 
    {
        yield return new WaitForSeconds (deadDelay);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
     {  
        if (direction == 1)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

         if (collision.gameObject.layer == 8)
         {
             grounded = true;
         }
     }

     void OnCollisionExit2D(Collision2D collision)
     {
         if (collision.gameObject.layer == 8)
         {
             grounded = false;
             Debug.Log("yo");
         }
     }
}
