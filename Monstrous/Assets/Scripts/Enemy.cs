using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public float health;
    public float speed;
    public float contactDamage;

    public Rigidbody2D body;
    public Rigidbody2D player;

    public void Start() {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        Debug.Log("on creation of " + this.gameObject.name + ", player is " + player.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject.name + " collided with " + other.name);
        if (other.attachedRigidbody == player)
        {
            
            Destroy(this.gameObject);
        }
    }
}