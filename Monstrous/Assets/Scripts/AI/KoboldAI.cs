using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class KoboldAI : EnemyBase{
        
        [Header("Kobold AI")]
        public float minimumCooldown = 0.3f;
        public float maximumCooldown = 2f;
        public float strafeSpeed = 7f;
        
        [Header("Monitors")]
        [SerializeField] float timer = 0f;
        [SerializeField] float targetTime = 1f;
        [SerializeField] bool right = true;

        void FixedUpdate(){
            timer += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
            if (timer >= targetTime){
                timer = 0f;
                targetTime = Random.Range(minimumCooldown, maximumCooldown);
                Vector3 dir = player.position - transform.position;
                if (right){
                    body.AddForce(-Vector3.Cross(dir, Vector3.up).normalized * strafeSpeed);
                }else{
                    body.AddForce(Vector3.Cross(dir, Vector3.up).normalized * strafeSpeed);
                }
                right = !right;
            }
        }
    }
}