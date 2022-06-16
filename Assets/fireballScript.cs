using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    
    public Transform tf;
    public float runSpeed = 0.15f;

    public GameObject enemy;
    private GameObject player;
    private Transform playerTF;
    private Vector3 target;
    private bool start = false;
    public bool shootThroughWall;

    void Start()
    {
        player = GameObject.Find("Player");
        playerTF = player.GetComponent<Transform>();
        
        //Get player's position
        target = playerTF.position;

        //Face the target
        tf.up = target - tf.position;
        StartCoroutine(startTimer());
    }
    
    void Update()
    {
        if (!GetComponent<Renderer>().isVisible && start)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {   
        //Move to target
        tf.position += tf.up * runSpeed;
    }

    void OnTriggerEnter2D (Collider2D other)
    { 
        if (start)
        {
            if (other.gameObject.tag == "Player") 
            {
                //Debug.Log("hit");
                Destroy(gameObject);
            }

            if (!shootThroughWall && other.gameObject.name != enemy.gameObject.name)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator startTimer() 
    {
        yield return new WaitForSeconds (0.01f);
        start = true;
    }
}
