using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class FacePlayer : MonoBehaviour{
        [SerializeField] private EnemyBase enemyBase;
        [SerializeField] private bool invert = false;
        void FixedUpdate(){
            //update the position of the enemy to be closer to the player
            if(enemyBase.playerLoc.position.x > transform.position.x){
                enemyBase.renderer.flipX = invert;
            }else{
                enemyBase.renderer.flipX = !invert;
            }
        }
    }
}