using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    void Awake(){
        speed += Random.Range(-2f, 2f);
    }

    // Update is called once per frame
    void Update(){
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (transform.position.x > 10){
            Destroy(gameObject);
        }
    }
}
