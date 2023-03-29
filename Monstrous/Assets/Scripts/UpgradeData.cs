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

    public UpgradeData(string _name, Sprite _image, int _defaultWeight, string _enemyType, int _runningWeight)
    {
        name  = _name;
        image = _image;
        enemyType = _enemyType;
        defaultWeight = _defaultWeight;
        runningWeight = _runningWeight;
    }

    public void resetWeight()
    {
        runningWeight = defaultWeight;
    }
}
