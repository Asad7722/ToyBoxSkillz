using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Analytics;


public class MissionChallenge : MonoBehaviour
{

    public Button OpenBoxButton;
    public PopupOpener DailyMissionGiftBox;
    // Use this for initialization
    void Start()
    {
        OpenBoxButton.interactable = false;
        MissionStart();

        if (PlayerPrefs.GetInt("MissionChallengeEnable") == 1 && PlayerPrefs.GetInt("MissionChallengeHediyeAlindi") == 0)
        {

            if (MissionComplete())
            {
                OpenBoxButton.interactable = true;
            }
            else
            {
                OpenBoxButton.interactable = false;
            }
        }
    }


    public static void MissionState()
    {

        Debug.Log(PlayerPrefs.GetInt("Mission_01") + "/" + PlayerPrefs.GetInt("Request" + PlayerPrefs.GetInt("Mission_01")));
        Debug.Log(PlayerPrefs.GetInt("Mission_02") + "/" + PlayerPrefs.GetInt("Request" + PlayerPrefs.GetInt("Mission_02")));
        Debug.Log(PlayerPrefs.GetInt("Mission_03") + "/" + PlayerPrefs.GetInt("Request" + PlayerPrefs.GetInt("Mission_03")));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void MissionStart()
    {
        if (PlayerPrefs.GetInt("MissionChallengeEnable") == 0)
        {
            int Mission_01 = UnityEngine.Random.Range(1, 7);
            int Mission_02 = UnityEngine.Random.Range(7, 13);
            int Mission_03 = UnityEngine.Random.Range(13, 22);

            PlayerPrefs.SetString("E" + "MissionChallenge", DateTime.Now.Ticks.ToString());
            PlayerPrefs.SetInt("MissionChallengeEnable", 1);
            PlayerPrefs.SetInt("Mission_01", Mission_01);
            PlayerPrefs.SetInt("Mission_02", Mission_02);
            PlayerPrefs.SetInt("Mission_03", Mission_03);
            PlayerPrefs.Save();
            MissionReset();
            Debug.Log("Mission_01,Mission_02,Mission_03: " + Mission_01 + "," + Mission_02 + "," + Mission_03);

            int Request1 = UnityEngine.Random.Range(Configuration.instance.Request1_Min, Configuration.instance.Request1_Max);
            int Request2 = UnityEngine.Random.Range(Configuration.instance.Request2_Min, Configuration.instance.Request2_Max);
            int Request3 = UnityEngine.Random.Range(Configuration.instance.Request3_Min, Configuration.instance.Request3_Max);
            int Request4 = UnityEngine.Random.Range(Configuration.instance.Request4_Min, Configuration.instance.Request4_Max);
            int Request5 = UnityEngine.Random.Range(Configuration.instance.Request5_Min, Configuration.instance.Request5_Max);



            //Mission 1
            PlayerPrefs.SetInt("Request" + Mission_01, Request1);
            PlayerPrefs.Save();
            Debug.Log("Request" + Mission_01 + " :" + Request1);

            //Mission 2
            if (Mission_02 >= 7 && Mission_02 <= 9)
            {
                PlayerPrefs.SetInt("Request" + Mission_02, Request2);
                PlayerPrefs.Save();
                Debug.Log("Request" + Mission_02 + " :" + Request2);
            }
            else if (Mission_02 >= 10 && Mission_02 <= 12)
            {
                PlayerPrefs.SetInt("Request" + Mission_02, Request3);
                PlayerPrefs.Save();
                Debug.Log("Request" + Mission_02 + " :" + Request3);
            }


            //Mission 3
            if (Mission_03 >= 13 && Mission_03 <= 17)
            {
                PlayerPrefs.SetInt("Request" + Mission_03, Request4);
                PlayerPrefs.Save();
                Debug.Log("Request" + Mission_03 + " :" + Request4);
            }
            else if (Mission_03 >= 18)
            {
                PlayerPrefs.SetInt("Request" + Mission_03, Request5);
                PlayerPrefs.Save();
                Debug.Log("Request" + Mission_03 + " :" + Request5);
            }

        }
    }
    public static bool MissionComplete()
    {
        if (PlayerPrefs.GetInt("" + PlayerPrefs.GetInt("Mission_01")) >= PlayerPrefs.GetInt("Request" + PlayerPrefs.GetInt("Mission_01")) &&
            PlayerPrefs.GetInt("" + PlayerPrefs.GetInt("Mission_02")) >= PlayerPrefs.GetInt("Request" + PlayerPrefs.GetInt("Mission_02")) &&
            PlayerPrefs.GetInt("" + PlayerPrefs.GetInt("Mission_03")) >= PlayerPrefs.GetInt("Request" + PlayerPrefs.GetInt("Mission_03")))
        {

            Debug.Log("MISSION COMPLETE");
            return true;
        }
        else
        {
            Debug.Log("MISSION NOT COMPLETE");
            return false;
        }

    }

    public static void MissionReset()
    {
        PlayerPrefs.SetInt("1", 0);
        PlayerPrefs.SetInt("2", 0);
        PlayerPrefs.SetInt("3", 0);
        PlayerPrefs.SetInt("4", 0);
        PlayerPrefs.SetInt("5", 0);
        PlayerPrefs.SetInt("6", 0);
        PlayerPrefs.SetInt("7", 0);
        PlayerPrefs.SetInt("8", 0);
        PlayerPrefs.SetInt("9", 0);
        PlayerPrefs.SetInt("10", 0);
        PlayerPrefs.SetInt("11", 0);
        PlayerPrefs.SetInt("12", 0);
        PlayerPrefs.SetInt("13", 0);
        PlayerPrefs.SetInt("14", 0);
        PlayerPrefs.SetInt("15", 0);
        PlayerPrefs.SetInt("16", 0);
        PlayerPrefs.SetInt("17", 0);
        PlayerPrefs.SetInt("18", 0);
        PlayerPrefs.SetInt("19", 0);
        PlayerPrefs.SetInt("20", 0);
        PlayerPrefs.SetInt("21", 0);
        PlayerPrefs.Save();

    }
    public void OpenBox()
    {
        if (MissionComplete())
        {
            //DailyMissionGiftBox.OpenPopup();
            Configuration.instance.OpenGiftBoxPopup("Treasure Box", REWARD_TYPE.MissionChallengeGiftBox);
            AnalyticsEvent.Custom("MissionWinPopup");
            NewLifeRewardTime.instance.StartRewardLife(Configuration.instance.WinChallengeRewardTime);


        }

    }
    public static void ShowMissionChallenge()
    {

        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var nesne = Instantiate(Resources.Load(Configuration.ShowMissionChallenge())) as GameObject;
        nesne.transform.SetParent(m_canvas.transform, false);
    }
    public static void ShowMiniMissionChallenge()
    {

        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var nesne = Instantiate(Resources.Load(Configuration.ShowMiniMissionChallenge())) as GameObject;
        nesne.transform.SetParent(m_canvas.transform, false);
    }
}
