using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public float health;
    public float speed;
    public float contactDamage;
    public string enemyType;

    public GameObject spiderPart;
    public GameObject zombiePart;
   

    public Rigidbody2D body;
    public Rigidbody2D player;
    private bool isColliding = false; //used to prevent taking damage multiple times a single projectile;

    public void Start() {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        //Debug.Log("on creation of " + this.gameObject.name + ", player is " + player.name);

        Debug.Log(this.gameObject.name + " has " + health);
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
        Debug.Log(this.gameObject.GetComponent<Enemy>().enemyType + " took " + dam + " damage, health: " + health);
        if (health <= 0)
        {
            dropPart(this.gameObject.GetComponent<Enemy>().enemyType);
            Debug.Log("entering drop");
            Destroy(this.gameObject);
        }
    }

    private void dropPart(string enemyType)
    {
        if (enemyType.Equals("spider"))
        {
            Debug.Log("entering spider drop");
            GameObject EnemyPart = Instantiate(spiderPart, this.gameObject.GetComponent<Transform>().position, Quaternion.identity, null);
        }
        else if (enemyType.Equals("zombie"))
        {
            Debug.Log("entering zombie drop");
            GameObject EnemyPart = Instantiate(zombiePart, this.gameObject.GetComponent<Transform>().position, Quaternion.identity, null);
        }
        
    }

    private IEnumerator stopColliding()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }

    
}