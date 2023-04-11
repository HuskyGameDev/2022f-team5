using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData : MonoBehaviour
{
    public string name;
    public Sprite image;
    public string enemyType;
    public int defaultWeight;
    public int runningWeight;
    public string description;

    public UpgradeData(string _name, Sprite _image, int _defaultWeight, string _enemyType, int _runningWeight, string _description)
    {
        name  = _name;
        image = _image;
        enemyType = _enemyType;
        defaultWeight = _defaultWeight;
        runningWeight = _runningWeight;
        description = _description;
    }

    public void resetWeight()
    {
        runningWeight = defaultWeight;
    }
}
