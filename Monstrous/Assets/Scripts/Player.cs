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
    public int health=100;
    public int maxHealth=100;
    public int amount = 10;
  public bool MaxHealthUp;
  public HealthBar healthBar;
  public bool playerTakeDamage;

    //stats
    private float pHealth = 100;
    
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

    if (Input.GetKey(KeyCode.LeftShift))
        {
            TakeDamage(); 
        }

    
    }

    //called a number of times per second
    void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void TakeDamage(){
   health = health- amount;                
   healthBar.UpdateHealthBar(health);  }


    //this = player
    //other = enemy
    //called when other enters this, NOT when this enters other
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(this.gameObject.name + " collided with " + other.name);
        if ( other.CompareTag("Enemy") )
        {
            //Debug.Log("Take Damage");
            TakeDamage(other.gameObject.GetComponent<Enemy>().contactDamage);
        }
    }
    
    private void TakeDamage(float dam)
    {
        pHealth = pHealth - dam;
        Debug.Log(this.gameObject.name + " took " + dam + " damage, health: " + pHealth);
        if (pHealth <= 0)
        {
            //Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        //place code here to show game over screen or start menu on death
    }

}