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
        [SerializeField] private float aimVariability = 0.05f;
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
                    float distance = Vector3.Distance(playerLoc.position, transform.position);
                    if (distance > desiredDistance + distanceVariability){
                        transform.position = Vector2.MoveTowards(transform.position, playerLoc.position, speed * Time.fixedDeltaTime);
                    }else if(distance < desiredDistance - distanceVariability){
                        transform.position = Vector2.MoveTowards(transform.position, (-1* (playerLoc.position - transform.position)) + transform.position, speed * Time.fixedDeltaTime);
                    }
                    if (queuedState != States.NULL){
                        state = queuedState;
                        queuedState = States.NULL; 
                    }
                    break;
                case States.FIRING:
                    if (!started){
                        timer = 0;
                        started = true;
                        for (int i = 0; i < magicLocs.Length; i++) StartCoroutine(bulletSpawner(i * bulletSpawnTimeDif, magicLocs[i]));
                    }
                    timer += Time.fixedDeltaTime;
                    if (timer >= magicLocs.Length * bulletSpawnTimeDif + (2 * bulletFireDelay)){
                        state = States.DEFAULT;
                        started = false;
                    }
                    break;
                case States.SUMMONING:
                    if (!started){
                        timer = 0;
                        started = true;
                        for (int i = 0; i < magicLocs.Length; i++) StartCoroutine(undeadSpawner(i * bulletSpawnTimeDif, magicLocs[i]));
                    }
                    timer += Time.fixedDeltaTime;
                    if (timer >= magicLocs.Length * bulletSpawnTimeDif + (2 * bulletFireDelay)){
                        state = States.DEFAULT;
                        started = false;
                    }
                    break;
            }
        }

        private IEnumerator bulletSpawner(float waitTime, Transform loc){
            yield return new WaitForSeconds(waitTime);
            Blast blast = Instantiate(bullet, loc.position, Quaternion.identity).GetComponent<Blast>();
            blast.waitTime = bulletFireDelay;
            blast.direction = playerLoc.position - transform.position;
            blast.direction = new Vector3(blast.direction.x + Random.Range(-1 * aimVariability, aimVariability), blast.direction.y + Random.Range(-1 * aimVariability, aimVariability), 0).normalized;
        }

        private IEnumerator undeadSpawner(float waitTime, Transform loc){
            yield return new WaitForSeconds(waitTime);
            EnemyBase enemy = Instantiate(summonableEnemies[Random.Range(0, summonableEnemies.Length)], loc.position, Quaternion.identity).GetComponent<EnemyBase>();
            enemy.difficultyScale = difficultyScale;
        }

        private IEnumerator stateSwitcher(){
            yield return new WaitForSeconds(stateChangeTimer);
            float distance = Vector3.Distance(transform.position, playerLoc.position);
            if (distance > desiredDistance - distanceVariability && distance < desiredDistance + distanceVariability){
                switch (Random.Range(0, 2)){
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
