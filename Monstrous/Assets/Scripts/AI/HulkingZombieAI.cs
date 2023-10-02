using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class HulkingZombieAI : EnemyBase{
        private enum States{
            DEFAULT,
            LEAPING,
            THROWING
        }

        [SerializeField] private States state;
        [SerializeField] private GameObject shadow;
        [SerializeField] private float stateChangeTimer = 5f;
        [SerializeField] private float rangedAttackDistance = 5f;
        private States queuedState;

        public void Start(){
            base.Start();
            StartCoroutine(stateSwitcher());
        }

        private void FixedUpdate(){
            switch (state){
                case States.DEFAULT:
                    transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, speed * Time.fixedDeltaTime);
                    if (queuedState != null){
                        state = queuedState;
                        queuedState = null;
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
