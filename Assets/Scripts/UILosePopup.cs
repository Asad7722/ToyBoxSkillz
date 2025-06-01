using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Analytics;
//using AppsFlyerSDK;
public class UILosePopup : MonoBehaviour
{
   
    public SceneTransition toMap;
    public Text scoreText = null;
    public Text coinText;
    public Text skipCost;
    public Text moreCost;
    public Text StarAmountText;
    public Text StarLoseAmountText;
    protected Canvas m_canvas;
   
    public PopupOpener morePopupAgain;
    public PopupOpener morePopup;
    public PopupOpener shopPopup;
    public GameObject ArenaStar;

    public static UILosePopup instance;


    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        var board = GameObject.Find("Board").GetComponent<itemGrid>();
        itemGrid.MovesReward = false;
        coinText.text = CoreData.instance.GetPlayerCoin().ToString();
        skipCost.text = Configuration.instance.skipLevelCost.ToString();
        moreCost.text = Configuration.instance.keepPlayingCost.ToString();     
        if (Configuration.instance.ArenaMode)
        {
            ArenaStar.gameObject.SetActive(true);
            int arenanumber = PlayerPrefs.GetInt("arenanumber");
            int staramount = PlayerPrefs.GetInt("staramount");
            int starloseamount = PlayerPrefs.GetInt("starloseamount");
            StarAmountText.text = "" + staramount;
            StarLoseAmountText.text = "-" + starloseamount;
        }
        else
        {
            if(Configuration.instance.ArenaModePlayEnable)
            ArenaStar.gameObject.SetActive(false);
        }
        Language.StartGlobalTranslateWord(scoreText, "Score: ", 0, board.score.ToString());
    }

    public void MoreClick()
    {
        print("LoseOne");
        AudioManager.instance.ButtonClickAudio();
        morePopup.OpenPopup();
        GetComponent<Popup>().Close();
        itemGrid.MovesReward = false;
        AudioManager.instance.PopupLoseAudio();
        AudioManager.instance.GirlWowSound(12);
        int openedLevel = CoreData.instance.GetOpendedLevel();

        if (Configuration.instance.EpisodePlay)
        {
            if (openedLevel > Configuration.instance.maxLevel)
            {
                int currentStage = StageLoader.instance.Stage + 1448;
                

             
            }            
            else
            { 

            
            }
        }


       
        Configuration.instance.WinLevel = 0;
        if (Configuration.instance.GiftBox)
        {
            Configuration.instance.GiftBoxWinNum = 0;
        }


    }

    public void LosePopupAgain()
    {       
            AudioManager.instance.ButtonClickAudio();

            // toMap.PerformTransition();
            //GameObject.Find("MorePopup(Clone)").GetComponent<Popup>().Close();

            GetComponent<Popup>().Close();
            itemGrid.MovesReward = false;

            morePopupAgain.OpenPopup();        

    }
    public void LevelPlayClose()
    {
        GetComponent<SceneTransition2>().PerformTransition();
        AudioManager.instance.ButtonClickAudio();

    }
    public void ExitButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();

        toMap.PerformTransition();
       

    }

    public void ReplayButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();

        Configuration.instance.autoPopup = StageLoader.instance.Stage;

        toMap.PerformTransition();
    }

    public void SkipButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();

        var cost = Configuration.instance.skipLevelCost;

        // enough coin
        if (cost <= CoreData.instance.playerCoin)
        {
            AudioManager.instance.CoinPayAudio();

            // reduce coin
            CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin - cost);

            var board = GameObject.Find("Board").GetComponent<itemGrid>();

            if (board)
            {
                // save info
                board.SaveLevelInfo();
            }

            // go to map with auto popup of next level
            Configuration.instance.autoPopup = StageLoader.instance.Stage + 1;

            toMap.PerformTransition();
        }
        else
        {
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.page2())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);
        }
    }

    public void KeepButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();
        var cost = Configuration.instance.keepPlayingCost;
        // enough coin
        if (cost <= CoreData.instance.playerCoin)
        {
            AudioManager.instance.CoinPayAudio();
            // reduce coin
            CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin - cost);
            var board = GameObject.Find("Board").GetComponent<itemGrid>();
            if (board)
            {
                // add 5 more moves
                board.moveLeft = 5;
                // change the label
                board.UITop.Set5Moves();
                // change the game state
                board.state = GAME_STATE.WAITING_USER_SWAP;
                // reset and call hint
                board.checkHintCall = 0;
                board.Hint();
                //bool isReadyInter = Advertising.IsInterstitialAdReady();
                //if(!isReadyInter)
                //{
                //  Advertising.LoadInterstitialAd();
                //}
                itemGrid.instance.AddThreeBooster();
            }




            // close the popup
            if (GameObject.Find("LosePopup(Clone)"))
            {
                GameObject.Find("LosePopup(Clone)").GetComponent<Popup>().Close();
                itemGrid.MovesReward = false;

            }
            else if (GameObject.Find("LosePopupAgain(Clone)"))
            {
                GameObject.Find("LosePopupAgain(Clone)").GetComponent<Popup>().Close();
                itemGrid.MovesReward = false;

            }

         
           
        }
        // not enough coin
        else
        {

            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.page2())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);

        }

    }
    public void RewardKeepButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();
        //sameer
        //AdsManager.instance.ShowReward();
        itemGrid.MovesReward = true;
        //admanager.instance.ShowRewardedVideAdGeneric(9);
    }

    public void RewardedAdCompletedHandler()
{
        //sameerrewardcomplete
       //REWARD       
        var board = GameObject.Find("Board").GetComponent<itemGrid>();
        if (board)
        {
            // add 5 more moves
            board.moveLeft = 4;
            // change the label
            board.UITop.Set2Moves();
            // change the game state
            board.state = GAME_STATE.WAITING_USER_SWAP;
            // reset and call hint
            board.checkHintCall = 0;
            board.Hint();
            itemGrid.RewardMoreMoveUsed = true;
           
           

        }
        // close the popup
        var popup = GameObject.Find("LosePopup(Clone)");
        // close the popup
        if (GameObject.Find("LosePopup(Clone)"))
        {
            GameObject.Find("LosePopup(Clone)").GetComponent<Popup>().Close();
            itemGrid.MovesReward = false;

        }
        else if (GameObject.Find("LosePopupAgain(Clone)"))
        {
            GameObject.Find("LosePopupAgain(Clone)").GetComponent<Popup>().Close();
            itemGrid.MovesReward = false;

        }

        //AdsManager.LoadReward();
        //achievement
        Configuration.SaveAchievement("ach_watchAds", 1);

    }

    public void SkillzSubmitScore()
    {
        //skillzsubmitscore
    }

}



