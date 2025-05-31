using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Life : MonoBehaviour
{
    public float updateFrekans = 1.0f;
    float timeToGo;
    // UI
    public Text lifeText;
    public Text timerText;
    public GameObject Unlimited;
    public GameObject RewardLifeCounter;
    public Text RewardLifeCounterText;


    // Use this for initialization
    void Start()
    {
        timeToGo = Time.fixedTime + updateFrekans;
    }

    // Update is called once per frame
    //void Update()
    //{
       
    //}

    private void FixedUpdate()
    {
        if (Time.fixedTime >= timeToGo)
        {
            int RewardLifeCount = NewLife.instance.Rewardlives;
            //if (RewardLifeCount > 0)
            //{
            //    RewardLifeCounter.SetActive(true);
            //    RewardLifeCounterText.text = "" + RewardLifeCount;
            //}
            //else
            //{
            //    RewardLifeCounter.SetActive(false);

            //}

            if (NewLife.instance.unlimitedLifePurchased)
            {
                timerText.text = "Unlimited";
                lifeText.text = "";
                Unlimited.SetActive(true);
            }
            else
            {
                // update timerText
                if (NewLife.instance.rewardLifeLive)
                {
                    timerText.text = NewLifeRewardTime.instance.ShowLifeTimeInMinutes();
                    lifeText.text = "";
                    Unlimited.SetActive(true);
                }
                else
                {
                    Unlimited.SetActive(false);
                    if (NewLife.instance.lives < NewLife.instance.maxLives)
                    {
                        timerText.text = NewLife.instance.showLifeTimeInMinutes();
                        lifeText.text = NewLife.instance.lives.ToString();
                    }
                    else
                    {
                        timerText.text = "Full";
                        lifeText.text = NewLife.instance.maxLives.ToString();

                    }
                }


            }
            // Do your thang
            timeToGo = Time.fixedTime + updateFrekans;
        }
    }
}
