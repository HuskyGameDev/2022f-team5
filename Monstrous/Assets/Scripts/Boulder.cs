using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour{
    public Vector3 direction;
    [SerializeField] private float rollSpeed = 5f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private Rigidbody2D body;

    // Update is called once per frame
    void Update(){
        body.MovePosition(transform.position + (direction * rollSpeed * Time.deltaTime));
    }
}
