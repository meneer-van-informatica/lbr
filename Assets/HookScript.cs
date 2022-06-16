using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    public Transform tf;
    public SpriteRenderer renderer;
    public Color standard = new Color();
    public Color orange = new Color();

    public float runSpeed = 0.25f;
    private bool arrived = false;
    
    public float velocityX = 15f;
    public float velocityY = 15f;

    private GameObject playerObject;
    private Transform player;
    private Rigidbody2D playerRb;

    public GameObject enemyObject;
    private bossScript bossScript;
    private HookEnemyScript hookScript;
    public Transform enemy;

    public GameObject rope;
    
    public bool usedWithBoss = false;

    void Start()
    {
        //Assign player
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Transform>();
        playerRb = playerObject.GetComponent<Rigidbody2D>();

        //Face player
        tf.up = player.position - tf.position;

        //Assign enemy
        enemy = enemyObject.GetComponent<Transform>();

        //USE:
        if (usedWithBoss)
        {
            bossScript = enemyObject.GetComponent<bossScript>();
        }
        else 
        {
            hookScript = enemyObject.GetComponent<HookEnemyScript>();
        }

    }

    void Update()
    {
        if (Vector3.Distance(tf.position, enemy.position) > 6)
        {
            //Debug.Log("arrived at location");
            arrived = true;
            renderer.color = orange;
            //Face enemy (parent)
            tf.up = enemy.position - tf.position;
        }
        else
        {
            renderer.color = standard;
        }

        if (!arrived)
        {
            Instantiate(rope, new Vector3(tf.position.x, tf.position.y, tf.position.z + 1), Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        //Move towards target
        tf.position += tf.up * runSpeed;
    }

    void OnTriggerEnter2D (Collider2D other)
    {   
        if (other.gameObject.name == enemyObject.name && arrived)
        {   
            if (usedWithBoss)
            {
                bossScript.hookAttack = false;
            }
            else
            {
                hookScript.hookAttack = false;
            }
            
            Destroy(gameObject);

        }
        else if (other.gameObject.tag == "Rope" && arrived)
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Player" && !arrived)
        {
            arrived = true;
            tf.up = enemy.position - tf.position;
            //Debug.Log("early return");
            hitEnemy();
        }
        else if (other.gameObject.layer == 8 && other.gameObject.name != enemyObject.name && !arrived) //Boss also has 'ground'-layer
        {
            arrived = true;
            tf.up = enemy.position - tf.position;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.tag == "Rope" && arrived)
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.name == enemyObject.name && arrived)
        {
            if (usedWithBoss)
            {
                bossScript.hookAttack = false;
            }
            else
            {
                hookScript.hookAttack = false;
            }

            Destroy(gameObject);
        }
    }

    void hitEnemy()
    {
        playerRb.velocity = new Vector2(velocityX, velocityY);
    }
}
