using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : UpgradeAbs
{
    public float ASInc = 5; // Attack speed incremenet

    protected override void DoUpgrade(GameObject tar)
    {
        tar.GetComponent<Weapons>().baseAttackAS /=1.1f ;
    }
}
