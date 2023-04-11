using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monstrous.AI{
    public class EnemyBase : MonoBehaviour
    {
        [Header("Stats")]
        public float health = 50f;
        public float speed = 3f;
        public float damage = 10f;
        public float invulnerabilityTime = 0.3f;
        public string enemyID;

        [Header("Parts")]
        public GameObject part;
        public Sprite partSprite;

        [Header("Sounds")]
        public AudioSource source;
        public AudioClip[] damageSounds;
        public AudioClip[] deathSounds;

        [Header("Body")]
        public Rigidbody2D body;
        public Animator animator;
        public SpriteRenderer renderer;

        [Header("Extra Public Variables")]
        public Transform player;
        public float difficultyScale = 0f;
    
        private bool colliding = false;

        private void Start(){
            health = health + (health * (difficultyScale * 0.3f));
            speed = speed + (speed * (difficultyScale * 0.3f));
            damage = (damage + (damage * difficultyScale * 0.3f));
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        public void dealDamage(float strength){
            if (colliding) return;
            colliding = true;
            health -= strength;
            if (health < 0) die();
            else if (!source.isPlaying){
                source.clip = damageSounds[Random.Range(0, damageSounds.Length)];
                source.Play();
            }
            StartCoroutine(stopColliding());
        }

        private void die(){
            AudioSource.PlayClipAtPoint(deathSounds[Random.Range(0, deathSounds.Length)], gameObject.transform.position);
            GameObject droppedPart = Instantiate(part, transform.position, Quaternion.identity);
            droppedPart.GetComponent<EnemyPart>().setValues( 75, enemyID);
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