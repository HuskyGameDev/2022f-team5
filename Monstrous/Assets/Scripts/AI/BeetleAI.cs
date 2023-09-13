using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class BeetleAI : EnemyBase{
        void FixedUpdate(){
            //TODO: Add the actual behavior here
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
        }
    }
}