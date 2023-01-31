using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    
    // objects
    public Rigidbody2D rb;
    //public Animator animator;
    public HealthBar healthBar;
    public HealthBar expBar;
    public AudioSource steps1;
    public AudioSource steps2;
    public GameObject upgrades;

    // variables
    [SerializeField]
    public Vector2 movement;
    private bool isColliding = false; //used to prevent taking damage multiple times a single enemy;
    bool facingRight = false;
    
    // stats
    public float pHealth = 100;
    public float pMaxHealth = 100;
    public float expValue = 0;
    public float moveSpeed = 5f;
    public float levelNum = 1f;
    public float levelUpExp = 0f;

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

        if(movement.x > 0 && !facingRight)
        {
            flipChar();
        }
        else if(movement.x < 0 && facingRight)
        {
            flipChar();
        }
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
            SceneManager.LoadScene("Menu");
        }
    }

    private void GainExp(float exp)
    {
        Debug.Log(exp);
        expValue = expValue + exp;
        //Exp curve 
        levelUpExp = (float)(100+(levelNum/0.15)*(levelNum/0.15));
         Debug.Log(levelUpExp);
         Debug.Log(levelNum);
        if (expValue >= levelUpExp)
        {
            LevelUp();
            Debug.Log("You Have Leveled Up!");
            expValue = expValue- levelUpExp; 
        }
        expBar.UpdateHealthBar(expValue);
        expBar.UpdateHealthBarMax(levelUpExp);
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

    private void LevelUp()
    {
        upgrades.SetActive(true);
        levelNum++;
    }

    private void flipChar()
    {
        Vector3 currentScale = this.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}