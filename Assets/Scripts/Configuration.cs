using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
//sing System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.Analytics;
 
#if UNITY_EDITOR
using UnityEditor;
#endif




public enum REWARD_TYPE
{
    NONE,
    Box1,
    Box2,
    Box3,
    DailyGiftBox,
    StarGiftBox,
    ScoreGiftBox,
    LevelGiftBox,
    StarChallengeGiftBox,
    LevelChallengeGiftBox,
    MissionChallengeGiftBox,
    ArenaGiftBox,
    EpisodeGiftBox,
    AchievementGiftBox,
    RewardGems,
    ToyCollectionReward

}

public enum TILE_TYPE
{
    NONE,
    PASS_THROUGH,
    LIGHT_TILE,
    DARD_TILE

}

// waffle
public enum WAFFLE_TYPE
{
    NONE,
    WAFFLE_1,
    WAFFLE_2,
    WAFFLE_3 // do not use
}

public enum ITEM_TYPE
{
    NONE,

    ITEM_RAMDOM,
    ITEM_COLORCONE,
    ITEM_BOMB,
    ITEM_BIGBOMB,
    ITEM_MEDBOMB,
    ITEM_COLORHUNTER,
    ITEM_COLORHUNTER1,
    ITEM_COLORHUNTER2,
    ITEM_COLORHUNTER3,
    ITEM_COLORHUNTER4,
    ITEM_COLORHUNTER5,
    ITEM_COLORHUNTER6,
    ITEM_CROSS,
    ITEM_COLUMN,
    ITEM_ROW,


    BlueBox,

    GreenBox,

    ORANGEBOX,

    PURPLEBOX,

    REDBOX,

    YELLOWBOX,

    BREAKABLE,

    ROCKET_RANDOM,
    ROCKET_1,
    ROCKET_2,
    ROCKET_3,
    ROCKET_4,
    ROCKET_5,
    ROCKET_6,

    MINE_1_LAYER,
    MINE_2_LAYER,
    MINE_3_LAYER,
    MINE_4_LAYER,
    MINE_5_LAYER,
    MINE_6_LAYER,

    ROCK_CANDY_RANDOM,
    ROCK_CANDY_1,
    ROCK_CANDY_2,
    ROCK_CANDY_3,
    ROCK_CANDY_4,
    ROCK_CANDY_5,
    ROCK_CANDY_6,

    COLLECTIBLE_1,
    COLLECTIBLE_2,
    COLLECTIBLE_3,
    COLLECTIBLE_4,
    COLLECTIBLE_5,
    COLLECTIBLE_6,
    COLLECTIBLE_7,
    COLLECTIBLE_8,
    COLLECTIBLE_9,
    COLLECTIBLE_10,
    COLLECTIBLE_11,
    COLLECTIBLE_12,
    COLLECTIBLE_13,
    COLLECTIBLE_14,
    COLLECTIBLE_15,
    COLLECTIBLE_16,
    COLLECTIBLE_17,
    COLLECTIBLE_18,
    COLLECTIBLE_19,
    COLLECTIBLE_20
}

// Lock
public enum LOCK_TYPE
{
    NONE,
    LOCK_1
}

public enum GAME_STATE
{
    PREPARING_LEVEL,
    WAITING_USER_SWAP,
    PRE_WIN_AUTO_PLAYING,
    OPENING_POPUP,
    NO_MATCHES_REGENERATING,
    DESTROYING_ITEMS

}

public enum BOOSTER_TYPE
{
    NONE = 0,
    SINGLE_BREAKER,
    COLUMN_BREAKER,
    ROW_BREAKER,
    RAINBOW_BREAKER,
    OVEN_BREAKER,
    BEGIN_FIVE_MOVES,
    BEGIN_RAINBOW_BREAKER,
    BEGIN_BOMB_BREAKER
}

public enum FIND_DIRECTION
{
    NONE = 0,
    ROW,
    COLUMN
}

public enum BREAKER_EFFECT
{
    NORMAL = 0,
    BIG_ROW_BREAKER,
    BIG_COLUMN_BREAKER,
    CROSS,
    CROSS_X_BREAKER,
    BOMB_X_BREAKER,
    BIG_BOMB_X_BREAKER,
    MED_BOMB_X_BREAKER,
    COLUMN_EFFECT,
    ROW_EFFECT,
    NONE
}

public enum TARGET_TYPE
{
    NONE = 0,
    SCORE, // do not use
    ITEM,
    BREAKABLE,
    WAFFLE,
    COLLECTIBLE,
    COLUMN_ROW_BREAKER,
    BOMB_BREAKER,
    X_BREAKER,
    LOCK,
    COLORCONE,
    BIGBOMB,
    MEDBOMB,
    ROCKET,
    TOYMINE,
    ROCK_CANDY
}

public enum SWAP_DIRECTION
{
    NONE,
    TOP,
    RIGHT,
    BOTTOM,
    LEFT
}
public enum CURRENT_SCENE
{
    MENU,
    MAP,
    PLAY
}

