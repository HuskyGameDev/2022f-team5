using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Monstrous.AI;

public class Bone_Attack : MonoBehaviour
{
    public Rigidbody2D proj;
    public Transform target;
    private Vector3 start;
    private Vector2 direction;
    [SerializeField] private Camera mainCamera;
    public float damage;
    public GameObject projectileBreak;
    private delegate void MoveBoomerang();
    private float positionCalc = 0;

    //Awake
    void Awake()
    {
        damage = GameObject.FindWithTag("Player").GetComponent<Weapons>().boneAttackBaseDam;
        mainCamera = Camera.main;
        start = transform.position;
        Vector2 temp = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = temp - proj.position;
        direction.Normalize();
        LookAt2D(transform, temp);
        transform.Rotate(0f, 0f, -90f, Space.Self);
        target = GameObject.FindWithTag("Temp").GetComponent<Transform>();
        //MoveBoomerang = MoveThrow();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        positionCalc += 0.2f * Time.deltaTime;
        transform.Rotate(0.0f, 0.0f, 3.0f, Space.Self);
        // if arrived switch move method to return
    }

    private void OnTriggerEnter2D(Collider2D collided){
        if (collided.tag == "Enemy") collided.gameObject.GetComponent<EnemyBase>().dealDamage(damage);
        if (collided.gameObject.tag == "Obstacle")
        {
            Instantiate(projectileBreak, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void LookAt2D(Transform transform, Vector2 target)
    {
        Vector2 current = transform.position;
        var direction = target - current;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    //boomerang will start by moving away
    private void MoveThrow()
    {
        transform.position = Vector3.Slerp(start, target.position, positionCalc);
    }

    //after reaching the target, instead move back to the player
    private void MoveReturn()
    {

    }
}