using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreShots : UpgradeAbs
{
    public void DoUpgrade(GameObject tar)
    {
        tar.GetComponent<Weapons>().baseAttackNumShots++;
    }
}
