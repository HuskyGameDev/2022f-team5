using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : MonoBehaviour
{
    public float ASInc = 5; // Attack speed incremenet

    public void Upgrade(GameObject tar)
    {
        tar.GetComponent<Weapons>().baseAttackAS /=.9f ;
    }
}
