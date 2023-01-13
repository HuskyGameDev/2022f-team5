using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeAbs : MonoBehaviour
{
    public string type; //which type of enemy an upgrade is derived from ei. speed is type spider

    protected abstract void DoUpgrade(GameObject target);
}
