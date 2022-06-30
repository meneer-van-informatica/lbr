using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{

    //GroundCheck (SO --> Casper)
    public Transform groundCheck;
    private float GC_width = 0.9f;
    private float GC_height = 0.3f;
    public LayerMask groundLayer;

    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(groundCheck.position, new Vector3(GC_width, GC_height, 0), 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    //---
    //Components
    public Transform tf;
    public GameObject playerObject;
    public Transform player;
    public GameObject hook;
    public GameObject projectile;
    public Rigidbody2D rb;
    public BoxCollider2D selfBox;
    public BoxCollider2D platformBox;
    public PlayerMovement movementScript;
    public SpriteRenderer renderer;
    //---
    //Attack variables
    private int currentAttack = 1; // 1 = hook, 2 = throw projectile
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

    public float runSpeed = 6f;
    public float fallSpeed = -8f;

    //---
    //Jumping variables
    private int currentHeight = 0; //0 = ground, 1 = platform
    //Dimensions of platform
    private float platformLeft = -11.0f;
    private float platformRight = 2.2f;
    private float platformHeight = -5.6f;

    private bool jumping = true;
    public float verticalDelay = 3f; //Delay between jumps/descends

    public float jumpVelocity = 4f;
    //---
    //Health
    private int health = 100;
    public int playerDamage = 10;

    public Color black = new Color();
    public Color standard = new Color();
    //---

    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Transform>();
        movementScript = player.GetComponent<PlayerMovement>();
        StartCoroutine(startDelay());
    }
    
    IEnumerator startDelay() 
    {
        yield return new WaitForSeconds (startDelayCount);
        start = true;
        StartCoroutine(horizontal());
        StartCoroutine(vertical());
    }

    private fireballScript ballScript;

    void FixedUpdate()
    {   
        distance = Vector3.Distance(tf.position, player.position);

        if (distance <= attackRange && attackReady && start && !dead) //&& isGrounded()?
        {
            currentAttack = Random.Range(1,3); //Choose next attack, 1,2 or 3. Only 1 is hook attack, others are projectile
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

        
        if (tf.position.y > platformHeight && !isGrounded())
        {   
            
            jumping = false;
            currentHeight = 1;
        }
        

        if (start && !dead && !hookAttack)
        {
            if (!jumping && !isGrounded())
            {
                rb.velocity = new Vector2(direction * runSpeed, fallSpeed); //runSpeed/2, but gets stuck on edge of platform
            }
            else if (currentHeight == 1 && tf.position.y < platformHeight - 0.08f && isGrounded() && !jumping)
            {
                rb.velocity = new Vector2(direction * runSpeed/2, fallSpeed);
            }
            else if (walking && !jumping && isGrounded())
            {
                rb.velocity = new Vector2(direction * runSpeed, 0);
            }
            else if (jumping)
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
        //  If on the ground && the player is higher && I'm under the platform
        if (currentHeight == 0 && player.position.y > -5f && tf.position.x >= platformLeft && tf.position.x <= platformRight)
        {
            //jump
            jumping = true;            
        }

        else if (currentHeight == 1 && player.position.y < tf.position.y && !hookAttack)
        {
            //Drop
            Physics2D.IgnoreCollision(selfBox, platformBox);
            yield return new WaitForSeconds (1);
            Physics2D.IgnoreCollision(selfBox, platformBox, false);
        }

        yield return new WaitForSeconds (verticalDelay);
        StartCoroutine(vertical());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.tag == "Floor")
        {
            currentHeight = 0;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            health -= playerDamage;
            if (health <= 0)
            {
                //Die:
                dead = true;
                tf.eulerAngles = new Vector3(0, 0, 270);
                StartCoroutine(die());
            }
            else
            {
                renderer.color = black;
                StartCoroutine(damage());

            }
        }
    }

    IEnumerator die()
    {
        movementScript.winBool = true;
        renderer.color = black;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    IEnumerator damage()
    {
        yield return new WaitForSeconds(0.2f);
        renderer.color = standard;
    }    
}