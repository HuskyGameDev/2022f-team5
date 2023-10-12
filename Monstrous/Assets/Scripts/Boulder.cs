using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monstrous.AI;

public class Boulder : MonoBehaviour{
    public Vector3 direction;
    public float rollSpeed = 5f;
    public float damage = 50f;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float deathTimer = 10f;

    void Start(){
        StartCoroutine(die());
    }

    // Update is called once per frame
    void FixedUpdate(){
        body.MovePosition(transform.position + (direction * rollSpeed * Time.fixedDeltaTime));
    }

    public void OnTriggerEnter2D(Collider2D c){
        if (c.tag == "Enemy" && c.GetComponent<HulkingZombieAI>() == null){
            c.GetComponent<EnemyBase>().dealDamage(damage);
        }else if (c.tag == "Player"){
            c.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private IEnumerator die(){
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }
}
