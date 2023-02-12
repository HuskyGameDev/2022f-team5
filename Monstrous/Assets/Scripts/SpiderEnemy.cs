using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SpiderEnemy : Enemy
{ 
    public int counter;
   
    // Start is called before the first frame update
    void Start()
    {
       base.Start();
       
    }

    /**
    void FixedUpdate(){
        counter++;
        if(counter > 499){
            health = health * 1.01f;
            speed = speed*1.01f;
            contactDamage = contactDamage*1.1f;
            counter=0;
            
        }
        Debug.Log(counter);
        Debug.Log(health);
    }
**/
}

