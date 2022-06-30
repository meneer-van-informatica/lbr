using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour
{
    public GameObject playerObject;
    public Transform player;
    public Transform Enemy;
    public Rigidbody2D rb;

    public int direction = -1;
    private float distance;
    public float runSpeed = 2f;
    private bool dead = false;
    public int health = 100;
    public int playerDamage = 25;

    //Different colors to indicate state:
    public Color standard = new Color();
    public Color orange = new Color();
    public Color red = new Color();
    public Color black = new Color();

    public SpriteRenderer renderer;
    //--

    private bool inWalkingRange = false;
    public float walkingRange = 8f;
    public float shootingRange = 4f;

    //Shooting variables:
    public GameObject fireball;
    public float shootingDelay = 1f;
    private bool waiting = false;
    public float ballOffset = 0.5f;
    private bool first = true; //If this is true, it's player's first time to get in shooting-range
    public bool shootThroughWall = true;
    //--

    //Variables for damage to enemy:
    public float killSeconds = 1f;
    public float appearanceSeconds = 0.2f;
    private bool appearanceStart = false;
    //--

    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Transform>();
    }

    void Update()
    {
        //Calculate distance between enemy and player
        distance = Vector3.Distance(Enemy.position, player.position);
            //Debug.Log(distance);

        //If not dead, face in direction of player
        if (!dead)
        {
            if (player.position.x < Enemy.position.x)
            {
                Enemy.eulerAngles = new Vector3(0, 0, 0);
                direction = -1;
            }
            else
            {
                Enemy.eulerAngles = new Vector3(0, 180, 0);
                direction = 1;
            }
        }

        //Different enemy states based on distance
        if (Math.Abs(player.position.x - Enemy.position.x) > 0.8)
        {
            if (distance <= shootingRange)
            {
                renderer.color = red;
                inWalkingRange = false;
                if (first)
                {   
                    first = false;
                    shoot();
                }
                shootTimer();
            }
            
            else if (distance <= walkingRange)
            {   
                renderer.color = orange;
                inWalkingRange = true;
                first = true;
            }

            else if (distance > walkingRange)
            {
                renderer.color = standard;
                inWalkingRange = false;
                first = true;

            }
        }
        else
        {
            shootTimer();
        }
    }

    void FixedUpdate()
    {
        if (inWalkingRange)
        {
            rb.velocity = new Vector2(direction * runSpeed, 0);
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
        shoot();
        waiting = false;
    }

    private fireballScript script;

    void shoot()
    {   
        //Spawns a "fireball"
        if (!dead)
        {   
            var newBall = Instantiate(fireball, new Vector3(Enemy.position.x, Enemy.position.y + ballOffset, -1), Quaternion.identity);
            script = newBall.GetComponent<fireballScript>();
            script.shootThroughWall = shootThroughWall;
            script.enemy = gameObject;
        }
    }

    //This is the trigger collider for head-hitbox
    void OnTriggerEnter2D (Collider2D other)
    {   
        if (other.gameObject.tag == "Player")
        {
            health -= playerDamage;
            if (health <= 0)
            {
                killEnemy();
            }
            else
            {
                changeAppearance();
            }
        }
    }

    void killEnemy()
    {
        dead = true;
        rb.gravityScale = 50; //Because otherwise enemy falls down like a feather
        Enemy.eulerAngles = new Vector3(0, 0, 270); //Lay on side
        renderer.color = black;
        StartCoroutine(killWait()); //Start a timer before removing enemy from scene
    }

    IEnumerator killWait() 
    {
        yield return new WaitForSeconds (killSeconds);
        Destroy(gameObject);
    }

    void changeAppearance()
    {
        renderer.color = black; //Indicate that damage has been done
        if (!appearanceStart)
        {
            appearanceStart = true;
            StartCoroutine(appearanceWait());
        }
    }

    IEnumerator appearanceWait() 
    {
        yield return new WaitForSeconds (appearanceSeconds);
        renderer.color = red;
        appearanceStart = false;
    }
}