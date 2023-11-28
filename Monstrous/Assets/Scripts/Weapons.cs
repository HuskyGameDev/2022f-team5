using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapons : MonoBehaviour
{
    //controller
    private delegate void attackMethod();
    attackMethod attacks;
    public InputAction aimControls;

    [Header("Basic Attack")]
    public Transform start;
    public GameObject shot;
    public float baseAttackAS = 1.0f;
    private float timing = 0.0f;
    public float baseAttackBaseDam = 45;
    public int baseAttackNumShots = 5;
    private float baseAttackShotDur = 0.25f; //the length of time during which all shots are fired per attack
    public int baseAttackPCount = 0;

    [Header("Swing Attack")]
    public Transform swingStart;

    [Header("Bone Attack")]
    public Transform boneStart;
    public GameObject boneShot;
    public float boneAttackAS = 1.0f;
    private float boneTiming = 0.0f;
    public static float boneAttackBaseDam = 40;

    // Start is called before the first frame update
    void Start()
    {
        attacks = attack_base;
        gainBoomerang();
    }

    // Update is called once per frame
    void Update()
    {
        attacks();
    }

    void BaseAttackShoot()
    {
        StartCoroutine(ShootShot(baseAttackNumShots) );
    }

    private IEnumerator ShootShot(int shots) //if anybody has a better name for this feel free to refactor
    {
        BasicAttack arrow = Instantiate(shot, start.position, Quaternion.identity).GetComponent<BasicAttack>();
        arrow.damage = baseAttackBaseDam;
        arrow.pierce = baseAttackPCount;
        arrow.altDirection = aimControls.ReadValue<Vector2>();
        yield return new WaitForSeconds((float)(baseAttackShotDur / baseAttackNumShots));
        if(shots > 1)
            StartCoroutine(ShootShot(shots - 1));
    }

    void BoneAttackThrow()
    {
        var newBone = Instantiate(boneShot, boneStart.position, Quaternion.identity);
        newBone.GetComponent<Bone_Attack>().altDirection = aimControls.ReadValue<Vector2>();
    }

    void attack_base()
    {
        timing += Time.deltaTime;
        if (timing > baseAttackAS)
        {
            timing = 0.0f;
            BaseAttackShoot();
        }
    }

    void attack_base_bone()
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

    public void gainBoomerang()
    {
        attacks = attack_base_bone;
    }

    private void OnEnable()
    {
        aimControls.Enable();
    }
    private void OnDisable()
    {
        aimControls.Disable();
    }
}