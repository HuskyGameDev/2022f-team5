using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [Header("Basic Attack")]
    public Transform start;
    public GameObject shot;
    public float baseAttackAS = 1.0f;
    private float timing = 0.0f;
    public float baseAttackBaseDam = 34;
    public int baseAttackNumShots = 5;
    private float baseAttackShotDur = 0.5f; //the length of time during which all shots are fired per attack
    public float baseAttackPCount = 1.0f;

    [Header("Swing Attack")]
    public Transform swingStart;

    [Header("Bone Attack")]
    public Transform boneStart;
    public GameObject boneShot;
    public float boneAttackAS = 1.0f;
    private float boneTiming = 0.0f;
    public float boneAttackBaseDam = 34;

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

        boneTiming += Time.deltaTime;
        if (boneTiming > boneAttackAS)
        {
            boneTiming = 0.0f;
            BoneAttackThrow();
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

    void BoneAttackThrow()
    {
        Instantiate(boneShot, boneStart.position, Quaternion.identity);
    }
}