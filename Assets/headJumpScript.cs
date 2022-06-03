using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headJumpScript : MonoBehaviour
{
    public SpriteRenderer renderer;
    public GameObject enemy;
    private int health = 100;
    public Transform tf;
    public Rigidbody2D rb;

    public Color black = new Color();
    public Color standard = new Color();

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyGetDamage();
        }
    }

    void enemyGetDamage() //Apply damage and check if not dead
    {
        health -= 25;
        //Debug.Log(health);
        if (health <= 0)
        {
            killEnemy();
        }
        else
        {
            changeAppearance();
        }
    }

    private float appearanceSeconds = 0.2f;
    private bool appearanceStart = false;

    void changeAppearance() //Change color for short period indicating damage
    {
        renderer.color = black;
        if (!appearanceStart)
        {
            appearanceStart = true;
            StartCoroutine(appearanceWait());
        }
    }

    IEnumerator appearanceWait() 
    {
        yield return new WaitForSeconds (appearanceSeconds);
        renderer.color = standard;
        appearanceStart = false;
    }

    private float killSeconds = 1f;

    void killEnemy() //First lay on ground, after 3 seconds Destroy
    {   
        //Debug.Log("Kill");
        rb.gravityScale = 50;
        tf.eulerAngles = new Vector3(0, 0, 270);
        renderer.color = black;
        StartCoroutine(killWait());
    }

    IEnumerator killWait() 
    {
        yield return new WaitForSeconds (killSeconds);
        //Debug.Log("Destroy");
        Destroy(enemy);
    }

}
