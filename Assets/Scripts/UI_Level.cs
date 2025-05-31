using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class UI_Level : MonoBehaviour
{
   
    public bool RewardPlay = false;
    public Text Label;
    public Image star1;
    public Image star2;
    public Image star3;

    public Image tick1;
    public Image tick2;
    public Image tick3;
    public Image add1;
    public Image add2;
    public Image add3;
    public Text number1;
    public Text number2;
    public Text number3;



    public Text targetText;

    public SceneTransition transition;
    public PopupOpener beginBooster1Popup;
    public PopupOpener beginBooster2Popup;
    public PopupOpener beginBooster3Popup;

    public bool avaialbe1;
    public bool avaialbe2;
    public bool avaialbe3;

    public Image booster1;
    public Image booster2;
    public Image booster3;

    public GameObject locked1;
    public GameObject locked2;
    public GameObject locked3;

    public Text lockedText1;
    public Text lockedText2;
    public Text lockedText3;
    public SceneTransition toMap;
  
    public GAME_STATE state;

    public static UI_Level instance;

    void Start()
    {
        if (instance == null)
            instance = this;

        int openedLevel = CoreData.instance.GetOpendedLevel();

        if (Configuration.instance.RandomPlay)
        {
            Language.StartGlobalTranslateWord(Label, "RANDOM PLAY", 0, "");

        }
        else if (Configuration.instance.GiftBox)
        {

            Language.StartGlobalTranslateWord(Label, "Remaining Level: ", Configuration.instance.GiftBoxRequiredWinLevel - Configuration.instance.GiftBoxWinNum, "");

        }
        else if (Configuration.instance.ArenaMode)
        {
            int arenanumber = PlayerPrefs.GetInt("arenanumber");
            if (arenanumber == 11)
            {
                Language.StartGlobalTranslateWord(Label, "ARENA LEGENDARY", 0, "");

            }
            else
            {
                Language.StartGlobalTranslateWord(Label, "ARENA ", arenanumber, "");

            }
        }
        else
        {
            if (openedLevel > Configuration.instance.maxLevel)
            {
                Language.StartGlobalTranslateWord(Label, "Level ", 0, (StageLoader.instance.Stage + 1448).ToString());

            }           
            else
            {
                Language.StartGlobalTranslateWord(Label, "Level ", 0, StageLoader.instance.Stage.ToString());

            }

        }



        bool giftbox = Configuration.instance.GiftBox;
        bool randomplay = Configuration.instance.RandomPlay;
        bool arenaplay = Configuration.instance.ArenaMode;


        if (!PlayerPrefs.HasKey("beginbosterHelpShowed") && StageLoader.instance.Stage>= Configuration.instance.beginFiveMovesLevel)
        {
            Configuration.instance.MessageBoardInfo = "You can start with begin boosters!";
            // Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            //var diffuse = Instantiate(Resources.Load("Prefabs/HELP/DiffuseMap")) as GameObject;
            // var starbox = GameObject.Find("StarTarget");
            //var scoreBox = GameObject.Find("ScoreTarget");
            var helpPopup = Instantiate(Resources.Load("Prefabs/HELP/HelpPopupMap")) as GameObject;

            if (helpPopup != null)
            {
                helpPopup.transform.SetParent(gameObject.transform, false);
                helpPopup.transform.localPosition = new Vector3(0, 200, 0);
            }


            PlayerPrefs.SetInt("beginbosterHelpShowed", 1);
            PlayerPrefs.Save();
        }
        Configuration.instance.beginFiveMoves = false;
        Configuration.instance.beginRainbow = false;
        Configuration.instance.beginBombBreaker = false;


        // begin boosters
        for (int i = 1; i <= 3; i++)
        {
            int boosterAmount = 0;
            Image tick = null;
            Image add = null;
            Text number = null;
            bool avaialbe = false;
            Image booster = null;
            GameObject locked = null;
            Text lockedText = null;

            switch (i)
            {
                case 1:
                    boosterAmount = CoreData.instance.beginFiveMoves;
                    tick = tick1;
                    add = add1;
                    number = number1;
                    avaialbe1 = (StageLoader.instance.Stage < Configuration.instance.beginFiveMovesLevel) ? false : true;
                    avaialbe = avaialbe1;
                    booster = booster1;
                    locked = locked1;
                    lockedText = lockedText1;
                    break;
                case 2:
                    boosterAmount = CoreData.instance.beginRainbow;
                    tick = tick2;
                    add = add2;
                    number = number2;
                    avaialbe2 = (StageLoader.instance.Stage < Configuration.instance.beginRainbowLevel) ? false : true;
                    avaialbe = avaialbe2;
                    booster = booster2;
                    locked = locked2;
                    lockedText = lockedText2;
                    break;
                case 3:
                    boosterAmount = CoreData.instance.beginBombBreaker;
                    tick = tick3;
                    add = add3;
                    number = number3;
                    avaialbe3 = (StageLoader.instance.Stage < Configuration.instance.beginBombBreakerLevel) ? false : true;
                    avaialbe = avaialbe3;
                    booster = booster3;
                    locked = locked3;
                    lockedText = lockedText3;
                    break;
            }

            if (avaialbe == true)
            {
                if (boosterAmount > 0)
                {
                    number.text = boosterAmount.ToString();
                    add.gameObject.SetActive(false);
                    tick.gameObject.SetActive(false);
                }
                else
                {
                    number.text = "0";
                    add.gameObject.SetActive(true);
                    tick.gameObject.SetActive(false);
                }
            }
            else
            {
                number.text = "0";
                number.gameObject.transform.parent.gameObject.SetActive(false);
                add.gameObject.SetActive(false);
                tick.gameObject.SetActive(false);
                booster.gameObject.SetActive(false);
                locked.SetActive(true);

                switch (i)
                {
                    case 1:
                        Language.StartGlobalTranslateWord(lockedText, "Require Level ", Configuration.instance.beginFiveMovesLevel, "");

                        break;
                    case 2:
                        Language.StartGlobalTranslateWord(lockedText, "Require Level ", Configuration.instance.beginRainbowLevel, "");

                        break;
                    case 3:
                        Language.StartGlobalTranslateWord(lockedText, "Require Level ", Configuration.instance.beginBombBreakerLevel, "");

                        break;
                }
            }
        }

    }


    public void PlayButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();
        Configuration.instance.playing = true;
        if (Configuration.instance.RandomPlay)
        {
            int openedLevel = CoreData.instance.GetOpendedLevel();
            if (openedLevel == Configuration.instance.EpisodemaxLevel)
            {
                Configuration.instance.giftnum = 0;

                StartCoroutine(SceneChange());

            }
            else
            {
                //SHOW REWARD ADS
                //sameer
                //AdsManager.instance.ShowReward();
                RewardPlay = false;
                //admanager.instance.ShowRewardedVideAdGeneric(6);
            }
        }
        else
        {
            Configuration.instance.giftnum = 0;
            StartCoroutine(SceneChange());
        }

    }



    IEnumerator SceneChange()
    {
        if (NewLife.instance.unlimitedLifePurchased || NewLife.instance.rewardLifeLive || NewLife.instance.lives > 0)
        {
            
            while (!StageLoader.instance.StageReady)
            {
                yield return null;
            }

            Transition.LoadLevel("Play", 0.1f, Color.black);
            NewLife.instance.lives--;
        }
        else
        {

            GameObject.Find("MapScene").GetComponent<MapScene>().LifeButtonClick();

        }

       
    }


    public void PlayRewardButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();
        //SHOW REWARD ADS
        //sameer
        //AdsManager.instance.ShowReward();
        RewardPlay = true;
        //admanager.instance.ShowRewardedVideAdGeneric(7);
    }

    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }
    public void CloseRandomPlay()
    {
        Configuration.instance.RandomPlay = false;
        Configuration.instance.GiftBox = false;
        Configuration.instance.ArenaMode = false;
        Configuration.instance.EpisodePlay = false;
        Configuration.instance.playing = false;
        Configuration.instance.RewardPlay = false;
        Configuration.instance.OneTimePlay = false;
    }
    public void DisableAll()
    {
        Configuration.instance.RandomPlay = false;
        Configuration.instance.GiftBox = false;
        Configuration.instance.ArenaMode = false;
        Configuration.instance.EpisodePlay = false;
        Configuration.instance.playing = false;
        Configuration.instance.RewardPlay = false;
        Configuration.instance.OneTimePlay = false;
    }

    public void GoToMap()
    {
        AudioManager.instance.ButtonClickAudio();

        toMap.PerformTransition();
    }

    public void levelplayclose()
    {
        GetComponent<SceneTransition2>().PerformTransition();
    }
    public void BeginBoosterClick(int booster)
    {
        var avaiable = false;

        switch (booster)
        {
            case 1:
                avaiable = avaialbe1;
                break;
            case 2:
                avaiable = avaialbe2;
                break;
            case 3:
                avaiable = avaialbe3;
                break;
        }

        if (avaiable == false)
        {
            return;
        }

        AudioManager.instance.ButtonClickAudio();

        int number = 0;

        switch (booster)
        {
            case 1:
                number = CoreData.instance.beginFiveMoves;
                break;
            case 2:
                number = CoreData.instance.beginRainbow;
                break;
            case 3:
                number = CoreData.instance.beginBombBreaker;
                break;
        }

        if (number > 0)
        {
            switch (booster)
            {
                case 1:
                    if (Configuration.instance.beginFiveMoves == false)
                    {
                        tick1.gameObject.SetActive(true);
                        number1.gameObject.SetActive(false);
                        Configuration.instance.beginFiveMoves = true;
                        
                    }
                    else
                    {
                        tick1.gameObject.SetActive(false);
                        number1.gameObject.SetActive(true);
                        Configuration.instance.beginFiveMoves = false;
                    }
                    break;
                case 2:
                    if (Configuration.instance.beginRainbow == false)
                    {
                        tick2.gameObject.SetActive(true);
                        number2.gameObject.SetActive(false);
                        Configuration.instance.beginRainbow = true;
                        
                    }
                    else
                    {
                        tick2.gameObject.SetActive(false);
                        number2.gameObject.SetActive(true);
                        Configuration.instance.beginRainbow = false;
                    }
                    break;
                case 3:
                    if (Configuration.instance.beginBombBreaker == false)
                    {
                        tick3.gameObject.SetActive(true);
                        number3.gameObject.SetActive(false);
                        Configuration.instance.beginBombBreaker = true;
                       
                    }
                    else
                    {
                        tick3.gameObject.SetActive(false);
                        number3.gameObject.SetActive(true);
                        Configuration.instance.beginBombBreaker = false;
                    }
                    break;
            }
        }
        else
        {
            switch (booster)
            {
                case 1:
                    beginBooster1Popup.OpenPopup();
                    break;
                case 2:
                    beginBooster2Popup.OpenPopup();
                    break;
                case 3:
                    beginBooster3Popup.OpenPopup();
                    break;
            }
        }
    }
   
    //sameerrewardedcomplete
    public void RewardedAdCompletedHandler()
    {
        //REWARD      

        if (Configuration.instance.RandomPlay)
        {

            Configuration.instance.giftnum = 0;

            // if enough life
            StartCoroutine(SceneChange());
            Configuration.instance.RewardPlay = true;

        }
        else if (RewardPlay)
        {

            Configuration.instance.playing = true;
            Configuration.instance.giftnum = 0;

            StartCoroutine(SceneChange());
            Configuration.instance.RewardPlay = true;

        }

        //AdsManager.LoadReward();
        //achievement
        Configuration.SaveAchievement("ach_watchAds", 1);
    }

}
