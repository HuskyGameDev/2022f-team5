using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ZombieEnemy : Enemy
{

    private void Start()
    {
         // base zombie health 100
         // base zombie move speed 2.3
         // base zombie damage 17
        base.Start();

    }

    private void Update()
    {
        // enemy movement

    }

}
