using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 150;
    public static bool timerIsRunning = false;
    public static bool timeOut;
    public Text timeText;
    public static Timer instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        //if (PlayerPrefs.GetInt("FirstTime") == 0)
        //{
        //    gameObject.SetActive(false);
        //}
    }
    private void Start()
    {
        
        
        timeRemaining = PlayerPrefs.GetFloat("SkillzTimer");
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
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
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
}