using SkillzSDK;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

public sealed class SkillzGameController : MonoBehaviour
{

    private const string GameScene = "Gameplay";  // Your game scene name
    private const string ProgressionRoomScene = "ProgressionRoom"; // Your player progression room
    private const string StartMenuScene = "StartMenu";  // Your menu scene exiting Skillz will return to (optional)
    public string levelsSperatedbyComma;
    public static List<int> levels;
    public static int Lives, Viking,Boss,SelectedMatch,Hammers,StartingLevel,TimerMultiplier,ReviveMultiplier,HammerMultiplier,GamesPlayed,
        GamesWon,PlayerLevel, openedLevel;
    public static float Timer;


    //public static int playerlvl;
    //public static int gamesPlayed;
    public bool hasRecievedData;
    List<string> playerDefaultData = new List<string>()
    {
        "games_played",
        "games_won",
        "average_score",
        "player_level"
    };
    // Called when a player chooses a tournament and the match countdown expires
    public void OnMatchWillBegin(Match matchInfo)
    {
        //FireBaseAnalytics.instance.Log_Event("Match_Begin");
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
        //UIManager.instance.loadingScreen.SetActive(false);
        //SceneManager.LoadScene(ProgressionRoomScene);
    }

    // Called when a player chooses Exit Skillz from the side menu
  
    public void OnSkillzWillExit()
    {
        Debug.Log("Exit working ");
        //FireBaseAnalytics.instance.Log_Event("Skillz_Exit");
        //UIManager.instance.onExitSkillz();
        Transition.LoadLevel("Menu", 0.1f, Color.black);
        GameObject.FindObjectOfType<FirstScene>()?.loadingScreen.SetActive(false);
        //SceneManager.LoadScene("StartMenu");
    }

  
}