public class Configuration : MonoBehaviour
{
    public static Configuration instance = null;
    //public string PackageName;
    //public string PackageCost;
    public int PurchasedId;
    public bool Loaded;
  
   
    public REWARD_TYPE rewardType;
    [Header("Game Type")]
    public string Lang;
    public bool Amazon;
    public bool Udp;   
    public bool pause;
    public bool Tutorial;
    public bool RemoteConfigOk;
    public bool EpisodePopup;
    public bool MenuSceneBypass;
    public float WaitMenuInitSeconds = 5f;
    public int WinLevel;
    public int LoseLevel;
    private int oldLevel;
    public CURRENT_SCENE CurrentScene;

    [Header("Message Popup")]

    public bool MessagePopup;
    public string MessageBoardTitle;
    public string MessageBoardInfo;


    [Header("NewLife")]
    public float lifeReplenishTime = 900f;
    public bool DailyRewardTimeLive;
    public float DailyRewardTime;
    public float WinChallengeRewardTime;
    public int recoveryCostPerLife;

    [Header("Free Boxes")]
    public int activeFreeBox;
    public string GiftBoxTitle;
    public float FreeBox1Time = 180f;
    public float FreeBox2Time = 360f;
    public float FreeBox3Time = 600f;
    public int _ReadyFreeBoxesCount;
    public int _ActiveFreeBoxesCount;
    public int ReadyFreeBoxesCount
    {
        set
        {
            _ReadyFreeBoxesCount = value;
            PlayerPrefs.SetInt("ReadyFreeBoxesCount", _ReadyFreeBoxesCount);

        }
        get
        {
            return _ReadyFreeBoxesCount;
        }


    }
    public int ActiveFreeBoxesCount
    {
        set
        {
            _ActiveFreeBoxesCount = value;
            PlayerPrefs.SetInt("ActiveFreeBoxesCount", _ActiveFreeBoxesCount);
            PlayerPrefs.Save();
        }
        get
        {
            return _ActiveFreeBoxesCount;
        }
    }




    [Header("Notifcation")]
    public bool DailyGiftCall = true;
    public bool StarChallengeCall = true;
    public bool LifeCall = true;
    public bool RepeatCall = true;
    public int RepeatHour = 20;
    //public NotificationRepeat repeatType = NotificationRepeat.EveryDay;
    public string repeatTitle = "Started Party";
    public string repeatSubtitle = "Blast Time";
    public string repeatMessage = "Starting Blast Party! Come and open gift boxes";
    public bool RepeatCalled = false;



    [Header("Setup Game")]

    public bool StarChallengeEvent;
    public bool MissionChallengeEvent;
    public bool LevelChallengeEvent;
   
   
    public bool GemboxEvent;
    public bool LevelBoxEvent;
    public bool EpisodeBoxEvent;
    public bool Bildirim;
    public bool GameServices;
    public bool NoSleep;
    public bool MoreGames;
    public bool OfflinePunishment;
    public bool BoostersCorrection;

 
 


 
    public int FirstGoMap;

    [Header("Start Levels")]
    public int BeginLevelDailyBonus = 8;
    public int BeginLevelStarChallenge = 10;
    public int BeginLevelMissionChallenge = 18;
    public int BeginLevelFortuneWheel = 15;
    public int BeginLevelLevelChallenge = 20;
    public int BeginLevelGembox = 25;
    public int BeginLevelRateUs = 12;
     

    [Header("STARBOX VE SCOREBOX")]
    public int StarBoxFullAmount;
    public int ScoreBoxFullAmount;
    public int GiftGemsAmountMin;
    public int GiftGemsAmountMax;
    public int GiftAmountMax;
    public int GiftAmountMin;
    public int GiftTiklanmadi;

    [Header("LEVEL BOX")]
    public int ShowLevelBoxEvery;
    public int LevelBoxGiftAmount;

    [Header("GIFT BOX")]
    public int GiftBoxGiftAmount;
    public int GiftBoxWinNum;
    public int GiftBoxRequiredWinLevel = 5;
    public int[] LevelGiftBoxRangeA, LevelGiftBoxRangeB;

    [Header("STAR CHALLENGE")]
    public int StarChallengeStarAmount;
    public int StarChallengeGiftAmount;

    [Header("MISSION CHALLENGE REQUEST")]
    public int Request1_Min = 250;
    public int Request1_Max = 550;

    public int Request2_Min = 50;
    public int Request2_Max = 250;

    public int Request3_Min = 5;
    public int Request3_Max = 12;

    public int Request4_Min = 5;
    public int Request4_Max = 15;

    public int Request5_Min = 3;
    public int Request5_Max = 9;

    [Header("GEMBOX")]
    public int gemboxMinAmount;
    public int gemboxEveryLevelBonusAmount;
    public int gemboxMaxAmount;
    public bool GemboxActive;
    public bool GemboxDeactive;
    public int reenable;
 


    // max level
    [Header("LEVEL SETTINGS")]
    public int maxLevel;
    public int EpisodemaxLevel;
    public int episode;
    public int[] EpisodesRange;
    public string[] EpisodesLabel;

