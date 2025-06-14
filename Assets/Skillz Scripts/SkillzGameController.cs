using SkillzSDK;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

public sealed class SkillzGameController : MonoBehaviour
{

   
    public static List<int> levels;
    public static int   openedLevel;
    public static float Timer;

     
    public void OnMatchWillBegin(Match matchInfo)
    {
        
        PlayerPrefs.SetInt("LevelWin", 0);

        openedLevel = Configuration.instance.LevelNumber();
       
        StageLoader.instance.LoadLevel(openedLevel);
        Debug.Log("Opened Level" + openedLevel);
        PlayerPrefs.SetFloat("SkillzTimer", 180);
        PlayerPrefs.SetInt("BaseScore", 0);
        Transition.LoadLevel("Play", 0.2f, Color.black);

        Configuration.instance.resetData();
    }

public void OnProgressionRoomEnter()
    {
        Debug.Log("progress working ");
      
    }

   
  
    public void OnSkillzWillExit()
    {
        Debug.Log("Exit working ");
         
        Transition.LoadLevel("Menu", 0.1f, Color.black);
        GameObject.FindObjectOfType<FirstScene>()?.loadingScreen.SetActive(false);
        
    }

  
}