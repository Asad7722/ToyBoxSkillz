using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Analytics;

public class EventTimer : MonoBehaviour
{
    public static EventTimer instance = null;
    public enum EVENT_STATE
    {
        IDLE,
        RUNING,
        READY
    }
    public EVENT_STATE eventState;
    public int EventID;

    public string EventName;   
    public float EventTime;
    public Text EventTimerText;
    public bool RunTimer;
    public GameObject StartTimerButon;
    public GameObject GiftObject;
    public GameObject EventObject;
    DateTime timeOfPause;

    private float _eventTime;
    public double elapsedtime;
    private float timeLeft;
   

    void Awake()
    {             
        if (!PlayerPrefs.HasKey("EventTimeSpan_" + EventName))
        {
            PlayerPrefs.SetString("event_UpdateTime" + EventName, DateTime.Now.ToString());
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("CounterAdded_" + EventName))
        {
            PlayerPrefs.SetInt("CounterAdded_" + EventName, 0);
            PlayerPrefs.Save();
        }

        if(EventID ==1)
        {
            EventTime = Configuration.instance.FreeBox1Time;
        }
       else if (EventID == 2)
        {
            EventTime = Configuration.instance.FreeBox2Time;
        }
       else if (EventID == 3)
        {
            EventTime = Configuration.instance.FreeBox3Time;
        }
        EventTimeSpan = PlayerPrefs.GetFloat("EventTimeSpan_" + EventName, 0);
       
        //timerToAdd calculates the time lapsed till since the game was last shut down
        float timerToAdd = (float)(System.DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("event_UpdateTime" + EventName))).TotalSeconds + PlayerPrefs.GetFloat("timer_" + EventName); ;

        Debug.Log("timerToAdd =" + timerToAdd);

