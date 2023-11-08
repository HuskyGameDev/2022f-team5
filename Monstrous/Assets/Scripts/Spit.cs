using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour{
    public float travelSpeed = 10f;
    public float damage = 20f;
    public Vector3 target;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float destroyDistance = 0.2f;

    // Update is called once per frame
    void FixedUpdate(){
        transform.position = Vector2.MoveTowards(transform.position, target, travelSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, target) < destroyDistance) die();
    }

    public void OnTriggerEnter2D(Collider2D collided){
        if (collided.tag == "Player"){
            collided.GetComponent<Player>().TakeDamage(damage);
            die();
        }
    }

    private void die(){
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
