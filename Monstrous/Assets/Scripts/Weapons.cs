using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Transform start;
    public GameObject shot;
    
    //- -variables for basic attack- -
    private float baseAttackAS;
    //private float baseAttackVel;
    public float baseAttackBaseDam = 34;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        baseAttackAS += Time.deltaTime;
        if (baseAttackAS > 1.0f)
        {
            baseAttackAS = 0.0f;
            BaseAttackShoot();
        }
    }

    void BaseAttackShoot()
    {
        Instantiate(shot, start.position, start.rotation);
    }
}
