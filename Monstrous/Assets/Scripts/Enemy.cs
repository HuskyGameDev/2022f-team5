using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour {
    
    public float health;
    public float speed;
    public float contactDamage;
    public string enemyType;
    public float diffScale;

    public GameObject spiderPart;
    public GameObject zombiePart;

    public AudioSource damageSound;
    public AudioClip deathSound;

    public Animator animator;
    public Rigidbody2D body;
    public Rigidbody2D player;
    public EnemySpawner enemySpawner;
    private bool isColliding = false; //used to prevent taking damage multiple times a single projectile;

    public SpriteRenderer sprite;

    public void Start() {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        damageSound.enabled = false;
        
        health = health * diffScale;
        speed = speed*diffScale;
        contactDamage = (contactDamage+ (contactDamage * diffScale))/2;
        //Debug.Log(health);
        //Debug.Log("on creation of " + this.gameObject.name + ", player is " + player.name);

        //Debug.Log(this.gameObject.name + " has " + health);
    }
    public void FixedUpdate(){


    }
    //called when other enters this, NOT when this enters other
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(this.gameObject.name + " collided with " + other.name);
        if ( other.CompareTag("ProjAtk") )
        {
            if (isColliding) return;
            isColliding = true;

            //Debug.Log(this.gameObject.name + " collided with " + other.name);
            //Destroy(this.gameObject);
            takeDamage(other.gameObject.GetComponent<BasicAttack>().damage);
            Destroy(other.gameObject);// delete the projectile, in the future this will control penetration stats

            StartCoroutine(stopColliding());
        }
    }

    private void takeDamage(float dam)
    {
        health = health - dam;
        if (!damageSound.isPlaying)
        {
            damageSound.enabled = true;
            damageSound.Play();
        }


        
        //Debug.Log(this.gameObject.GetComponent<Enemy>().enemyType + " took " + dam + " damage, health: " + health);
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathSound, gameObject.transform.position);
            dropPart(this.gameObject.GetComponent<Enemy>().enemyType);
            //Debug.Log("entering drop");
            Destroy(this.gameObject);
        }
    }

    private void dropPart(string enemyType)
    {
        if (enemyType.Equals("spider"))
        {
            //Debug.Log("entering spider drop");
            GameObject EnemyPart = Instantiate(spiderPart, this.gameObject.GetComponent<Transform>().position, Quaternion.identity, null);
        }
        else if (enemyType.Equals("zombie"))
        {
            //Debug.Log("entering zombie drop");
            GameObject EnemyPart = Instantiate(zombiePart, this.gameObject.GetComponent<Transform>().position, Quaternion.identity, null);
        }
        
    }

    private IEnumerator stopColliding()
    {
        yield return new WaitForEndOfFrame();
        sprite.material.color = Color.red;
        yield return new WaitForSeconds(.5f);
        sprite.material.color = Color.white;
        isColliding = false;
    }

    public void setDiff(float diff){
        diffScale = diff;
    }
}