        if (timerToAdd < EventTimeSpan)
        {
            RunTimer = true;
            //update life counter depending upon how much time has been lapsed
            UpdateTimer(timerToAdd);
            eventState = EVENT_STATE.RUNING;
            activeFreeBox();
        }
        else
        {
            RunTimer = false;
            GiftReady();            
            EventTimeSpan = 0;
         
        }

    }

    public void GiftReady()
    {
        
        EventTimerText.text = showEventElapsedTime();
        if (!PlayerPrefs.HasKey("EvenName_" + EventName + "giftclaimed"))
        {
            GiftObject.SetActive(false);
            //StartTimerButon.SetActive(true);
            PlayerPrefs.SetInt("EvenName_" + EventName + "giftclaimed", 1);
            PlayerPrefs.Save();
            eventState = EVENT_STATE.IDLE;
            return;
        }

        if (PlayerPrefs.GetInt("EvenName_" + EventName + "giftclaimed") == 1)
        {
            GiftObject.SetActive(false);
            //StartTimerButon.SetActive(true);
            eventState = EVENT_STATE.IDLE;
        }
        else
        {
            GiftObject.SetActive(true);
            StartTimerButon.SetActive(false);
            eventState = EVENT_STATE.READY;
            Configuration.instance.activeFreeBox = 0;
            if (PlayerPrefs.GetInt("CounterAdded_" + EventName) != 1)
            {
                //int _ReadyFreeBoxesCount = PlayerPrefs.GetInt("ReadyFreeBoxesCount");
                //int _ActiveFreeBoxesCount = PlayerPrefs.GetInt("ActiveFreeBoxesCount");

                //PlayerPrefs.SetInt("ReadyFreeBoxesCount", _ReadyFreeBoxesCount + 1);
                //PlayerPrefs.SetInt("ActiveFreeBoxesCount", _ActiveFreeBoxesCount - 1);

                Configuration.instance.ReadyFreeBoxesCount++;
                Configuration.instance.ActiveFreeBoxesCount--;

                PlayerPrefs.Save();
                PlayerPrefs.SetInt("CounterAdded_" + EventName, 1);
                PlayerPrefs.Save();

            }
        }

    }
    public void GiftOpened()
    {
        GiftObject.SetActive(false);
        //StartTimerButon.SetActive(true);        
        PlayerPrefs.SetInt("EvenName_" + EventName + "giftclaimed", 1);
        PlayerPrefs.SetInt("CounterAdded_" + EventName, 0);
        PlayerPrefs.Save();
        Configuration.instance.ReadyFreeBoxesCount--;
        eventState = EVENT_STATE.IDLE;
        AnalyticsEvent.Custom("FreeBox" + EventID + " opened");
    }



    public void ResetEventAction()
    {
        GiftObject.SetActive(false);
        StartTimerButon.SetActive(false);
       
    }

    public void StartTimer(float time)
    {
        if (!RunTimer)
        {
            EventTimeSpan += time;
            RunTimer = true;
            PlayerPrefs.SetString("event_UpdateTime" + EventName, DateTime.Now.ToString());
            PlayerPrefs.Save();
            ResetEventAction();
            if (Configuration.instance.ActiveFreeBoxesCount == 0)
            {
                Configuration.instance.ActiveFreeBoxesCount++;
            }

            Configuration.instance.ActiveFreeBoxesCount++;
            NotificationsSetup.NotificationCall(time, EventName);
            PlayerPrefs.SetInt("EvenName_" + EventName + "giftclaimed", 0);
            PlayerPrefs.Save();
            eventState = EVENT_STATE.RUNING;
            activeFreeBox();
        }
    }
    public void StartTimerButton()
    {
       
        if(!RunTimer)
        {
            AudioManager.instance.ButtonClickAudio();
            EventTimeSpan = EventTime;
            RunTimer = true;
            PlayerPrefs.SetString("event_UpdateTime" + EventName, DateTime.Now.ToString());
            PlayerPrefs.Save();
            ResetEventAction();
            if(Configuration.instance.ActiveFreeBoxesCount==0)
            {
                Configuration.instance.ActiveFreeBoxesCount++;
            }
           
            NotificationsSetup.NotificationCall(EventTime,EventName);
            StartTimerButon.SetActive(false);
            PlayerPrefs.SetInt("EvenName_" + EventName + "giftclaimed", 0);
            PlayerPrefs.Save();
            eventState = EVENT_STATE.RUNING;

            activeFreeBox();
            AnalyticsEvent.Custom("FreeBox" + EventID + " start_unlock");


        }
       

    }
    void activeFreeBox()
    {
        Configuration.instance.activeFreeBox = EventID;
    }

    private void Update()
    {
        // life counter needs update only if lives are less than maxLives
        if (eventState == EVENT_STATE.IDLE && Configuration.instance.WinLevel >= 3 && Configuration.instance.ActiveFreeBoxesCount == 0)
        {
            EventObject.SetActive(true);
            GiftObject.SetActive(false);
            StartTimerButon.SetActive(true);
        }
        else
        {
            StartTimerButon.SetActive(false);
        }
        if (RunTimer)
        {
            elapsedtime += Time.deltaTime;            
            EventTimerText.text = showEventElapsedTime();
            EventObject.SetActive(true);
            GiftObject.SetActive(false);
            StartTimerButon.SetActive(false);
            //PlayerPrefs.SetFloat("timer_" + EventName, (float)elapsedtime);
            //when elapsedtime becomes greater than 900sec, we update a life
            if (elapsedtime > EventTimeSpan)
            {
                RunTimer = false;                
                GiftReady();
                elapsedtime = 0;
                EventTimeSpan = 0;
               
            }
        }
        else if (Configuration.instance.activeFreeBox == EventID)
        {
            EventObject.SetActive(true);

            StartTimerButton();
        }

        else if (Configuration.instance.activeFreeBox != EventID && eventState == EVENT_STATE.IDLE && Configuration.instance.ActiveFreeBoxesCount != 0)
        {
            EventObject.SetActive(false);

        }

        else if (eventState == EVENT_STATE.READY)
        {

            EventObject.SetActive(true);
            GiftObject.SetActive(true);
            StartTimerButon.SetActive(false);
        }
        

    }

    public void OpenGiftBox()
    {
        if(EventID == 1)
        {
            Configuration.instance.OpenGiftBoxPopup("Gift Box", REWARD_TYPE.Box1);
        }
        else if (EventID == 2)
        {
            Configuration.instance.OpenGiftBoxPopup("Gift Box", REWARD_TYPE.Box2);
        }
        else if (EventID == 3)
        {
            Configuration.instance.OpenGiftBoxPopup("Gift Box", REWARD_TYPE.Box3);
        }        
     
    }
   

    public float EventTimeSpan
    {
        set
        {
            _eventTime = value;
            PlayerPrefs.SetFloat("EventTimeSpan_" + EventName, _eventTime);
            PlayerPrefs.Save();
        }
        get
        {
            return _eventTime;
        }
    }
    //PlayerPref "LifeUpdateTime" stores the value in string of the time when the life counter lives was last updated
    void UpdateTimer(double timerToAdd)
    {
        if (RunTimer)
        {
           // PlayerPrefs.SetFloat("timer_" + EventName, (float)elapsedtime);
            //PlayerPrefs.Save();
            int livesToAdd = Mathf.FloorToInt((float)timerToAdd / EventTimeSpan);
            elapsedtime = (float)timerToAdd % EventTimeSpan;


            if (livesToAdd >= 1)
            {
                RunTimer = false;
                GiftReady();
                elapsedtime = 0;
                EventTimeSpan = 0;
            }
        }

        PlayerPrefs.SetString("event_UpdateTime" + EventName, DateTime.Now.ToString());
        PlayerPrefs.Save();

    }

    public string showEventElapsedTime()
    {
        if(RunTimer)
        {
             timeLeft = EventTimeSpan - (float)elapsedtime;
        }
        else
        {
             timeLeft = EventTime;
        }
        
        int day = Mathf.FloorToInt(timeLeft / 86400);
        int hour = Mathf.FloorToInt((timeLeft / 3600) % 24);
        int min = Mathf.FloorToInt((timeLeft / 60) % 60);
        int sec = Mathf.FloorToInt(timeLeft % 60);
        if (day > 0)
        {
            if (hour > 0)
            {
                return day + "d " + hour.ToString("00") + "h";
            }
            else
            {
                return day + "d";
            }

        }
        else if (hour > 0)
        {
            if (min > 0)
            {
                return hour + "h " + min.ToString("00") + "m";
            }
            else
            {
                return hour + "h";
            }

        }
        else if (min > 0)
        {

            if (sec > 0)
            {
                return min + "m " + sec.ToString("00") + "s";
            }
            else
            {
                return min + "m";
            }

        }
        else
        {
            return sec.ToString("00") + " s";
        }


    }   

    void OnApplicationPause(bool isPause)
    {
        try
        {
            if (eventState == EVENT_STATE.RUNING)
            {
                if (isPause)
                {
                    //save the system time at which the application went in background 
                    timeOfPause = System.DateTime.Now;
                    SaveLifeTimer();
                }
                else
                {
                    if (timeOfPause == default(DateTime))
                    {
                        timeOfPause = System.DateTime.Now;
                    }
                    float timerToAdd = (float)(System.DateTime.Now - timeOfPause).TotalSeconds;
                    elapsedtime += timerToAdd;
                    UpdateTimer(elapsedtime);
                }
            }
        }
        catch 
        {

          
        }
        
    }

    void OnApplicationQuit()
    {
        //print("Configuration: On application quit / Exit date time: " + DateTime.Now.ToString() + " / Life: " + life + " / Timer: " + timer);
        try
        {
            SaveLifeTimer();
        }
        catch
        {

            
        }
        
    }

    private void OnDestroy()
    {
        SaveLifeTimer();
    }

    public void SaveLifeTimer()
    {
        if(eventState == EVENT_STATE.RUNING)
        {
            PlayerPrefs.SetFloat("timer_" + EventName, (float)elapsedtime);
            PlayerPrefs.Save();
            Debug.Log("timer_" + EventName + (float)elapsedtime);
        }

    }
}
