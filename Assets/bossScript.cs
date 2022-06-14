using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    //Components
    public Transform tf;
    public GameObject playerObject;
    public Transform player;
    public GameObject hook;
    public GameObject projectile;
    public Rigidbody2D rb;
    public BoxCollider2D selfBox;
    public BoxCollider2D platformBox;
    //---
    //Attack variables
    private int currentAttack = 1; // 1 = hook, 2 = throw beer
    private float distance;
    private bool attackReady = true;
    public bool hookAttack = false; //The hook-script makes it false when hook is destroyed

    public float attackRange = 5f;
    public float cooldown = 2f;
    public bool shootThroughWall = true;
    //---
    //Walking variables
    private bool walking = false;

    public float walkDuration = 2.5f;
    public float walkDelay = 4f;

    private int direction = -1;
    private int startDelayCount = 1;
    private bool start = false;
    private bool dead = false;
    private bool grounded = true;

    public float runSpeed = 6f;
    public float fallSpeed = -8f;

    private float levelLeft = -16.1f;
    private float levelRight = 8f;
    //---
    //Jumping variables
    private int currentHeight = 0; //0 = ground, 1 = platform
    //Dimensions of platform
    private float platformLeft = -6.5f;
    private float platformRight = 5.9f;
    public float platformHeight = -1.5f;

    public bool jumping = true;
    public float verticalDelay = 3f; //Delay between jumps/descends

    private bool jumpFirst = true;

    public float jumpVelocity = 4f;
    //---

    //only hook when not walking, else just fireball

    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Transform>();
        StartCoroutine(startDelay());
    }
    
    IEnumerator startDelay() 
    {
        yield return new WaitForSeconds (startDelayCount);
        start = true;
        StartCoroutine(horizontal());
        StartCoroutine(vertical());
    }

    void Update()
    {
        distance = Vector3.Distance(tf.position, player.position);

        if (distance < 2.021f)
        {
            grounded = true;
        }
        
        if (tf.position.x < levelLeft || tf.position.x > levelRight)
        {
            walking = false;
        }
    }

    private fireballScript ballScript;

    void FixedUpdate()
    {   
        distance = Vector3.Distance(tf.position, player.position);

        if (distance <= attackRange && attackReady && start)
        {
            currentAttack = Random.Range(1,3); //Choose next attack
            attackReady = false;
            StartCoroutine(cooldownTimer()); 

            if (currentAttack == 1)
            {
                spawnHook();
                hookAttack = true;
            }
            else
            {
                var newBall = Instantiate(projectile, new Vector3(tf.position.x, tf.position.y, -1), Quaternion.identity);
                ballScript = newBall.GetComponent<fireballScript>();
                ballScript.shootThroughWall = shootThroughWall;
                ballScript.enemy = gameObject;
            }
        }

        if (tf.position.y > platformHeight)
        {   
            if (jumpFirst)
            {
                jumping = false;
                grounded = false;
                currentHeight = 1;
                jumpFirst = false;
            }
        }

        if (start && !dead)
        {
            if (walking && !jumping && grounded && !hookAttack)
            {
                rb.velocity = new Vector2(direction * runSpeed, 0);
            }
            else if (!jumping && !grounded)
            {
                rb.velocity = new Vector2(direction * runSpeed/2, fallSpeed);
            }
            else if (jumping && !hookAttack)
            {
                rb.velocity = new Vector2(0, jumpVelocity);
            }
        }
    }

    IEnumerator cooldownTimer() 
    {
        yield return new WaitForSeconds (cooldown);
        attackReady = true;
    }

    IEnumerator horizontal()
    {   
        if (player.position.x > tf.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        walking = true;
        yield return new WaitForSeconds (walkDuration);
        walking = false;

        yield return new WaitForSeconds (walkDelay);
        StartCoroutine(horizontal());
    }

    IEnumerator vertical()
    {   
        if (currentHeight == 0 && player.position.y > -5f && tf.position.x >= platformLeft && tf.position.x <= platformRight)
        {
            //jump
            jumpFirst = true;
            jumping = true;
            currentHeight = 1;
        }

        else if (currentHeight == 1 && player.position.y < tf.position.y)
        {
            //descend
            Physics2D.IgnoreCollision(selfBox, platformBox);
            yield return new WaitForSeconds (1);
            Physics2D.IgnoreCollision(selfBox, platformBox, false);
            currentHeight = 0;
        }

        yield return new WaitForSeconds (verticalDelay);
        StartCoroutine(vertical());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.layer == 8 && !jumping)
        {
            grounded = true;
        }

        if (collision.gameObject.tag == "Floor")
        {
            currentHeight = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {   
        if (collision.gameObject.layer == 8 && !jumping)
        {
            grounded = true;
        }
    }

    private HookScript script;

    void spawnHook()
    {
        var newHook = Instantiate(hook, new Vector3(tf.position.x, tf.position.y, 0), Quaternion.identity);
        script = newHook.GetComponent<HookScript>();
        script.enemyObject = gameObject;
        script.usedWithBoss = true;
    }

    /*void OnTriggerEnter2D()
    {
        grounded = true;
    }

    void OnTriggerExit2D()
    {
        grounded = false;
    }*/
}