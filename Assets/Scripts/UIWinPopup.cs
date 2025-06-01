using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Analytics;
//using AppsFlyerSDK;

public class UIWinPopup : MonoBehaviour
{
    public float countDuration = 1.5f;
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
           


               
 
            }           
            else
            {
                //Unity Analytic Level Complete Rapor        
             

              
                
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
           /* CountToScore(timetextsc, secondsrem);
            CountToScore(timetextsc, secondsrem);

            CountToScore(timetextsc, secondsrem);

            CountToScore(timetextsc, secondsrem);*/

            timetextsc.text = secondsrem + "";
            timeTotalsc.text = remaingTime.ToString();
            movesrem.text = itemGrid.instance.moveLeft.ToString();
            int movesmult = itemGrid.instance.moveLeft * 10;
            movesTotalsc.text = movesmult.ToString();
            _totalScore = board.score + remaingTime+ movesmult;
            
        }
        else
        {
            _totalScore = board.score;
           
            timetextsc.text = "0";
            movesrem.text = "0";
            timeTotalsc.text = "0";
            movesTotalsc.text = "0";
          
        }
        TotalScoreText.text = "Total Score: " +_totalScore.ToString();
        skillzscore = _totalScore;
        print("BOARDSCORE"+board.score);
      
        SkillzCrossPlatform.SubmitScore(_totalScore, OnSuccess, OnFailure);
 
        ButtonPlay.SetActive(true);
//#endif
    
        //Episode Box
      
    }
    private IEnumerator CountToScore(Text scoreText, int target)
    {
        int currentScore = 0;
        float elapsed = 0f;

        while (elapsed < countDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / countDuration);
            currentScore = Mathf.RoundToInt(Mathf.Lerp(0, target, t));
            scoreText.text = currentScore.ToString();
            yield return null;
        }

        // Ensure exact final score
        scoreText.text = target.ToString();
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
