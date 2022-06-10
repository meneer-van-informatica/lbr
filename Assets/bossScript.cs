using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{

    private int currentAttack = 1; // 1 = hook, 2 = throw potion
    private float distance;
    private bool ready = true;

    public Transform tf;
    public Transform player;
    public GameObject hook;
    public GameObject projectile;

    public float attackRange = 5f;
    public float cooldown = 2f;

    void Update()
    {
        distance = Vector3.Distance(tf.position, player.position);

        if (distance <= attackRange && ready)
        {
            attack();
        }

    }

    void attack()
    {
        //STILL INSERT HOOK ATTACK
        if (currentAttack == 1)
        {
            Instantiate(hook, new Vector3(tf.position.x, tf.position.y, 0), Quaternion.identity); 
        }
        else
        {
            Instantiate(projectile, new Vector3(tf.position.x, tf.position.y, 0), Quaternion.identity);
        }
        
        ready = false;

        StartCoroutine(cooldownTimer()); 

        currentAttack  = Random.Range(1,3); //Choose next attack
    }

    IEnumerator cooldownTimer() 
    {
        yield return new WaitForSeconds (cooldown);
        ready = true;
    }
}
