using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
 

public class FirstScene : MonoBehaviour
{
    //private FirebaseApp app;
    public GameObject PlayButon,tutInfbutton, Play_2Butt,tutbuttonMIne;
    public GameObject loadingScreen;
    private bool SafeStartLive;
    void Awake()
    {

        if (!PlayerPrefs.HasKey("Bildirim"))
        {
            PlayerPrefs.SetInt("Bildirim", 1);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("NoSleep"))
        {
            PlayerPrefs.SetInt("NoSleep", 1);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("isNotifEnabled"))
        {
            PlayerPrefs.SetInt("isNotifEnabled", 1);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("CurrentEpisode"))
        {
            PlayerPrefs.SetInt("CurrentEpisode", 1);
            PlayerPrefs.Save();
        }
    }
    // Use this for initialization
    void Start()
    {

        if(PlayerPrefs.GetInt("oNclick")==0)
        {
            tutbuttonMIne.SetActive(true);
            PlayButon.SetActive(true);
            tutInfbutton.SetActive(false);
            Play_2Butt.SetActive(false);

        }
        else
        {
            tutbuttonMIne.SetActive(false);
            PlayButon.SetActive(false);
            tutInfbutton.SetActive(true);
            Play_2Butt.SetActive(true);
        }
        Application.targetFrameRate = 60;
        PlayButon.SetActive(false);
        Invoke("SafeStart", 15f);

        LanguageSetup();

        Configuration.instance.CurrentEpisode = PlayerPrefs.GetInt("CurrentEpisode");
        Configuration.instance.MenuToMap = true;
        Configuration.instance.CurrentScene = CURRENT_SCENE.MENU;


        if (Configuration.instance.Bildirim && PlayerPrefs.GetInt("isNotifEnabled") == 1)
        {
            PlayerPrefs.SetInt("Bildirim", 1);
            PlayerPrefs.Save();
            bool isInit = false;// Notifications.IsInitialized();
            if (!isInit)
            {
                //Notifications.Init();
                Debug.Log("Notification module is initalizing.");
            }
        }
        else
        {
            PlayerPrefs.SetInt("Bildirim", 0);
            PlayerPrefs.Save();
            //Notifications.CancelAllPendingLocalNotifications();
            Debug.Log("CancelAllPendingLocalNotifications");
        }

        AnalyticsEvent.GameStart();

        if (!PlayerPrefs.HasKey("FirstInteraction") && CoreData.instance.openedLevel <= 1)
        {
            PlayerPrefs.SetInt("FirstInteraction", 1);
            PlayerPrefs.Save();
            AnalyticsEvent.Custom("FirstInteraction");
        }

        StartCoroutine(Bypass());
    }
    public void PLaytutpref()
    {
        PlayerPrefs.SetInt("oNclick", 1);
        
    }
    public void SafeStart()
    {
        if (!SafeStartLive)
        {
            return;
        }

        SafeStartLive = true;
        //if (CoreData.instance.openedLevel <= 1 && Configuration.instance.FirstGoMap != 0)
        //{

        //    Configuration.instance.CurrentEpisode = 1;

        //    StageLoader.instance.Stage = 1;
        //    StageLoader.instance.LoadLevel(1);

        //    if (StageLoader.instance.StageReady)
        //    {

        //        Transition.LoadLevel("Play", 0.1f, Color.black);

        //    }
        //    else
        //    {
        //        PlayButon.SetActive(true);
        //    }


        //}
        //else
        //{
        //    Configuration.instance.CurrentEpisode = PlayerPrefs.GetInt("CurrentEpisode");

        //    Transition.LoadLevel("Map", 0.1f, Color.black);


        //}
    }

#if UNITY_IOS
    //sameertransparent
    /// <summary>
    /// Callback invoked with the user's decision
    /// </summary>
    /// <param name="status"></param>
    private void OnAuthorizationRequestDone(AppTrackingTransparency.AuthorizationStatus status)
    {
        switch (status)
        {
            case AppTrackingTransparency.AuthorizationStatus.NOT_DETERMINED:
                Debug.Log("AuthorizationStatus: NOT_DETERMINED");
                break;
            case AppTrackingTransparency.AuthorizationStatus.RESTRICTED:
                Debug.Log("AuthorizationStatus: RESTRICTED");
                break;
            case AppTrackingTransparency.AuthorizationStatus.DENIED:
                Debug.Log("AuthorizationStatus: DENIED");
                break;
            case AppTrackingTransparency.AuthorizationStatus.AUTHORIZED:
                Debug.Log("AuthorizationStatus: AUTHORIZED");
                break;
        }

        // Obtain IDFA
        Debug.Log(string.Format("IDFA: {0}", AppTrackingTransparency.IdentifierForAdvertising()));
    }
#endif

