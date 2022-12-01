using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    private float fixedDeltaTime;
    public static bool Paused = false;

    //when this panel is awakened by the player leveling up, generate an upgrade in each slot
    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 0f;
        Paused = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        Paused = true;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        Paused = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
