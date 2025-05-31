using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Analytics;
//using AppsFlyerSDK;

public class UIWinPopup : MonoBehaviour
{
    
    //public static UIWinPopup instance = null;
     public Text Label = null;
    public Text scoreText = null;
    public Text TimeBonusText = null;
    public Text TotalScoreText = null;
     public Text timetextsc,blockssc,movesrem,baseTotalsc,timeTotalsc,movesTotalsc;
    public Text StarAmountText = null;
    public Text TotalStarAmountText = null;
    public Image star1 = null;
    public Image star2 = null;
    public Image star3 = null;
    public GameObject loadingScreen;
  
    //public GameObject Rateus = null;
    //public GameObject ToyBox = null;
    //public GameObject DoubleScore = null;
    //public GameObject DoubleStar = null;
    //public GameObject DoubleStarSimge = null;
    //public GameObject gemboxdisable = null;

    public GameObject ButtonPlay = null;
    
    public GameObject ArenaStar = null;
    public GameObject ArenaRutbe = null;
 


    public Text ArenaRutbeText = null;
    public bool x2Star = false;
    public bool x2Score = false;   




void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
}


    void Start()
    {
        
        //if (instance == null)
        //{
        //    instance = this;
        //}
        var board = GameObject.Find("Board").GetComponent<itemGrid>();


       

        //Configuration.instance.WinLevel++;
        //AI.instance.LoseLevel = 0;

        // hide the button
        BackgroundMusic.instance.WinMusic();
        int openedLevel = CoreData.instance.GetOpendedLevel();
 

        var star = board.star;
        int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
        int arenanumber = PlayerPrefs.GetInt("arenanumber");

      
        if(Configuration.instance.EpisodePlay)
        {
            if (openedLevel > Configuration.instance.maxLevel)
            {
                int currentStage = StageLoader.instance.Stage + 1448;

                //Unity Analytic Level Complete Rapor        
                AnalyticsEvent.LevelComplete(currentStage.ToString());


               
 
            }           
            else
            {
                //Unity Analytic Level Complete Rapor        
                AnalyticsEvent.LevelComplete(StageLoader.instance.Stage.ToString());

              
                
            }
        }     

       


        if (Configuration.instance.RandomPlay)
        {
            Language.StartGlobalTranslateWord(Label, "RANDOM PLAY", 0, "");
            //StarLanguage(7, 0, "");
            //Label.text = "RANDOM PLAY";
        }
        else if (Configuration.instance.GiftBox)
        {
            if(Configuration.instance.GiftBoxRequiredWinLevel > Configuration.instance.GiftBoxWinNum)
            {
                Language.StartGlobalTranslateWord(Label, "Remaining Level: ", Configuration.instance.GiftBoxRequiredWinLevel - Configuration.instance.GiftBoxWinNum, "");
                AnalyticsEvent.LevelSkip("Remaining Level ", Configuration.instance.GiftBoxRequiredWinLevel - Configuration.instance.GiftBoxWinNum);
            }         
        }
        else if (Configuration.instance.ArenaMode)
        {
            ArenaStar.gameObject.SetActive(true);
            //ArenaRutbe.gameObject.SetActive(true);
            Label.text = "";

            int staramount = PlayerPrefs.GetInt("staramount");
            int winamount = PlayerPrefs.GetInt("winamount");
            StarAmountText.text = "+" + winamount;
            TotalStarAmountText.text = "" + staramount;
        }
        else if (StageLoader.instance.Stage >= Configuration.instance.EpisodemaxLevel)
        {
            //ArenaRutbe.gameObject.SetActive(true);
            Language.StartGlobalTranslateWord(Label, "RANDOM PLAY", 0, "");
        }
        else
        {
           
        }


        star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);

        // start coroutine to show stars
        StartCoroutine(ShowStars());



        // ButtonPlay.gameObject.SetActive(false);      
    
        int remaingTime = (int)Timer.instance.timeRemaining * 25;
        int secondsrem = (int)Timer.instance.timeRemaining;
       
        int _totalScore;

        //Language.StartGlobalTranslateWord(scoreText, "Score: ", 0, board.score.ToString());
        blockssc.text = board.score + "";
        baseTotalsc.text = board.score + "";
        if (PlayerPrefs.GetInt("LevelWin") == 1)
        {
            timetextsc.text = secondsrem + "";
            timeTotalsc.text = remaingTime.ToString();
            movesrem.text = itemGrid.instance.moveLeft.ToString();
            int movesmult = itemGrid.instance.moveLeft * 10;
            movesTotalsc.text = movesmult.ToString();
            _totalScore = board.score + remaingTime+ movesmult;
            //TimeBonusText.text = secondsrem.ToString()+" X 25";
            //FireBaseAnalytics.instance.Log_Event("Game_Ended_Time_Remaining_" + (int)Timer.instance.timeRemaining);
            //FireBaseAnalytics.instance.Log_Event("Moves_Left_" + itemGrid.instance.moveLeft);
        }
        else
        {
            _totalScore = board.score;
            //TimeBonusText.text = "Time Bonus: 0";
            timetextsc.text = "0";
            movesrem.text = "0";
            timeTotalsc.text = "0";
            movesTotalsc.text = "0";
            //FireBaseAnalytics.instance.Log_Event("Game_Ended_Through_Settings");
        }
        TotalScoreText.text = "Total Score: " +_totalScore.ToString();
        skillzscore = _totalScore;
        print("BOARDSCORE"+board.score);
      
        SkillzCrossPlatform.SubmitScore(_totalScore, OnSuccess, OnFailure);
 
        ButtonPlay.SetActive(true);
