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
        [SerializeField] private GameObject shadow;
        [SerializeField] private float stateChangeTimer = 5f;
        [SerializeField] private float rangedAttackDistance = 5f;
        [SerializeField] private float jumpTime = 3f;
        private States queuedState;

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
                    break;
                case States.THROWING:
                    break;
            }
        }

        private IEnumerator stateSwitcher(){
            yield return new WaitForSeconds(stateChangeTimer);
            if (Vector3.Distance(transform.position, playerLoc.position) > rangedAttackDistance){
                switch (Random.Range(0, 1)){
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
