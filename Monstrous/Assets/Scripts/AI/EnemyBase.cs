using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monstrous.AI{
    public abstract class EnemyBase : MonoBehaviour
    {
        [Header("Stats")]
        public float health = 65f;
        public float speed = 3f;
        public float damage = 13f;
        public float invulnerabilityTime = 0.025f;
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
        public Player player;
        public Transform playerLoc;
        public float difficultyScale = 0f;
        public int destroyRange = 50;
    
        private bool colliding = false;

        public void Start(){
            health = health + (health * (difficultyScale * 0.13f));
            speed = speed + (speed * (difficultyScale * 0.025f));
            damage = (damage + (damage * difficultyScale * 0.16f));
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            playerLoc = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        public void Update(){
            if (Vector2.Distance(transform.position, playerLoc.position) > destroyRange) Destroy(gameObject);
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
            onDeath();
            AudioSource.PlayClipAtPoint(deathSounds[Random.Range(0, deathSounds.Length)], gameObject.transform.position);
            GameObject droppedPart = Instantiate(part, transform.position, Quaternion.identity);
            droppedPart.GetComponent<EnemyPart>().setValues( 75, enemyID);
            droppedPart.GetComponent<SpriteRenderer>().sprite = partSprite;
            Destroy(gameObject);

            //incScore();
        }

        private IEnumerator stopColliding(){
            yield return new WaitForEndOfFrame();
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(invulnerabilityTime);
            renderer.material.color = Color.white;
            colliding = false;
        }

        //Overridden by individual AI scripts to do things when attacking
        public abstract void onAttack();
        public abstract void onDeath();
    }
}