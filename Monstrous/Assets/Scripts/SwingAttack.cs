using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAttack : MonoBehaviour
{
    public Animator animator;
    private float timing = 2.5f;
    public float SwingAtkAS = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        //collecting the Swing upgrade will enable the attack
        //will also add/enable
    }

    void Update()
    {
        timing += Time.deltaTime;
        if (timing > SwingAtkAS)
        {
            timing = 0.0f;
            Swing();
        }
    }

    public void Swing()
    {
        animator.SetTrigger("Attack");
    }
}
