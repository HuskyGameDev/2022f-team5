using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
  public Slider slider;
  public Player player;

    public GameObject textbox;
    public TextMeshProUGUI hoverText;
    public TextMeshPro storedText;

    public void UpdateHealthBar(float health) {
   slider.value=health;
  }
  public void UpdateHealthBarMax(float max){
   slider.maxValue = max;
  }

    void Start()
    {
        textbox.SetActive(false);
    }

    public void OnMouseOver()
    {
        hoverText.text = storedText.text + slider.value + "/" + (int)slider.maxValue;
        textbox.SetActive(true);
    }

    public void OnMouseExit()
    {
        textbox.SetActive(false);
    }
}