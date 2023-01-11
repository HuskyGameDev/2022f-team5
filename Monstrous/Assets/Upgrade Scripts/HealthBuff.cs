using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : UpgradeAbs
{
    public float healthInc = 20;

    public void DoUpgrade(GameObject target)
    {
        Player play = target.GetComponent<Player>();
        float proportion = play.pHealth / play.pMaxHealth;
        play.pMaxHealth += healthInc;
        play.pHealth = play.pMaxHealth * proportion;
        play.healthBar.UpdateHealthBar(play.pHealth);
    }
}
