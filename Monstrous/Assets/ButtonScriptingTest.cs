using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScriptingTest : MonoBehaviour
{
    public Button thisButton;
    public string message;

    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(foo);
    }

    void foo()
    {
        Debug.Log(message);
    }
    
}
