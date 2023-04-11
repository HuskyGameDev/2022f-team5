using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class SpiderAI : EnemyBase{
        
        [Header("Spider AI")]
        public float dashRange = 7f;
        public float dashSpeed = 8f;
        public float chargeTime = 2f;
        public float dashLength = 5f;
        public float dashCooldown = 5f;
        public PolygonCollider2D collider;

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
                    Physics2D.IgnoreCollision(player.gameObject.GetComponent<BoxCollider2D>(), collider, true);
                    body.AddForce((player.position - transform.position) * dashSpeed);
                    StartCoroutine(enableCollision());
                }
            }
        }
        private IEnumerator enableCollision(){
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(dashLength);
            Physics2D.IgnoreCollision(player.gameObject.GetComponent<BoxCollider2D>(), collider, false);
        }
    }
}