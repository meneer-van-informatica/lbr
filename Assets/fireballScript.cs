using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    
    public Transform tf;
    public float runSpeed = 0.15f;

    private GameObject player;
    private Transform playerTF;
    private Vector3 target;

    void Start()
    {
        player = GameObject.Find("Player");
        playerTF = player.GetComponent<Transform>();
        
        //Get player's position
        target = playerTF.position;

        //Face the target
        tf.up = target - tf.position;
    }
    
    void Update()
    {
        if (!GetComponent<Renderer>().isVisible)
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
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log("hit");
            Destroy(gameObject);
        }
    }
}
