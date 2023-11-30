using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class DragonAI : EnemyBase{
        private enum States{
            DEFAULT,
            CHARGING,
            BREATHING,
            SWIPING,
            NULL
        }

        [SerializeField] private States state = States.DEFAULT;
        [SerializeField] private float stateChangeTimer = 5f;
        [SerializeField] private float rangedAttackDistance = 5f;
        [SerializeField] private float breathTime = 5f;
        [SerializeField] private float chargeLength = 1f;
        [SerializeField] private ParticleSystem chargeParticles;
        [SerializeField] private ParticleSystem breathParticles;
        [SerializeField] private GameObject fire;
        [SerializeField] private Transform mouthPosition;
        [SerializeField] private float fireDelay;
        [SerializeField] private float swipeTime = 1f;
        [SerializeField] private LayerMask damagedLayers;
        private States queuedState;
        private bool started = false;
        private float timer;
        private float fireTimer;
        private Vector3 target;

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
                case States.CHARGING:
                    if (!started){
                        started = true;
                        timer = 0f;
                        ParticleSystem.MainModule main = chargeParticles.main;
                        main.duration = chargeLength;
                        chargeParticles.Play();
                    }
                    timer += Time.fixedDeltaTime;
                    if (timer > chargeLength){
                        started = false;
                        state = States.BREATHING;
                    }
                    break;
                case States.BREATHING:
                    if (!started){
                        started = true;
                        timer = 0f;
                        ParticleSystem.MainModule main = breathParticles.main;
                        breathParticles.transform.LookAt(playerLoc.position);
                        breathParticles.transform.Rotate(new Vector3(0, -90, 0));
                        main.duration = breathTime;
                        breathParticles.Play();
                        target = new Vector3(playerLoc.position.x, playerLoc.position.y, playerLoc.position.z);
                        attack.clip = attackSounds[Random.Range(0, 1)];
                        attack.Play();
                    }
                    timer += Time.deltaTime;
                    fireTimer += Time.deltaTime;
                    if (fireTimer >= fireDelay){
                        fireTimer -= fireDelay;
                        Fire ball = Instantiate(fire, mouthPosition.position, Quaternion.identity).GetComponent<Fire>();
                        ball.damage = damage * 0.7f;
                        ball.speed = 22f;
                        ball.direction = (target - mouthPosition.position).normalized;
                    }
                    if (timer >= breathTime){
                        started = false;
                        state = States.DEFAULT;
                    }
                    break;
                case States.SWIPING:
                    if (!started){
                        started = true;
                        timer = 0f;
                    }
                    timer += Time.fixedDeltaTime;
                    if (timer >= swipeTime){
                        started = false;
                        state = States.DEFAULT;
                    }
                    break;
            }
        }

        private IEnumerator stateSwitcher(){
            yield return new WaitForSeconds(stateChangeTimer);
            if (Vector3.Distance(transform.position, playerLoc.position) > rangedAttackDistance){
                queuedState = States.CHARGING;
            }else{
                switch (Random.Range(0, 2)){
                    case 0:
                        queuedState = States.DEFAULT;
                        break;
                    case 1:
                        queuedState = States.SWIPING;
                        break;
                }
            }
            StartCoroutine(stateSwitcher());
        }

        public override void onAttack(){}
        public override void onDeath(){}
    }
}
