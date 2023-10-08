using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class BeetleAI : EnemyBase{
        [SerializeField] private Sprite uncurledSprite;
        [SerializeField] private Sprite curledSprite;
        [SerializeField] private float curledHealthBoost = 1.5f;
        [SerializeField] private float uncurlDist = 15f;
        [SerializeField] private float minCurlDist = 2f;
        [SerializeField] private float maxCurlDist = 7f;
        [SerializeField] private float minCurlTime = 5f;
        bool mode = false;
        float timer;
        float curlDist;

        void Awake(){
            curlDist = Random.Range(minCurlDist, maxCurlDist);
        }

        void FixedUpdate(){
            if (!mode){
                transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, speed * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, playerLoc.position) <= curlDist){
                    mode = true;
                    renderer.sprite = curledSprite;
                    health *= curledHealthBoost;
                    timer = minCurlTime;
                }
            }else if ((timer <= 0) && (Vector3.Distance(transform.position, playerLoc.position) > uncurlDist)){
                mode = false;
                renderer.sprite = uncurledSprite;
                health /= curledHealthBoost;
            }else{
                timer -= Time.fixedDeltaTime;
            }
        }

        public override void onAttack(){}
        public override void onDeath(){}
    }
}