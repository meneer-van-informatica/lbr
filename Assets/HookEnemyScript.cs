using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookEnemyScript : MonoBehaviour
{
    public Transform self;
    public GameObject playerObject;
    public Transform player;
    public Rigidbody2D rb;

    private float distance;
    private float hookDelay = 3f;
    private bool waiting = false;
    private bool first = false;
    private bool dead = false;
    private int health = 100;
    private float direction = -1;

    public float hookDistance = 6;
    public float hookOffset = 0.5f;
    public float runSpeed = 5f;

    public bool hookAttack = false; //The hook-script makes it false when hook is destroyed

    public GameObject hookObject;

    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Transform>();
    }

    void Update()
    {
        distance = Vector3.Distance(self.position, player.position);

        if (distance <= hookDistance) //In range
        {   
            if (!waiting)
            {
                if (first)
                {   
                    first = false;
                    spawn();
                }
                waiting = true;
                StartCoroutine(shootTimer());
            }
        }
        else
        {
            first = true;
        }
    }

    void FixedUpdate()
    {   
        if (player.position.x < self.position.x)
        {
            direction = -1;
            self.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            direction = 1;
            self.eulerAngles = new Vector3(0, 180, 0);
        }

        if (!dead && distance < hookDistance + 2 && !hookAttack)
        {
            rb.velocity = new Vector2(direction * runSpeed, 0);
        }
    }

    IEnumerator shootTimer() 
    {
        yield return new WaitForSeconds (hookDelay);
        waiting = false;
        if (distance <= hookDistance)
        {
            spawn();
        }
    }

    private HookScript script;

    void spawn()
    {   
        if (!dead)
        {
            var newHook = Instantiate(hookObject, new Vector3(self.position.x, self.position.y + hookOffset, 0), Quaternion.identity);
            script = newHook.GetComponent<HookScript>();
            script.enemyObject = gameObject;
            script.usedWithBoss = false;
            hookAttack = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            health -= 25;
            if (health <= 0)
            {
                die();
            }
        }
    }

    IEnumerator deadWait()
    {
        yield return new WaitForSeconds (1);
        Destroy(gameObject);
    }

    void die()
    {
        dead = true;
        rb.mass = 50;
        self.eulerAngles = new Vector3(0, 0, 270); //Lay on side
        StartCoroutine(deadWait());
    }
}
