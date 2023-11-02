using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Monstrous.AI;
using Monstrous.Camera;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    // objects
    public Rigidbody2D rb;
    public HealthBar healthBar;
    public HealthBar expBar;
    public AudioSource steps1;
    public AudioSource steps2;
    public GameObject upgrades;
    public SpriteRenderer sprite;
    public InputAction playerControls;
    public Image vignette;
    [SerializeField] private Camera mainCamera;

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
    public float speedDebuff = 1f;

    // 
    public float levelNum = 0f;
    public float levelUpExp = 100f;

    void Start()
    {
        gameObject.tag = "Player";
        steps1.enabled = false;
        steps2.enabled = false;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //input
        movement = playerControls.ReadValue<Vector2>();
        movement.Normalize(); //doesn't seem to actually normalize, diagonal movement still looks faster than linear

        if(movement.x > 0 && !facingRight)
        {
            flipChar();
        }
        else if(movement.x < 0 && facingRight)
        {
            flipChar();
        }

        if ((pHealth /*/ pMaxHealth*/ > 35) && (vignette.enabled == true))
        {
            vignette.enabled = false;
        }
    }

    //called a number of times per second
    void FixedUpdate()
    {
        //movement
        //rb.MovePosition(rb.position + movement * (moveSpeed / speedDebuff) * Time.fixedDeltaTime);
        rb.velocity = new Vector2(movement.x *moveSpeed, movement.y * moveSpeed);
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
            //Debug.Log("Take Damage");
            TakeDamage(other.gameObject.GetComponent<EnemyBase>().damage);
            other.gameObject.GetComponent<EnemyBase>().onAttack();
        } else if ( other.CompareTag("Pickup") )
        {
            EnemyPart Temp = other.gameObject.GetComponent<EnemyPart>();
            GainExp(Temp.partValue);
            updateWeights(Temp.enemyType);
            Destroy(other.gameObject);
        }
     
    }
    
    public void TakeDamage(float dam){
        if (isColliding) return;
        isColliding = true;
        StartCoroutine(stopColliding());
        pHealth = pHealth - dam;
        healthBar.UpdateHealthBar(pHealth);
        //mainCamera.gameObject.GetComponent<CameraMovement>().knockCam();
        if((pHealth/*/pMaxHealth*/ <= 35) && (vignette.enabled == false))
        {
            vignette.enabled = true;
        }
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
        levelUpExp = (float)(100 + Mathf.Pow(levelNum/0.65f, 2f) );

         //Debug.Log(levelUpExp);
         //Debug.Log(levelNum);
        if (expValue >= levelUpExp)
        {
            LevelUp();
            Debug.Log("You Have Leveled Up!");
            expValue = expValue- levelUpExp; 
        }
        expBar.UpdateHealthBar(expValue);
        expBar.UpdateHealthBarMax(levelUpExp, 0);
    }

    private void OnDestroy()
    {
        //place code here to show game over screen or start menu on death
    }
    
    private IEnumerator stopColliding()
    {
        sprite.material.color = Color.red;
        yield return new WaitForSeconds(.5f);
        isColliding = false;
        sprite.material.color = Color.white;
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

    private void updateWeights(string type)
    {
        foreach (GameObject upObj in GameObject.FindGameObjectsWithTag("Upgrade"))
        {
            UpgradeData Temp = upObj.GetComponent<UpgradeData>();
            if (Temp.enemyType == type)
            {
                Temp.runningWeight++;
            }
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
}