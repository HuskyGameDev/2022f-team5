using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Monstrous.AI;

public class Bone_Attack : MonoBehaviour
{
    public Rigidbody2D proj;
    private Vector3 target;
    private Vector3 start;
    private Vector2 direction;
    [SerializeField] private Camera mainCamera;
    public float damage;
    public GameObject projectileBreak;
    public Transform player;
    private delegate void MoveBoomerang();
    MoveBoomerang moveMethod;
    private float positionCalc = 0f;
    private bool returning = false;

    //Awake
    void Start()
    {
        damage = GameObject.FindWithTag("Player").GetComponent<Weapons>().boneAttackBaseDam;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        mainCamera = Camera.main;
        start = transform.position;
        Vector2 temp = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = temp - proj.position;
        direction.Normalize();
        transform.Rotate(0f, 0f, -90f, Space.Self);
        moveMethod = MoveThrow;
        
        //target = GameObject.FindWithTag("Temp").GetComponent<Transform>();
        target = direction * 15.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        positionCalc += 0.8f * Time.deltaTime;
        transform.Rotate(0.0f, 0.0f, 12.0f, Space.Self);
        moveMethod();
        if((Vector3.Distance(transform.position, target) <= 1f) && !returning )
        {
            moveMethod = MoveReturn;
            positionCalc = 0f;
            returning = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collided){
        if (collided.tag == "Enemy") collided.gameObject.GetComponent<EnemyBase>().dealDamage(damage);
        if (collided.gameObject.tag == "Obstacle")
        {
            Instantiate(projectileBreak, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collided.gameObject.tag == "Player" && returning)
        {
            Destroy(gameObject);
        }
    }

    //boomerang will start by moving away
    private void MoveThrow()
    {
        transform.position = Vector3.Lerp(start, target, positionCalc);
    }

    //after reaching the target, instead move back to the player
    private void MoveReturn()
    {
        transform.position = Vector3.Lerp(target, player.position, positionCalc);
    }
}