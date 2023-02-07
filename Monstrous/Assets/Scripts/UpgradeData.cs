using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData : MonoBehaviour
{
    public string name;
    public Sprite image;

    public UpgradeData(string _name, Sprite _image)
    {
        name  = _name;
        image = _image;
    }
}
