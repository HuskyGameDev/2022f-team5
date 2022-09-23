using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D proj;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        proj.velocity = player.GetComponent<Rigidbody2D>().velocity * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
