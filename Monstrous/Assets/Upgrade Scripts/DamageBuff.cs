using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : UpgradeAbs
{
    public float damageInc = 5;

    protected override void DoUpgrade(GameObject tar)
    {
        tar.GetComponent<Weapons>().baseAttackBaseDam += damageInc;
    }
}
