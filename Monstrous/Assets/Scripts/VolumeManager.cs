using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    public GameObject textbox;
    public TextMeshProUGUI hoverText;

    // Start is called before the first frame update
    void Start()
    {
        hoverText.text = "VOLUME: " + (float)volumeSlider.value;

        SetVolume();
    }

    public void ChangeVolume()
    {
        //AudioListener.volume = volumeSlider.value;
        hoverText.text = "VOLUME: " + (int)(volumeSlider.value * 100) + "%";
    }

    private void SetVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");

    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("soundVolume", volumeSlider.value);
        AudioListener.volume = PlayerPrefs.GetFloat("soundVolume");
        Debug.Log("sound is: " + PlayerPrefs.GetFloat("soundVolume"));
    }

}
