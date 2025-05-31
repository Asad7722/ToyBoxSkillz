using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Analytics;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class MapScene : MonoBehaviour
{
  
    public static MapScene instance = null;
    public bool GetGift = false;
    protected Canvas h_canvas;
    public static int GiftTiklanmadi;
    public PopupOpener levelPopup = null;
    public Text coinText = null;
    protected Canvas m_canvas;
    public InputField InputText = null;
   // public Text starText;
    public Text sandikText = null;
    public Text puanText = null;    
    public PopupOpener biberPopup = null;    
    public GameObject scrollContent = null;
    public PopupOpener lifePopup = null;
    public PopupOpener starGiftPopup = null;
    public PopupOpener puanGiftPopup = null;
    public GameObject starGift = null;
    public GameObject puanGift = null;
    public GameObject starshake = null;
    public GameObject puanshake = null;
    public Image starprogress = null;
    public Image scoreprogress = null;
   // public Text toplamScore;
    public GameObject starinfo = null;
    public GameObject scoreinfo = null;
    public Text scoreinfotext = null;
    public Text starinfotext = null;
    float canvasHeight;    
    private Texture def_Texture;    
  
    public string bannerPlacement = "banner";
    int seed2;
    [Header("Boster Sum Values")]
    public float BoostersNum;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Application.targetFrameRate = 60;
       
        Configuration.instance.CurrentScene = CURRENT_SCENE.MAP;

        GetGift = false;
        Configuration.instance.RewardPlay = false;
        Configuration.instance.OneTimePlay = false;
        if(!PlayerPrefs.HasKey("FirstRewardLife"))
        {
            NewLife.instance.Rewardlives += 3;
            PlayerPrefs.SetInt("FirstRewardLife", 1);
            PlayerPrefs.Save();
            
        }
       

        if (!Configuration.instance.MissionChallengeEvent || PlayerPrefs.GetInt("MissionChallengeHediyeAlindi") == 1)
        {
            MissionChallenge.MissionReset();
        }

        if(CoreData.instance.openedLevel > Configuration.instance.BeginLevelMissionChallenge)
        {
            MissionChallenge.MissionStart();
        }
       
       

        //StarChallenge
        if (Configuration.instance.StarChallengeEvent)
        {
            if (PlayerPrefs.GetInt("StarChallengeEnable") == 1 && PlayerPrefs.GetInt("StarChallengeHediyeAlindi") == 0)
            {
                Configuration.instance.StarChallenge = true;
            }
            else
            {
                Configuration.instance.StarChallenge = false;
            }
        }
        
        //Sandiklar();
        GetPrefabs();


        // Start AutoPopup
        if (Configuration.instance.autoPopup > 0 && Configuration.instance.autoPopup <= Configuration.instance.maxLevel)
        {
            Configuration.instance.playing = true;           

            if (Configuration.instance.MissionChallengeEvent && MissionChallenge.MissionComplete() && PlayerPrefs.GetInt("MissionChallengeEnable") == 1 && PlayerPrefs.GetInt("MissionChallengeHediyeAlindi") == 0)
            {
                MissionChallenge.ShowMissionChallenge();
                UpdateOldProgressBar();
                UpdateOldScoreStarAmountLabel();
            }
            else
            {
                StartCoroutine(OpenLevelPopup());
            }

        }
        else
        {

            Configuration.instance.RandomPlay = false;
            Configuration.instance.ArenaMode = false;
            Configuration.instance.GiftBox = false;
            Configuration.instance.EpisodePlay = false;
            Configuration.instance.playing = false;
            StartCoroutine(StartGame());
        }
        coinText.text = CoreData.instance.GetPlayerCoin().ToString();
       
        BackgroundMusic.instance.MapMusic();
       
    }  
   
    
    public void GetPrefabs()
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        Canvas p_canvas = GameObject.Find("Canvaspromo").GetComponent<Canvas>();
        GameObject RightIcons = GameObject.Find("RIGHTICONS");
        GameObject LeftIcons = GameObject.Find("LEFTICONS");

        //Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Canvas d_canvas = GameObject.Find("CanvasDiffuse").GetComponent<Canvas>();

        //Star Challenge Event
        if (Configuration.instance.StarChallengeEvent)
        {

            if (openedLevel >= Configuration.instance.BeginLevelStarChallenge)
            {
                var eventfab = Instantiate(Resources.Load("Prefabs/EVENTS/StarChallenge")) as GameObject;
                eventfab.transform.SetParent(RightIcons.transform, false);

                //if (PlayerPrefs.GetInt("StarChallengeEnable") == 0 && PlayerPrefs.GetInt("StarChallengeHediyeAlindi") == 0)
                //{
                //    PlayerPrefs.SetString("E" + "StarChallenge", DateTime.Now.Ticks.ToString());
                //    PlayerPrefs.SetInt("StarChallengeNum", 0);
                //    PlayerPrefs.Save();
                //    Configuration.instance.StarChallenge = true;
                //}

                if (PlayerPrefs.GetInt("ShowHelpStarChallenge") == 0 && !GameObject.Find("Hand(Clone)"))
                {
                    GetGift = true;
                    //var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
                    var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
                    //diffuse.transform.SetParent(d_canvas.transform, false);
                    //eventfab.transform.SetParent(d_canvas.transform, false);
                    prefab.transform.SetParent(eventfab.transform, false);
                    PlayerPrefs.SetInt("ShowHelpStarChallenge", 1);
                    PlayerPrefs.Save();
                }
            }
        }
        #region commented by sameer wheel
        //Gift Wheel Event
        //if (Configuration.instance.GiftWheelEvent)
        //{

        //    if (openedLevel >= Configuration.instance.BeginLevelFortuneWheel)
        //    {
        //        var eventfab = Instantiate(Resources.Load("Prefabs/EVENTS/FortuneWheel")) as GameObject;
        //        eventfab.transform.SetParent(RightIcons.transform, false);
        //        if (PlayerPrefs.GetInt("ShowHelpFortuneWheel") == 0 && !GameObject.Find("Hand(Clone)"))
        //        {
        //            GetGift = true;
        //            //var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
        //            var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
        //           // diffuse.transform.SetParent(d_canvas.transform, false);
        //            //eventfab.transform.SetParent(d_canvas.transform, false);
        //            prefab.transform.SetParent(eventfab.transform, false);
        //            PlayerPrefs.SetInt("ShowHelpFortuneWheel", 1);
        //            PlayerPrefs.Save();
        //        }
        //    }
        //}

        //Daily Gift Event
        //if (Configuration.instance.DailyGiftEvent)
        //{

        //    if (openedLevel >= Configuration.instance.BeginLevelDailyBonus)
        //    {
        //        var eventfab = Instantiate(Resources.Load("Prefabs/EVENTS/DailyBonus")) as GameObject;
        //        eventfab.transform.SetParent(RightIcons.transform, false);

        //        if (PlayerPrefs.GetInt("ShowHelpDailyBonus") == 0 && !GameObject.Find("Hand(Clone)"))
        //        {
        //            eventfab.GetComponent<EventManager>()._Event1Enable = true;
        //            eventfab.GetComponent<EventManager>().E1_Icon.SetActive(true);

        //            GetGift = true;
        //            //var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
        //            var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
        //            //diffuse.transform.SetParent(d_canvas.transform, false);
        //            //eventfab.transform.SetParent(d_canvas.transform, false);
        //            prefab.transform.SetParent(eventfab.transform, false);
        //            PlayerPrefs.SetInt("ShowHelpDailyBonus", 1);
        //            PlayerPrefs.Save();
        //        }
        //    }
        //}

        //5 TO 5 Event
        #endregion
        if (Configuration.instance.LevelChallengeEvent)
        {

            if (openedLevel >= Configuration.instance.BeginLevelLevelChallenge)
            {
                var eventfab = Instantiate(Resources.Load("Prefabs/EVENTS/LevelChallenge")) as GameObject;
                eventfab.transform.SetParent(LeftIcons.transform, false);
                if (PlayerPrefs.GetInt("ShowHelpLevelChallenge") == 0 && !GameObject.Find("Hand(Clone)"))
                {
                    GetGift = true;
                    //var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
                    var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
                    //diffuse.transform.SetParent(d_canvas.transform, false);
                    //eventfab.transform.SetParent(d_canvas.transform, false);
                    prefab.transform.SetParent(eventfab.transform, false);
                    PlayerPrefs.SetInt("ShowHelpLevelChallenge", 1);
                    PlayerPrefs.Save();
                }

            }
        }

        //Mission Challenge Event
        if (Configuration.instance.MissionChallengeEvent)
        {
            if (openedLevel >= Configuration.instance.BeginLevelMissionChallenge)
            {
                var eventfab = Instantiate(Resources.Load("Prefabs/EVENTS/MissionChallenge")) as GameObject;
                eventfab.transform.SetParent(LeftIcons.transform, false);

                //if (PlayerPrefs.GetInt("MissionChallengeEnable") == 0 && PlayerPrefs.GetInt("MissionChallengeHediyeAlindi") == 0)
                //{
                //    MissionChallenge.MissionReset();
                //    PlayerPrefs.SetString("E" + "MissionChallenge", DateTime.Now.Ticks.ToString());
                //    PlayerPrefs.Save();                   
                //}
                if (PlayerPrefs.GetInt("ShowHelpMissionChallenge") == 0 && !GameObject.Find("Hand(Clone)"))
                {                   
                    GetGift = true;
                    //var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
                    var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
                    //diffuse.transform.SetParent(d_canvas.transform, false);
                   // eventfab.transform.SetParent(d_canvas.transform, false);
                    prefab.transform.SetParent(eventfab.transform, false);
                    PlayerPrefs.SetInt("ShowHelpMissionChallenge", 1);
                    PlayerPrefs.Save();
                }
            }
        }
        
       

       

        //Gembox Event
        if (Configuration.instance.GemboxEvent)
        {
            if (openedLevel >= Configuration.instance.BeginLevelGembox)
            {
                //Gembox Kontrol
                int GemboxAktif = PlayerPrefs.GetInt("Event1Enable" + "GEM BOX");
                if (GemboxAktif == 1)
                {
                    Configuration.instance.GemboxActive = true;
                    Configuration.instance.GemboxDeactive = false;
                }
                else
                {
                    Configuration.instance.GemboxActive = false;
                    Configuration.instance.GemboxDeactive = true;
                }
                if (openedLevel >= Configuration.instance.BeginLevelGembox)
                {
                    var eventfab = Instantiate(Resources.Load("Prefabs/EVENTS/Gembox")) as GameObject;
                    eventfab.transform.SetParent(LeftIcons.transform, false);
                    if (PlayerPrefs.GetInt("ShowHelpGembox") == 0 && !GameObject.Find("Hand(Clone)") && PlayerPrefs.GetInt("Event1Enable" + "GEM BOX") == 1)
                    {
                        eventfab.GetComponent<EventManager>()._Event1Enable = true;
                        eventfab.GetComponent<EventManager>().E1_Icon.SetActive(true);


                        GetGift = true;
                       // var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
                        var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
                        //diffuse.transform.SetParent(d_canvas.transform, false);
                       // eventfab.transform.SetParent(d_canvas.transform, false);
                        prefab.transform.SetParent(eventfab.transform, false);
                        PlayerPrefs.SetInt("ShowHelpGembox", 1);
                        PlayerPrefs.Save();
                    }
                }

            }
        }

        // help star box score box
        if (!PlayerPrefs.HasKey("StarBoxScoreBoxHelpShowed") && CoreData.instance.openedLevel <= 10 && Configuration.instance.PlayedLevel > 1)
        {
            Configuration.instance.pause = true;
           
            //var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
            var starbox = GameObject.Find("StarKutu");
            var scoreBox = GameObject.Find("PuanKutu");

            //if (diffuse != null)
            //    diffuse.transform.SetParent(d_canvas.transform, false);

            //if (starbox != null)
            //    starbox.transform.SetParent(d_canvas.transform, false);

            //if (scoreBox != null)
            //    scoreBox.transform.SetParent(d_canvas.transform, false);

            Configuration.instance.MessageBoardInfo = "Collect Stars and Score to open the gift boxes!";
            var helpPopup = Instantiate(Resources.Load("Prefabs/HELP/HelpPopupMap")) as GameObject;
            if (helpPopup != null)
            {
                helpPopup.transform.SetParent(d_canvas.transform, false);
                helpPopup.transform.localPosition = new Vector3(0, -200, 0);
            }


            PlayerPrefs.SetInt("StarBoxScoreBoxHelpShowed", 1);
            PlayerPrefs.Save();
        }      
        

    }

    IEnumerator StartGame()
    {

        if (Configuration.instance.MenuToMap)
        {
            ResetScoreProgressBar();
            ResetScoreAmountLabel();
            ResetStarProgressBar();
            ResetStarsAmountLabel();
        }
        else
        {
            UpdateFinalScoreProgressBar();
            UpdateFinalStarProgressBar();
            UpdateFinalStarsAmountLabel();
            UpdateFinalScoreAmountLabel();
        }
        //Canvas d_canvas = GameObject.Find("CanvasDiffuse").GetComponent<Canvas>();
       
        yield return new WaitForSeconds(0.2f);
        //POPUP 1
        int ShowPopup = UnityEngine.Random.Range(0, 4);
        if (ShowPopup == 1)
        {
            
            int seed = UnityEngine.Random.Range(0, 2);

            if (seed == 0) //GEM BOX  
            {
               
                if (PlayerPrefs.GetInt("GemboxShowed") == 0)
                {
                    int GemboxAktif = PlayerPrefs.GetInt("Event1Enable" + "GEM BOX");
                    var Gembox = GameObject.Find("Gembox(Clone)");
                    if (Gembox && GemboxAktif == 1 && !GetGift)
                    {
                        Gembox.GetComponent<EventManager>().E1_Pencere_goster();
                        PlayerPrefs.GetInt("GemboxShowed", 1);
                        PlayerPrefs.Save();
                        Configuration.instance.CloseClick = false;
                    }
                    else
                    {
                        Configuration.instance.CloseClick = true;
                    }
                }
                else
                {
                    Configuration.instance.CloseClick = true;
                }
            }
            else //Promo Popup   
            {
                   

                if (Configuration.instance.Promo0 && PlayerPrefs.GetInt("Promo0") == 1 && PlayerPrefs.GetInt("PromoShowed") == 0)
                {
                    if (Configuration.instance.MenuToMap && !GetGift)
                    {
                        ShowPromo0();
                        PlayerPrefs.SetInt("PromoShowed", 1);
                        PlayerPrefs.Save();

                        Configuration.instance.CloseClick = false;

                    }
                    else
                    {
                        Configuration.instance.CloseClick = true;
                    }
                }
                else if (Configuration.instance.Promo1 && PlayerPrefs.GetInt("Promo1") == 1 && PlayerPrefs.GetInt("PromoShowed") == 0)
                {
                    if (Configuration.instance.MenuToMap && !GetGift)
                    {
                        ShowPromo1();
                        PlayerPrefs.SetInt("PromoShowed", 1);
                        PlayerPrefs.Save();
                        Configuration.instance.CloseClick = false;

                    }
                    else
                    {
                        Configuration.instance.CloseClick = true;
                    }

                }
                else if (Configuration.instance.Promo2 && PlayerPrefs.GetInt("Promo2") == 1 && PlayerPrefs.GetInt("PromoShowed") == 0)
                {
                    if (Configuration.instance.MenuToMap && !GetGift)
                    {
                        ShowPromo2();
                        PlayerPrefs.SetInt("PromoShowed", 1);
                        PlayerPrefs.Save();
                        Configuration.instance.CloseClick = false;

                    }
                    else
                    {
                        Configuration.instance.CloseClick = true;
                    }
                }
                else
                {
                    Configuration.instance.CloseClick = true;
                }

            }


        }
        else
        {

            //MORE GAMES    

            if (Configuration.instance.MoreGames)
            {
                
                    if ((PlayerPrefs.GetInt("TBMB Odul Verildi", 0) == 0 || PlayerPrefs.GetInt("FCM Odul Verildi", 0) == 0 || PlayerPrefs.GetInt("CRAZY Odul Verildi", 0) == 0) && !GetGift)
                    {
                        ShowMoreGames();
                        Configuration.instance.CloseClick = false;
                    }
                    else
                    {
                        Configuration.instance.CloseClick = true;
                    }
                              

            }
            else
            {
                Configuration.instance.CloseClick = true;
            }


        }

        //POPUP 2
        while (!Configuration.instance.CloseClick)
        {
            yield return null;
        }

        
        seed2 = UnityEngine.Random.Range(0, 5);
        
       
        if (seed2 == 0)
        {
            //5 TO 5     

            if (Configuration.instance.LevelChallengeEvent)
            {
                int Event1Enable = PlayerPrefs.GetInt("Event1Enable" + "Gift Box");
                if (Event1Enable == 1 && CoreData.instance.openedLevel >= Configuration.instance.BeginLevelLevelChallenge && !GetGift)
                {
                    GiftBoxGoster();                   
                    Configuration.instance.CloseClick = false;
                }
                else
                {
                    Configuration.instance.CloseClick = true;
                }
            }
            else
            {
                Configuration.instance.CloseClick = true;
            }
        }
        else if(seed2 == 1)
        {
            //STAR CHALENGE     
           
            if (Configuration.instance.MenuToMap)
            {
                if (Configuration.instance.StarChallengeEvent)
                {
                    if (PlayerPrefs.GetInt("StarChallengeHediyeAlindi") == 0 && CoreData.instance.openedLevel >= Configuration.instance.BeginLevelLevelChallenge && !GetGift)
                    {
                        ShowStarChallenge();
                        Configuration.instance.CloseClick = false;
                    }
                    else
                    {
                        Configuration.instance.CloseClick = true;
                    }

                }
                else
                {
                    Configuration.instance.CloseClick = true;
                }
            }
            else
            {
                Configuration.instance.CloseClick = true;
            }
        }
        else 
        {
            //Free Boxes popup           
            if (CoreData.instance.openedLevel > 7 && Configuration.instance.MenuToMap && Configuration.instance.ActiveFreeBoxesCount == 0 && Configuration.instance.ReadyFreeBoxesCount == 0)
            {
                Canvas PopupCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                var InfoPrefab = Instantiate(Resources.Load("Prefabs/Map/FreeGiftBoxesPopup")) as GameObject;
                InfoPrefab.transform.SetParent(PopupCanvas.transform, false);
                Configuration.instance.CloseClick = false;
            }
            else
            {
                Configuration.instance.CloseClick = true;
            }
        }

        //POPUP 3 info popup
        while (!Configuration.instance.CloseClick)
        {
            yield return null;
        }
        if (Configuration.instance.MessagePopup && Configuration.instance.MenuToMap)
        {
            //Info Popup               
            Configuration.OpenInfoPopup();
            Configuration.instance.CloseClick = false;
        }
        else
        {
            Configuration.instance.CloseClick = true;
        }

        //Daily Reward Time popup
        while (!Configuration.instance.CloseClick)
        {
            yield return null;
        }
        if (Configuration.instance.DailyRewardTimeLive && PlayerPrefs.GetInt("DailyRewardLifeTimeAlindi") == 0)
        {

            NewLifeRewardTime.instance.StartRewardLife(Configuration.instance.DailyRewardTime);
            PlayerPrefs.SetInt("DailyRewardLifeTimeAlindi", 1);
            PlayerPrefs.Save();
            Configuration.instance.CloseClick = false;
        }
        else
        {
            Configuration.instance.CloseClick = true;
        }


        #region Box Animations
        //Star Box progress bar
        while (!Configuration.instance.CloseClick)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        var StarKutu = GameObject.Find("StarTarget");
        if (Configuration.instance.MenuToMap)
        {
            StartCoroutine(CountUpToTarget(sandikText, starprogress, 0, CoreData.instance.GetPlayerStars(), Configuration.instance.StarBoxFullAmount, 1, StarKutu));
            // StartCoroutine(CountUpToTargetEfect(StarKutu, 4));
        }
        yield return new WaitForSeconds(0.1f);
        if (CoreData.instance.playerStars >= Configuration.instance.StarBoxFullAmount)
        {
            starGift.SetActive(true);
            starshake.SetActive(true);
            Configuration.instance.GiftTiklanmadi++;
            Debug.Log("GiftTiklanmadi " + GiftTiklanmadi);

            if ((PlayerPrefs.GetInt("starhelpshow") == 0 || Configuration.instance.GiftTiklanmadi % 9 == 0) && !GameObject.Find("Hand(Clone)"))
            {
                GetGift = true;
                //var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
                var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
                //diffuse.transform.SetParent(d_canvas.transform, false);
               // GameObject.Find("StarKutu").transform.SetParent(d_canvas.transform, false);
                prefab.transform.SetParent(starGift.transform, false);
                PlayerPrefs.SetInt("starhelpshow", 1);
                PlayerPrefs.Save();
            }
            AudioManager.instance.giftbuton();
        }
        else
        {
            starGift.SetActive(false);
            starshake.SetActive(false);

        }

        //Score Box progress bar      
        yield return new WaitForSeconds(0.8f);
        if (Configuration.instance.MenuToMap)
        {
            var ScoreKutu = GameObject.Find("ScoreTarget");

            StartCoroutine(CountUpToTarget(puanText, scoreprogress, 0, CoreData.instance.playerPuan, Configuration.instance.ScoreBoxFullAmount, 5000, ScoreKutu));
            //StartCoroutine(CountUpToTargetEfect(ScoreKutu, 4));
        }
        yield return new WaitForSeconds(0.1f);
        if (CoreData.instance.playerPuan >= Configuration.instance.ScoreBoxFullAmount)
        {
            puanGift.SetActive(true);
            puanshake.SetActive(true);
            GetGift = true;
            Configuration.instance.GiftTiklanmadi++;

            if ((PlayerPrefs.GetInt("scorehelpshow") == 0 || Configuration.instance.GiftTiklanmadi % 9 == 0) && !GameObject.Find("Hand(Clone)"))
            {
                GetGift = true;

                //var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
                var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
                //diffuse.transform.SetParent(d_canvas.transform, false);
               // GameObject.Find("PuanKutu").transform.SetParent(d_canvas.transform, false);
                prefab.transform.SetParent(puanGift.transform, false);

                PlayerPrefs.SetInt("scorehelpshow", 1);
                PlayerPrefs.Save();
            }
            AudioManager.instance.giftbuton();

        }
        else
        {
            puanGift.SetActive(false);
            puanshake.SetActive(false);

        }


        //Box Animasyonları
        if (Configuration.instance.MenuToMap)
        {
            //Star Challenge Animasyon 
            yield return new WaitForSeconds(0.8f);
            if (Configuration.instance.StarChallenge && !Configuration.instance.GiftBox && Configuration.instance.BeginLevelStarChallenge <= CoreData.instance.openedLevel)
            {
               
                var StarChallengeTarget = GameObject.Find("StarChallengeTarget");
                StartCoroutine(CountUpToTargetEfect(StarChallengeTarget, 3, 1));
            }

            //5 TO 5 Animasyon 
            yield return new WaitForSeconds(0.8f);
            if (Configuration.instance.LevelChallengeEvent && Configuration.instance.BeginLevelLevelChallenge <= CoreData.instance.openedLevel)
            {              
                if (Configuration.instance.WinLevel > 0)
                {
                    var LevelChallengeTarget = GameObject.Find("LevelChallengeTarget");
                    StartCoroutine(CountUpToTargetEfect(LevelChallengeTarget, 3, 1));
                }
            }

            yield return new WaitForSeconds(0.1f);
        }       

        Configuration.instance.MenuScene = false;

      
        #endregion


    }
    IEnumerator OpenLevelPopup()
    {
        UpdateOldProgressBar();
        UpdateOldScoreStarAmountLabel();

        bool giftboxbool = Configuration.instance.GiftBox;
        bool randomplaybool = Configuration.instance.RandomPlay;
        bool arenaplaymode = Configuration.instance.ArenaMode;

        //Canvas d_canvas = GameObject.Find("CanvasDiffuse").GetComponent<Canvas>();

        //New Episode Popup

        if(Configuration.instance.EpisodePopup)
        {
            Configuration.instance.EpisodePopup = false;
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.NewEpisodePopup())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);
            AudioManager.instance.giftbuton();
        }
        
        //Star Animasyon 
        yield return new WaitForSeconds(0.2f);
        if ((CoreData.instance.playerStars- Configuration.instance.WinStarAmount) >= Configuration.instance.StarBoxFullAmount)
        {
            starGift.SetActive(true);
            starshake.SetActive(true);
            Configuration.instance.GiftTiklanmadi++;
            Debug.Log("GiftTiklanmadi " + GiftTiklanmadi);

            if ((PlayerPrefs.GetInt("starhelpshow") == 0 || Configuration.instance.GiftTiklanmadi % 9 == 0) && !GameObject.Find("Hand(Clone)"))
            {
                GetGift = true;
                //var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
                var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
                //diffuse.transform.SetParent(d_canvas.transform, false);
                //GameObject.Find("StarKutu").transform.SetParent(d_canvas.transform, false);
                prefab.transform.SetParent(starGift.transform, false);
                PlayerPrefs.SetInt("starhelpshow", 1);
                PlayerPrefs.Save();
            }
            AudioManager.instance.giftbuton();
        }
        else
        {
            starGift.SetActive(false);
            starshake.SetActive(false);

        }
        yield return new WaitForSeconds(0.5f);
        //sameer map stars
        var StarKutu = GameObject.Find("StarTarget");
        if (Configuration.instance.WinStarAmount > 0)
        {
            for (int i = 0; i < Configuration.instance.WinStarAmount; i++)
            {
                var prefab = Instantiate(Resources.Load(Configuration.StarGold())) as GameObject;

                var PlayButton = GameObject.Find("PLAY");
                var startPosition = PlayButton.transform.position;
                var endPosition = StarKutu.transform.position;

                prefab.GetComponent<SpriteRenderer>().sortingOrder = 15;
                prefab.transform.SetParent(PlayButton.transform, true);
                prefab.transform.position = StarKutu.transform.position;

                var bending = new Vector3(2, 1, 2);
                var timeToTravel = 0.5f;
                var timeStamp = Time.time;


                while (Time.time < timeStamp + timeToTravel)
                {
                    var currentPos = Vector3.Lerp(startPosition, endPosition, (Time.time - timeStamp) / timeToTravel);

                    currentPos.x += bending.x * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);
                    currentPos.y += bending.y * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);
                    currentPos.z += bending.z * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);

                    prefab.transform.position = currentPos;
                    yield return new WaitForSeconds(.05f);

                }


                //GameObject explosion = Instantiate(Resources.Load(Configuration.RingExplosion()) as GameObject);
                //if (explosion != null)
                //{
                //    explosion.transform.position = StarKutu.transform.position;
                //}
                //AudioManager.instance.Star1Audio();           
                Destroy(prefab);
                StartCoroutine(CountUpToTarget(sandikText, starprogress, (CoreData.instance.GetPlayerStars() - Configuration.instance.WinStarAmount) + i, (CoreData.instance.GetPlayerStars() - Configuration.instance.WinStarAmount + 1) + i, Configuration.instance.StarBoxFullAmount, 1, StarKutu));
            }

            //Kutu Dolarsa
            if (CoreData.instance.playerStars >= Configuration.instance.StarBoxFullAmount)
            {
                starGift.SetActive(true);
                starshake.SetActive(true);
                Configuration.instance.GiftTiklanmadi++;
                Debug.Log("GiftTiklanmadi " + GiftTiklanmadi);

                if ((PlayerPrefs.GetInt("starhelpshow") == 0 || Configuration.instance.GiftTiklanmadi % 9 == 0) && !GameObject.Find("Hand(Clone)"))
                {
                    GetGift = true;
                   // var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
                    var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
                    //diffuse.transform.SetParent(d_canvas.transform, false);
                   // GameObject.Find("StarKutu").transform.SetParent(d_canvas.transform, false);
                    prefab.transform.SetParent(starGift.transform, false);
                    PlayerPrefs.SetInt("starhelpshow", 1);
                    PlayerPrefs.Save();
                }
                AudioManager.instance.giftbuton();
            }
        }


        //Score Animasyon 
        yield return new WaitForSeconds(0.2f);
        if ((CoreData.instance.GetPlayerPuan() - Configuration.instance.WinScoreAmount) >= Configuration.instance.ScoreBoxFullAmount)
        {
            puanGift.SetActive(true);
            puanshake.SetActive(true);
            GetGift = true;
            Configuration.instance.GiftTiklanmadi++;

            if ((PlayerPrefs.GetInt("scorehelpshow") == 0 || Configuration.instance.GiftTiklanmadi % 9 == 0) && !GameObject.Find("Hand(Clone)"))
            {
                GetGift = true;

               // var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
                var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
                //diffuse.transform.SetParent(d_canvas.transform, false);
               // GameObject.Find("PuanKutu").transform.SetParent(d_canvas.transform, false);
                prefab.transform.SetParent(puanGift.transform, false);

                PlayerPrefs.SetInt("scorehelpshow", 1);
                PlayerPrefs.Save();
            }
            AudioManager.instance.giftbuton();

        }
        else
        {
            puanGift.SetActive(false);
            puanshake.SetActive(false);

        }
        yield return new WaitForSeconds(0.1f);
        var ScoreKutu = GameObject.Find("ScoreTarget");
        if (Configuration.instance.WinScoreAmount > 0)
        {
            for (int i = 0; i < Configuration.instance.WinScoreAmount; i += 1000)
            {
                var prefabscore = Instantiate(Resources.Load(Configuration.ProgressGoldStar())) as GameObject;

                var PlayButton = GameObject.Find("PLAY");
                var startPosition = PlayButton.transform.position;
                var endPosition = ScoreKutu.transform.position;

                prefabscore.GetComponent<SpriteRenderer>().sortingOrder = 15;
                prefabscore.transform.SetParent(PlayButton.transform, true);
                prefabscore.transform.position = ScoreKutu.transform.position;

                var bending = new Vector3(-2, 1, 2);
                var timeToTravel = 0.05f;
                var timeStamp = Time.time;

                while (Time.time < timeStamp + timeToTravel)
                {
                    var currentPos = Vector3.Lerp(startPosition, endPosition, (Time.time - timeStamp) / timeToTravel);

                    currentPos.x += bending.x * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);
                    currentPos.y += bending.y * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);
                    currentPos.z += bending.z * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);

                    prefabscore.transform.position = currentPos;
                    yield return new WaitForSeconds(.05f);

                }


                //GameObject explosion = Instantiate(Resources.Load(Configuration.RingExplosion()) as GameObject);
                //if (explosion != null)
                //{
                //    explosion.transform.position = ScoreKutu.transform.position;
                //}
                //AudioManager.instance.Star2Audio();
                Destroy(prefabscore);
                StartCoroutine(CountUpToTarget(puanText, scoreprogress, (CoreData.instance.GetPlayerPuan() - Configuration.instance.WinScoreAmount) + i, (CoreData.instance.GetPlayerPuan() - Configuration.instance.WinScoreAmount) + 1000 + i, Configuration.instance.ScoreBoxFullAmount, 1000, ScoreKutu));
            }
            if (CoreData.instance.GetPlayerPuan() >= Configuration.instance.ScoreBoxFullAmount)
            {
                puanGift.SetActive(true);
                puanshake.SetActive(true);
                GetGift = true;
                Configuration.instance.GiftTiklanmadi++;

                if ((PlayerPrefs.GetInt("scorehelpshow") == 0 || Configuration.instance.GiftTiklanmadi % 9 == 0) && !GameObject.Find("Hand(Clone)"))
                {
                    GetGift = true;

                   // var diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
                    var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
                    //diffuse.transform.SetParent(d_canvas.transform, false);
                    //GameObject.Find("PuanKutu").transform.SetParent(d_canvas.transform, false);
                    prefab.transform.SetParent(puanGift.transform, false);

                    PlayerPrefs.SetInt("scorehelpshow", 1);
                    PlayerPrefs.Save();
                }
                AudioManager.instance.giftbuton();

            }
        }
        UpdateFinalScoreProgressBar();
        UpdateFinalScoreAmountLabel();
        
        //Star Challenge Animasyon 
        yield return new WaitForSeconds(0.2f);
        if (Configuration.instance.StarChallenge && !Configuration.instance.GiftBox && Configuration.instance.BeginLevelStarChallenge <=CoreData.instance.openedLevel)
        {
            var StarChallenge = GameObject.Find("StarChallenge");
            var StarChallengeTarget = GameObject.Find("StarChallengeTarget");
            if (Configuration.instance.WinStarAmount > 0)
            {
                for (int i = 1; i <= Configuration.instance.WinStarAmount; i++)
                {
                    var prefab = Instantiate(Resources.Load(Configuration.StarGold())) as GameObject;

                    var PlayButton = GameObject.Find("PLAY");
                    var startPosition = PlayButton.transform.position;
                    var endPosition = StarChallengeTarget.transform.position;
                    prefab.GetComponent<SpriteRenderer>().sortingOrder = 15;
                    prefab.transform.SetParent(PlayButton.transform, true);
                    prefab.transform.position = StarChallengeTarget.transform.position;

                    var bending = new Vector3(2, 1, 2);
                    var timeToTravel = 0.5f;
                    var timeStamp = Time.time;


                    while (Time.time < timeStamp + timeToTravel)
                    {
                        var currentPos = Vector3.Lerp(startPosition, endPosition, (Time.time - timeStamp) / timeToTravel);

                        currentPos.x += bending.x * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);
                        currentPos.y += bending.y * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);
                        currentPos.z += bending.z * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);

                        prefab.transform.position = currentPos;
                        yield return new WaitForSeconds(.05f);

                    }


                    GameObject explosion = Instantiate(Resources.Load(Configuration.RingExplosion()) as GameObject);
                    if (explosion != null)
                    {
                        explosion.transform.position = StarChallengeTarget.transform.position;
                    }
                    AudioManager.instance.Star1Audio();
                    Destroy(prefab);
                }
            }
        }

        //5 TO 5 Animasyon 
        yield return new WaitForSeconds(0.2f);
        if (Configuration.instance.LevelChallengeEvent && Configuration.instance.BeginLevelLevelChallenge <= CoreData.instance.openedLevel)
        {
            var LevelChallengeTarget = GameObject.Find("LevelChallengeTarget");
            if (Configuration.instance.GiftBoxWinNum > 0 && Configuration.instance.GiftBox)
            {
               
                    var prefab = Instantiate(Resources.Load(Configuration.StarGold())) as GameObject;

                    var PlayButton = GameObject.Find("PLAY");
                    var startPosition = PlayButton.transform.position;
                    var endPosition = LevelChallengeTarget.transform.position;
                    prefab.GetComponent<SpriteRenderer>().sortingOrder = 15;
                    prefab.transform.SetParent(PlayButton.transform, true);
                    prefab.transform.position = LevelChallengeTarget.transform.position;

                    var bending = new Vector3(2, 1, 2);
                    var timeToTravel = 0.5f;
                    var timeStamp = Time.time;


                    while (Time.time < timeStamp + timeToTravel)
                    {
                        var currentPos = Vector3.Lerp(startPosition, endPosition, (Time.time - timeStamp) / timeToTravel);

                        currentPos.x += bending.x * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);
                        currentPos.y += bending.y * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);
                        currentPos.z += bending.z * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);

                        prefab.transform.position = currentPos;
                        yield return new WaitForSeconds(.05f);

                    }

                    GameObject explosion = Instantiate(Resources.Load(Configuration.RingExplosion()) as GameObject);
                    if (explosion != null)
                    {
                        explosion.transform.position = LevelChallengeTarget.transform.position;
                    }
                    AudioManager.instance.Star1Audio();
                    Destroy(prefab);
            }
        }

        //Mission mini popup
        yield return new WaitForSeconds(0.5f);
        if (Configuration.instance.MissionChallengeEvent && !MissionChallenge.MissionComplete() && PlayerPrefs.GetInt("MissionChallengeEnable") == 1 && PlayerPrefs.GetInt("MissionChallengeHediyeAlindi") == 0)
        {
            if(MissionChallenge.MissionComplete())
            {

                MissionChallenge.ShowMissionChallenge();
                Configuration.instance.CloseClick = false;
            }
            else
            {
                MissionChallenge.ShowMiniMissionChallenge();
                Configuration.instance.CloseClick = false;
            }               
        }
        else
        {
            Configuration.instance.CloseClick = true;
        }
        
        while (!Configuration.instance.CloseClick)
        {
            yield return null;
        }

        if (CoreData.instance.openedLevel == 7 && Configuration.instance.ActiveFreeBoxesCount == 0)
        {
            Canvas PopupCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var InfoPrefab = Instantiate(Resources.Load("Prefabs/Map/FreeGiftBoxesPopup")) as GameObject;
            InfoPrefab.transform.SetParent(PopupCanvas.transform, false);
            Configuration.instance.CloseClick = false;
        }
        else
        {
            Configuration.instance.CloseClick = true;
        }
        while (!Configuration.instance.CloseClick)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        //Open Level POPUP
        StageLoader.instance.Stage = Configuration.instance.autoPopup;
        StageLoader.instance.LoadLevel(StageLoader.instance.Stage);
        Configuration.instance.autoPopup = 0;
        if(!Configuration.instance.pause)
        {
            if (Configuration.instance.EpisodePlay && !Configuration.instance.GiftBox && !Configuration.instance.pause)
            {
                if (!GetGift)
                {
                    levelPopup.OpenPopup();
                }
            }
            else
            {
                levelPopup.OpenPopup();
            }
        }
       
    }
    IEnumerator CountUpToTarget(Text Label, Image ProgressBar, int CurrentValue, int TargetValue, int RequestValue, int speed, GameObject TargetNull)
    {
        float TargetValueFloat = (float)TargetValue;
        float CurrentValueFloat = (float)CurrentValue;
        float RequestValueFloat = (float)RequestValue;
        //if (TargetValue == 0)
        //{
        //    if (CoreData.instance.playerPuan == 0)
        //    {
        //        puanText.text = "0" + "/" + RequestValue.ToString();
        //        scoreprogress.fillAmount = 0;
        //    }
        //    if (CoreData.instance.GetPlayerStars() == 0)
        //    {
        //        sandikText.text = "0" + "/" + RequestValue.ToString();
        //        starprogress.fillAmount = 0;
        //    }

        //}

        while (CurrentValue < TargetValue)
        {
            CurrentValue += speed;//= TargetValueFloat / (smooth / Time.deltaTime); // or whatever to get the speed you like
            CurrentValue = Mathf.Clamp(CurrentValue, 0, TargetValue);
            Label.text = CurrentValue.ToString() + "/" + RequestValue.ToString();
            ProgressBar.fillAmount = (float)CurrentValue / (float)RequestValue;
            AudioManager.instance.CoinPayAudio();
            // AudioManager.instance.Star1Audio();

            GameObject explosion = Instantiate(Resources.Load(Configuration.RingExplosion()) as GameObject);
            if (explosion != null)
            {
                explosion.transform.position = TargetNull.transform.position;
            }
            yield return new WaitForSeconds(0.1f);

            // AudioManager.instance.DropAudio();
        }
        //while (CurrentValueFloat < TargetValueFloat)
        //{
        //    CurrentValueFloat += TargetValueFloat / (smooth / Time.deltaTime); // or whatever to get the speed you like
        //    CurrentValueFloat = Mathf.Clamp(CurrentValueFloat, 0, TargetValueFloat);
        //    Label.text = (int)CurrentValueFloat + "/" + RequestValueFloat.ToString();
        //    ProgressBar.fillAmount = CurrentValueFloat / RequestValueFloat;           
        //    yield return null;           
        //    // AudioManager.instance.PopupBosTikirtiAudio();
        //}
    }

    IEnumerator CountUpToTargetEfect(GameObject TargetNull, int soundCount, int smooth)
    {
        float count = 0;
        float soundCountFloat = (float)soundCount;

        while (count < soundCount)
        {
            count += smooth; //= Time.deltaTime/ count;
            GameObject explosion = Instantiate(Resources.Load(Configuration.RingExplosion()) as GameObject);
            if (explosion != null)
            {
                explosion.transform.position = TargetNull.transform.position;
            }
            AudioManager.instance.Star1Audio();
            yield return new WaitForSeconds(.1f);
        }
    }
    void Update()
    {
            #region Scroll

            // var position = canvasHeight / 2 - TargetPosition().y;

            //   var y = scrollContent.GetComponent<RectTransform>().localPosition.y;


            #endregion

            #region CloseHelp

            if (Input.GetMouseButton(0))
        {
            // close Level popup
            if (GameObject.Find("Hand(Clone)"))
            {
                Destroy(GameObject.Find("Hand(Clone)"));               
            }
            if (GameObject.Find("Diffuse(Clone)"))
            {               
                Destroy(GameObject.Find("Diffuse(Clone)"));
            }
        }

        #endregion
        #region Button

        if (Input.GetKeyDown(KeyCode.Escape))
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

        #endregion     
    }
    //PAGES Prefab
 
    public void page1()
    {
        
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.page1())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);
           
       

       
    }
    public void page2()
    {

            return;
            //Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            //var nesne = Instantiate(Resources.Load(Configuration.page2())) as GameObject;
            //nesne.transform.SetParent(m_canvas.transform, false);
          
    }
    public void page3()
    {
       
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.page3())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);
            
        
    }
    public void page4()
    {
       
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.page4())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);
            
       
    }
    public void page5()
    {
        
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.page5())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);
            
        
    }
    public void page6()
    {
      
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.page6())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);
            
       
    }
    public void page7()
    {
       
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.page7())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);
            

       
    }
       
   
    public void gemboxenable()
    {

        int usedgame = PlayerPrefs.GetInt("usedgame", 0);
        int totalusedgame = PlayerPrefs.GetInt("totalusedgame", 0);
        int reenable = Configuration.instance.reenable;
        int firststart = Configuration.instance.BeginLevelGembox;
        if (totalusedgame >= firststart)
        {
            if (Configuration.instance.GemboxDeactive)
            {
                if (usedgame >= reenable)
                {
                    GemBox.instance.sifirla();
                    GemBox.instance.enablegembox();
                    GemBox.instance.TurnWheelButtonClick();
                    Configuration.instance.GemboxActive = true;
                    Configuration.instance.GemboxDeactive = false;


                }
            }
        }
    }
    public void ShowMoreGames()
    {
        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        int seed = 1;// UnityEngine.Random.Range(1, 4);
        
            if (seed == 1)
            {
                //var nesne = Instantiate(Resources.Load(Configuration.MoregamesHOB())) as GameObject;
                //nesne.transform.SetParent(m_canvas.transform, false);
            }
            else if (seed == 2)
            {
                var nesne = Instantiate(Resources.Load(Configuration.MoregamesCB())) as GameObject;
                nesne.transform.SetParent(m_canvas.transform, false);
            }
            else if (seed == 3)
            {
                var nesne = Instantiate(Resources.Load(Configuration.MoregamesMB())) as GameObject;
                nesne.transform.SetParent(m_canvas.transform, false);
            }
             
    }
    public void ShowStarChallenge()
    {

        if (PlayerPrefs.GetInt("StarChallengeEnable") == 0 && PlayerPrefs.GetInt("StarChallengeHediyeAlindi") == 0)
        {
            PlayerPrefs.SetString("E" + "StarChallenge", DateTime.Now.Ticks.ToString());
            PlayerPrefs.SetInt("StarChallengeNum", 0);
            PlayerPrefs.Save();
        }
        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var nesne = Instantiate(Resources.Load(Configuration.ShowStarChallenge())) as GameObject;
        nesne.transform.SetParent(m_canvas.transform, false);


    }
    public void ShowMissionChallenge()
    {
        MissionChallenge.ShowMissionChallenge();
    }
    public void NewGamesPopup()
    {
        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var nesne = Instantiate(Resources.Load(Configuration.Newgames())) as GameObject;
        nesne.transform.SetParent(m_canvas.transform, false);
    }
    public void ShowPromo0()
    {
        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var nesne = Instantiate(Resources.Load(Configuration.promoWindowPromo0())) as GameObject;
        nesne.transform.SetParent(m_canvas.transform, false);
    }
    public void ShowPromo1()
    {
        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var nesne = Instantiate(Resources.Load(Configuration.promoWindowPromo1())) as GameObject;
        nesne.transform.SetParent(m_canvas.transform, false);
    }
    public void ShowPromo2()
    {
        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var nesne = Instantiate(Resources.Load(Configuration.promoWindowPromo2())) as GameObject;
        nesne.transform.SetParent(m_canvas.transform, false);
    }
    //public void pozisyon()
    //{
    //    var currentPosition = Vector3.zero;

    //    if (StageLoader.instance.Stage == 0)
    //    {
    //        currentPosition = TargetPosition();
    //    }
    //    else
    //    {
    //        currentPosition = levels.transform.GetChild(StageLoader.instance.Stage).GetComponent<RectTransform>().localPosition;
    //    }

    //    scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, canvasHeight / 2 - currentPosition.y, 0);
    //}
    public void GiftBoxGoster()
    {
        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var nesne = Instantiate(Resources.Load(Configuration.GiftBoxPopup())) as GameObject;
        nesne.transform.SetParent(m_canvas.transform, false);
    }
    public void ArenaPopupGoster()
    {
        int staramount = PlayerPrefs.GetInt("staramount");
        bool ArenaPopupGosterildi = Configuration.instance.ArenaPopupGosterildi;
        if (staramount == 0 && !ArenaPopupGosterildi)
        {
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.ArenaPopup())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);
            Configuration.instance.ArenaPopupGosterildi = true;
        }

    }
  
    public void stargifts()
    {
        if (!GameObject.Find("Stargift(Clone)"))
        {
            starGiftPopup.OpenPopup();
        }
    }
    //Puan Gift Button
    public void Puangifts()
    {
        if (!GameObject.Find("Puangift(Clone)"))
        {
            puanGiftPopup.OpenPopup();
        }

    }
    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }
    public void CoinButtonClick()
    {
        if (!GameObject.Find("ShopPopupMap(Clone)"))
        {

            page2();
            //if (!m_canvas) m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            //var shop = Instantiate(Resources.Load("Prefabs/Play/Popup/ShopPopupPlay")) as GameObject;
            //shop.transform.SetParent(m_canvas.transform, false);
            ////shopPopup.OpenPopup();
        }
    }
    public void Bildirimetikla()
    {
        if (GameObject.Find("LevelPlay(Clone)"))
        {
            GameObject.Find("LevelPlay(Clone)").GetComponent<Popup>().Close();

        }
        AudioManager.instance.GingerbreadExplodeAudio();
    }
    public void LifeButtonClick()
    {
        if (!GameObject.Find("LifePopup(Clone)"))
        {
            lifePopup.OpenPopup();
        }
    }
    public void hepsiniac()
    {
        CoreData.instance.SaveOpendedLevel(399);
    }
    public void hepsinsil()
    {
        PlayerPrefs.DeleteAll();
    }
    public void prefabgetir()
    {
        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var nesne = Instantiate(Resources.Load(Configuration.LoadingImage())) as GameObject;
        nesne.transform.SetParent(m_canvas.transform, false);
    }
    //CRAZY VE PARTY TIME FARKLI
    public void FoundTargetButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();
        var person = GameObject.Find("TargetPointer") as GameObject;
        int openedLevel = CoreData.instance.GetOpendedLevel();
        if (openedLevel >= 31)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, canvasHeight / 2 - TargetPosition().y + 200, 0);
        }
        if (openedLevel < 6)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, 2250, 0);
        }
        if (openedLevel >= 6 && openedLevel <= 14)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, 2200 - TargetPosition().y, 0);
        }
        if (openedLevel >= 15 && openedLevel <= 22)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, 1300 - TargetPosition().y, 0);
        }
        if (openedLevel >= 23 && openedLevel <= 30)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, 300 - TargetPosition().y, 0);
        }

        Settings.instance.CloseMenu();
       
    }
    public void FoundTargetButtonClick2()
    {
        var person = GameObject.Find("TargetPointer") as GameObject;
        int openedLevel = CoreData.instance.GetOpendedLevel();
        if (openedLevel >= 31)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, canvasHeight / 2 - TargetPosition().y + 200, 0);
        }
        if (openedLevel < 6)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, 2250, 0);
        }
        if (openedLevel >= 6 && openedLevel <= 14)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, 2200 - TargetPosition().y, 0);
        }
        if (openedLevel >= 15 && openedLevel <= 22)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, 1300 - TargetPosition().y, 0);
        }
        if (openedLevel >= 23 && openedLevel <= 30)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, 300 - TargetPosition().y, 0);
        }
    }
    IEnumerator ScrollContent(Vector3 target)
    {
        if (target.y > 0) target.y = 0;

        var from = scrollContent.GetComponent<RectTransform>().localPosition;
        float step = Time.fixedDeltaTime;
        float t = 0;

        while (t <= 1.0f)
        {
            t += step;
            scrollContent.GetComponent<RectTransform>().localPosition = Vector3.Lerp(from, target, t);
            yield return new WaitForFixedUpdate();
        }

        scrollContent.GetComponent<RectTransform>().localPosition = target;
    }
    Vector3 TargetPosition()
    {
        var currentPosition = Vector3.zero;
        var person = GameObject.Find("TargetPointer") as GameObject;

        currentPosition = person.GetComponent<RectTransform>().localPosition;

        return currentPosition;
    }
    public void UpdateCoinAmountLabel()
    {
        coinText.text = CoreData.instance.GetPlayerCoin().ToString();
    }

    //Amount Label
    public void ResetStarsAmountLabel()
    {
        int StarBoxFull = Configuration.instance.StarBoxFullAmount;
        sandikText.text = "0" + "/" + StarBoxFull.ToString();
    }
    public void ResetScoreAmountLabel()
    {
        int ScoreBoxFull = Configuration.instance.ScoreBoxFullAmount;
        puanText.text = "0" + "/" + ScoreBoxFull.ToString();
    }
    public void UpdateOldScoreStarAmountLabel()
    {
        // Toplam Puan odul
        int ScoreBoxFull = Configuration.instance.ScoreBoxFullAmount;
        puanText.text = (CoreData.instance.playerPuan - Configuration.instance.WinScoreAmount).ToString() + "/" + ScoreBoxFull.ToString();
        // Star Gift    
        int StarBoxFull = Configuration.instance.StarBoxFullAmount;
        sandikText.text = (CoreData.instance.GetPlayerStars() - Configuration.instance.WinStarAmount).ToString() + "/" + StarBoxFull.ToString();


    }
    public void UpdateFinalStarsAmountLabel()
    {
        int StarBoxFull = Configuration.instance.StarBoxFullAmount;
        sandikText.text = CoreData.instance.GetPlayerStars().ToString() + "/" + StarBoxFull.ToString();
    }
    public void UpdateFinalScoreAmountLabel()
    {
        int ScoreBoxFull = Configuration.instance.ScoreBoxFullAmount;
        puanText.text = CoreData.instance.playerPuan.ToString() + "/" + ScoreBoxFull.ToString();
    }

    //update star progress bar
    public void ResetScoreProgressBar()
    {
        scoreprogress.fillAmount = 0;
    }
    public void ResetStarProgressBar()
    {
        starprogress.fillAmount = 0;
    }
    public void UpdateOldProgressBar()
    {
        int ScoreBoxFull = Configuration.instance.ScoreBoxFullAmount;
        int StarBoxFull = Configuration.instance.StarBoxFullAmount;
        float StarBox = (float)StarBoxFull;
        float ScoreBox = (float)ScoreBoxFull;
        starprogress.fillAmount = (CoreData.instance.playerStars - Configuration.instance.WinStarAmount) / StarBox;
        scoreprogress.fillAmount = (CoreData.instance.playerPuan - Configuration.instance.WinScoreAmount) / ScoreBox;
    }
    public void UpdateFinalScoreProgressBar()
    {
        int ScoreBoxFull = Configuration.instance.ScoreBoxFullAmount;

        float ScoreBox = (float)ScoreBoxFull;

        scoreprogress.fillAmount = CoreData.instance.playerPuan / ScoreBox;
    }
    public void UpdateFinalStarProgressBar()
    {

        int StarBoxFull = Configuration.instance.StarBoxFullAmount;
        float StarBox = (float)StarBoxFull;

        starprogress.fillAmount = CoreData.instance.playerStars / StarBox;

    }

    // show leaderboard
    public void Showleaderboard()
    {
//        if (GameServices.IsInitialized())
//        {
//            GameServices.ShowLeaderboardUI();
//        }
//        else
//        {
//#if UNITY_ANDROID
//            GameServices.Init();    // start a new initialization process
//#elif UNITY_IOS
//    Debug.Log("Cannot show leaderboard UI: The user is not logged in to Game Center.");
//#endif
//        }
    }
    // show achievement
    public void Showachievement()
    {
//        if (GameServices.IsInitialized())
//        {
//            GameServices.ShowAchievementsUI();
//        }
//        else
//        {
//#if UNITY_ANDROID
//            GameServices.Init();    // start a new initialization process
//#elif UNITY_IOS
//    Debug.Log("Cannot show achievements UI: The user is not logged in to Game Center.");
//#endif
//        }
    }
    public void UpdateStarsButon()
    {
        starGift.SetActive(false);
        starshake.SetActive(false);
        AudioManager.instance.giftsound();
    }
    public void UpdatePuanButon()
    {
        puanGift.SetActive(false);
        puanshake.SetActive(false);
        AudioManager.instance.giftsound();
    }

    public void Removeads()
    {
        //sameer
        //Advertising.RemoveAds();
        PlayerPrefs.SetInt("removeads", 1);
        PlayerPrefs.Save();
    }
    public void ResetRemoveads()
    {
        //sameer
        //Advertising.ResetRemoveAds();
    }
    public void hepsinisil()
    {
        PlayerPrefs.DeleteAll();
    }
    public void unlimitedLife()
    {
        NewLife.instance.lives = NewLife.instance.maxLives;
        NewLife.instance.unlimitedLifePurchased = true;
        PlayerPrefs.SetInt("unlimitedlife", 1);
        PlayerPrefs.Save();
    }
    public void resetunlimitedLife()
    {
        PlayerPrefs.SetInt("unlimitedlife", 0);
        PlayerPrefs.Save();
    }
    public void odullureklamgoster()
    {
        //sameerreward
        ////admanager.instance.ShowRewardedVideAdGeneric(0);
        //AdsManager.instance.ShowReward();
    }  
    //void OnEnable()
    //{       
    //    GameServices.UserLoginSucceeded += OnUserLoginSucceeded;
    //    GameServices.UserLoginFailed += OnUserLoginFailed;       
    //}    
    //void OnDisable()
    //{       
    //    GameServices.UserLoginSucceeded -= OnUserLoginSucceeded;
    //    GameServices.UserLoginFailed -= OnUserLoginFailed;      
    //}  
    void OnUserLoginSucceeded()
    {
        Debug.Log("User logged in successfully.");
    }
    void OnUserLoginFailed()
    {
        Debug.Log("User login failed.");
    }
    public static void FirstSetup()
    {
        //   CoreData.instance.SaveOpendedLevel(0);
        PlayerPrefs.DeleteAll();
        CoreData.instance.SavePlayerCoin(25);
        CoreData.instance.SavePlayerStars(0);
        CoreData.instance.SavePlayerPuan(25);
        CoreData.instance.SaveColumnBreaker(CoreData.instance.columnBreaker = 2);
        CoreData.instance.SaveBeginRainbow(CoreData.instance.beginRainbow = 2);
        CoreData.instance.SaveRainbowBreaker(CoreData.instance.rainbowBreaker = 2);
        CoreData.instance.SaveOvenBreaker(CoreData.instance.ovenBreaker = 2);
        CoreData.instance.SaveRowBreaker(CoreData.instance.rowBreaker = 2);
        CoreData.instance.SaveSingleBreaker(CoreData.instance.singleBreaker = 2);
        CoreData.instance.SaveBeginFiveMoves(CoreData.instance.beginFiveMoves = 2);
        CoreData.instance.SaveBeginBombBreaker(CoreData.instance.beginBombBreaker = 2);
        NewLife.instance.lives = 5;
    }
    public void showstarinfopopup()
    {
        
        StartCoroutine(starinfopopup());

    }
    public void showscoreinfopopup()
    {
       
        StartCoroutine(scoreinfopopup());

    }
    IEnumerator starinfopopup()
    {
        int StarBoxFull = Configuration.instance.StarBoxFullAmount;
        starinfo.gameObject.SetActive(true);
        Language.StartGlobalTranslateWord(starinfotext, "Collect stars to get the Star Toy Box!", 0, "");

        // starinfotext.text = "Collect " + StarBoxFull + " stars to get the Star Toy Box!";
        yield return new WaitForSeconds(3.0f);
        starinfo.gameObject.SetActive(false);
    }
    IEnumerator scoreinfopopup()
    {
        int ScoreBoxFull = Configuration.instance.ScoreBoxFullAmount;
        scoreinfo.gameObject.SetActive(true);
        Language.StartGlobalTranslateWord(scoreinfotext, "Collect score to get the Score Toy Box!", 0, "");

        //scoreinfotext.text = "Collect " + ScoreBoxFull + " score to get the Score Toy Box!";
        yield return new WaitForSeconds(3.0f);
        scoreinfo.gameObject.SetActive(false);

    }
    // Event handlers
    void OnApplicationQuit()
    {
        //print("Configuration: On application quit / Exit date time: " + DateTime.Now.ToString() + " / Life: " + life + " / Timer: " + timer);

        AnalyticsEvent.GameOver(CoreData.instance.openedLevel, "Map Scene");       
    }



}


