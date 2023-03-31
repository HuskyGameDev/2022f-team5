using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Monstrous.Data;

public class BasicAttack : MonoBehaviour
{
    public Rigidbody2D proj;
    private Vector2 direction;
    [SerializeField] private Camera mainCamera;

    public float speed = 20;
    public float damage;

    public GameObject projectileBreak;

    void Awake()
    {
        damage = GameObject.FindWithTag("Player").GetComponent<Weapons>().baseAttackBaseDam;
        mainCamera = Camera.main;
        Vector2 temp = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = temp - proj.position;
        direction.Normalize();
        LookAt2D(transform, temp);
        transform.Rotate(0f, 0f, -90f, Space.Self);
        StartCoroutine(despawnProj());
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        proj.MovePosition(proj.position + direction * speed * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collided){
        if (collided.tag == "Enemy") collided.gameObject.GetComponent<EnemyData>().dealDamage(damage);
        if (collided.gameObject.tag != "Player" && collided.gameObject.tag != "Loader" && collided.gameObject.tag != "Room" && collided.gameObject.tag != "Pickup" && collided.gameObject.tag != "ProjAtk"){
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

    IEnumerator despawnProj()
    {
        yield return new WaitForSeconds(5);
        Destroy(proj.gameObject);
    }
}