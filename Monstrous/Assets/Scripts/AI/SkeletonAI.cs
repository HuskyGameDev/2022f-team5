using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class SkeletonAI : EnemyBase{
        void FixedUpdate(){
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
            //update the position of the enemy to be closer to the player
            if(player.position.x > transform.position.x){
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else{
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
