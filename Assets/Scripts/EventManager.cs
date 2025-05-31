using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(EventManager))]
public class EventManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EventManager myScript = (EventManager)target;
        if (GUILayout.Button("Delete Key"))
        {
            myScript.deletekey();
        }
        if (GUILayout.Button("Delete All Key"))
        {
            myScript.deleteAllkey();
        }
    }
}
#endif
public enum Event_STATUS
{
    ENABLE,
    DISABLE
}

public class EventManager : MonoBehaviour
{

    public static EventManager instance = null;
    public float updateFrekans=2.0f;
    float timeToGo;
    #region EVENT 1
    [Header("EVENT 1")]
    public bool ACTIVE;
    public bool ACIKBASLASIN;
    public string EventName;
    public Text LabelText;
    public bool _Event1Enable;
    public bool Loop;
    public bool LoopVisible;
    public GameObject E1_Icon;
    public Button E1_Button;
    public PopupOpener E1_prefab;
    public Text E1_TimerText;
    public bool Gembox;
    public bool StarChallenge;
    public bool MissionChallengeBool;
    public bool GiftBoxEvent;
    public bool DailyGift;
    public bool TimerVisible = true;
    public bool Promo0;
    public bool Promo1;
    public bool Promo2;

    [Header("ENABLE TIME")]
    [RangeAttribute(0, 29)]
    public int E1_E_TimerDays;
    [RangeAttribute(0, 23)]
    public int E1_E_TimerHours;
    [RangeAttribute(0, 59)]
    public int E1_E_TimerMinutes;
    [RangeAttribute(0, 59)]
    public int E1_E_TimerSeconds;


    [Header("DISABLE TIME")]
    [RangeAttribute(0, 29)]
    public int E1_D_TimerDays;
    [RangeAttribute(0, 23)]
    public int E1_D_TimerHours;
    [RangeAttribute(0, 59)]
    public int E1_D_TimerMinutes;
    [RangeAttribute(0, 59)]
    public int E1_D_TimerSeconds;

    private int _E1_E_GecenTimeDays;
    private int _E1_E_GecenTimeHours;
    private int _E1_E_GecenTimeMinutes;
    private int _E1_E_GecenTimeSeconds;

    private int _E1_D_GecenTimeDays;
    private int _E1_D_GecenTimeHours;
    private int _E1_D_GecenTimeMinutes;
    private int _E1_D_GecenTimeSeconds;

    private DateTime E1_E_StartTime;
    private DateTime E1_D_StartTime;
    // private const string EVENT_E = "event_e";
    //  private const string "D" = ""D"";
    #endregion


    // Use this for initialization
    private void Awake()
    {

        #region EVENT 1             
        if (ACIKBASLASIN)
        {
            if (!PlayerPrefs.HasKey("E" + EventName))
            {
                PlayerPrefs.SetString("E" + EventName, DateTime.Now.Ticks.ToString());
                _Event1Enable = true;
            }
            else
            {
                int Event1Enable = PlayerPrefs.GetInt("Event1Enable" + EventName);
                if (Event1Enable == 1)
                {
                    _Event1Enable = true;
                }
                else
                {
                    _Event1Enable = false;
                    if (!PlayerPrefs.HasKey("D" + EventName))
                    {
                        PlayerPrefs.SetString("D" + EventName, DateTime.Now.Ticks.ToString());
                        _Event1Enable = false;
                    }
                    else
                    {
                        _Event1Enable = false;
                    }
                }
            }
        }
        else
        {
            if (!PlayerPrefs.HasKey("E" + EventName))
            {
                PlayerPrefs.SetString("E" + EventName, DateTime.Now.Ticks.ToString());
                _Event1Enable = false;
            }
            else
            {
                int Event1Enable = PlayerPrefs.GetInt("Event1Enable" + EventName);
                if (Event1Enable == 1)
                {
                    _Event1Enable = true;
                }
                else
                {
                    _Event1Enable = false;
                    if (!PlayerPrefs.HasKey("D" + EventName))
                    {
                        PlayerPrefs.SetString("D" + EventName, DateTime.Now.Ticks.ToString());
                        _Event1Enable = false;
                    }
                    else
                    {
                        _Event1Enable = false;
                    }
                }
            }
        }




        #endregion
    }




    // Update is called once per frame  
    private void Start()
    {
        timeToGo = Time.fixedTime + updateFrekans;

        if (_Event1Enable)
        {
            SetTimerE();
        }
        else
        {
            SetTimerD();
        }
    }


