using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootObject : MonoBehaviour
{

    public moveToObject moveScript;
    public bool inRange = false;
    public GameObject fireball;
    public Transform tf;
    public float shootingDelay = 1f;

    public SpriteRenderer renderer;

    public Color red = new Color(0, 0, 1, 1); //(r,g,b,a)
    public Color standard = new Color(227, 6, 225, 1); //(r,g,b,a) 
    public Color orange = new Color(225, 100, 6, 1); //(r,g,b,a)  

    private bool waiting = false;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = FindObjectOfType<moveToObject>();
        //Debug.Log(moveScript.inRange); //read it
        //moveScript.inRange = false; //change it
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            shootTimer();
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            //Debug.Log ("Player entered shooting range");
            renderer.color = red;
            shoot();
            inRange = true;
            moveScript.inRange = false;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {

            //Debug.Log ("Player left shooting range");
            if (moveScript.inTrigger == true)
            {
                moveScript.inRange = true;
                renderer.color = orange;
            }
            else {

                renderer.color = standard;
            }
            inRange = false;
        }
    }

    void shootTimer()
    {
        if (!waiting)
        {
            waiting = true;
            StartCoroutine(Wait()); 
        }
    }

    IEnumerator Wait() 
    {
        yield return new WaitForSeconds (shootingDelay);
        //Debug.Log("bang bang bitches");
        shoot();
        waiting = false;
    }

    void shoot()
    {
        Instantiate(fireball, new Vector3(tf.position.x, -1.35f, 0), Quaternion.identity);
    }
}
