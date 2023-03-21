using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData : MonoBehaviour
{
    public string name;
    public Sprite image;
    public int defaultWeight;
    public string enemyType;

    public UpgradeData(string _name, Sprite _image, int _defaultWeight, string _enemyType)
    {
        name  = _name;
        image = _image;
        defaultWeight = _defaultWeight;
        enemyType = _enemyType;
    }
}
