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
        PlayerPrefs.SetFloat("soundVolume", 1f);
        SetVolume();
        textbox.SetActive(false);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    private void SetVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }

    public void OnMouseOver()
    {
        hoverText.text = "Volume: " + (int)(volumeSlider.value * 100) + "%";
        textbox.SetActive(true);
    }

    public void OnMouseExit()
    {
        textbox.SetActive(false);
    }
}
