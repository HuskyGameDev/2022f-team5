using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public int exp;
    
    public void UpdateProgressBar(int exp)
    {
        slider.value = exp;
    }
}
