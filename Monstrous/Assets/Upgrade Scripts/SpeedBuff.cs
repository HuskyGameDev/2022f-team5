using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : UpgradeAbst
{
    public float speedInc = .2f;

    public override void Upgrade(GameObject target)
    {
        Player play = target.GetComponent<Player>();
        play.moveSpeed += speedInc;
    }
}