//#endif
        #region GiftBox, Challenge, LevelBox, RateUS
        // Gift BOX 

        //        if (Configuration.instance.GiftBoxWinNum >= Configuration.instance.GiftBoxRequiredWinLevel)
        //        {
        //            //GiftBoxWin.SetActive(true);
        //            Configuration.instance.OpenGiftBoxPopup("Gift Box", REWARD_TYPE.LevelChallengeGiftBox);
        //            AnalyticsEvent.Custom("5to5WinPopup");
        //            NewLifeRewardTime.instance.StartRewardLife(Configuration.instance.WinChallengeRewardTime * 2);
        //            Configuration.instance.GiftBoxWinNum = 0;
        //            Language.StartGlobalTranslateWord(Label, "congratulations!");

        //        }

        //        // Star Challenge BOX   
        //        if (Configuration.instance.StarChallenge)
        //        {
        //            if (StarChallengeNum >= Configuration.instance.StarChallengeStarAmount)
        //            {
        //                //StarChallengeWin.SetActive(true);
        //                Configuration.instance.OpenGiftBoxPopup("Star Challenge GiftBox", REWARD_TYPE.StarChallengeGiftBox);
        //                Notifications.CancelPendingLocalNotification(EM_NotificationsConstants.UserCategory_notification_category_StarCall);
        //                AnalyticsEvent.Custom("StarChallengeWinPopup");
        //                NewLifeRewardTime.instance.StartRewardLife(Configuration.instance.WinChallengeRewardTime);

        //            }
        //        }

        //        // LEVEL BOX
        //        if (Configuration.instance.EpisodePlay && Configuration.instance.LevelBoxEvent)
        //        {
        //            openedLevel = CoreData.instance.GetOpendedLevel();

        //            if (PlayerPrefs.GetInt("levelgift alindi" + openedLevel, 0) == 0)
        //            {
        //                int ShowLevelBoxEvery = Configuration.instance.ShowLevelBoxEvery;
        //                int levelboxgoster = (openedLevel - 1) % ShowLevelBoxEvery;
        //                if (levelboxgoster == 0)
        //                {
        //                    //levelbox.SetActive(true);
        //                    Configuration.instance.OpenGiftBoxPopup("Level Box", REWARD_TYPE.LevelGiftBox);
        //                }
        //            }
        //        }


        //        // RATE US
        //        if (PlayerPrefs.GetInt(Configuration.instance.RateKey, 0) == 0)
        //        {
        //            openedLevel = CoreData.instance.GetOpendedLevel();
        //            int ShowRateUsEvery = Configuration.instance.ShowRateUsEvery;
        //            int rateusgoster = openedLevel % ShowRateUsEvery;

        //            if (StageLoader.instance.Stage == Configuration.instance.BeginLevelRateUs)
        //            {

        //#if UNITY_IOS
        //                // Check if it's eligible to show the rating dialog and then show it
        //                if (StoreReview.CanRequestRating())
        //                {
        //                    StoreReview.RequestRating();
        //                    Configuration.instance.RateShowed = true;
        //                }
        //#elif UNITY_ANDROID
        //                ShowRate();               
        //#endif



        //            }
        //            else if (StageLoader.instance.Stage > Configuration.instance.BeginLevelRateUs && rateusgoster == 0 && !Configuration.instance.RateShowed)
        //            {
        //#if UNITY_IOS       
        //                // Check if it's eligible to show the rating dialog and then show it
        //                if (StoreReview.CanRequestRating())
        //                {
        //                    StoreReview.RequestRating();
        //                    Configuration.instance.RateShowed = true;
        //                }
        //#elif UNITY_ANDROID
        //                ShowRate();                
        //#endif

        //            }
        //        }
        #endregion

        //Episode Box
        if (Configuration.instance.EpisodePlay)
        {
            int MaxEpisode = Configuration.instance.episode;
            int currentepisode = Configuration.instance.CurrentEpisode;
            if (PlayerPrefs.GetInt("episodes" + currentepisode) == 0 && openedLevel > Configuration.instance.BeginLevelDailyBonus || Configuration.instance.EpisodesRange[1]+1 == openedLevel)
            {

                PlayerPrefs.SetInt("episodes" + currentepisode, 1);
                PlayerPrefs.Save();

                Configuration.instance.EpisodePopup = true;
                if (Configuration.instance.EpisodeBoxEvent)
                {
                   Configuration.instance.OpenGiftBoxPopup("Episode Box", REWARD_TYPE.EpisodeGiftBox);
                }
            }
        }
