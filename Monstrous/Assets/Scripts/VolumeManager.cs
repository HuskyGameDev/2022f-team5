using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("soundVolume", 1f);
        SetVolume();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    private void SetVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }

}
