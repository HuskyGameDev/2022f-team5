using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : UpgradeAbs
{
    public float ASInc = 5; // Attack speed incremenet

    public void DoUpgrade(GameObject tar)
    {
        tar.GetComponent<Weapons>().baseAttackAS /=.9f ;
    }
}
