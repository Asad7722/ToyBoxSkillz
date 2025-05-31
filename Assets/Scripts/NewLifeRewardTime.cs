using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewLifeRewardTime : MonoBehaviour
{
    public static NewLifeRewardTime instance = null;
    
    //Each life replenishes in 15minutes or 900 seconds     
    
    DateTime timeOfPause;

    public float _rewardTime;
    public double timerForLife;
   


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        if (!PlayerPrefs.HasKey("rewardLifeTime"))
        {
            PlayerPrefs.SetString("RewardLifeTimeUpdateTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }

      
        rewardLifeTime = PlayerPrefs.GetFloat("rewardLifeTime",0);
        

        //update life counter only if lives are less than maxLives


        //timerToAdd calculates the time lapsed till since the game was last shut down
        float timerToAdd = (float)(System.DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("RewardLifeTimeUpdateTime"))).TotalSeconds + PlayerPrefs.GetFloat("rewardlifetimerForLife"); ;

        Debug.Log("timerToAdd =" + timerToAdd);          
      
        if (timerToAdd < rewardLifeTime)
        {
            NewLife.instance.rewardLifeLive = true;
            //update life counter depending upon how much time has been lapsed
            UpdateLives(timerToAdd);
        }
        else
        {
            NewLife.instance.rewardLifeLive = false;
            rewardLifeTime = 0;
            timerForLife = 0;
                    
          
        }

       


    }

    public void StartRewardLife(float time)
    {
        if(time==0)
        {
            return;
        }
        if (NewLife.instance.unlimitedLifePurchased)
        {
            return;
        }
        if(NewLife.instance.rewardLifeLive)
        {
            rewardLifeTime += time;          
            
        }
        else
        {
            timerForLife = 0;
            rewardLifeTime = time;             

        }
        
         Configuration.OpenInfoPopup("New Gift", Language.TranslateWord("Thanks for playing.") + "\n" + Language.TranslateWord("Here is your gift")+":" + "\n" + ShowLifeTimeInMinutes() + "\n" + Language.TranslateWord("unlimited life"));


        NewLife.instance.rewardLifeLive = true;
        PlayerPrefs.SetString("RewardLifeTimeUpdateTime", DateTime.Now.ToString());
        PlayerPrefs.Save();

       

       

    }
    private void Update()
    {
        // life counter needs update only if lives are less than maxLives
        if (NewLife.instance.rewardLifeLive)
        {
            timerForLife += Time.deltaTime;

            //when timerForLife becomes greater than 900sec, we update a life
            if (timerForLife > rewardLifeTime)
            {
                NewLife.instance.rewardLifeLive = false;
                timerForLife = 0;
                rewardLifeTime = 0;
            }
        }
       
       

    }

    public float rewardLifeTime
    {
        set
        {
            _rewardTime = value;
            PlayerPrefs.SetFloat("rewardLifeTime", _rewardTime);
            PlayerPrefs.Save();
        }
        get
        {
            return _rewardTime;
        }
    }
    //PlayerPref "LifeUpdateTime" stores the value in string of the time when the life counter lives was last updated
    void UpdateLives(double timerToAdd)
    {
        if (NewLife.instance.rewardLifeLive)
        {

            int livesToAdd = Mathf.FloorToInt((float)timerToAdd / rewardLifeTime);
            timerForLife = (float)timerToAdd % rewardLifeTime;
            

            if (livesToAdd >= 1)
            {
                NewLife.instance.rewardLifeLive = false;
                timerForLife = 0;
                rewardLifeTime = 0;
            }                      
        }

        PlayerPrefs.SetString("RewardLifeTimeUpdateTime", DateTime.Now.ToString());
        PlayerPrefs.Save();

    }

    public string ShowLifeTimeInMinutes()
    {
        float timeLeft = rewardLifeTime - (float)timerForLife;
        int day = Mathf.FloorToInt(timeLeft / 86400);
        int hour = Mathf.FloorToInt((timeLeft / 3600) % 24);
        int min = Mathf.FloorToInt((timeLeft/60) % 60);
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
        else 
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
       

    }

    void OnApplicationPause(bool isPause)
    {
        if (!NewLife.instance.rewardLifeLive)
        {
            return;
        }
        if (isPause)
        {
            try
            {
                timeOfPause = System.DateTime.Now;
                SaveLifeTimer();
            }
            catch 
            {

                
            }
           
           
        }
        else
        {

            try
            {
                if (timeOfPause == default(DateTime))
                {
                    timeOfPause = System.DateTime.Now;
                }
                float timerToAdd = (float)(System.DateTime.Now - timeOfPause).TotalSeconds;
                timerForLife += timerToAdd;
                UpdateLives(timerForLife);
            }
            catch
            {


            }

            
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
        if(!NewLife.instance.rewardLifeLive)
        {
            return;
        }
        PlayerPrefs.SetFloat("rewardlifetimerForLife", (float)timerForLife);
        PlayerPrefs.Save();
        Debug.Log("rewardlifetimerForLife =" + (float)timerForLife);
    }
}
