using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.Data{
    public class EnemyData : MonoBehaviour
    {
        public float health = 50f;
        public float speed = 3f;
        public float damage = 10f;
        public float difficultyScale = 1f;
        public float invulnerabilityTime = 0.3f;
        public string enemyID;
        public GameObject part;
        public Sprite partSprite;
        public AudioSource source;
        public AudioClip damageSound;
        public AudioClip deathSound;
        public Animator animator;
        public Rigidbody2D body;
        public SpriteRenderer renderer;
        public EnemySpawner spawner;
    
        private bool colliding = false;

        private void Start(){
            health = health * difficultyScale;
            speed = speed * difficultyScale;
            contactDamage = (contactDamage + (contactDamage * difficultyScale))/2;
        }

        public void dealDamage(float strength){
            if (colliding) return;
            colliding = true;
            health -= strength;
            if (health < 0) die();
            if (!source.isPlaying){
                source.clip = damageSound;
                source.Play();
            }
            StartCoroutine(stopColliding());
        }

        private void die(){
            AudioSource.PlayClipAtPoint(deathSound, gameObject.transform.position);
            GameObject droppedPart = Instantiate(part, transform.position, Quaternion.identity);
            droppedPart.GetComponent<EnemyPart>().setValues(200, enemyID);
            droppedPart.GetComponent<SpriteRenderer>().sprite = partSprite;
            Destroy(gameObject);
        }

        private IEnumerator stopColliding(){
            yield return new WaitForEndOfFrame();
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(invulnerabilityTime);
            renderer.material.color = Color.white;
            colliding = false;
        }
    }
}