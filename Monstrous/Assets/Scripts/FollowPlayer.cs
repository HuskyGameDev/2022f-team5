using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private float moveSpeed = 2;
    Transform target;
    
    //
    private void Awake()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        //moveSpeed = SpiderEnemy.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //update the position of the enemy to be closer to the player
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
    }

}