#region Achievemend and Leaderboard
        // Achievement and leaderboard
        //int toplamscore = CoreData.instance.GetToplamScore();
        //int toplamstar = 0;

        //for (int i = 1; i <= CoreData.instance.GetOpendedLevel(); i++)
        //{
        //    toplamstar += CoreData.instance.GetLevelStar(i);
        //}
        //if (!Configuration.instance.Amazon && Configuration.instance.GameServices)
        //{

        //    GameServices.ReportScore(toplamscore, EM_GameServicesConstants.Leaderboard_score_leaderboard);
        //    GameServices.ReportScore(toplamstar, EM_GameServicesConstants.Leaderboard_star_leaderboard);
        //    if (toplamstar > 74)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_collect_star_75);
        //    }
        //    if (toplamstar > 299)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_collect_star_300);
        //    }
        //    if (toplamstar > 599)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_collect_star_600);
        //    }
        //    if (toplamstar > 899)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_collect_star_900);
        //    }
        //    if (toplamscore > 199999)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_collect_score_200000);
        //    }
        //    if (toplamscore > 399999)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_collect_score_400000);
        //    }

        //    if (toplamscore > 599999)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_collect_score_600000);
        //    }



        //}

        //if (Configuration.instance.WinLevel  == 3 && Configuration.instance.ActiveFreeBoxesCount == 0)
        //{
        //    Canvas PopupCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //    var InfoPrefab = Instantiate(Resources.Load("Prefabs/Map/FreeGiftBoxesPopup")) as GameObject;
        //    InfoPrefab.transform.SetParent(PopupCanvas.transform, false);
        //}
        //else
        //{
        //    Configuration.instance.CloseClick = true;
        //}

        ////Achievement
        //Configuration.SaveAchievement("ach_winLevel", 1);
