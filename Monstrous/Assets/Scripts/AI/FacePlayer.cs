using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class FacePlayer : MonoBehaviour{
        public EnemyBase enemyBase;
        void FixedUpdate(){
            //update the position of the enemy to be closer to the player
            if(enemyBase.player.position.x > transform.position.x){
                enemyBase.renderer.flipX = false;
            }
            else{
                enemyBase.renderer.flipX = true;
            }
        }
    }
}