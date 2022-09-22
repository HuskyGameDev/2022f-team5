using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Transform start;
    public GameObject shot;
    private float update;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        update += Time.deltaTime;
        if (update > 1.0f)
        {
            update = 0.0f;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(shot, start.position, start.rotation);
    }
}
