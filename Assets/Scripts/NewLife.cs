using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewLife : MonoBehaviour {
    public static NewLife instance = null;
    public bool rewardLifeLive;
    public bool unlimitedLifePurchased;
    [Header("LIFE TIMER")]
    public int maxLives = 5;
    //Each life replenishes in 15minutes or 900 seconds
    
   

    DateTime timeOfPause;


    public double timerForLife;
    // The number of lives that the player has
    public int _lives;
    public int _Rewardlives;

    #region Commented by Sameer for Lives
    public int lives
    {
        set
        {
            if (unlimitedLifePurchased || rewardLifeLive)
            {
                return;
            }
            _lives = value;
            PlayerPrefs.SetInt("Lives", _lives);
            PlayerPrefs.Save();

        }
        get
        {
            return _lives;
        }
    }

    public int Rewardlives
    {
        set
        {
            if (unlimitedLifePurchased)
            {
                return;
            }
            _Rewardlives = value;
            PlayerPrefs.SetInt("RewardLives", _Rewardlives);
            PlayerPrefs.Save();
        }
        get
        {
            return _Rewardlives;
        }
    }
    #endregion
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

        DontDestroyOnLoad(gameObject);
        UnlimitedLife();


        //if (!PlayerPrefs.HasKey("Lives"))
        //{
        //    PlayerPrefs.SetString("LifeUpdateTime", DateTime.Now.ToString());
        //    PlayerPrefs.Save();
        //}
        //lives = PlayerPrefs.GetInt("Lives", maxLives);
        //Rewardlives = PlayerPrefs.GetInt("RewardLives");

        //update life counter only if lives are less than maxLives
        //if (lives < maxLives)
        //{
        //    //timerToAdd calculates the time lapsed till since the game was last shut down
        //    float timerToAdd = (float)(System.DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("LifeUpdateTime"))).TotalSeconds + PlayerPrefs.GetFloat("timerForLife");
        //    //update life counter depending upon how much time has been lapsed
        //    UpdateLives(timerToAdd);
        //}
        //if (PlayerPrefs.GetInt("unlimitedlife") == 1)
        //{
        //}
    }


    private void Update()
    {
        if(unlimitedLifePurchased)
        {
            return;
        }
        #region commented by sameer
        // life counter needs update only if lives are less than maxLives
        //if (lives < maxLives)
        //{
        //    timerForLife += Time.deltaTime;

        //    //when timerForLife becomes greater than 900sec, we update a life
        //    if (timerForLife > Configuration.instance.lifeReplenishTime)
        //    {
        //        timerForLife = 0;
        //        lives++; //you get a life after 15 minutes
        //        //It is advised to use UpdateLives(timerForLife) here instead of lives++;
        //    }
        //}
        //else
        //{
        //    timerForLife = 0;
        //}
        #endregion
    }

    //PlayerPref "LifeUpdateTime" stores the value in string of the time when the life counter lives was last updated
    #region commented by sameer
    //void UpdateLives(double timerToAdd)
    //{
    //    if (lives < maxLives)
    //    {

    //        int livesToAdd = Mathf.FloorToInt((float)timerToAdd / Configuration.instance.lifeReplenishTime);
    //        timerForLife = (float)timerToAdd % Configuration.instance.lifeReplenishTime;
    //        lives += livesToAdd;
    //        if (lives >= maxLives)
    //        {
    //            lives = maxLives;
    //            timerForLife = 0;
    //        }
    //    }

    //    PlayerPrefs.SetString("LifeUpdateTime", DateTime.Now.ToString());
    //    PlayerPrefs.Save();
    //}
    #endregion
    public string showLifeTimeInMinutes()
    {
        float timeLeft = Configuration.instance.lifeReplenishTime - (float)timerForLife;
        int min = Mathf.FloorToInt(timeLeft / 60);
        int sec = Mathf.FloorToInt(timeLeft % 60);
        return min + ":" + sec.ToString("00");
    }

    void OnApplicationPause(bool isPause)
    {
        //if (lives == maxLives || unlimitedLifePurchased)
        //{
        //    return;
        //}
        //if (isPause)
        //{
        //    try
        //    {
        //        //save the system time at which the application went in background 
        //        timeOfPause = System.DateTime.Now;
        //        SaveLifeTimer();
        //    }
        //    catch 
        //    {

                
        //    }
           
        //}
        //else
        //{
        //    try
        //    {
        //        if (timeOfPause == default(DateTime))
        //        {
        //            timeOfPause = System.DateTime.Now;
        //        }
        //        float timerToAdd = (float)(System.DateTime.Now - timeOfPause).TotalSeconds;
        //        timerForLife += timerToAdd;
        //        UpdateLives(timerForLife);
        //    }
        //    catch
        //    {


        //    }
           
        //}
    }

    void OnApplicationQuit()
    {
        //try
        //{
        //    SaveLifeTimer();
        //}
        //catch
        //{


        //}
    }

    public void SaveLifeTimer()
    {
        //if(lives==maxLives || unlimitedLifePurchased)
        //{
        //    return;
        //}
        //PlayerPrefs.SetFloat("timerForLife", (float)timerForLife);
        //PlayerPrefs.Save();
        //Debug.Log("timerForLife =" + (float)timerForLife);
    }
    private void OnDestroy()
    {
        //SaveLifeTimer();
    }
    public void UnlimitedLife()
    {
        //lives = maxLives;         unlimitedLifePurchased = true;         PlayerPrefs.SetInt("unlimitedlife", 1);         PlayerPrefs.Save();     }
}