   private void FixedUpdate()
    {
        if (Time.fixedTime >= timeToGo)
        {
            if (_Event1Enable)
            {
                UpdateE1_E_Timer();
            }
            else
            {
                UpdateE1_D_Timer();
            }
            // Do your thang
            timeToGo = Time.fixedTime + updateFrekans;
        }
    }
    #region DeleteAll
    public void deletekey()
    {
        PlayerPrefs.DeleteKey("E" + EventName);
        PlayerPrefs.DeleteKey("D" + EventName);
    }
    public void deleteAllkey()
    {
        PlayerPrefs.DeleteAll();

    }
    #endregion
    #region EVENT 1 
    public void SetTimerE()
    {
        // Reset the remaining time values
        _E1_E_GecenTimeDays = E1_E_TimerDays;
        _E1_E_GecenTimeHours = E1_E_TimerHours;
        _E1_E_GecenTimeMinutes = E1_E_TimerMinutes;
        _E1_E_GecenTimeSeconds = E1_E_TimerSeconds;

        // Get last free turn time value from storage
        // We can't save long int to PlayerPrefs so store this value as string and convert to long
        E1_E_StartTime = new DateTime(Convert.ToInt64(PlayerPrefs.GetString("E" + EventName, DateTime.Now.Ticks.ToString())))
            .AddDays(E1_E_TimerDays)
            .AddHours(E1_E_TimerHours)
            .AddMinutes(E1_E_TimerMinutes)
            .AddSeconds(E1_E_TimerSeconds);

    }
    public void SetTimerD()
    {

        // Reset the remaining time values
        _E1_D_GecenTimeDays = E1_D_TimerDays;
        _E1_D_GecenTimeHours = E1_D_TimerHours;
        _E1_D_GecenTimeMinutes = E1_D_TimerMinutes;
        _E1_D_GecenTimeSeconds = E1_D_TimerSeconds;

        // Get last free turn time value from storage
        // We can't save long int to PlayerPrefs so store this value as string and convert to long
        E1_D_StartTime = new DateTime(Convert.ToInt64(PlayerPrefs.GetString("D" + EventName, DateTime.Now.Ticks.ToString())))
            .AddDays(E1_D_TimerDays)
            .AddHours(E1_D_TimerHours)
            .AddMinutes(E1_D_TimerMinutes)
            .AddSeconds(E1_D_TimerSeconds);


    }

