using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : UpgradeAbs
{
    public float speedInc = .2f;

    protected override void DoUpgrade(GameObject target)
    {
        Player play = target.GetComponent<Player>();
        play.moveSpeed += speedInc;
    }
}
