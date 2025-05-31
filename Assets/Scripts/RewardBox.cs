using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System;
using System.Linq;

public class RewardBox : MonoBehaviour
{
    public static RewardBox instance = null;
    
    public GameObject DoubleButon;

    public Text TitleText;
    public Text GemsAmountText;
    public Text GiftAmountText;

    public bool ButonEnable = false;
    public bool DoubleGifts = false;
    private int GemAmount;
    private int GiftAmount;
    private int NodeCount;
    public List<int> rewardlist;
    //public int Probability = 100;
    public RewardBoxSector[] Sectors;




    void Start()
    {
        //rewardlist.Clear();
        //DoubleButon.SetActive(false);
        //TitleText.text = Configuration.instance.GiftBoxTitle;
        //labelupdate();
        //for (int i = 0; i < Sectors.Length; i++)
        //{
        //    Sectors[i].RewardNode.SetActive(false);

        //}



        //rewardlist = new List<int>();

        //switch (Configuration.instance.rewardType)
        //{
        //    case REWARD_TYPE.NONE:
        //        break;
        //    case REWARD_TYPE.Box1:
        //        Configuration.SaveAchievement("ach_open5freeBox", 1);
        //        break;
        //    case REWARD_TYPE.Box2:
        //        Configuration.SaveAchievement("ach_open5freeBox", 1);
        //        break;
        //    case REWARD_TYPE.Box3:
        //        Configuration.SaveAchievement("ach_open5freeBox", 1);
        //        break;
        //    case REWARD_TYPE.DailyGiftBox:
        //        Configuration.SaveAchievement("ach_open10dailygift", 1);
        //        break;
        //    case REWARD_TYPE.StarGiftBox:
        //        Configuration.SaveAchievement("ach_Open5StarBox", 1);
        //        break;
        //    case REWARD_TYPE.ScoreGiftBox:
        //        Configuration.SaveAchievement("ach_Open5ScoreBox", 1);
        //        break;
        //    case REWARD_TYPE.LevelGiftBox:

        //        break;
        //    case REWARD_TYPE.EpisodeGiftBox:
        //        Configuration.SaveAchievement("ach_open3episodebox", 1);
        //        break;
        //    case REWARD_TYPE.StarChallengeGiftBox:
        //        Configuration.SaveAchievement("ach_win5starchallenge", 1);
        //        break;
        //    case REWARD_TYPE.LevelChallengeGiftBox:
        //        Configuration.SaveAchievement("ach_win3levelchallenge", 1);
        //        break;
        //    case REWARD_TYPE.MissionChallengeGiftBox:
        //        Configuration.SaveAchievement("ach_win5dailymissionchallenge", 1);
        //        break;
        //    case REWARD_TYPE.ArenaGiftBox:
        //        break;
        //    case REWARD_TYPE.RewardGems:
        //        break;
        //    default:
        //        break;
        //}

        //if (Configuration.instance.rewardType == REWARD_TYPE.LevelChallengeGiftBox
        //    || Configuration.instance.rewardType == REWARD_TYPE.MissionChallengeGiftBox
        //    || Configuration.instance.rewardType == REWARD_TYPE.Box2
        //    || Configuration.instance.rewardType == REWARD_TYPE.Box3
        //    || Configuration.instance.rewardType == REWARD_TYPE.StarChallengeGiftBox
        //    || Configuration.instance.rewardType == REWARD_TYPE.AchievementGiftBox
        //    )
        //{
        //    NodeCount = 6;
        //}
        //else if (Configuration.instance.rewardType == REWARD_TYPE.RewardGems
        //    || Configuration.instance.rewardType == REWARD_TYPE.ToyCollectionReward             
        //    )
        //{
        //    NodeCount = 1;
        //    GetRewardGems();
        //    return;
        //}
        //else
        //{
        //    NodeCount = 3;
        //}


        //for (int j = 0; j < NodeCount; j++)
        //{
        //    double seed = UnityEngine.Random.Range(1, Sectors.Sum(sector => sector.Probability));


        //    int cumulativeProbability = 0;



        //    for (int i = 0; i < Sectors.Length; i++)
        //    {
        //        cumulativeProbability += Sectors[i].Probability;

        //        if (seed <= cumulativeProbability)
        //        {
        //            if (!rewardlist.Contains(i))
        //            {
        //                rewardlist.Add(i);
        //                if ((i == 4 && NewLife.instance.Rewardlives >= 10) || (NewLife.instance.unlimitedLifePurchased && (i == 4 || i == 10)))
        //                {
        //                    j--;
        //                    break;
        //                }

        //            }
        //            else
        //            {
        //                j--;
        //                break;
        //            }
        //            Sectors[i].RewardNode.SetActive(true);
        //            if (i == 1)
        //            {
        //                DoubleButon.SetActive(true);
        //            }
        //            break;
        //        }

        //    }
        //}

        
        



    }