    IEnumerator E1_E_Start()
    {
        PlayerPrefs.SetInt("Event1Enable" + EventName, 1);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1.0f);
        PlayerPrefs.SetString("E" + EventName, DateTime.Now.Ticks.ToString());
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1.0f);
        SetTimerE();
        yield return new WaitForSeconds(1.0f);
        E1_Icon.SetActive(true);
        if (Promo0)
        {
            PlayerPrefs.SetInt("Promo0", 1);
            PlayerPrefs.Save();
        }
        if (Promo1)
        {
            PlayerPrefs.SetInt("Promo1", 1);
            PlayerPrefs.Save();
        }
        if (Promo2)
        {
            PlayerPrefs.SetInt("Promo2", 1);
            PlayerPrefs.Save();
        }
        if (DailyGift)
        {
            PlayerPrefs.SetInt("DailyGiftsAlindi", 0);
            PlayerPrefs.SetInt("DailyRewardLifeTimeAlindi", 0);
            PlayerPrefs.Save();

        }

        E1_Button.interactable = true;

        yield return new WaitForSeconds(1.0f);
        _Event1Enable = true;
    }

    IEnumerator E1_D_Start()
    {
        PlayerPrefs.SetInt("Event1Enable" + EventName, 1);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1.0f);
        PlayerPrefs.SetString("D" + EventName, DateTime.Now.Ticks.ToString());
        PlayerPrefs.Save();
        SetTimerD();
        yield return new WaitForSeconds(1.0f);
        _Event1Enable = false;
        yield return new WaitForSeconds(1.0f);
        if (LoopVisible)
        {
            if (Gembox)
            {
                Configuration.instance.GemboxActive = false;
                Configuration.instance.GemboxDeactive = true;
            }
            if (StarChallenge)
            {
                
                PlayerPrefs.SetInt("StarChallengeNum", 0);
                PlayerPrefs.SetInt("StarChallengeEnable", 0);
                PlayerPrefs.SetInt("StarChallengeHediyeAlindi", 0);
                PlayerPrefs.Save();
            }
            if (MissionChallengeBool)
            {
                MissionChallenge.MissionReset();
                PlayerPrefs.SetInt("MissionChallengeEnable", 0);
                PlayerPrefs.SetInt("MissionChallengeHediyeAlindi", 0);
                PlayerPrefs.Save();
            }
            Language.StartGlobalTranslateWord(LabelText, "for activating ", 0, "");

            // LabelText.text = "for activating ";
            E1_Button.interactable = false;
        }
        else
        {
            E1_Icon.SetActive(false);
            if (Promo0)
            {
                PlayerPrefs.SetInt("Promo0", 0);
                PlayerPrefs.Save();
            }
            if (Promo1)
            {
                PlayerPrefs.SetInt("Promo1", 0);
                PlayerPrefs.Save();
            }
            if (Promo2)
            {
                PlayerPrefs.SetInt("Promo2", 0);
                PlayerPrefs.Save();
            }
            if (Gembox)
            {
                Configuration.instance.GemboxActive = false;
                Configuration.instance.GemboxDeactive = true;
            }

        }
    }




    void E1_IconGoster()
    {
        E1_Icon.SetActive(true);
        if (Promo0)
        {
            PlayerPrefs.SetInt("Promo0", 1);
            PlayerPrefs.Save();
        }
        if (Promo1)
        {
            PlayerPrefs.SetInt("Promo1", 1);
            PlayerPrefs.Save();
        }
        if (Promo2)
        {
            PlayerPrefs.SetInt("Promo2", 1);
            PlayerPrefs.Save();
        }
    }
    void E1_IconKapat()
    {
        E1_Icon.SetActive(false);
        if (Promo0)
        {
            PlayerPrefs.SetInt("Promo0", 0);
            PlayerPrefs.Save();
        }
        if (Promo1)
        {
            PlayerPrefs.SetInt("Promo1", 0);
            PlayerPrefs.Save();
        }
        if (Promo2)
        {
            PlayerPrefs.SetInt("Promo2", 0);
            PlayerPrefs.Save();
        }
        if (GiftBoxEvent)
        {
            Configuration.instance.GiftBox = false;
        }
    }
    public void E1_Pencere_goster()
    {
        if (GiftBoxEvent)
        {
            int openedLevel = CoreData.instance.GetOpendedLevel();
            if (openedLevel >= Configuration.instance.BeginLevelLevelChallenge)
            {
                Configuration.instance.GiftBox = true;
                E1_prefab.OpenPopup();
            }
            else
            {
                Language.StartGlobalTranslateWord(LabelText, "Unlock at level ", Configuration.instance.BeginLevelLevelChallenge, "");



                // Configuration.OpenInfoPopup("5to5 Challenge", "Unlock at level \n" + Configuration.instance.BeginLevelLevelChallenge.ToString());
                Configuration.OpenInfoPopup("5to5 Challenge", Language.TranslateWord("Unlock at level") + " " + Configuration.instance.BeginLevelLevelChallenge.ToString());
            }
        }
        else if (DailyGift)
        {
            int openedLevel = CoreData.instance.GetOpendedLevel();
            if (openedLevel >= Configuration.instance.BeginLevelDailyBonus)
            {

                if (PlayerPrefs.GetInt("DailyGiftsAlindi", 0) == 0)
                {
                    //E1_prefab.OpenPopup();
                    Configuration.instance.OpenGiftBoxPopup("Daily Gifts", REWARD_TYPE.DailyGiftBox);
                    NotificationsSetup.NotificationDailyGiftCall();
                    Debug.Log("RepeatLocalNotificationDailyGiftCall");
                }
            }
            else
            {
                Language.StartGlobalTranslateWord(LabelText, "Unlock at level ", Configuration.instance.BeginLevelDailyBonus, "");

                //open info popup             
                //Configuration.OpenInfoPopup("Daily Gifts", "Unlock at level \n" + Configuration.instance.BeginLevelDailyBonus.ToString());
                Configuration.OpenInfoPopup("Daily Gifts", Language.TranslateWord("Unlock at level") + " " + Configuration.instance.BeginLevelDailyBonus.ToString());

            }

        }
        else if (StarChallenge)
        {
            int openedLevel = CoreData.instance.GetOpendedLevel();
            if (openedLevel >= Configuration.instance.BeginLevelStarChallenge)
            {
                
                    if (PlayerPrefs.GetInt("StarChallengeEnable") == 0 && PlayerPrefs.GetInt("StarChallengeHediyeAlindi") == 0)
                    {
                        PlayerPrefs.SetString("E" + "StarChallenge", DateTime.Now.Ticks.ToString());
                        PlayerPrefs.SetInt("StarChallengeNum", 0);
                        PlayerPrefs.Save();
                    }
                    E1_prefab.OpenPopup();
            }
            else
            {
                Language.StartGlobalTranslateWord(LabelText, "Unlock at level ", Configuration.instance.BeginLevelStarChallenge, "");

                //open info popup              
                //Configuration.OpenInfoPopup("Star Challenge", "Unlock at level \n" + "" + Configuration.instance.BeginLevelStarChallenge);
                Configuration.OpenInfoPopup("Star Challenge", Language.TranslateWord("Unlock at level") + " " + Configuration.instance.BeginLevelStarChallenge.ToString());

            }


        }
        else if (MissionChallengeBool)
        {
            int openedLevel = CoreData.instance.GetOpendedLevel();
            if (openedLevel >= Configuration.instance.BeginLevelMissionChallenge)
            {
                if (PlayerPrefs.GetInt("MissionChallengeEnable") == 0 && PlayerPrefs.GetInt("MissionChallengeHediyeAlindi") == 0)
                {
                    MissionChallenge.MissionReset();
                    PlayerPrefs.SetString("E" + "MissionChallenge", DateTime.Now.Ticks.ToString());
                    PlayerPrefs.Save();
                }
                E1_prefab.OpenPopup();
            }
            else
            {
                Language.StartGlobalTranslateWord(LabelText, "Unlock at level ", Configuration.instance.BeginLevelMissionChallenge, "");

                //open info popup               
               // Configuration.OpenInfoPopup("DAILY MISSIONS", "Unlock at level \n" + "" + Configuration.instance.BeginLevelMissionChallenge);
                Configuration.OpenInfoPopup("DAILY MISSIONS", Language.TranslateWord("Unlock at level") + " " + Configuration.instance.BeginLevelMissionChallenge.ToString());

            }

        }
        else
        {
            E1_prefab.OpenPopup();
        }
    }
    public void UpdateE1_E_Timer()
    {
        // Don't count the time if we have free turn already
        if (_Event1Enable == false)
            return;

        //E1_E TIMER KONTROL       
        _E1_E_GecenTimeDays = (int)(E1_E_StartTime - DateTime.Now).Days;
        _E1_E_GecenTimeHours = (int)(E1_E_StartTime - DateTime.Now).Hours;
        _E1_E_GecenTimeMinutes = (int)(E1_E_StartTime - DateTime.Now).Minutes;
        _E1_E_GecenTimeSeconds = (int)(E1_E_StartTime - DateTime.Now).Seconds;


        // If the timer has ended
        if (_E1_E_GecenTimeDays <= 0 && _E1_E_GecenTimeHours <= 0 && _E1_E_GecenTimeMinutes <= 0 && _E1_E_GecenTimeSeconds <= 0)
        {
            Language.StartGlobalTranslateWord(E1_TimerText, "Ready", 0, "");

            // E1_TimerText.text = "waiting";
            // Now we have a free turn             
            // _Event1Enable = false;

            if (Loop)
            {
                //SetTimerD();
                StartCoroutine(E1_D_Start());
                if (DailyGift)
                {
                    PlayerPrefs.SetInt("DailyGiftsAlindi", 0);
                    PlayerPrefs.SetInt("DailyRewardLifeTimeAlindi", 0);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                _Event1Enable = false;
                E1_Icon.SetActive(false);
                if (Promo0)
                {
                    PlayerPrefs.SetInt("Promo0", 0);
                    PlayerPrefs.Save();
                }
                if (Promo1)
                {
                    PlayerPrefs.SetInt("Promo1", 0);
                    PlayerPrefs.Save();
                }
                if (Promo2)
                {
                    PlayerPrefs.SetInt("Promo2", 0);
                    PlayerPrefs.Save();
                }
            }

            //  E1_stop();         
        }
        else
        {
            // Show the remaining time
            if (_E1_E_GecenTimeDays <= 0)
            {
                if (TimerVisible)
                {
                    E1_TimerText.text = String.Format("{0:00}h :{1:00}m", _E1_E_GecenTimeHours, _E1_E_GecenTimeMinutes);
                }
                else
                {
                    Language.StartGlobalTranslateWord(E1_TimerText, "Ready", 0, "");
                }
            }
            if (_E1_E_GecenTimeDays <= 0 && _E1_E_GecenTimeHours <= 0)
            {
                if (TimerVisible)
                {
                    E1_TimerText.text = String.Format("{0:00}m :{1:00}s", _E1_E_GecenTimeMinutes, _E1_E_GecenTimeSeconds);
                }
                else
                {
                    Language.StartGlobalTranslateWord(E1_TimerText, "Ready", 0, "");
                }
            }
            if (_E1_E_GecenTimeDays > 0)
            {
                if (TimerVisible)
                {
                    E1_TimerText.text = String.Format("{0:00}d :{1:00}h", _E1_E_GecenTimeDays, _E1_E_GecenTimeHours);
                }
                else
                {
                    Language.StartGlobalTranslateWord(E1_TimerText, "Ready", 0, "");
                }
            }
            // We don't have a free turn yet
            _Event1Enable = true;

            E1_Icon.SetActive(true);

            if (DailyGift && PlayerPrefs.GetInt("DailyGiftsAlindi") == 1)
            {
                E1_Icon.SetActive(false);
            }

            if (Promo0)
            {
                PlayerPrefs.SetInt("Promo0", 1);
                PlayerPrefs.Save();
            }
            if (Promo1)
            {
                PlayerPrefs.SetInt("Promo1", 1);
                PlayerPrefs.Save();
            }
            if (Promo2)
            {
                PlayerPrefs.SetInt("Promo2", 1);
                PlayerPrefs.Save();
            }
            E1_Button.interactable = true;
            if (!Gembox && !GiftBoxEvent && !StarChallenge && !MissionChallengeBool)
            {
                //  Language.StartGlobalTranslateWord(LabelText, EventName, 0, "");

                // LabelText.text = EventName; 
            }
            else if (Gembox)
            {
                Configuration.instance.GemboxActive = true;
                Configuration.instance.GemboxDeactive = false;
                int CurrentGems = PlayerPrefs.GetInt("GemBoxAmount", 0);
                LabelText.text = "" + CurrentGems;
            }
            else if (GiftBoxEvent)
            {

                int openedLevel = CoreData.instance.GetOpendedLevel();
                if (openedLevel >= Configuration.instance.BeginLevelLevelChallenge)
                {
                    Language.StartGlobalTranslateWord(LabelText, "", 0, "");

                    // LabelText.text = "Live!";
                }


            }
            else if (StarChallenge)
            {
                int openedLevel = CoreData.instance.GetOpendedLevel();
                if (openedLevel >= Configuration.instance.BeginLevelStarChallenge)
                {
                    if (PlayerPrefs.GetInt("StarChallengeHediyeAlindi") == 0)
                    {
                        Language.StartGlobalTranslateWord(LabelText, "", 0, "");                      

                        Configuration.instance.StarChallenge = true;
                        Configuration.instance.EpisodePlay = true;                      

                        if (PlayerPrefs.GetInt("StarChallengeEnable") == 0)
                        {
                            PlayerPrefs.SetString("E" + "StarChallenge", DateTime.Now.Ticks.ToString());
                            PlayerPrefs.SetInt("StarChallengeEnable", 1);
                            PlayerPrefs.SetInt("StarChallengeNum", 0);
                            PlayerPrefs.Save();
                            NotificationsSetup.NotificationStarChallengeCall();
                        }
                    }

                    else
                    {
                        Language.StartGlobalTranslateWord(LabelText, "for activating ", 0, "");
                      

                        //Configuration.instance.StarChallenge = false;
                    }
                }
               

            }
            else if (MissionChallengeBool)
            {
                if (PlayerPrefs.GetInt("MissionChallengeHediyeAlindi") == 0)
                {
                    Language.StartGlobalTranslateWord(LabelText, "", 0, "");

                    //LabelText.text = "Live!";
                }

                else
                {
                    Language.StartGlobalTranslateWord(LabelText, "for activating ", 0, "");

                    //LabelText.text = "For Activing";
                }

            }
            else if (DailyGift)
            {
                if (PlayerPrefs.GetInt("DailyGiftsAlindi", 0) == 0)
                {

                    Language.StartGlobalTranslateWord(LabelText, "Get Gifts!", 0, "");

                    // LabelText.text = "Get Gifts!";
                    E1_Button.interactable = true;
                }
                else
                {
                    Language.StartGlobalTranslateWord(LabelText, "Remaining Time ", 0, "");

                    // LabelText.text = "Remaining Time ";
                    E1_Button.interactable = false;
                    E1_Icon.SetActive(false);
                }

            }

            PlayerPrefs.SetInt("Event1Enable" + EventName, 1);
            PlayerPrefs.Save();
        }
    }
    public void StarChallengePlay()
    {
        if (PlayerPrefs.GetInt("StarChallengeHediyeAlindi") == 0)
        {
            Configuration.instance.StarChallenge = true;
            Configuration.instance.EpisodePlay = true;
            // AudioManager.instance.ButtonClickAudio();
            int openedLevel = CoreData.instance.GetOpendedLevel();

            if (PlayerPrefs.GetInt("StarChallengeEnable") == 0)
            {
                PlayerPrefs.SetString("E" + "StarChallenge", DateTime.Now.Ticks.ToString());
                PlayerPrefs.SetInt("StarChallengeEnable", 1);
                PlayerPrefs.SetInt("StarChallengeNum", 0);
                PlayerPrefs.Save();
                NotificationsSetup.NotificationStarChallengeCall();
            }
        }
        //else
        //{
        //    Notifications.CancelPendingLocalNotification(EM_NotificationsConstants.UserCategory_notification_category_StarCall);
        //}
    }

    public void UpdateE1_D_Timer()
    {
        // Don't count the time if we have free turn already
        if (_Event1Enable == true)
            return;

        //E1_E TIMER KONTROL       
        _E1_D_GecenTimeDays = (int)(E1_D_StartTime - DateTime.Now).Days;
        _E1_D_GecenTimeHours = (int)(E1_D_StartTime - DateTime.Now).Hours;
        _E1_D_GecenTimeMinutes = (int)(E1_D_StartTime - DateTime.Now).Minutes;
        _E1_D_GecenTimeSeconds = (int)(E1_D_StartTime - DateTime.Now).Seconds;


        // If the timer has ended
        if (_E1_D_GecenTimeDays <= 0 && _E1_D_GecenTimeHours <= 0 && _E1_D_GecenTimeMinutes <= 0 && _E1_D_GecenTimeSeconds <= 0)
        {
            Language.StartGlobalTranslateWord(E1_TimerText, "Ready", 0, "");

            //E1_TimerText.text = "Waiting!";
            StartCoroutine(E1_E_Start());
        }
        else
        {
            // Show the remaining time
            if (_E1_D_GecenTimeDays <= 0)
            {
                if (TimerVisible)
                    E1_TimerText.text = String.Format("{0:00}h :{1:00}m", _E1_D_GecenTimeHours, _E1_D_GecenTimeMinutes);
            }
            if (_E1_D_GecenTimeDays <= 0 && _E1_D_GecenTimeHours <= 0)
            {
                if (TimerVisible)
                    E1_TimerText.text = String.Format("{0:00}m :{1:00}s", _E1_D_GecenTimeMinutes, _E1_D_GecenTimeSeconds);
            }
            if (_E1_D_GecenTimeDays > 0)
            {
                if (TimerVisible)
                    E1_TimerText.text = String.Format("{0:00}d :{1:00}h", _E1_D_GecenTimeDays, _E1_D_GecenTimeHours);
            }
            // We don't have a free turn yet
            _Event1Enable = false;
            if (LoopVisible)
            {
                Language.StartGlobalTranslateWord(LabelText, "for activating ", 0, "");

                //LabelText.text = "for Activating";
              //  E1_Button.interactable = false;
            }
            else
            {
                E1_Icon.SetActive(false);
                if (Promo0)
                {
                    PlayerPrefs.SetInt("Promo0", 0);
                    PlayerPrefs.Save();
                }
                if (Promo1)
                {
                    PlayerPrefs.SetInt("Promo1", 0);
                    PlayerPrefs.Save();
                }
                if (Promo2)
                {
                    PlayerPrefs.SetInt("Promo2", 0);
                    PlayerPrefs.Save();
                }
            }
            E1_Button.interactable = false;
            PlayerPrefs.SetInt("Event1Enable" + EventName, 0);
            PlayerPrefs.Save();
        }
    }

    #endregion
}
