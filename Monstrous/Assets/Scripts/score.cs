using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isTimerRunning;
    private float scoreCount = 0f;

    void Start()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timerText.text = $"Score : {scoreCount}";
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void incScore()
    {
        scoreCount += 10f;
    }
}
