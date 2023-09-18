using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class BeetleAI : EnemyBase{
        [SerializeField] private Sprite curledSprite;
        bool mode = false;
        void Awake(){
            StartCoroutine(curl());
        }

        void FixedUpdate(){
            if (!mode){
                transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, speed * Time.fixedDeltaTime);
            }
        }

        private IEnumerator curl(){
            yield return new WaitForSeconds(Random.Range(3f, 10f));
            mode = true;
            renderer.sprite = curledSprite;
        }

        public override void onAttack(){}
        public override void onDeath(){}
    }
}