    // ARENA
    [Header("ARENA MODE SETTINGS")]
    public bool ArenaModePlayEnable;
    public bool ArenaPopupGosterildi;
    public int ArenaWinCoinsMax;
    public int ArenaWinCoinsMin;
    public int ArenaLoseCoinsMax;
    public int ArenaLoseCoinsMin;
    public int[] GiftAmountText;
    public int[] GerekliStarText;
    public int[] LevelArenaRangeA, LevelArenaRangeB;

    [Header("Current Game")]
    public int CurrentEpisode;
    public bool playing;
    public bool RandomPlay;
    public bool EpisodePlay;
    public bool ArenaMode;
    public bool GiftBox;
    public bool StarChallenge;
    public bool RewardPlay;
    public bool OneTimePlay;

    [Header("RANDOM GIFTS")]
    public int gift1;
    public int giftnum;

    [Header("Configuration")]
    public float swapTime;
    public float destroyTime;
    public float dropTime;
    public float changingTime;
    public float hintDelay;
    public float destroyDelay;

    [Header("Score Collectable Items")]
    public int scoreItem;
    public int finishedScoreItem;

    [Header("Bonus Stars")]
    public int bonus1Star;
    public int bonus2Star;
    public int bonus3Star;

    [Header("Packages")]
    public int package1Amount;
    public int package2Amount;

    [Header("Powers")]
    public int beginFiveMovesLevel;
    public int beginRainbowLevel;
    public int beginBombBreakerLevel;

    [Header("Moves Cost")]
    public int beginFiveMovesCost1;
    public int beginFiveMovesCost2;
    [Header("Rainbow Cost")]
    public int beginRainbowCost1;
    public int beginRainbowCost2;
    [Header("Bomb Cost")]
    public int beginBombBreakerCost1;
    public int beginBombBreakerCost2;

    // play
    [Header("Misc Cost")]
    public int keepPlayingCost;
    public int skipLevelCost;

    [Header("Breaker Cost")]
    public int singleBreakerCost1;
    public int singleBreakerCost2;

    [Header("RB Cost")]
    public int rowBreakerCost1;
    public int rowBreakerCost2;

    [Header("CB Cost")]
    public int columnBreakerCost1;
    public int columnBreakerCost2;

    [Header("Rainbow/B Cost")]
    public int rainbowBreakerCost1;
    public int rainbowBreakerCost2;

    [Header("Oven/B Cost")]
    public int ovenBreakerCost1;
    public int ovenBreakerCost2;

    [Header("Hints")]
    public int plusMoves = 5;
    public bool showHint;

  

 

    [Header("Check")]
    // map config
    public int autoPopup;
    public int WinStarAmount;
    public int WinScoreAmount;


    [Header("Begin Options")]

    // play config
    public bool beginFiveMoves;
    public bool beginRainbow;
    public bool beginBombBreaker;

    [Header("Max Values")]
    public bool touchIsSwallowed;

    // settings
    public static int maxItems = 6;

    [Header("Check to disable debug")]
    public bool checkSwap; // TEST ONLY

    [Header("Encouraging Popup")]
    public int encouragingPopup;

    [Header("USED GAME")]
    public int usedgame;
    public bool CloseClick = false;
    public bool MenuToMap = false;
    //public bool FirstOpen = false;
    public bool MenuScene = true;
    public int PlayedLevel = 0;
    public bool SkipTutuorial;

    // game data
    public static string game_data = "items.dat";
    public static string opened_level = "opened_level";
    public static string level_statistics = "level_statistics";
    public static string level_star = "level_star";
    public static string level_score = "level_score";
    public static string level_number = "level_number";
    public static string player_coin = "player_coin";
    public static string single_breaker = "single_breaker";
    public static string row_breaker = "row_breaker";
    public static string column_breaker = "column_breaker";
    public static string rainbow_breaker = "rainbow_breaker";
    public static string oven_breaker = "oven_breaker";
    public static string begin_five_moves = "begin_five_moves";
    public static string begin_rainbow = "begin_rainbow";
    public static string begin_bomb_breaker = "begin_bomb_breaker";
    public static string player_stars = "player_stars";
    public static string player_puan = "player_puan";
    public static string gift_amount = "gift_amount";
    public static string toplam_score = "toplam_score";


    // life
    public static string exit_date_time = "string_exit_date_time";
    public static string stringLife = "string_life";
    public static string stringTimer = "string_timer";
    public static string first_setup_date = "string_first_setup_date";


    public GameObject CurruentEpisodeObje;

    public struct userAttributes { }
    public struct appAttributes { }

    public int LeveltoPlay;

    void Awake()
    {
        //skillz
        //sameerLevel
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        PlayerPrefs.SetInt("SkilzzEnd", 0);

        LevelNumber();
       
        //Unity Remote Config        
      

        DontDestroyOnLoad(gameObject);
        ReadyFreeBoxesCount = PlayerPrefs.GetInt("ReadyFreeBoxesCount");
        ActiveFreeBoxesCount = PlayerPrefs.GetInt("ActiveFreeBoxesCount");
    }
    
