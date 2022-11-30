using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : MonoBehaviour
{
    public float damageInc = 5;

    public void Upgrade(GameObject tar)
    {
        tar.GetComponent<Weapons>().baseAttackBaseDam += damageInc;
    }
}
