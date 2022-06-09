using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goombaScript : MonoBehaviour
{

    private int direction = -1;
    public float runSpeed = 5f;
    public Rigidbody2D rb;
    public Transform tf;
    private int startDelay = 1;
    private bool start = false;
    private bool dead = false;
    private float deadDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait() 
    {
        yield return new WaitForSeconds (startDelay);
        start = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (direction == 1)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("hit");
        }
    }

    void FixedUpdate()
    {
        if (start && !dead)
        {
            rb.velocity = new Vector2(direction * runSpeed, 0);
        }
    }

    //Jumped on head
    void OnTriggerEnter2D()
    {
        dead = true;
        rb.mass = 50;
        tf.eulerAngles = new Vector3(0, 0, 270); //Lay on side
        StartCoroutine(deadWait());
    }

    IEnumerator deadWait() 
    {
        yield return new WaitForSeconds (deadDelay);
        Destroy(gameObject);
    }
}
