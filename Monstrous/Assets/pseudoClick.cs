using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pseudoClick : MonoBehaviour
{
    // Start is called before the first frame update

    public KeyCode key;
    private Button myself;

    void Awake()
    {
        myself = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            myself.onClick.Invoke();
        }
    }
}
