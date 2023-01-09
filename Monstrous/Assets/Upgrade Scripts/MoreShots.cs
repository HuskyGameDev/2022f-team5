using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreShots : MonoBehaviour
{
    public void Upgrade(GameObject tar)
    {
        tar.GetComponent<Weapons>().baseAttackNumShots++;
    }
}
