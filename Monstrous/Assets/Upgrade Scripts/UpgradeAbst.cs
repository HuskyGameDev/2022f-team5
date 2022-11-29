using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeAbst : ScriptableObject
{
    public abstract void Upgrade(GameObject target);
}
