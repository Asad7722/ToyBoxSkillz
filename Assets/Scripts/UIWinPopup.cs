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
        Debug.LogError("Before Animate score ");
        AnimateScore(TotalScoreText,_totalScore);
     //   TotalScoreText.text = "Total Score: " +_totalScore.ToString();
        skillzscore = _totalScore;
        print("BOARDSCORE"+board.score);
      
        SkillzCrossPlatform.SubmitScore(_totalScore, OnSuccess, OnFailure);
 
        ButtonPlay.SetActive(true);
//#endif
    
        //Episode Box
      
    }

    private IEnumerator AnimateScore(Text scoreText, int targetScore)
    {
        int currentValue = 0;
        float duration = 0.5f; // Duration of the animation
        float elapsedTime = 0f;
        Debug.LogError("Before While Animate score ");
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            currentValue = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, t));
            scoreText.text = currentValue.ToString();

            Debug.LogError("Set Animate score "+ currentValue);
            yield return null;
        }
        Debug.LogError("After Animate score ");
        // Ensure the final value is set correctly
        scoreText.text = targetScore.ToString();
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
