using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goombaScript : MonoBehaviour
{

    public bool useBoundaries = false;
    public float leftBoundary;
    public float rightBoundary;

    public int direction = -1;
    public bool dead = false;
    public float runSpeed = 5f;
    public float fallSpeed = -6f;
    public Rigidbody2D rb;
    public Transform tf;
    private float startDelay = 0.5f;
    private bool start = false;
    private float deadDelay = 1f;

    public Transform groundCheck;
    private float GC_width = 1.1f;
    private float GC_height = 0.3f;
    public LayerMask groundLayer;

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
        //Debug.Log(direction);
        StartCoroutine(Wait());
    }

    IEnumerator Wait() 
    {
        yield return new WaitForSeconds (startDelay);
        start = true;
    }

    void FixedUpdate()
    {
        if (dead){return;}

        if (useBoundaries)
        {
            if (tf.position.x < leftBoundary || tf.position.x > rightBoundary)
            {

                //So theoretically the rotation is reversed now, but because Casper fucked the animation up it works
                if (direction == 1)
                {
                    direction = -1;
                    tf.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    direction = 1;
                    tf.eulerAngles = new Vector3(0, 0, 0);
                }
            }
        }

        if (start && !dead && isGrounded())
        {
            rb.velocity = new Vector2(direction * runSpeed, 0);
            //Debug.Log("walking");
        }
        else if (start && !dead && !isGrounded())
        {
            rb.velocity = new Vector2(direction * runSpeed/2, fallSpeed);
            //Debug.Log("falling");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {  
        dead = true;
        rb.mass = 50;
        tf.eulerAngles = new Vector3(0, 0, 270); //Lay on side
        StartCoroutine(deadWait());

        /*
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
        }
        */
    }

    IEnumerator deadWait() 
    {
        yield return new WaitForSeconds (deadDelay);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
     {  
        if (dead){return;}


        if (direction == 1)
        {
            direction = -1;
            tf.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            direction = 1;
            tf.eulerAngles = new Vector3(0, 0, 0);
        }
     }

     public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(groundCheck.position, new Vector3(GC_width, GC_height, 0), 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
