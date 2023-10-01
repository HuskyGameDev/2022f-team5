using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Monstrous.AI;

public class Bone_Attack : MonoBehaviour
{
    public Rigidbody2D proj;
    private Vector2 direction;
    [SerializeField] private Camera mainCamera;

    public float speed = 20;
    public float damage;

    public GameObject projectileBreak;

    //Awake
    void Awake()
    {
        damage = GameObject.FindWithTag("Player").GetComponent<Weapons>().boneAttackBaseDam;
        mainCamera = Camera.main;
        Vector2 temp = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = temp - proj.position;
        direction.Normalize();
        LookAt2D(transform, temp);
        transform.Rotate(0f, 0f, -90f, Space.Self);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        proj.MovePosition(proj.position + direction * speed * Time.fixedDeltaTime);
        //rotate some amount
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
}