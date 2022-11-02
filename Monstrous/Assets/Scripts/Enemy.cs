using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public float health;
    public float speed;
    public float contactDamage;

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
        Debug.Log(this.gameObject.name + " took " + dam + " damage, health: " + health);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator stopColliding()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }

    private void OnDestroy()
    {
        //place code here to drop a part on death
    }
}