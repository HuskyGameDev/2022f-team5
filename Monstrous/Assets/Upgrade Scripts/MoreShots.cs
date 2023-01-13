using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreShots : UpgradeAbs
{
    protected override void DoUpgrade(GameObject tar)
    {
        tar.GetComponent<Weapons>().baseAttackNumShots++;
    }
}
