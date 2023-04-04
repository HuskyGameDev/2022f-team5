using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isTimerRunning;

    void Start()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            float timeElapsed = Time.time - startTime;
            string minutes = ((int)timeElapsed / 60).ToString("00");
            string seconds = (timeElapsed % 60).ToString("00");
            string milliseconds = ((timeElapsed * 1000) % 1000).ToString("000");
            timerText.text = $"{minutes}:{seconds}:{milliseconds}";
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }
}