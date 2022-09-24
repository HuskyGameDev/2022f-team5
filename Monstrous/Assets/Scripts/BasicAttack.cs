using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicAttack : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D proj;
    private Rigidbody2D player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        //proj.velocity = player.velocity * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