#endregion
    }
    int skillzscore;
    private void OnSuccess()
    {
        ButtonPlay.SetActive(true);
        Debug.Log("my Success Call");
        //FireBaseAnalytics.instance.Log_Event("Skill_Submit_Score_Success");
    }

    private void OnFailure(string temp)
    {
        //thiscomment
        SkillzCrossPlatform.DisplayTournamentResultsWithScore(skillzscore);
        Debug.Log("my Failure Call");
    }
    //LEVEL EVENTS
    //public void appsFlyerLevelComplete(string levelNum)
    //{
    //    Dictionary<string, string> LevelComplete = new
    //          Dictionary<string, string>();
    //    LevelComplete.Add("Level Complete", levelNum);
    //    AppsFlyer.sendEvent("finished level " + levelNum, LevelComplete);
    //    AppsFlyer.sendEvent("LEVEL FUNNEL", LevelComplete);
    //    appsFlyerLevelAchieveLevel(levelNum);
    //}       



    //public void LogLevelCompleteEvent(string levelNum)
    //{
    //    var parameters = new Dictionary<string, object>();
    //    parameters["Level Complete"] = levelNum;
    //    FB.LogAppEvent(
    //        "finished level " + levelNum,
    //        1,
    //        parameters
    //    );
    //    FB.LogAppEvent(
    //        "LEVEL FUNNEL",
    //        1,
    //        parameters
    //    );
    //    LogAchieveLevelEvent(levelNum);
    //}



    //public void appsFlyerLevelAchieveLevel(string levelNum)
    //{
    //    Dictionary<string, string> LevelAchieve = new
    //          Dictionary<string, string>();
    //    LevelAchieve.Add(AFInAppEvents.LEVEL_ACHIEVED, levelNum);
    //    AppsFlyer.sendEvent(AFInAppEvents.LEVEL_ACHIEVED, LevelAchieve);
    //}

    //public void LogAchieveLevelEvent(string levelNum)
    //{
    //    var LevelAchieve = new Dictionary<string, object>();
    //    LevelAchieve[AppEventParameterName.Level] = levelNum;
    //    FB.LogAppEvent(
    //        AppEventName.AchievedLevel,
    //        null,
    //        LevelAchieve
    //    );
    //}


    public void gemboxenable()
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        int usedgame = PlayerPrefs.GetInt("usedgame", 0);
        int reenable = Configuration.instance.reenable;
        int totalusedgame = PlayerPrefs.GetInt("totalusedgame", 0);
        int firststart = Configuration.instance.BeginLevelGembox;

        //if (openedLevel >= firststart)
        //{

        //    if (Configuration.instance.GemboxDeactive)
        //    {
        //        gemboxdisable.SetActive(true);
        //    }
        //    else
        //    {
        //        gemboxdisable.SetActive(false);
        //    }
        //    if (Configuration.instance.GemboxActive)
        //    {
        //        gemboxanimasyon();
        //        AudioManager.instance.GingerbreadExplodeAudio();
        //    }
        //}
        //else { Showtoybox(1.5f); }

    }
    public void gemboxanimasyon()
    {
        //GEM BOX
        int CurrentGems = PlayerPrefs.GetInt("GemBoxAmount", 0);
        int GemBoxMaxAmount = Configuration.instance.gemboxMaxAmount;
        if (CurrentGems < GemBoxMaxAmount)
        {
            GetComponent<Animator>().Play("gembox");
            AudioManager.instance.CollectTargetAudio();

        }
        else
        {
            GetComponent<Animator>().Play("gemboxfail");
            AudioManager.instance.CookieCrushAudio();
            Showtoybox(5f);
        }
    }
    public void gemboxaddbonus()
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        int totalusedgame = PlayerPrefs.GetInt("totalusedgame", 0);
        int firststart = Configuration.instance.BeginLevelGembox;
        if (openedLevel >= firststart)
        {
            //GEM BOX
            int CurrentGems = PlayerPrefs.GetInt("GemBoxAmount", 0);
            int GemBoxBonus = Configuration.instance.gemboxEveryLevelBonusAmount;
            int GemBoxMaxAmount = Configuration.instance.gemboxMaxAmount;

            if (CurrentGems < GemBoxMaxAmount)
            {
                PlayerPrefs.SetInt("GemBoxAmount", CurrentGems + GemBoxBonus);
                PlayerPrefs.Save();
                AudioManager.instance.CoinAddAudio();
            }
        }
       Showtoybox(1.5f);

    }


    public void Showtoybox(float time)
    {
        StartCoroutine(StartShowToyBox(time));
      
    }

    IEnumerator StartShowToyBox(float time)
    {
       
        yield return new WaitForSeconds(time);

       GetComponent<Animator>().Play("toyboxwinyandangel");
        toy.instance.closeNewToy();
        AudioManager.instance.CollectTargetAudio();

    }
    public void CloseToyBox()
    {
        //ToyBox.SetActive(false);
    }
    public void likethislevel()
    {
        AudioManager.instance.ButtonClickAudio();
    }
    public void dislikethislevel()
    {
        AudioManager.instance.ButtonClickAudio();
    }

    public void ShowRate()
    {
        //Rateus.gameObject.SetActive(true);
        Configuration.instance.RateShowed = true;
    }

    public void closerateus()
    {
        //Rateus.gameObject.SetActive(false);
    }

    public void zipla()
    {
        AudioManager.instance.DropAudio();
    }
    public void bostikirti()
    {
        AudioManager.instance.PopupBosTikirtiAudio();
    }
    public void dolutikirti()
    {
        AudioManager.instance.PopupDolutikirtiAudio();
    }
    public void swosh()
    {
        AudioManager.instance.PopupSwoshAudio();
    }
    public void bonuscoins()
    {
        AudioManager.instance.PopupBonusCoinsAudio();
    }
    // GIFT BOX Ve RANDOM PLAY
    public void MapAutoPopup()
    {
        Configuration.instance.LeveltoPlay = UnityEngine.Random.Range(0, 5);
        PlayerPrefs.SetInt("SkilzzEnd", 0);
        SkillzCrossPlatform.ReturnToSkillz();


        //StartCoroutine(StartMapAutoPopup());
    }

    IEnumerator StartMapAutoPopup()
    {
        //PlayerPrefs.SetInt("FirstTime", 1);
        Configuration.instance.LeveltoPlay = UnityEngine.Random.Range(0, 5);
        PlayerPrefs.SetInt("SkilzzEnd", 0);
        //PlayerPrefs.SetInt("SkillzLevel", 0);
        //Transition.LoadLevel("Menu", 0.1f, Color.black);
        //thiscomment
        SkillzCrossPlatform.ReturnToSkillz();
        yield return new WaitForSeconds(3f);
        loadingScreen.SetActive(false);
        
        yield return new WaitForEndOfFrame();

        //if(Configuration.instance.Tutorial)
        //{
        //    //skillzscreen
        //    //Transition.LoadLevel("Map", 0.1f, Color.black);
        //}
        //else
        //{
        //    //Transition.LoadLevel("Menu", 0.1f, Color.black);
        //}
        //int openedLevel = CoreData.instance.GetOpendedLevel();

        //if (openedLevel <= Configuration.instance.FirstGoMap && openedLevel > 0)
        //{
        //    StageLoader.instance.Stage = openedLevel; 
        //    StageLoader.instance.LoadLevel(StageLoader.instance.Stage);
        //    yield return new WaitForEndOfFrame();

        //    while (!StageLoader.instance.StageReady)
        //    {
        //        yield return null;
        //    }
        //    Transition.LoadLevel("Map", 0.1f, Color.black);
        //    //Transition.LoadLevel("Play", 0.1f, Color.black);
        //}
        //else
        //{
        //    // BackgroundMusic.instance.MapMusic();
        //    bool giftboxbool = Configuration.instance.GiftBox;
        //    bool randomplaybool = Configuration.instance.RandomPlay;
        //    bool arenaplay = Configuration.instance.ArenaMode;
        //    int arenanumber = PlayerPrefs.GetInt("arenanumber");
        //    AudioManager.instance.ButtonClickAudio();
#region Commented for Reason Sameer
            //if (Configuration.instance.RandomPlay)
            //{
            //    int MaxLevel = Configuration.instance.maxLevel;
            //    var level = Random.Range(40, MaxLevel);
            //    StageLoader.instance.Stage = level;
            //    Configuration.instance.autoPopup = level;
            //}
            //else if (Configuration.instance.GiftBox)
            //{
            //    int MaxLevel = Configuration.instance.maxLevel;

            //    int GiftBoxWinNum = Configuration.instance.GiftBoxWinNum;

            //    //for (int i = 0; i < Configuration.instance.GiftBoxRequiredWinLevel; i++)
            //    //{
            //    //    if (GiftBoxWinNum == i)
            //    //    {
            //    var level = Random.Range(Configuration.instance.LevelGiftBoxRangeA[GiftBoxWinNum], Configuration.instance.LevelGiftBoxRangeB[GiftBoxWinNum]);
            //    StageLoader.instance.Stage = level;
            //    StageLoader.instance.LoadLevel(StageLoader.instance.Stage);
            //    Configuration.instance.autoPopup = level;
            //    //    }

            //    //}
            //}
            //else if (Configuration.instance.ArenaMode)
            //{

            //    int MaxLevel = Configuration.instance.maxLevel;
            //    var level = Random.Range(Configuration.instance.LevelArenaRangeA[arenanumber], Configuration.instance.LevelArenaRangeB[arenanumber]);
            //    StageLoader.instance.Stage = level;
            //    StageLoader.instance.LoadLevel(StageLoader.instance.Stage);
            //    Configuration.instance.autoPopup = level;

            //}
            //else
#endregion
            //if (StageLoader.instance.Stage >= Configuration.instance.EpisodemaxLevel && !giftboxbool && !randomplaybool && !arenaplay)
            //{
            //    int MaxLevel = Configuration.instance.maxLevel;
            //    var level = Random.Range(40, MaxLevel);
            //    StageLoader.instance.Stage = level;
            //    Configuration.instance.autoPopup = level;
            //    Configuration.instance.RandomPlay = true;
            //}
            //else
            //{
            //    Configuration.instance.autoPopup = StageLoader.instance.Stage + 1;
            //}

            //Transition.LoadLevel("Map", 0.1f, Color.black);
        //}
    }

    public void doubleScore()
    {
        AudioManager.instance.ButtonClickAudio();
        //sameer
        x2Score = true;
        x2Star = false;
        //admanager.instance.ShowRewardedVideAdGeneric(4);
        //AdsManager.instance.ShowReward();
       
    }
    public void doubleStar()
    {
        AudioManager.instance.ButtonClickAudio();
        //sameer
        x2Score = false;
        x2Star = true;
        //admanager.instance.ShowRewardedVideAdGeneric(5);
        //AdsManager.instance.ShowReward();
    }

    public void RewardAdsReward()
    {
        Configuration.SaveAchievement("ach_watchAds", 1);
        //if (x2Score)
        //{
        //    var board = GameObject.Find("Board").GetComponent<itemGrid>();
        //    int score = board.score;
        //    int scoredouble = score + score;
        //    scoreText.text = "Score: " + scoredouble.ToString();
        //    int toplamscore = CoreData.instance.GetPlayerPuan();
        //    CoreData.instance.SavePlayerPuan(toplamscore + score);
        //    DoubleScore.SetActive(false);
        //    AnalyticsEvent.Custom("DoubleScoreReceived");
        //    Configuration.instance.WinScoreAmount = Configuration.instance.WinScoreAmount+ Configuration.instance.WinScoreAmount;

        //}
        //if (x2Star)
        //{

        //    var board = GameObject.Find("Board").GetComponent<itemGrid>();
        //    int star = board.star;
        //    int stardouble = star + star;
        //    DoubleStarSimge.gameObject.SetActive(true);
        //    int toplamstar = CoreData.instance.GetPlayerStars();
        //    CoreData.instance.SavePlayerStars(toplamstar + star);
        //    if (Configuration.instance.StarChallenge)
        //    {
        //        int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
        //        PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + star);
        //        PlayerPrefs.Save();

        //        try {
        //            GameObject.Find("STARCHALLENGE").GetComponent<StarChallengeAmount>().UpdateAmountText();
        //        }
        //        catch { }             
        //    }
        //    DoubleStar.SetActive(false);
        //    AnalyticsEvent.Custom("DoubleStarReceived");
        //    Configuration.instance.WinStarAmount = Configuration.instance.WinStarAmount + Configuration.instance.WinStarAmount ;

        //}
        AudioManager.instance.ButtonClickAudio();        

    }
    IEnumerator ShowStars()
    {
        yield return new WaitForSeconds(0.5f);
        var board = GameObject.Find("Board").GetComponent<itemGrid>();
        var star = board.star;

        GameObject explosion1;
        GameObject explosion2;
        GameObject explosion3;

        switch (star)
        {
            case 1:

                star1.gameObject.SetActive(true);              
                AudioManager.instance.Star1Audio();
                explosion1 = Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion1.transform.SetParent(star1.gameObject.transform, false);

                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
                break;
            case 2:
                star1.gameObject.SetActive(true);                
                AudioManager.instance.Star1Audio();
                explosion1 = Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion1.transform.SetParent(star1.gameObject.transform, false);

                yield return new WaitForSeconds(0.5f);

                star2.gameObject.SetActive(true);
                AudioManager.instance.Star2Audio();
                explosion2 = Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion2.transform.SetParent(star2.gameObject.transform, false);

                star3.gameObject.SetActive(false);
                break;
            case 3:
                star1.gameObject.SetActive(true);               
                AudioManager.instance.Star1Audio();
                explosion1 = Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion1.transform.SetParent(star1.gameObject.transform, false);

                yield return new WaitForSeconds(0.5f);

                star2.gameObject.SetActive(true);
                AudioManager.instance.Star2Audio();
                explosion2 = Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion2.transform.SetParent(star2.gameObject.transform, false);

                yield return new WaitForSeconds(0.5f);

                star3.gameObject.SetActive(true);
                AudioManager.instance.Star3Audio();
                explosion3 = Instantiate(Resources.Load(Configuration.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion3.transform.SetParent(star3.gameObject.transform, false);
                break;
            default:
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
                break;
        }
        yield return new WaitForEndOfFrame();
        //if (Configuration.instance.GemboxEvent)
        //{
        //    gemboxenable();
        //}
        //else
        //{
        //   Showtoybox(1.5f);
        //}
        //yield return new WaitForEndOfFrame();
       // ButtonPlay.gameObject.SetActive(true);

    }

    /*public void ShowReward()
    {
        // Check if rewarded ad is ready
        bool isReadyReward = Advertising.IsRewardedAdReady();
        // Show it if it's ready
        //SHOW REWARD ADS
        if (Configuration.instance.UnityAdsReward)
        {
            if (Advertisement.IsReady("rewardedVideo"))
            {
                var options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show("rewardedVideo", options);
                AudioManager.instance.ButtonClickAudio();
            }
            else if (isReadyReward)
            {
                Advertising.ShowRewardedAd();
                AudioManager.instance.ButtonClickAudio();               
            }
            else
            {
                AdsManager.LoadReward();
            }
        }
        else
        {
            if (isReadyReward)
            {
                Advertising.ShowRewardedAd();
                AudioManager.instance.ButtonClickAudio();    

            }
            else if (Advertisement.IsReady("rewardedVideo"))
            {
                var options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show("rewardedVideo", options);
                AudioManager.instance.ButtonClickAudio();
                AdsManager.LoadReward();
            }
            else
            {
                AdsManager.LoadReward();
            }
        }
    }*/
    

    void RewardedAdCompletedHandler()
    {
        //sameerrewardedcomplete
        //REWARD
        //RewardAdsReward();
        AudioManager.instance.ButtonClickAudio();
        //REWARD  
        //AdsManager.LoadReward();
        //achievement
        


    }
   
    /*private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                //REWARD
                RewardAdsReward();
                AudioManager.instance.ButtonClickAudio();               
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
        }*/

    

}
