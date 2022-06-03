using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public Transform Enemy;

    void Update()
    {

        // this is wrong do .flipx
        if (player.position.x < Enemy.position.x)
        {
            Enemy.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            Enemy.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
