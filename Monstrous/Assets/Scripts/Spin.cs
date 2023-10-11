using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour{
    [SerializeField] private float rotationSpeed = 5f;
    // Update is called once per frame
    void Update(){
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}
