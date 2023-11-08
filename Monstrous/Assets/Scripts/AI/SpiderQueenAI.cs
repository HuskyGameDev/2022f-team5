using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class SpiderQueenAI : EnemyBase{
        private enum States{
            DEFAULT,
            WEBBING,
            SPITTING,
            NULL
        }

        [SerializeField] private States state = States.DEFAULT;
        [SerializeField] private float stateChangeTimer = 5f;
        [SerializeField] private float rangedDistance = 7f;
        [SerializeField] private GameObject web;
        [SerializeField] private GameObject spit;
        [SerializeField] private float fireDelay = 0.4f;
        [SerializeField] private int spitCount = 3;
        [SerializeField] private float waitTimer = 0.6f;
        private States queuedState;
        private bool started = false;
        private Vector3 target;
        private float timer;

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
                case States.WEBBING:
                    if (!started){
                        timer = 0;
                        started = true;
                        Web webb = Instantiate(web, transform.position, Quaternion.identity).GetComponent<Web>();
                        webb.targetLoc = playerLoc.position;
                    }
                    timer += Time.fixedDeltaTime;
                    if (timer > waitTimer){
                        state = States.DEFAULT;
                        started = false;
                    }
                    break;
                case States.SPITTING:
                    if (!started){
                        timer = 0;
                        started = true;
                        for (int i = 0; i < spitCount; i++){
                            StartCoroutine(spitAcid(fireDelay * i));
                        }
                    }
                    timer += Time.fixedDeltaTime;
                    if (timer > waitTimer){
                        state = States.DEFAULT;
                        started = false;
                    }
                    break;
            }
        }

        private IEnumerator spitAcid(float wait){
            yield return new WaitForSeconds(wait);
            Spit projectile = Instantiate(spit, transform.position, Quaternion.identity).GetComponent<Spit>();
            projectile.target = playerLoc.position;
        }

        private IEnumerator stateSwitcher(){
            yield return new WaitForSeconds(stateChangeTimer);
            float distance = Vector3.Distance(transform.position, playerLoc.position);
            if (distance > rangedDistance){
                switch (Random.Range(0, 2)){
                    case 0:
                        queuedState = States.WEBBING;
                        break;
                    case 1:
                        queuedState = States.SPITTING;
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
