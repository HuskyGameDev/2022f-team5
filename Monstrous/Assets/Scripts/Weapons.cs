using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Transform start;
    public GameObject shot;

    //- -variables for basic attack- -
    public float baseAttackAS = 1.0f;
    private float timing = 0.0f;
    public float baseAttackBaseDam = 34;
    public int baseAttackNumShots = 5;
    private float baseAttackShotDur = 0.5f; //the length of time during which all shots are fired per attack

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime;
        if (timing > baseAttackAS)
        {
            timing = 0.0f;
            BaseAttackShoot();
        }
    }

    void BaseAttackShoot()
    {
        StartCoroutine(ShootShot(baseAttackNumShots) );
    }

    private IEnumerator ShootShot(int shots) //if anybody has a better name for this feel free to refactor
    {
        Instantiate(shot, start.position, Quaternion.identity);
        yield return new WaitForSeconds((float)(baseAttackShotDur / baseAttackNumShots));
        if(shots > 1)
            StartCoroutine(ShootShot(shots - 1));
    }
}
