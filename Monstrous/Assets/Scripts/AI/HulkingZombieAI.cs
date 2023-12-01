using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class HulkingZombieAI : EnemyBase{
        private enum States{
            DEFAULT,
            LEAPING,
            THROWING,
            NULL
        }

        [SerializeField] private States state = States.DEFAULT;
        [SerializeField] private float stateChangeTimer = 5f;
        [SerializeField] private float rangedAttackDistance = 5f;
        [SerializeField] private float jumpTime = 3f;
        [SerializeField] private GameObject boulder;
        [SerializeField] private float baseBoulderDamage = 50f;
        [SerializeField] private float baseBoulderSpeed = 8f;
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private LayerMask damagedLayers;
        [SerializeField] private float jumpDamageRadius = 2.5f;
        [SerializeField] private float jumpDamageRatio = 1.2f;
        private States queuedState;
        private bool started = false;
        private Vector3 target;
        private float timer;
        private Vector3 center;
        private Vector3 startCenter;
        private Vector3 endCenter;

        public void Start(){
            base.Start();
            StartCoroutine(stateSwitcher());
        }

        private void FixedUpdate(){
            switch (state){
                case States.DEFAULT:
                    transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, speed * Time.fixedDeltaTime);
                    if (queuedState != States.NULL){
                        state = queuedState;
                        queuedState = States.NULL;
                    }
                    break;
                case States.LEAPING:
                    if (!started){
                        target = playerLoc.position;
                        started = true;
                        timer = 0f;
                        center = (transform.position + target) / 2;
                        center -= new Vector3(0, 3, 0);
                        startCenter = transform.position - center;
                        endCenter = target - center;
                        attack.clip = attackSounds[0];
                        attack.Play();
                    }
                    transform.position = Vector3.Slerp(startCenter, endCenter, timer / jumpTime) + center;
                    timer += Time.deltaTime;
                    if (Vector3.Distance(transform.position, target) < 0.5f){
                        state = States.DEFAULT;
                        started = false;
                        particles.Play();
                        attack.clip = attackSounds[1];
                        attack.Play();
                        Collider2D[] collided = Physics2D.OverlapCircleAll(transform.position, jumpDamageRadius, damagedLayers);
                        foreach (Collider2D c in collided){
                            if (c.tag == "Enemy" && c.GetComponent<HulkingZombieAI>() == null){
                                c.GetComponent<EnemyBase>().dealDamage(damage * jumpDamageRatio);
                            }else if (c.tag == "Player"){
                                c.GetComponent<Player>().TakeDamage(damage * jumpDamageRatio);
                            }
                        }
                    }
                    break;
                case States.THROWING:
                    GameObject b = Instantiate(boulder, transform.position, Quaternion.identity);
                    Boulder bould = b.GetComponent<Boulder>();
                    attack.clip = attackSounds[2];
                    attack.Play();
                    bould.direction = (playerLoc.position - transform.position).normalized;
                    bould.damage = baseBoulderDamage;
                    bould.rollSpeed = baseBoulderSpeed;
                    state = States.DEFAULT;
                    break;
            }
        }

        private IEnumerator stateSwitcher(){
            yield return new WaitForSeconds(stateChangeTimer);
            if (Vector3.Distance(transform.position, playerLoc.position) > rangedAttackDistance){
                switch (Random.Range(0, 2)){
                    case 0:
                        queuedState = States.LEAPING;
                        break;
                    case 1:
                        queuedState = States.THROWING;
                        break;
                }
            }else{
                queuedState = States.DEFAULT;
            }
            StartCoroutine(stateSwitcher());
        }

        public override void onAttack(){}
        public override void onDeath(){}
    }
}
