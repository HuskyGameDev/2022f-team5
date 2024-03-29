using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class HellhoundAI : EnemyBase{
        [Header("Hellhound")]
        [SerializeField] private float debuffStrength = 0.5f;
        [SerializeField] private float munchDelay = 2f;
        private Vector2 playerOffset;
        private bool attacking = false;

        void FixedUpdate(){
            if (attacking){
                transform.position = playerLoc.position + new Vector3(playerOffset.x, playerOffset.y, 0);
            }else{
                transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, (speed / speedDebuff) * Time.fixedDeltaTime);
            }
        }
    
        public override void onAttack(){
            if (!attacking){
                playerOffset = transform.position - playerLoc.position;
                player.speedDebuff += debuffStrength;
                attacking = true;
                StartCoroutine(munching());
            }
        }

        public override void onDeath(){
            if (attacking) player.speedDebuff -= debuffStrength;
        }

        private IEnumerator munching(){
            yield return new WaitForSeconds(munchDelay);
            player.TakeDamage(damage / 2);
            StartCoroutine(munching());
        }
    }
}