using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Monstrous.AI;

public class Bone_Attack : MonoBehaviour
{
    public Rigidbody2D proj;
    private Vector3 target;
    private Vector3 start;
    private Vector2 direction;
    [SerializeField] private Camera mainCamera;
    public GameObject projectileBreak;
    public Transform player;
    public InputAction aimControls;
    private delegate void MoveBoomerang();
    MoveBoomerang moveMethod;
    private float positionCalc = 0f;
    private float lifetime = 0f;
    private bool returning = false;
    private float refrenceVal = 1.03f;

    //Awake
    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        mainCamera = Camera.main;
        start = transform.position;
        direction = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.Rotate(0f, 0f, -90f, Space.Self);
        moveMethod = MoveThrow;
        target = direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifetime+=Time.deltaTime;
        transform.Rotate(0.0f, 0.0f, 12.0f, Space.Self);
        //position is calculated like y=-(x-z)^2+z^2
        //      where x is lifetime
        //      and z is reference value, tweek this to adjust how far and how long the boomerang travels
        positionCalc = -(Mathf.Pow(lifetime-refrenceVal, 2)) + Mathf.Pow(refrenceVal,2);
        moveMethod();
        if(lifetime>refrenceVal)
        {
            moveMethod = MoveReturn;
            returning = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collided){
        if (collided.tag == "Enemy") collided.gameObject.GetComponent<EnemyBase>().dealDamage(Weapons.boneAttackBaseDam);
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
        transform.position = Vector3.LerpUnclamped(start, target, positionCalc);
    }

    //after reaching the target, instead move back to the player
    private void MoveReturn()
    {
        transform.position = Vector3.LerpUnclamped(player.position, target, positionCalc);
    }
}