    public void resetData()
    {
        CoreData.instance.singleBreaker = 3;
        CoreData.instance.columnBreaker = 3;
        CoreData.instance.rowBreaker = 3;
        CoreData.instance.rainbowBreaker = 3;
        CoreData.instance.ovenBreaker = 3;
    }
    public int LevelNumber()
    {
      
        LeveltoPlay =  SkillzCrossPlatform.Random.Range(15, 40);
       
        return LeveltoPlay;
    }

    public void SetWinLevel()
    {
       int Level = StageLoader.instance.Stage;
        if (Level != oldLevel)
        {
            LoseLevel = 0;
            oldLevel = Level;
            WinLevel++;
        }
        else
        {
            LoseLevel++;
            WinLevel = 0;
        }
    }
    
   
    

    void OnDestroy()
    {
      
       
        Debug.Log("--APPLY SETTINGS");
    }


    public void deleteallkey()
    {
        PlayerPrefs.DeleteAll();
    }
    public void deleteDataFile()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Configuration.game_data))
        {         
           File.Delete(Application.persistentDataPath + "/" + Configuration.game_data);
        }

    }

    public static void OpenInfoPopup(string Title = null, string infotext = null)
    {
        if (Title != null)
            Configuration.instance.MessageBoardTitle = Title;

        if (infotext != null)
            Configuration.instance.MessageBoardInfo = infotext;

        Canvas PopupCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var InfoPrefab = Instantiate(Resources.Load("Prefabs/Map/InfoPopup")) as GameObject;

        InfoPrefab.transform.SetParent(PopupCanvas.transform, false);
    }

    public static void SaveAchievement(string id, int Count)
    {
        int curruentNum = 0;
        if (PlayerPrefs.HasKey(id))
        {
            curruentNum = PlayerPrefs.GetInt(id);

        }
        else
        {
            curruentNum = 0;
            PlayerPrefs.SetInt(id, 0);
            PlayerPrefs.Save();
        }

        PlayerPrefs.SetInt(id, curruentNum + Count);
        PlayerPrefs.Save();
    }

    public static void OpenInfoPopupAutoClose(string Title = null, string infotext = null, float time = 2)
    {
        if (Title != null)
            Configuration.instance.MessageBoardTitle = Title;

        if (infotext != null)
            Configuration.instance.MessageBoardInfo = infotext;


        Canvas PopupCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var InfoPrefab = Instantiate(Resources.Load("Prefabs/Map/InfoPopupAutoClose")) as GameObject;


        if (time != 0)
            InfoPrefab.GetComponent<Popup>().AutoCloseSeconds = time;

        AudioManager.instance.PopupSwoshAudio();
        InfoPrefab.transform.SetParent(PopupCanvas.transform, false);
    }

    public void OpenGiftBoxPopup(string Title, REWARD_TYPE Type)
    {
        //if (GameObject.Find("GiftBox(Clone)"))
        //{
        //    return;
        //}

        //GiftBoxTitle = Title;
        //rewardType = Type;
        //Canvas r_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //var rewardfab = Instantiate(Resources.Load("Prefabs/Map/GiftBox")) as GameObject;
        //rewardfab.transform.SetParent(r_canvas.transform, false);
    }
    public void SetCurrentEpisode()
    {

        int episode = Configuration.instance.episode;
        int[] EpisodesRange = Configuration.instance.EpisodesRange;
        int openedLevel = CoreData.instance.GetOpendedLevel();

        int MaxLevel = 0;

        for (int i = 0; i < episode; i++)
        {
            MaxLevel += EpisodesRange[i];
            if (openedLevel > MaxLevel)
            {
                CurrentEpisode = i + 1;
                PlayerPrefs.SetInt("CurrentEpisode", i + 1);
                PlayerPrefs.Save();
            }
        }

    }

    #region Prefab

    // node
    public static string NodePrefab()
    {
        return "Prefabs/Play/Node";
    }

    // tile
    public static string LightTilePrefab()
    {
        return "Prefabs/Play/TileLayer/LightTile";
    }

    public static string DarkTilePrefab()
    {
        return "Prefabs/Play/TileLayer/DarkTile";
    }

    public static string NoneTilePrefab()
    {
        return "Prefabs/Play/TileLayer/NoneTile";
    }

    // tile border
    public static string TileBorderTop()
    {
        return "Prefabs/Play/TileLayer/TopBorder/";
    }

    public static string TileBorderBottom()
    {
        return "Prefabs/Play/TileLayer/BottomBorder/";
    }

    public static string TileBorderLeft()
    {
        return "Prefabs/Play/TileLayer/LeftBorder/";
    }

    public static string TileBorderRight()
    {
        return "Prefabs/Play/TileLayer/RightBorder/";
    }

    // Blue Items
    public static string Item1()
    {
        return "Prefabs/items/bluebox";
    }

    public static string Item1Bomb()
    {
        return "Prefabs/items/blue_bomb";
    }

    public static string Item1Column()
    {
        return "Prefabs/items/blue_column";
    }

    public static string Item1Row()
    {
        return "Prefabs/items/blue_row";
    }

    public static string Item1Cross()
    {
        return "Prefabs/items/blue_cross";
    }

    // Green items
    public static string Item2()
    {
        return "Prefabs/items/greenbox";
    }

    // Orange items
    public static string Item3()
    {
        return "Prefabs/items/orangebox";
    }

    // Purple items
    public static string Item4()
    {
        return "Prefabs/items/purplebox";
    }


    // Red Items
    public static string Item5()
    {
        return "Prefabs/items/redbox";
    }

    // Yellow items
    public static string Item6()
    {
        return "Prefabs/items/yellowbox";
    }


    // Toy Color Cone
    public static string ItemColorCone()
    {
        return "Prefabs/items/colorcone";
    }
    public static string BigBomb()
    {
        return "Prefabs/items/big_bomb";
    }
    public static string MedBomb()
    {
        return "Prefabs/items/med_bomb";
    }
    public static string ColorHunter()
    {
        return "Prefabs/items/colorhunter";
    }
    public static string ColorHunter1()
    {
        return "Prefabs/items/colorhunter1";
    }
    public static string ColorHunter2()
    {
        return "Prefabs/items/colorhunter2";
    }
    public static string ColorHunter3()
    {
        return "Prefabs/items/colorhunter3";
    }
    public static string ColorHunter4()
    {
        return "Prefabs/items/colorhunter4";
    }
    public static string ColorHunter5()
    {
        return "Prefabs/items/colorhunter5";
    }
    public static string ColorHunter6()
    {
        return "Prefabs/items/colorhunter6";
    }
    // Breakable
    public static string Breakable()
    {
        return "Prefabs/Collectable Items/breakable";
    }
    public static string Breakable1()
    {
        return "Prefabs/Collectable Items/newitemprefab/breakable1";
    }
    public static string Breakable2()
    {
        return "Prefabs/Collectable Items/newitemprefab/breakable2";
    }
    public static string Breakable3()
    {
        return "Prefabs/Collectable Items/newitemprefab/breakable3";
    }
    public static string Breakable4()
    {
        return "Prefabs/Collectable Items/newitemprefab/breakable4";
    }
    public static string Breakable5()
    {
        return "Prefabs/Collectable Items/newitemprefab/breakable5";
    }
    public static string Breakable6()
    {
        return "Prefabs/Collectable Items/newitemprefab/breakable6";
    }
    public static string Breakable7()
    {
        return "Prefabs/Collectable Items/newitemprefab/breakable7";
    }
    public static string Breakable8()
    {
        return "Prefabs/Collectable Items/newitemprefab/breakable8";
    }
    //Rocket toy Collectable
    public static string Rocket1()
    {
        return "Prefabs/Collectable Items/rocket_1";
    }

    public static string Rocket2()
    {
        return "Prefabs/Collectable Items/rocket_2";
    }

    public static string Rocket3()
    {
        return "Prefabs/Collectable Items/rocket_3";
    }

    public static string Rocket4()
    {
        return "Prefabs/Collectable Items/rocket_4";
    }

    public static string Rocket5()
    {
        return "Prefabs/Collectable Items/rocket_5";
    }

    public static string Rocket6()
    {
        return "Prefabs/Collectable Items/rocket_6";
    }

    public static string RocketGeneric()
    {
        return "Prefabs/Collectable Items/rocket_generic";
    }

    // Toy Mines1

    public static string ToyMine1()
    {

        return "Prefabs/Collectable Items/toymine_1";
    }

    public static string ToyMine2()
    {
        return "Prefabs/Collectable Items/toymine_2";
    }

    public static string ToyMine3()
    {
        return "Prefabs/Collectable Items/toymine_3";
    }

    public static string ToyMine4()
    {
        return "Prefabs/Collectable Items/toymine_4";
    }

    public static string ToyMine5()
    {
        return "Prefabs/Collectable Items/toymine_5";
    }

    public static string ToyMine6()
    {
        return "Prefabs/Collectable Items/toymine_6";
    }


    // Toy Mines1a

    public static string ToyMine1a()
    {

        return "Prefabs/Collectable Items/newitemprefab/toymine_1a";
    }

    public static string ToyMine2a()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_2a";
    }

    public static string ToyMine3a()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_3a";
    }

    public static string ToyMine4a()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_4a";
    }

    public static string ToyMine5a()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_5a";
    }

    public static string ToyMine6a()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_6a";
    }


    // Toy Mines1b

    public static string ToyMine1b()
    {

        return "Prefabs/Collectable Items/newitemprefab/toymine_1b";
    }

    public static string ToyMine2b()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_2b";
    }

    public static string ToyMine3b()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_3b";
    }

    public static string ToyMine4b()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_4b";
    }

    public static string ToyMine5b()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_5b";
    }

    public static string ToyMine6b()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_6b";
    }

    // Toy Mines1c

    public static string ToyMine1c()
    {

        return "Prefabs/Collectable Items/newitemprefab/toymine_1c";
    }

    public static string ToyMine2c()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_2c";
    }

    public static string ToyMine3c()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_3c";
    }

    public static string ToyMine4c()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_4c";
    }

    public static string ToyMine5c()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_5c";
    }

    public static string ToyMine6c()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_6c";
    }

    // Toy Mines1d

    public static string ToyMine1d()
    {

        return "Prefabs/Collectable Items/newitemprefab/toymine_1d";
    }

    public static string ToyMine2d()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_2d";
    }

    public static string ToyMine3d()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_3d";
    }

    public static string ToyMine4d()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_4d";
    }

    public static string ToyMine5d()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_5d";
    }

    public static string ToyMine6d()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_6d";
    }

    // Toy Mines1e

    public static string ToyMine1e()
    {

        return "Prefabs/Collectable Items/newitemprefab/toymine_1e";
    }

    public static string ToyMine2e()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_2e";
    }

    public static string ToyMine3e()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_3e";
    }

    public static string ToyMine4e()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_4e";
    }

    public static string ToyMine5e()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_5e";
    }

    public static string ToyMine6e()
    {
        return "Prefabs/Collectable Items/newitemprefab/toymine_6e";
    }

    // rock candy
    public static string LegoBox1()
    {
        return "Prefabs/Collectable Items/lego_box_1";
    }

    public static string LegoBox2()
    {
        return "Prefabs/Collectable Items/lego_box_2";
    }

    public static string LegoBox3()
    {
        return "Prefabs/Collectable Items/lego_box_3";
    }

    public static string LegoBox4()
    {
        return "Prefabs/Collectable Items/lego_box_4";
    }

    public static string LegoBox5()
    {
        return "Prefabs/Collectable Items/lego_box_5";
    }

    public static string LegoBox6()
    {
        return "Prefabs/Collectable Items/lego_box_6";
    }

    public static string LegoBoxGeneric()
    {
        return "Prefabs/Collectable Items/lego_box_generic";
    }

    // rock candy A
    public static string LegoBox1a()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_1a";
    }

    public static string LegoBox2a()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_2a";
    }

    public static string LegoBox3a()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_3a";
    }

    public static string LegoBox4a()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_4a";
    }

    public static string LegoBox5a()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_5a";
    }

    public static string LegoBox6a()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_6a";
    }

    public static string LegoBoxGenerica()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_generica";
    }

    // rock candy B
    public static string LegoBox1b()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_1b";
    }

    public static string LegoBox2b()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_2b";
    }

    public static string LegoBox3b()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_3b";
    }

    public static string LegoBox4b()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_4b";
    }

    public static string LegoBox5b()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_5b";
    }

    public static string LegoBox6b()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_6b";
    }

    public static string LegoBoxGenericb()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_genericb";
    }


    // rock candy C
    public static string LegoBox1c()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_1c";
    }

    public static string LegoBox2c()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_2c";
    }

    public static string LegoBox3c()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_3c";
    }

    public static string LegoBox4c()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_4c";
    }

    public static string LegoBox5c()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_5c";
    }

    public static string LegoBox6c()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_6c";
    }

    public static string LegoBoxGenericc()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_genericc";
    }

    // rock candy D
    public static string LegoBox1d()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_1d";
    }

    public static string LegoBox2d()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_2d";
    }

    public static string LegoBox3d()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_3d";
    }

    public static string LegoBox4d()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_4d";
    }

    public static string LegoBox5d()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_5d";
    }

    public static string LegoBox6d()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_6d";
    }

    public static string LegoBoxGenericd()
    {
        return "Prefabs/Collectable Items/newitemprefab/lego_box_genericd";
    }


    // collectible
    public static string Collectible1()
    {
        return "Prefabs/Collectable Items/toy_1";
    }

    public static string Collectible2()
    {
        return "Prefabs/Collectable Items/toy_2";
    }

    public static string Collectible3()
    {
        return "Prefabs/Collectable Items/toy_3";
    }

    public static string Collectible4()
    {
        return "Prefabs/Collectable Items/toy_4";
    }

    public static string Collectible5()
    {
        return "Prefabs/Collectable Items/toy_5";
    }

    public static string Collectible6()
    {
        return "Prefabs/Collectable Items/toy_6";
    }

    public static string Collectible7()
    {
        return "Prefabs/Collectable Items/toy_7";
    }

    public static string Collectible8()
    {
        return "Prefabs/Collectable Items/toy_8";
    }

    public static string Collectible9()
    {
        return "Prefabs/Collectable Items/toy_9";
    }

    public static string Collectible10()
    {
        return "Prefabs/Collectable Items/toy_10";
    }

    public static string Collectible11()
    {
        return "Prefabs/Collectable Items/toy_11";
    }

    public static string Collectible12()
    {
        return "Prefabs/Collectable Items/toy_12";
    }

    public static string Collectible13()
    {
        return "Prefabs/Collectable Items/toy_13";
    }

    public static string Collectible14()
    {
        return "Prefabs/Collectable Items/toy_14";
    }

    public static string Collectible15()
    {
        return "Prefabs/Collectable Items/toy_15";
    }

    public static string Collectible16()
    {
        return "Prefabs/Collectable Items/toy_16";
    }

    public static string Collectible17()
    {
        return "Prefabs/Collectable Items/toy_17";
    }

    public static string Collectible18()
    {
        return "Prefabs/Collectable Items/toy_18";
    }

    public static string Collectible19()
    {
        return "Prefabs/Collectable Items/toy_19";
    }

    public static string Collectible20()
    {
        return "Prefabs/Collectable Items/toy_20";
    }

    public static string CollectibleBox()
    {
        return "Prefabs/Collectable Items/toy_box";
    }

    // blue box explosions effects
    public static string BlueBoxExplosion()
    {
        return "Effects/Bluebox Explosion";
    }

    public static string GreenBoxExplosion()
    {
        return "Effects/Greenbox Explosion";
    }

    public static string OrangeCookieExplosion()
    {
        return "Effects/Orangebox Explosion";
    }

    public static string PurpleCookieExplosion()
    {
        return "Effects/Purplebox Explosion";
    }

    public static string RedCookieExplosion()
    {
        return "Effects/Redbox Explosion";
    }

    public static string YellowCookieExplosion()
    {
        return "Effects/Yellowbox Explosion";
    }

    // breaker explosion effects
    public static string BreakerExplosion1()
    {
        return "Effects/Blue Bomb Explosion";
    }

    public static string BreakerExplosion2()
    {
        return "Effects/Green Bomb Explosion";
    }

    public static string BreakerExplosion3()
    {
        return "Effects/Orange Bomb Explosion";
    }

    public static string BreakerExplosion4()
    {
        return "Effects/Purple Bomb Explosion";
    }

    public static string BreakerExplosion5()
    {
        return "Effects/Red Bomb Explosion";
    }

    public static string BreakerExplosion6()
    {
        return "Effects/Yellow Bomb Explosion";
    }

    // generic
    public static string ColumnRowBreaker()
    {
        return "Prefabs/Collectable Items/column_row_breaker";
    }

    public static string GenericBombBreaker()
    {
        return "Prefabs/Collectable Items/generic_bomb";
    }

    public static string GenericXBreaker()
    {
        return "Prefabs/Collectable Items/generic_cross";
    }

    // rainbow explosion
    public static string RainbowExplosion()
    {
        return "Effects/GlowStars";
    }

    // ring
    public static string RingExplosion()
    {
        return "Effects/Moves Ring";
    }

    // column explosion
    public static string ColRowBreaker1()
    {
        return "Effects/Blue Striped";
    }

    // Breakable explosion
    public static string BreakableExplosion()
    {
        return "Effects/breakable Explosion";
    }

    // chocolate explosion
    public static string MineExplosion()
    {
        return "Effects/toymine Explosion";
    }

    public static string ColRowBreaker2()
    {
        return "Effects/Green Striped";
    }

    public static string ColRowBreaker3()
    {
        return "Effects/Orange Striped";
    }

    public static string ColRowBreaker4()
    {
        return "Effects/Purple Striped";
    }

    public static string ColRowBreaker5()
    {
        return "Effects/Red Striped";
    }

    public static string ColRowBreaker6()
    {
        return "Effects/Yellow Striped";
    }

    // booster
    public static string BoosterActive()
    {
        return "Effects/Booster Active";
    }

    // column breaker animation
    public static string ColumnBreakerAnimation1()
    {
        return "Stripes/StripeAnim1";
    }

    public static string ColumnBreakerAnimation2()
    {
        return "Stripes/StripeAnim2";
    }

    public static string ColumnBreakerAnimation3()
    {
        return "Stripes/StripeAnim3";
    }

    public static string ColumnBreakerAnimation4()
    {
        return "Stripes/StripeAnim4";
    }

    public static string ColumnBreakerAnimation5()
    {
        return "Stripes/StripeAnim5";
    }

    public static string ColumnBreakerAnimation6()
    {
        return "Stripes/StripeAnim6";
    }

    // big column breaker animation
    public static string BigColumnBreakerAnimation1()
    {
        return "Stripes/BigStripeAnim1";
    }

    public static string BigColumnBreakerAnimation2()
    {
        return "Stripes/BigStripeAnim2";
    }

    public static string BigColumnBreakerAnimation3()
    {
        return "Stripes/BigStripeAnim3";
    }

    public static string BigColumnBreakerAnimation4()
    {
        return "Stripes/BigStripeAnim4";
    }

    public static string BigColumnBreakerAnimation5()
    {
        return "Stripes/BigStripeAnim5";
    }

    public static string BigColumnBreakerAnimation6()
    {
        return "Stripes/BigStripeAnim6";
    }

    // waffle

    public static string Waffle1()
    {
        return "Prefabs/Collectable Items/waffle_1";
    }

    public static string Waffle2()
    {
        return "Prefabs/Collectable Items/waffle_2";
    }

    public static string Waffle3()
    {
        return "Prefabs/Collectable Items/waffle_3";
    }

    // waffle a

    public static string Waffle1a()
    {
        return "Prefabs/Collectable Items/newitemprefab/waffle_1a";
    }

    public static string Waffle2a()
    {
        return "Prefabs/Collectable Items/newitemprefab/waffle_2a";
    }

    public static string Waffle3a()
    {
        return "Prefabs/Collectable Items/newitemprefab/waffle_3a";
    }

    // waffle b

    public static string Waffle1b()
    {
        return "Prefabs/Collectable Items/newitemprefab/waffle_1b";
    }

    public static string Waffle2b()
    {
        return "Prefabs/Collectable Items/newitemprefab/waffle_2b";
    }

    public static string Waffle3b()
    {
        return "Prefabs/Collectable Items/newitemprefab/waffle_3b";
    }

    // Lock
    public static string Lock1()
    {
        return "Prefabs/Collectable Items/Locked";
    }

    // cake
    public static string Doll(string name)
    {
        return "Doll/" + name;

    }

    // star
    public static string StarGold()
    {
        return "Prefabs/Play/UI/StarGold";
    }
    public static string StarGoldFX()
    {
        return "Effects/StarGoldFX";
    }


    // mask
    public static string Mask()
    {
        return "Prefabs/Play/Mask";
    }



    // Loading Image
    public static string LoadingImage()
    {
        return "Prefabs/Map/Loading Image";
    }
    // Map Scene
    public static string promoWindow600()
    {
        return "Prefabs/Promo/promoWindow600";
    }
    public static string promoWindow1200()
    {
        return "Prefabs/Promo/promoWindow1200";
    }

    public static string MoregamesTB()
    {
        return "Prefabs/Promo/TB";
    }
    public static string MoregamesCB()
    {
        return "Prefabs/Promo/CB";
    }
    public static string MoregamesMB()
    {
        return "Prefabs/Promo/MB";
    }
    public static string MoregamesHOB()
    {
        return "Prefabs/Promo/HOB";
    }
    public static string ShowStarChallenge()
    {
        return "Prefabs/Promo/StarChallengePopup";
    }
    public static string ShowMissionChallenge()
    {
        return "Prefabs/Promo/MissionChallengePopup";
    }
    public static string ShowMiniMissionChallenge()
    {
        return "Prefabs/Promo/MiniMissionChallengePopup";
    }
    public static string Newgames()
    {
        return "Prefabs/Promo/Newgames";
    }
    public static string promoWindowPromo0()
    {
        return "Prefabs/Promo/promoWindowPack4";
    }
    public static string promoWindowPromo1()
    {
        return "Prefabs/Promo/promoWindowPack2";
    }
    public static string promoWindowPromo2()
    {
        return "Prefabs/Promo/promoWindow1200";
    }
    public static string NewEpisodePopup()
    {
        return "Prefabs/Promo/NewEpisodePopup";
    }
    public static string ThankYouForPurchase()
    {
        return "Prefabs/Promo/ThankYouForPurchase";
    }
    public static string PurchaseFailed()
    {
        return "Prefabs/Promo/PurchaseFailed";
    }
    public static string NewArenaPopup()
    {
        return "Prefabs/Promo/NewArenaPopup";
    }
    public static string GiftBoxPopup()
    {
        return "Prefabs/Promo/GiftBoxPopup";
    }
    public static string ArenaPopup()
    {
        return "Prefabs/Promo/ArenaPopup";
    }
    public static string Help()
    {
        return "Prefabs/TutorialScreens";
    }
    //PAGES

    public static string page1()
    {
        return "Prefabs/PAGES/page1";
    }
    public static string page2()
    {
        return "Prefabs/PAGES/page2";
    }
    public static string page3()
    {
        return "Prefabs/PAGES/page3";
    }
    public static string page4()
    {
        return "Prefabs/PAGES/page4";
    }
    public static string page5()
    {
        return "Prefabs/PAGES/page5";
    }
    public static string page6()
    {
        return "Prefabs/PAGES/page6";
    }
    public static string page7()
    {
        return "Prefabs/PAGES/page7";
    }

    //Episodes




    // Progress gold star
    public static string ProgressGoldStar()
    {
        return "Prefabs/Play/UI/StarGold";
    }

    // Win pop up star explode
    public static string StarExplode()
    {
        return "Effects/StarExplode";
    }

    #endregion


    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        //if (!pauseStatus)
        //{
        //    try
        //    {
        //        if (FB.IsInitialized)
        //        {
        //            FB.ActivateApp();
        //        }
        //        else
        //        {
        //            //Handle FB.Init
        //            FB.Init(() => {
        //                FB.ActivateApp();
        //            });
        //        }
        //    }
        //    catch 
        //    {

                
        //    }
            //app resume
           
        //}
    }

}
#if UNITY_EDITOR
[CustomEditor(typeof(Configuration))]
public class ConfigurationEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Configuration myScript = (Configuration)target;
        if (GUILayout.Button("Reset All Pref"))
        {
            myScript.deleteallkey();
        }
        if (GUILayout.Button("Delete Data Fies"))
        {
            myScript.deleteDataFile();
        }
    }
}

#endif

