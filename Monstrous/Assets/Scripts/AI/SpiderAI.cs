using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class SpiderAI : EnemyBase{
        
        [Header("Spider AI")]
        public float dashRange = 7f;
        public float dashSpeed = 8f;
        public float chargeTime = 2f;
        public float dashCooldown = 5f;

        float timer = 50f;
        bool charging = false;
        
        // Update is called once per frame
        void FixedUpdate(){
            timer += Time.deltaTime;
            if (!charging){
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
                if (timer > dashCooldown) timer = dashCooldown;
                if (Vector3.Distance(player.position, transform.position) < dashRange && timer == dashCooldown){
                    charging = true;
                    timer = 0f;
                }
            }else{
                if (timer >= chargeTime){
                    charging = false;
                    timer = 0f;
                    body.AddForce((player.position - transform.position) * dashSpeed);
                }
            }
        }
    }
}