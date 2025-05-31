using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Settings.
/// </summary>
public class Settings : MonoBehaviour
{
	public Image MenuButtonImage;
	public Sprite MenuOpenedSprite;
	public Sprite MenuClosedSprite;
	public GameObject SettingContent;
	bool isMenuOpened = false;
    bool isGameMenuOpened = false;
    public GameObject uiTranspPanel;



    public static Settings instance;

    void Start()
    {
        instance = this;
    }

    /// <summary>
    /// Raises the menu button pressed event.
    /// </summary>
    public void OnMenuButtonPressed ()
	{
		if (InputManager.instance.canInput (1F)) {
			AudioManager.instance.PlayButtonClickSound ();
			if (!isMenuOpened)
            {
				OpenMenu ();
				return;
			} 
            else
            {
                CloseMenu();
                return;
            }
			//GameController.instance.OnBackButtonPressed ();
		}
	}

    public void OnGameMenuButtonPressed()
    {
        if (InputManager.instance.canInput(1F))
        {
            AudioManager.instance.PlayButtonClickSound();
            if (!isGameMenuOpened)
            {
                //FireBaseAnalytics.instance.Log_Event("Game_Paused");
                OpenGameMenu();
                return;
            }
            else
            {
                //FireBaseAnalytics.instance.Log_Event("Game_Resume");
                CloseGameMenu();
                return;
            }
            //GameController.instance.OnBackButtonPressed ();
        }
    }
    /// <summary>
    /// Opens the menu.
    /// </summary>
    void OpenMenu ()
	{
		isMenuOpened = true;
		MenuButtonImage.sprite = MenuOpenedSprite;
		GetComponent<Animator> ().Play ("Open-Settings");
        

    }
    void OpenGameMenu()
    {
        isGameMenuOpened = true;
        MenuButtonImage.sprite = MenuOpenedSprite;
        GetComponent<Animator>().Play("gameopen");
        //uiTranspPanel.SetActive(true);
        uiTranspPanel.transform.GetComponent<itemGrid>().enabled = false;

        Timer.timerIsRunning = false;
  


    }

    public void CloseMenu ()
	{
		//AudioManager.instance.PlayButtonClickSound ();
		isMenuOpened = false;
		MenuButtonImage.sprite = MenuClosedSprite;
		GetComponent<Animator> ().Play ("Close-Settings");
        


    }
    public void CloseGameMenu()
    {
        //AudioManager.instance.PlayButtonClickSound ();
        isGameMenuOpened = false;
        MenuButtonImage.sprite = MenuClosedSprite;
        GetComponent<Animator>().Play("gameclose");
        //uiTranspPanel.SetActive(false);
        Timer.timerIsRunning = true;
        uiTranspPanel.transform.GetComponent<itemGrid>().enabled = true;
    }
    public void quitgamebutton()
    {
        // close Level popup
        if (GameObject.Find("LevelPopup(Clone)"))
        {
            GameObject.Find("LevelPopup(Clone)").GetComponent<Popup>().Close();

        }
        else
        {
            Application.Quit();
        }

    }

    /// <summary>
    /// Raises the leader board button pressed event.
    /// </summary>
    public void OnLeaderBoardButtonPressed ()
	{
		if (InputManager.instance.canInput ()) {
			AudioManager.instance.PlayButtonClickSound ();
			
            CloseMenu();
        //    GoogleAnalyticsV4.instance.LogScreen("LeaderBoard");
//            if (GameServices.IsInitialized())
//            {
//                GameServices.ShowLeaderboardUI();
//            }
//            else
//            {
//#if UNITY_ANDROID
//                GameServices.Init();    // start a new initialization process
//#elif UNITY_IOS
//    Debug.Log("Cannot show leaderboard UI: The user is not logged in to Game Center.");
//#endif
//            }

        }
	}

	/// <summary>
	/// Raises the achievements button pressed event.
	/// </summary>
	public void OnAchievementsButtonPressed ()
	{
		if (InputManager.instance.canInput ()) {
			AudioManager.instance.PlayButtonClickSound ();
			Debug.Log ("Achievement stuff goes here..");
            CloseMenu();
       //     GoogleAnalyticsV4.instance.LogScreen("Achievement");
//            if (GameServices.IsInitialized())
//            {
//                GameServices.ShowAchievementsUI();
//            }
//            else
//            {
//#if UNITY_ANDROID
//                GameServices.Init();    // start a new initialization process
//#elif UNITY_IOS
//    Debug.Log("Cannot show achievements UI: The user is not logged in to Game Center.");
//#endif
//            }
        }
	}

    public void MoregameButtonClick()
    {
        if (InputManager.instance.canInput())
        {
            AudioManager.instance.PlayButtonClickSound();
            string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/developer/aydin-karabudak/id1052609585";
#elif UNITY_ANDROID
            appstoreUrl = "https://play.google.com/store/apps/dev?id=7250183505735303936";
#endif

            Application.OpenURL(appstoreUrl);
         //   GoogleAnalyticsV4.instance.LogScreen("More Games buton");
                CloseMenu();
            }
      }

    public void feedback()
    {
        string content = "Please send us your advice: ";
        Application.OpenURL("mailto:info@bibergames.com?subject=ToyBoxMagicBlast Feedback&body=" + content);
    }
    


    /// <summary>
    /// Raises the about button pressed event.
    /// </summary>
    public void RateUs ()
	{
		if (InputManager.instance.canInput ()) {
			AudioManager.instance.PlayButtonClickSound ();
            string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/app/toy-blast-party-time/id1208107706?mt=8";
#elif UNITY_ANDROID
            appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxblastmagic";
#endif

            Application.OpenURL(appstoreUrl);
           // GoogleAnalyticsV4.instance.LogScreen("RATE US");
            PlayerPrefs.SetInt("Ratedus", 1);
            CloseMenu();
        }
	}
    public void rateustbmb()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/app/toy-blast-party-time/id1208107706?mt=8";
#elif UNITY_ANDROID
        appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxblastmagic";
#endif

        Application.OpenURL(appstoreUrl);
        //GoogleAnalyticsV4.instance.LogScreen("RATE US");
        PlayerPrefs.SetInt("Ratedus", 1);
    }
    public void RateUsCrazy()
    {
        if (InputManager.instance.canInput())
        {
            AudioManager.instance.PlayButtonClickSound();
            string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/app/toy-blast-party-time/id1208107706?mt=8";
#elif UNITY_ANDROID
            appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxcrazycubes";
#endif

            Application.OpenURL(appstoreUrl);
          //  GoogleAnalyticsV4.instance.LogScreen("RATE US");
            PlayerPrefs.SetInt("Ratedus", 1);
            CloseMenu();
        }
    }

    void OnEnable()
	{
	}

	void OnDisable()
	{
		#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		//GamePlay.instance.TogglePauseGame(false);
		#endif

	}
}