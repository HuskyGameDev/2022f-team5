using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class Blast : MonoBehaviour{
        public Vector3 direction;
        public float speed = 5f;
        public float damage = 50f;
        public float waitTime;
        public AudioSource attack;
        [SerializeField] private float deathTimer = 10f;
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private GameObject deathParticles;
        private bool started = false;

        void Start(){
            StartCoroutine(start());
        }

        void FixedUpdate(){
            if (started){
                body.MovePosition(transform.position + (direction * speed * Time.fixedDeltaTime));
            }
        }

        private IEnumerator start(){
            yield return new WaitForSeconds(waitTime);
            started = true;
            StartCoroutine(die());
        }

        private IEnumerator die(){
            yield return new WaitForSeconds(deathTimer);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collided){
            if (collided.tag == "Player"){
                collided.GetComponent<Player>().TakeDamage(damage);
                Instantiate(deathParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}