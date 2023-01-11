using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
  public Slider slider;
  public Player player;
  public void UpdateHealthBar(float health) {
   slider.value=health;
  }
 
}