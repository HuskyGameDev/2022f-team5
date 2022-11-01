using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //variables
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    //public Animator animator;
    [SerializeField]
    public Vector2 movement;

    void Start()
    {
        gameObject.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");          

        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical",   movement.y);
        //animator.SetFloat("Speed",      movement.sqrMagnitude);
    }

    //called a number of times per second
    void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    //this = player
    //other = enemy
    //called when other enters this, NOT when this enters other
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(this.gameObject.name + " collided with " + other.name);
        if ( other.CompareTag("Enemy") )
        {
            Debug.Log("Take Damage");
        }
    }
    
}