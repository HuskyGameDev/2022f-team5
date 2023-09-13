using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class BatAI : EnemyBase{
        void FixedUpdate(){
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
        }
    }
}