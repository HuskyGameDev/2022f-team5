using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicAttack : MonoBehaviour
{
    public Rigidbody2D proj;
    private Vector2 direction;
    [SerializeField] private Camera mainCamera;

    public float speed = 10;
    public float damage = 34;

    void Awake()
    {
        mainCamera = Camera.main;
        Vector2 temp = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //direction.x = temp.x - proj.position.x;
        //direction.y = temp.y - proj.position.y;
        direction = temp - proj.position;
        direction.Normalize();


    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        proj.MovePosition(proj.position + direction * speed * Time.fixedDeltaTime);
    }
}