    IEnumerator Bypass()
    {
        while (!Ready() && !SafeStartLive)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(Configuration.instance.WaitMenuInitSeconds);


        if (Configuration.instance.MenuSceneBypass)
        {
            PlayButon.SetActive(false);
            StartCoroutine(StartAutoGame());
        }
        else { PlayButon.SetActive(true); }
    }
    public bool Ready()
    {
        if (Configuration.instance.Loaded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlayButtonEnable()
    {
        PlayButon.SetActive(true);
    }

    public void LanguageSetup()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Chinese:
                {
                    Configuration.instance.Lang = "ZH";
                    break;
                }
            case SystemLanguage.ChineseSimplified:
                {
                    Configuration.instance.Lang = "ZH";
                    break;
                }
            case SystemLanguage.ChineseTraditional:
                {
                    Configuration.instance.Lang = "ZH";
                    break;
                }
            case SystemLanguage.Portuguese:
                {
                    Configuration.instance.Lang = "BR";
                    break;
                }
            case SystemLanguage.Spanish:
                {
                    Configuration.instance.Lang = "ES";
                    break;
                }
            case SystemLanguage.Italian:
                {
                    Configuration.instance.Lang = "IT";
                    break;
                }
            case SystemLanguage.English:
                {
                    Configuration.instance.Lang = "EN";
                    break;
                }
            case SystemLanguage.Korean:
                {
                    Configuration.instance.Lang = "KO";
                    break;
                }
            case SystemLanguage.French:
                {
                    Configuration.instance.Lang = "FR";
                    break;
                }
            case SystemLanguage.German:
                {
                    Configuration.instance.Lang = "DE";
                    break;
                }
            case SystemLanguage.Polish:
                {
                    Configuration.instance.Lang = "PL";
                    break;
                }
            case SystemLanguage.Russian:
                {
                    Configuration.instance.Lang = "RU";
                    break;
                }
            case SystemLanguage.Turkish:
                {
                    Configuration.instance.Lang = "TR";
                    break;
                }
            case SystemLanguage.Japanese:
                {
                    Configuration.instance.Lang = "JA";
                    break;
                }
            default:
                {
                    Configuration.instance.Lang = "EN";
                    break;
                }
        }
    }

    IEnumerator StartAutoGame()
    {
        SafeStartLive = false;
        if (CoreData.instance.openedLevel <= 1)
        {
            Configuration.instance.EpisodePlay = true;
            Configuration.instance.CurrentEpisode = 1;
            PlayerPrefs.SetInt("CurrentEpisode", 1);
            PlayerPrefs.Save();
        }

        if (CoreData.instance.openedLevel <= 1 && Configuration.instance.FirstGoMap != 0)
        {
            Configuration.instance.CurrentEpisode = 1;

            StageLoader.instance.Stage = 1;
            StageLoader.instance.LoadLevel(1);

            while (!StageLoader.instance.StageReady)
            {
                yield return null;
            }
            //Transition.LoadLevel("Play");
            StartCoroutine(RunFade("Play"));
        }
        else
        {
            Configuration.instance.CurrentEpisode = PlayerPrefs.GetInt("CurrentEpisode");
            // Transition.LoadLevel("Map");
            StartCoroutine(RunFade("Map"));

        }

    }
    private IEnumerator RunFade(string level)
    {
        PlayButon.SetActive(false);
        gameObject.transform.SetAsLastSibling();
        GC.Collect();
        //// add loading image
        //var loadingImage = Instantiate(PopupManager.instance.LoadingImagePopup) as GameObject;
        //loadingImage.transform.SetParent(m_canvas.transform, false);
        //if (level == "Play")
        //{
        //    while (!StageLoader.instance.StageReady)
        //    {
        //        yield return null;
        //    }
        //}


        //yield return new WaitForEndOfFrame();


        SceneManager.LoadScene(level);


        yield return new WaitForEndOfFrame();

        Destroy(gameObject);
    }
    public void StartGame()
    {
     
        //FireBaseAnalytics.instance.Log_Event("Play_Button_Clicked");
        if (SafeStartLive)
        {
            goto SafeStart;
        }
        //Load Friend Data        
        if (!Ready())
        {
            PlayButon.SetActive(true);
            return;
        }
        SafeStartLive = false;
    //print("this coming here ");
    SafeStart:
        //if (PlayerPrefs.GetInt("FirstTime") == 0)//  (CoreData.instance.openedLevel <= 1 && Configuration.instance.FirstGoMap != 0)
        //{
        //    Configuration.instance.CurrentEpisode = 1;            
        //    StageLoader.instance.Stage = 1;
        //    StageLoader.instance.LoadLevel(1);

        //    if (StageLoader.instance.StageReady)
        //    {
        //        Transition.LoadLevel("Play", 0.1f, Color.black);
        //    }
        //    else
        //    {
        //        PlayButon.SetActive(true);
        //    }
        //}
        //else
        //skillzlevelstart

        //PlayerPrefs.SetInt("LevelWin", 0);
        //openedLevel = Configuration.instance.LeveltoPlay;
        //StageLoader.instance.LoadLevel(openedLevel);
        //Debug.Log("Opened Level" + openedLevel);
        //PlayerPrefs.SetFloat("SkillzTimer", 180);
        //Transition.LoadLevel("Play", 0.2f, Color.black);
//#if  UNITY_EDITOR
 //thiscomment

        SkillzCrossPlatform.LaunchSkillz();
        loadingScreen.SetActive(true);
/*#else
        loadingScreen.SetActive(true);
        PlayerPrefs.SetInt("LevelWin", 0);
        openedLevel = Configuration.instance.LevelNumber();
        Debug.Log("Skillz Level: " + openedLevel);
        StageLoader.instance.LoadLevel(openedLevel);
        Debug.Log("Opened Level" + openedLevel);
        PlayerPrefs.SetFloat("SkillzTimer", 180);
        Transition.LoadLevel("Play", 0.2f, Color.black);
#endif*/
        //Configuration.instance.CurrentEpisode = PlayerPrefs.GetInt("CurrentEpisode");
        //Transition.LoadLevel("Map", 0.1f, Color.black);

    }
    int openedLevel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnApplicationQuit()
    {
        AnalyticsEvent.GameOver(CoreData.instance.openedLevel, "Menu Scene");

    }

}
