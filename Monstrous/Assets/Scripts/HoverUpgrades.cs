using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverUpgrades : MonoBehaviour
{

    public GameObject textbox;
    public TextMeshProUGUI hoverText;
    public TextMeshPro storedText;

    // Start is called before the first frame update
    void Start()
    {
        textbox.SetActive(false);
    }

    public void OnMouseOver()
    {
        hoverText.text = storedText.text;
        textbox.SetActive(true);
    }

    public void OnMouseExit()
    {
        textbox.SetActive(false);
    }
}