    public void GetRewardGems()
    {
        DoubleButon.SetActive(true);

       
        Sectors[1].RewardNode.SetActive(true);

    }
    public void labelupdate()
    {
        if (Configuration.instance.rewardType == REWARD_TYPE.Box1)
        {
            GemAmount = UnityEngine.Random.Range(5, 8);
            GiftAmount = 1;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.Box2)
        {
            GemAmount = UnityEngine.Random.Range(10, 15);
            GiftAmount = 2;

        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.Box3)
        {
            GemAmount = UnityEngine.Random.Range(15, 20);
            GiftAmount = 3;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.DailyGiftBox)
        {
            GemAmount = UnityEngine.Random.Range(Configuration.instance.GiftGemsAmountMin, Configuration.instance.GiftGemsAmountMax);
            GiftAmount = 1;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.StarGiftBox)
        {
            GemAmount = UnityEngine.Random.Range(Configuration.instance.GiftGemsAmountMin, Configuration.instance.GiftGemsAmountMax);
            GiftAmount = 1;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.ScoreGiftBox)
        {
            GemAmount = UnityEngine.Random.Range(Configuration.instance.GiftGemsAmountMin, Configuration.instance.GiftGemsAmountMax);
            GiftAmount = 1;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.LevelGiftBox)
        {
            GemAmount = UnityEngine.Random.Range(Configuration.instance.GiftGemsAmountMin, Configuration.instance.GiftGemsAmountMax);
            GiftAmount = 1;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.StarChallengeGiftBox)
        {
            GemAmount = UnityEngine.Random.Range(Configuration.instance.StarChallengeStarAmount - Configuration.instance.StarChallengeStarAmount / 3, Configuration.instance.StarChallengeStarAmount);
            GiftAmount = 3;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.LevelChallengeGiftBox)
        {
            GemAmount = UnityEngine.Random.Range(Configuration.instance.GiftBoxGiftAmount - Configuration.instance.GiftBoxGiftAmount / 3, Configuration.instance.GiftBoxGiftAmount);
            GiftAmount = 3;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.MissionChallengeGiftBox)
        {
            GemAmount = UnityEngine.Random.Range(30, 60);
            GiftAmount = 3;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.AchievementGiftBox)
        {
            GemAmount = UnityEngine.Random.Range(30, 60);
            GiftAmount = UnityEngine.Random.Range(1, 4);
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.ToyCollectionReward)
        {
            GemAmount = UnityEngine.Random.Range(Configuration.instance.GiftGemsAmountMin, Configuration.instance.GiftGemsAmountMax);
            GiftAmount = 1;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.RewardGems)
        {
            GemAmount = UnityEngine.Random.Range(Configuration.instance.GiftGemsAmountMin, Configuration.instance.GiftGemsAmountMax);
            GiftAmount = 1;
        }
        else if (Configuration.instance.rewardType == REWARD_TYPE.ArenaGiftBox)
        {
            int arenanumber = PlayerPrefs.GetInt("arenanumber");
            for (int i = 2; i < 12; i++)
            {
                if (arenanumber == i)
                {
                    GemAmount = Configuration.instance.GiftAmountText[i];
                }
            }

            GiftAmount = 6;
        }
        else
        {
            GemAmount = UnityEngine.Random.Range(Configuration.instance.GiftGemsAmountMin, Configuration.instance.GiftGemsAmountMax);
            GiftAmount = UnityEngine.Random.Range(Configuration.instance.GiftAmountMin, Configuration.instance.GiftAmountMax);

        }

        PlayerPrefs.SetInt("GemAmount", GemAmount);
        PlayerPrefs.SetInt("GiftAmount", GiftAmount);
        PlayerPrefs.Save();
        GemsAmountText.text = "" + GemAmount;
        Language.StartGlobalTranslateWord(GiftAmountText, "Select gifts ", GiftAmount, "");

        // GiftAmountText.text = "Select " + GiftAmount + " gifts";
    }
    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }
    public void GemsLabelUpdate()
    {
        if (Configuration.instance.ArenaMode)
        {
            Debug.Log("ArenaModdegil");
        }
        else
        {
            Debug.Log("ArenaModdegil");
        }
    }
    public void ButtonPayAudio()
    {
        //AudioManager.instance.giftsound();
    }


    public void p1()//row
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift1 alindi");
            //if (DoubleGifts)
            //{
            //    CoreData.instance.SaveRowBreaker(CoreData.instance.rowBreaker += 2);
            //    AnalyticsEvent.Custom("Reward_Double_RowBooster");

            //}
            //else
            //{
            CoreData.instance.SaveRowBreaker(CoreData.instance.rowBreaker += 1);
            AnalyticsEvent.Custom("Reward_RowBooster");

            //}

            AudioManager.instance.giftsound();
            Sectors[0].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p2()//gem
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift2 alindi");
            int GemAmount = PlayerPrefs.GetInt("GemAmount");
            if (DoubleGifts)
            {
                CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 2 * GemAmount);
                AnalyticsEvent.Custom("Reward_Double_Coin");


            }
            else
            {
                CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += GemAmount);
                AnalyticsEvent.Custom("Reward_Coin");

            }


            try { GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel(); }
            catch { }


            AudioManager.instance.giftsound();
            Sectors[1].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p3()//begin bomb
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift3 alindi");
            //if (DoubleGifts)
            //{
            //    CoreData.instance.SaveBeginBombBreaker(CoreData.instance.beginBombBreaker += 2);
            //    AnalyticsEvent.Custom("Reward_Double_beginBomb");

            //}
            //else
            //{
            CoreData.instance.SaveBeginBombBreaker(CoreData.instance.beginBombBreaker += 1);
            AnalyticsEvent.Custom("Reward_beginBomb");

            //}


            AudioManager.instance.giftsound();
            Sectors[2].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p4()//beginxbreaker
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift4 alindi");
            //if (DoubleGifts)
            //{
            //    CoreData.instance.SaveBeginFiveMoves(CoreData.instance.beginFiveMoves += 2);
            //    AnalyticsEvent.Custom("Reward_Double_BeginXbreaker");

            //}
            //else
            //{
            CoreData.instance.SaveBeginFiveMoves(CoreData.instance.beginFiveMoves += 1);
            AnalyticsEvent.Custom("Reward_BeginXbreaker");

            //}


            AudioManager.instance.giftsound();
            Sectors[3].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p5()//5 life
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift5 alindi");


            //if (DoubleGifts)
            //{
            //    NewLife.instance.Rewardlives += 10 - (NewLife.instance.maxLives - NewLife.instance.lives);
            //    NewLife.instance.lives = NewLife.instance.maxLives;
            //    AnalyticsEvent.Custom("DoubleReward_MaxLife");

            //}
            //else
            //{
            //sameer
            //if (NewLife.instance.maxLives - NewLife.instance.lives < 5)
            //{
            //    NewLife.instance.Rewardlives += 5 - (NewLife.instance.maxLives - NewLife.instance.lives);
            //    NewLife.instance.lives = NewLife.instance.maxLives;
            //}
            //else
            //{
            //    NewLife.instance.lives = NewLife.instance.maxLives;
            //}
            //AnalyticsEvent.Custom("Reward_MaxLife");

            //}



            AudioManager.instance.giftsound();
            Sectors[4].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p6()//begin colorhunter
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift6 alindi");
            //if (DoubleGifts)
            //{
            //    CoreData.instance.SaveBeginRainbow(CoreData.instance.beginRainbow += 2);
            //    AnalyticsEvent.Custom("Reward_Double_beginRainbow");

            //}
            //else
            //{
            CoreData.instance.SaveBeginRainbow(CoreData.instance.beginRainbow += 1);
            AnalyticsEvent.Custom("Reward_beginRainbow");

            //}

            AudioManager.instance.giftsound();
            Sectors[5].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p7()//20m life
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift5 alindi");


            //if (DoubleGifts)
            //{
            //    NewLifeRewardTime.instance.StartRewardLife(2400);
            //    AnalyticsEvent.Custom("double_reward_20m_unlimited_life");

            //}
            //else
            //{
            NewLifeRewardTime.instance.StartRewardLife(1200);
            AnalyticsEvent.Custom("reward_20m_unlimited_life");

            //}



            AudioManager.instance.giftsound();
            Sectors[10].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p8()//1h life
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift8 alindi");


            //if (DoubleGifts)
            //{
            //    NewLifeRewardTime.instance.StartRewardLife(7200);
            //    AnalyticsEvent.Custom("double_reward_1h_unlimited_life");

            //}
            //else
            //{
            NewLifeRewardTime.instance.StartRewardLife(3600);
            AnalyticsEvent.Custom("reward_1h_unlimited_life");

            //}



            AudioManager.instance.giftsound();
            Sectors[10].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p9()//2h life
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift5 alindi");


            //if (DoubleGifts)
            //{
            //    NewLifeRewardTime.instance.StartRewardLife(14400);
            //    AnalyticsEvent.Custom("double_reward_2h_unlimited_life");

            //}
            //else
            //{
            NewLifeRewardTime.instance.StartRewardLife(7200);
            AnalyticsEvent.Custom("reward_2h_unlimited_life");

            // }



            AudioManager.instance.giftsound();
            Sectors[10].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p10()//mixer
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift6 alindi");
            //if (DoubleGifts)
            //{
            //    CoreData.instance.SaveOvenBreaker(CoreData.instance.ovenBreaker += 2);
            //    AnalyticsEvent.Custom("Reward_Double_MixerBooster");

            //}
            //else
            //{
            CoreData.instance.SaveOvenBreaker(CoreData.instance.ovenBreaker += 1);
            AnalyticsEvent.Custom("Reward_MixerBooster");

            //}


            AudioManager.instance.giftsound();
            Sectors[6].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p11()//Color Hunter
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift5 alindi");
            //if (DoubleGifts)
            //{
            //    CoreData.instance.SaveRainbowBreaker(CoreData.instance.rainbowBreaker += 2);
            //    AnalyticsEvent.Custom("Reward_ColorHunter");

            //    AudioManager.instance.giftsound();

            //    CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            //    StartCoroutine(UpdatestargiftPopup());
            //}
            //else
            //{
            CoreData.instance.SaveRainbowBreaker(CoreData.instance.rainbowBreaker += 1);
            AnalyticsEvent.Custom("Reward_ColorHunter");
            AudioManager.instance.giftsound();

            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
            //}
            Sectors[7].RewardNode.SetActive(false);

        }
    }
    public void p12()//single Booster
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift4 alindi");
            //if (DoubleGifts)
            //{
            //    CoreData.instance.SaveSingleBreaker(CoreData.instance.singleBreaker += 2);
            //    AnalyticsEvent.Custom("Reward_Double_SingleBooster");

            //}
            //else
            //{
            CoreData.instance.SaveSingleBreaker(CoreData.instance.singleBreaker += 1);
            AnalyticsEvent.Custom("Reward_SingleBooster");


            //}

            AudioManager.instance.giftsound();

            Sectors[8].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }
    }
    public void p13()//Column
    {
        if (!ButonEnable)
        {
            ButonEnable = true;
            Debug.Log("gift2 alindi");
            //if (DoubleGifts)
            //{
            //    CoreData.instance.SaveColumnBreaker(CoreData.instance.columnBreaker += 2);
            //    AnalyticsEvent.Custom("Reward_Double_ColumnBooster");

            //}
            //else
            //{
            CoreData.instance.SaveColumnBreaker(CoreData.instance.columnBreaker += 1);
            AnalyticsEvent.Custom("Reward_ColumnBooster");

            //}

            AudioManager.instance.giftsound();

            Sectors[9].RewardNode.SetActive(false);
            CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
            StartCoroutine(UpdatestargiftPopup());
        }

    }

    public void ShowRewardAds()
    {
        //sameer
        //admanager.instance.ShowRewardedVideAdGeneric(2);
        //AdsManager.instance.ShowReward();

    }
    IEnumerator UpdatestargiftPopup()
    {

        int GiftAmount = PlayerPrefs.GetInt("GiftAmount");
        if (CoreData.instance.giftAmount >= GiftAmount)
        {
            GetComponent<Popup>().Close();
            if (Configuration.instance.rewardType == REWARD_TYPE.ScoreGiftBox)
            {
                CoreData.instance.SavePlayerPuan(0);

                try
                {
                    GameObject.Find("MapScene").GetComponent<MapScene>().UpdatePuanButon();
                    GameObject.Find("MapScene").GetComponent<MapScene>().UpdateFinalScoreAmountLabel();
                    GameObject.Find("MapScene").GetComponent<MapScene>().UpdateFinalScoreProgressBar();
                }
                catch { }
            }
            if (Configuration.instance.rewardType == REWARD_TYPE.StarGiftBox)
            {
                CoreData.instance.SavePlayerStars(0);
                try
                {
                    GameObject.Find("MapScene").GetComponent<MapScene>().UpdateStarsButon();
                    GameObject.Find("MapScene").GetComponent<MapScene>().UpdateFinalStarsAmountLabel();
                    GameObject.Find("MapScene").GetComponent<MapScene>().UpdateFinalStarProgressBar();
                }
                catch { }
            }
            if (Configuration.instance.rewardType == REWARD_TYPE.DailyGiftBox)
            {
                PlayerPrefs.SetInt("DailyGiftsAlindi", 1);
                PlayerPrefs.Save();
                //  GoogleAnalyticsV4.instance.LogScreen("Daily Gifts Alindi");
            }
            if (Configuration.instance.rewardType == REWARD_TYPE.LevelGiftBox)
            {
                int openedLevel = CoreData.instance.GetOpendedLevel();
                PlayerPrefs.SetInt("levelgift alindi" + openedLevel, 1);
            }
            if (Configuration.instance.rewardType == REWARD_TYPE.StarChallengeGiftBox)
            {
                if (Configuration.instance.StarChallenge)
                {

                    int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
                    if (StarChallengeNum >= Configuration.instance.StarChallengeStarAmount)
                    {
                        PlayerPrefs.SetInt("StarChallengeNum", 0);
                        PlayerPrefs.SetInt("StarChallengeEnable", 0);
                        PlayerPrefs.SetInt("StarChallengeHediyeAlindi", 1);
                        PlayerPrefs.Save();
                        Configuration.instance.StarChallenge = false;
                    }
                }
            }
            if (Configuration.instance.rewardType == REWARD_TYPE.LevelChallengeGiftBox)
            {
                if (Configuration.instance.GiftBox)
                {
                    Configuration.instance.GiftBoxWinNum = 0;
                }

            }
            if (Configuration.instance.rewardType == REWARD_TYPE.MissionChallengeGiftBox)
            {

                PlayerPrefs.SetInt("MissionChallengeHediyeAlindi", 1);
                PlayerPrefs.Save();
                MissionChallenge.MissionReset();

            }


            Configuration.instance.rewardType = REWARD_TYPE.NONE;


            CoreData.instance.SaveGiftAmount(0);
            AudioManager.instance.CoinAddAudio();
        }
        yield return new WaitForSeconds(1.0f);
        ButonEnable = false;
    }
    
    //sameerrewardedadcomplete
    public void RewardedAdCompletedHandler()
    {
        //REWARD
        DoubleButon.SetActive(false);
        DoubleGifts = true;

        Sectors[1].DoubleNode.SetActive(true);
        GemsAmountText.text = (GemAmount * 2).ToString();

        AudioManager.instance.Star1Audio();
        //REWARD  

        //achievement
        Configuration.SaveAchievement("ach_watchAds", 1);
    }


}
/**
 * One sector on the wheel
 */
[Serializable]
public class RewardBoxSector : System.Object
{
    [Tooltip("Text object where value will be placed (not required)")]
    public GameObject RewardNode;
    public GameObject DoubleNode;

    [Tooltip("Chance that this sector will be randomly selected")]
    [RangeAttribute(0, 100)]
    public int Probability = 100;
}


