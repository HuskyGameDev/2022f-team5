using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class HellhoundAI : EnemyBase{
        [SerializeField] private float debuffStrength = 0.5f;
        private Vector2 playerOffset;
        private bool attacking = false;

        void FixedUpdate(){
            if (attacking){
                transform.position = playerLoc.position + new Vector3(playerOffset.x, playerOffset.y, 0);
            }else{
                transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, speed * Time.fixedDeltaTime);
            }
        }
    
        public override void onAttack(){
            if (!attacking){
                playerOffset = transform.position - playerLoc.position;
                StartCoroutine(munching());
                player.speedDebuff += debuffStrength;
                attacking = true;
            }
        }

        public override void onDeath(){
            player.speedDebuff -= debuffStrength;
        }

        private IEnumerator munching(){
            yield return new WaitForSeconds(1f);
            player.TakeDamage(damage);
            StartCoroutine(munching());
        }
    }
}