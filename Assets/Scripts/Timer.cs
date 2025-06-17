using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 150;
    public static bool timerIsRunning = false;
    public  bool timeOut;
    public Text timeText;
    public static Timer instance;

    // Private action reference
    private static Action timeoutAction;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        timeRemaining = PlayerPrefs.GetFloat("SkillzTimer", 150); // Default value if not set
        timerIsRunning = true;
        timeOut = false;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                PlayerPrefs.SetFloat("SkillzTimer", timeRemaining);
                DisplayTime(timeRemaining);
            }
            else
            {
                timeOut = true;
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining);
                TriggerTimeOutAction(); // 🔔 Call the action
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TriggerTimeOutAction()
    {
        Debug.Log("Time has run out! Triggering timeout action.");
        timeoutAction?.Invoke(); // Safely invoke if not null
    }

    // 🔧 Public method to set the action
    public  void SetTimeoutAction(Action action)
    {
        timeoutAction = action;
    }
}
