using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class LichAI : EnemyBase{
        private void FixedUpdate(){
            transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, speed * Time.fixedDeltaTime);
        }

        public override void onAttack(){}
        public override void onDeath(){}
    }
}
