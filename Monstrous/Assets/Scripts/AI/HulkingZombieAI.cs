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
        private States queuedState;
        private bool started = false;
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
                case States.LEAPING:
                    Debug.Log("Jumping");
                    if (!started){
                        target = playerLoc.position;
                        started = true;
                    }
                    Vector3.Slerp(transform.position, target, Time.deltaTime);
                    if (Vector3.Distance(transform.position, target) < 0.5f){
                        state = States.DEFAULT;
                    }
                    break;
                case States.THROWING:
                    Debug.Log("Throwing");
                    GameObject b = Instantiate(boulder, transform.position, Quaternion.identity);
                    b.GetComponent<Boulder>().direction = (playerLoc.position - transform.position).normalized;
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
