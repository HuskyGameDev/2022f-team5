using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour{
    public Vector3 direction;
    public float speed = 5f;
    public float damage = 50f;
    public float waitTime;
    [SerializeField] private float deathTimer = 10f;
    [SerializeField] private Rigidbody2D body;
    private bool started = false;

    void Start(){
        direction = new Vector3(1, 1, 1).normalized;
        StartCoroutine(start());
    }

    void FixedUpdate(){
        if (started){
            body.MovePosition(transform.position + (direction * speed * Time.fixedDeltaTime));
        }
    }

    private IEnumerator start(){
        yield return new WaitForSeconds(waitTime);
        started = true;
        StartCoroutine(die());
    }

    private IEnumerator die(){
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }
}