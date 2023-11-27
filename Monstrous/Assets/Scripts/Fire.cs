using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.AI;

public class Fire : MonoBehaviour{
    public Vector3 target;
    public float speed = 1f;
    public float damage = 5f;
    [SerializeField] private CircleCollider2D collider;
    [SerializeField] private float deathTime = 1f;
    [SerializeField] private float finalSize = 3.6f;
    private float timer = 0f;

    // Update is called once per frame
    void FixedUpdate(){
        timer += Time.fixedDeltaTime;
        if (timer >= deathTime){
            Destroy(gameObject);
        }
        collider.radius = finalSize * (timer / deathTime);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collided){
        if (collided.gameObject.tag == "Player"){
            collided.gameObject.GetComponent<Player>().TakeDamage(damage);
        }else if (collided.gameObject.tag == "Enemy" && collided.gameObject != gameObject){
            collided.gameObject.GetComponent<EnemyBase>().dealDamage(damage);
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, collider.radius);
    }
}
