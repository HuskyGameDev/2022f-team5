using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.AI;

public class Fire : MonoBehaviour{
    public Vector3 direction;
    public float speed;
    public float damage = 5f;
    [SerializeField] private CircleCollider2D collider;
    [SerializeField] private float deathTime = 5f;
    [SerializeField] private float finalSize = 3.6f;
    [SerializeField] private Rigidbody2D body;
    private float timer = 0f;

    // Update is called once per frame
    void FixedUpdate(){
        timer += Time.fixedDeltaTime;
        if (timer >= deathTime){
            Destroy(gameObject);
        }
        collider.radius = finalSize * (timer / deathTime);
        body.MovePosition(transform.position + (direction * speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collided){
        if (collided.tag == "Player"){
            collided.GetComponent<Player>().TakeDamage(damage);
        }else if (collided.tag == "Enemy"){
            EnemyBase e = collided.GetComponent<EnemyBase>();
            if (e.enemyID != "dragon"){
                e.dealDamage(damage);
            }
        }
    }
}
