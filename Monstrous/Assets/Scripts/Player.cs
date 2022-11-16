using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    // objects
    public Rigidbody2D rb;
    //public Animator animator;
    public HealthBar healthBar;
    public HealthBar expBar;
    public AudioSource steps1;
    public AudioSource steps2;

    // variables
    public float moveSpeed = 5f;
    [SerializeField]
    public Vector2 movement;
    private bool isColliding = false; //used to prevent taking damage multiple times a single enemy;
    
    // stats
    private float pHealth = 100;
    private float pMaxHealth = 100;
    public float expValue = 0;

    void Start()
    {
        gameObject.tag = "Player";
        steps1.enabled = false;
        steps2.enabled = false;
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
        if( ! (movement.Equals( Vector2.zero ) ) )
        {
            steps1.enabled = true;
            steps1.loop = true;
        }
        else
        {
            steps1.enabled = false;
        }
    }

    


    //this = player
    //other = enemy
    //called when other enters this, NOT when this enters other
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(this.gameObject.name + " collided with " + other.name);
        if ( other.CompareTag("Enemy") )
        {
             if (isColliding) return;
            isColliding = true;

            //Debug.Log("Take Damage");
            TakeDamage(other.gameObject.GetComponent<Enemy>().contactDamage);

            StartCoroutine(stopColliding());

        } else if ( other.CompareTag("Pickup") )
        {
            GainExp(other.gameObject.GetComponent<EnemyPart>().partValue);
            Destroy(other.gameObject);
        }
     
    }
    
    private void TakeDamage(float dam)
    {
        pHealth = pHealth - dam;
       healthBar.UpdateHealthBar(pHealth);
        if (pHealth <= 0)
        {
            //Destroy(this.gameObject);
        }
    }

    private void GainExp(float exp)
    {
        Debug.Log(exp);
        expValue = expValue + exp;
        
        if (expValue >= 1000)
        {
            Debug.Log("You Have Leveled Up!");
            expValue = expValue- 1000; 
        }
        expBar.UpdateHealthBar(expValue);
    }

    private void OnDestroy()
    {
        //place code here to show game over screen or start menu on death
    }
    
    private IEnumerator stopColliding()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }


}