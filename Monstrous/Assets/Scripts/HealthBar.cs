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
    public RectTransform bar;

    public void UpdateHealthBar(float health) {
   slider.value=health;
  }
  public void UpdateHealthBarMax(float max, float change){
        slider.maxValue = max;
        if(change != 0)
        {
            if (bar.sizeDelta.x + change <= 1365)
            {
                bar.sizeDelta = new Vector2(bar.sizeDelta.x + change, bar.sizeDelta.y);
                bar.anchoredPosition = new Vector2(bar.anchoredPosition.x + (change/3.5f), bar.anchoredPosition.y);
            }
            else
            {
                bar.sizeDelta = new Vector2(1365, bar.sizeDelta.y);
                bar.anchoredPosition = new Vector2(400, bar.anchoredPosition.y);
            }
        }
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