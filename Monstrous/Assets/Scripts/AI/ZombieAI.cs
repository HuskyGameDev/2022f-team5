using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class ZombieAI : EnemyBase{
        // Update is called once per frame
        void FixedUpdate(){
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
        }
    }
}