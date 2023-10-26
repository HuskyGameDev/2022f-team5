using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class LichAI : EnemyBase{
        private enum States{
            DEFAULT,
            FIRING,
            SUMMONING,
            NULL
        }

        [SerializeField] private States state = States.DEFAULT;
        [SerializeField] private float stateChangeTimer = 5f;
        [SerializeField] private float desiredDistance = 5f;
        [SerializeField] private float distanceVariability = 2f;
        [SerializeField] private Transform[] magicLocs;
        [Header("Magic Bullets")]
        [SerializeField] private GameObject bullet;
        [SerializeField] private float bulletSpawnTimeDif = 0.25f;
        [SerializeField] private float bulletFireDelay = 0.5f;
        [Header("Summoning")]
        [SerializeField] private GameObject[] summonableEnemies;
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
                    Debug.Log("Shmoovin");
                    float distance = Vector3.Distance(playerLoc.position, transform.position);
                    if (distance > desiredDistance + distanceVariability){
                        transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, speed * Time.fixedDeltaTime);
                    }else if(distance < desiredDistance - distanceVariability){
                        transform.position = Vector2.MoveTowards(transform.position, -1 * playerLoc.position, speed * Time.fixedDeltaTime);
                    }
                    if (queuedState != States.NULL){
                        state = queuedState;
                        queuedState = States.NULL; 
                    }
                    break;
                case States.FIRING:
                    Debug.Log("Gun");
                    if (!started){
                        timer = 0;
                        started = true;
                        for (int i = 0; i < magicLocs.Length; i++) StartCoroutine(bulletSpawner(i * bulletSpawnTimeDif, magicLocs[i]));
                    }
                    timer += Time.fixedDeltaTime;
                    if (timer >= magicLocs.Length * bulletSpawnTimeDif + (2 * bulletFireDelay)){
                        state = States.DEFAULT;
                    }
                    break;
                case States.SUMMONING:
                    Debug.Log("Summoning (forever)");
                    break;
            }
        }

        private IEnumerator bulletSpawner(float waitTime, Transform loc){
            yield return new WaitForSeconds(waitTime);
            Blast blast = Instantiate(bullet, loc.position, Quaternion.identity).GetComponent<Blast>();
            blast.waitTime = bulletFireDelay;
        }

        private IEnumerator stateSwitcher(){
            yield return new WaitForSeconds(stateChangeTimer);
            float distance = Vector3.Distance(transform.position, playerLoc.position);
            if (distance > desiredDistance - distanceVariability && distance < desiredDistance + distanceVariability){
                switch (Random.Range(0, 1)){ //CHANGE THIS TO (0, 2) TO RE-ENABLE SUMMONING
                    case 0:
                        queuedState = States.FIRING;
                        break;
                    case 1:
                        queuedState = States.SUMMONING;
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
