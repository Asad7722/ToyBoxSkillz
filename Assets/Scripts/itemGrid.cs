using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.Analytics;
//using AppsFlyerSDK;


public class itemGrid : MonoBehaviour
{
    public static itemGrid instance = null;
    protected Canvas m_canvas;
    public GameObject touchRippleEffect;
 
    public int poolSize = 10;


    private List<GameObject> pool;
    public bool helpBoard;
    public int CallReset;
    public bool ResetBoardApply;
    public bool Destroyed;
    public float CallResetBoardTime = 5f;
    public bool balanced;
    public int LevelChallengeWinNum;
    [Header("Help")]
    public bool SkipTutuorial;
    public bool listenersinglebreaker;
    public Item TapCube;
    public GameObject NextButon;
    private bool Next;

    [Header("Nodes")]
    public List<Node> nodes = new List<Node>();

    [Header("Board variables")]
    public GAME_STATE state;
    public bool RewardBooster = false;
    public static bool MovesReward = false;
    public static bool RewardMoreMoveUsed = false;
    public int RewardBoosterNum = 4;
    public Text RewardBoosterNumText;
    public bool lockSwapHelp;
    public bool lockSwap;
    public bool merging;
    public bool huntering;
    public bool GameOver;
    public bool boosterdestroying;
    public int moveLeft;
    public int dropTime;
    public int score;
    public int star;
    public int stars;
    public int target1Left;
    public int target2Left;
    public int target3Left;
    public int target4Left;



    [Header("Booster")]
    public BOOSTER_TYPE booster;
    public List<Item> boosterItems = new List<Item>();
    public Item ovenTouchItem;

    [Header("Check")]
    public int destroyingItems;
    public int droppingItems;
    public int flyingItems;
    public int matching;

    [Header("collectable items")]
    public bool movingGingerbread;
    public bool generatingGingerbread;
    public bool skipGenerateGingerbread;
    public bool showingInspiringPopup;
    public int skipGingerbreadCount;

    [Header("Item Lists")]
    public List<Item> changingList;
    public List<Item> sameColorList;
    public List<Item> MatchesBoosterKomsuList;
    public List<ITEM_TYPE> MatchesBoosterKomsuTypeList;

    public List<Item> MatchesKomsuList;

    [Header("Swap")]
    public Item touchedItem;
    public Item swappedItem;

    // UI
    [Header("UI")]
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;

    public UITarget UITarget;
    public UITop UITop;

    // Popup
    [Header("Popup")]
    public PopupOpener targetPopup;
    public PopupOpener timeupPopup;

    public PopupOpener completedPopup;
    public PopupOpener winPopup;
    public PopupOpener losePopup;
    public PopupOpener noMatchesPopup;
    public PopupOpener plus5MovesPopup;
    public PopupOpener excellentPopup;
    public PopupOpener greatPopup;
    public PopupOpener awesomePopup;
    public PopupOpener brilliantPopup;
    public PopupOpener fabPopup;
    public PopupOpener morePopup;

    // hint
    [Header("Hint")]
    public int checkHintCall;
    public int showHintCall;
    public List<Item> hintItems = new List<Item>();

    // private
    Vector3 firstNodePosition;

    public GameObject BackMusic;
    public List<Item> TutorialCubesList;

    //public List<int> randomNumbers;

    #region START
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }


    }

    void Start()
    {
        Application.targetFrameRate = 60;
        Configuration.instance.CurrentScene = CURRENT_SCENE.PLAY;
        NextButon.SetActive(false);
        Configuration.instance.CloseClick = true;
        RewardMoreMoveUsed = false;
        MovesReward = false;
        Configuration.instance.WinStarAmount = 0;
        Configuration.instance.WinScoreAmount = 0;
        Configuration.instance.MenuToMap = false;
        Configuration.instance.PlayedLevel++;
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject ripple = Instantiate(touchRippleEffect);
            ripple.SetActive(false);  // deactivate initially
            pool.Add(ripple);
        }
        //for (int i = 0; i < 500; i++)
        //{
        //    int q = SkillzCrossPlatform.Random.Range(0, 0);
        //}

        int openedLevel = CoreData.instance.GetOpendedLevel();
        if (Configuration.instance.EpisodePlay)
        {
            if (openedLevel > Configuration.instance.maxLevel)
            {
                int currentStage = StageLoader.instance.Stage + 1448;
                //Unity Analytic Level Start Rapor        
               

             

               

            }
            else
            {
                //Unity Analytic Level Start Rapor        
               

         
            } 
        }

        Configuration.instance.SetWinLevel();
        BackgroundMusic.instance.GamePlayMusic();

        try
        {
            RewardBoosterNumText.text = "" + RewardBoosterNum;
        }
        catch
        {
        }

        if (StageLoader.instance.Stage > 1)
        {
            int playlevelcount = PlayerPrefs.GetInt("playlevelcount");

            if (playlevelcount > 100)
            {
                PlayerPrefs.SetInt("playlevelcount", 1);
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetInt("playlevelcount", playlevelcount + 1);
                PlayerPrefs.Save();
            }
        }

        state = GAME_STATE.PREPARING_LEVEL;
        moveLeft = StageLoader.instance.moves;
        target1Left = StageLoader.instance.target1Amount;
        target2Left = StageLoader.instance.target2Amount;
        target3Left = StageLoader.instance.target3Amount;
        target4Left = StageLoader.instance.target4Amount;

        GenerateBoard();
        state = GAME_STATE.WAITING_USER_SWAP;
        TargetPopup();
        Timer.instance.SetTimeoutAction(()=>
        {
         
            TimeupPopup();

         });
        //Life CALL
        if (Configuration.instance.LifeCall && PlayerPrefs.GetInt("isNotifEnabled") == 1 && GameObject.Find("NewLife"))
        {
            if (NewLife.instance.lives < NewLife.instance.maxLives)
            {
                if (!InitCheck())
                    return;
                NotificationsSetup.NotificationLifeCall();
            }
        }

    }
    public GameObject GetRipple()
    {
        foreach (GameObject ripple in pool)
        {
            if (!ripple.activeInHierarchy)
            {
                ripple.SetActive(true);
                return ripple;
            }
        }

        // Optional: if all are active, instantiate new one or return null
        GameObject newRipple = Instantiate(touchRippleEffect);
        pool.Add(newRipple);
        return newRipple;
    }
    ////LEVEL EVENTS
    //public void appsFlyerLevelStart(string levelNum)
    //{
    //    Dictionary<string, string> LevelStart = new
    //          Dictionary<string, string>();
    //    LevelStart.Add("Level Start", levelNum);
    //    AppsFlyer.sendEvent("started level "+ levelNum, LevelStart);
    //    AppsFlyer.sendEvent("LEVEL FUNNEL", LevelStart);

    //}




    //public void LogLevelStartEvent(string levelNum)
    //{
    //    var parameters = new Dictionary<string, object>();
    //    parameters["Level Start"] = levelNum;
    //    FB.LogAppEvent(
    //        "started level " + levelNum,
    //        1,
    //        parameters
    //    );

    //    FB.LogAppEvent(
    //       "LEVEL FUNNEL",
    //       1,
    //       parameters
    //   );
    //}

    //LEVEL EVENTS


    public void PositionReset()
    {
        foreach (var item in GetListItems())
        {
            if (item != null)
            {
                item.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10 - item.node.i;
            }
        }
    }


    #endregion

    #region HELP
    public void ShowHelp()
    {
        if (Configuration.instance.SkipTutuorial)
        {

            return;
        }

        //SHOW HELP
        bool giftbox = Configuration.instance.GiftBox;
        bool randomplay = Configuration.instance.RandomPlay;
        bool arenaplay = Configuration.instance.ArenaMode;
        if (!giftbox && !randomplay && !arenaplay)
        {
            int Stage = StageLoader.instance.Stage;
            if (Stage == 1 ||
                Stage == 2 ||
                Stage == 3 ||
                Stage == 4 ||
                Stage == 5 ||
                Stage == 6 ||
                Stage == 7 ||
                Stage == 8 ||
                Stage == 9 ||
                Stage == 11 ||
                Stage == 13 ||
                Stage == 15 ||
                Stage == 18 ||
                Stage == 20 ||
                Stage == 23 ||
                Stage == 27 ||
                Stage == 29 ||
                Stage == 40)
            {
                StartGetHelpPopup(Stage);
            }
        }

    }
    //sameer
    public void StartGetHelpPopup(int helpNumber = 0, string text = null)
    {
        switch (helpNumber)
        {
            case 1: //tap2  
                StartCoroutine(ShowHelpTap2());
                break;

                //case 2: //bomb
                //    StartCoroutine(ShowHelpBomb());
                //    break;

                //case 3: //rocket
                //    StartCoroutine(ShowHelpRocket());
                //    break;

                //case 4: //xbreaker
                //    StartCoroutine(ShowHelpXbreaker());
                //    break;

                //case 5: //big bomb
                //    StartCoroutine(ShowHelpBigBomb());
                //    break;

                //case 6://collect toy and color hunter
                //    StartCoroutine(ShowHelpColorHunterAndCollectToy());
                //    break;

                //case 7: //merge booster                              
                //    StartCoroutine(ShowHelpMergeBoosters());
                //    break;

                //case 8: //merge booster  

                //    StartCoroutine(ShowHelpBoosters("Single Booster", "SINGLE BREAKER! \n Destroys a cube!"));

                //    break;

                //case 9: //ice
                //    StartCoroutine(ShowHelpItem(ITEM_TYPE.NONE, true, false, false, false, false, false, "Tap the cubes next to a ice to collect it!"));
                //    break;

                //case 11: //row                
                //    StartCoroutine(ShowHelpBoosters("Row Booster", "ROW BREAKER! \n Destroys a row!"));
                //    break;

                //case 13: //column               
                //    StartCoroutine(ShowHelpBoosters("Column Booster", "COLUMN BREAKER! \n Destroy a column!"));
                //    break;

                //case 15: //bubble
                //    StartCoroutine(ShowHelpItem(ITEM_TYPE.NONE, false, true, false, false, false, false, "Tap the cubes next to a bubble to collect it!"));
                //    break;

                //case 18: //chocolate
                //    StartCoroutine(ShowHelpItem(ITEM_TYPE.NONE, false, false, true, false, false, false, "Tap the cubes next to a chocolate to collect it!"));
                //    break;

                //case 20: //COLOR BREAKER               
                //    StartCoroutine(ShowHelpBoosters("Rainbow Booster", "COLOR BREAKER! \n It destroys cubes of the same color you choose!"));
                //    break;

                //case 23: //BLENDER BALL
                //    StartCoroutine(ShowHelpBoosters("Oven Booster", "BLENDER BALL! \n shuffles all the cubes!"));
                //    break;

                //case 27: //Ball
                //    StartCoroutine(ShowHelpItem(ITEM_TYPE.NONE, false, false, false, false, true, false, "Tap the same colored cubes next to a Ball to collect!"));
                //    break;

                //case 29: //lego
                //    StartCoroutine(ShowHelpItem(ITEM_TYPE.NONE, false, false, false, false, false, true, "Tap the same colored cubes next to a item to collect!"));
                //    break;

                //case 40: //lock
                //    StartCoroutine(ShowHelpItem(ITEM_TYPE.NONE, false, false, false, true, false, false, "Tap the same colored cubes next to a LOCK to collect!"));
                //    break;

                //default:
                //    Configuration.instance.MessageBoardInfo = text;
                //    break;
        }
    }

    //ShowHelpTap2
    IEnumerator ShowHelpTap2()
    {
        helpBoard = true;
        Configuration.instance.pause = true;
        Configuration.instance.MessageBoardInfo = "Tap 2 or more same colored cubes";

        TutorialCubesList.Add(nodes[39].item);
        TutorialCubesList.Add(nodes[40].item);

        FocusObject();

        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        HelpPrefab();
        HandBoardPrefab(nodes[40].item.gameObject);

        //Wait user tap
        yield return new WaitForSeconds(0.5f);
        Configuration.instance.pause = false;
        if (TutorialCubesList.Count > 0)
        {
            while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
            {
                yield return null;
            }
        }

        Configuration.instance.pause = true;
        yield return StartCoroutine(StartCloseHelp());

        Configuration.instance.MessageBoardInfo = "Collect the remaining items to complete goals.";
        if (!m_canvas) m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        DiffusePrefab(GameObject.Find("DiffuseLayer"));

        var goal = GameObject.Find("GOAL");

        yield return new WaitForSeconds(0.5f);
        ArrowPrefab(goal, -60);
        HelpPrefab(GameObject.Find("TOPCENTER"), 0, true);

        //while (!Input.GetMouseButtonUp(0))
        //{
        //    yield return null;
        //}

        //yield return StartCoroutine(StartCloseHelp());
        helpBoard = false;
    }

    //ShowHelpBomb
    IEnumerator ShowHelpBomb()
    {
        helpBoard = true;
        Configuration.instance.pause = true;
        Configuration.instance.MessageBoardInfo = "Tap 5 same colored cubes to make a BOMB!";

        TutorialCubesList.Add(nodes[31].item);
        TutorialCubesList.Add(nodes[39].item);
        TutorialCubesList.Add(nodes[40].item);
        TutorialCubesList.Add(nodes[48].item);
        TutorialCubesList.Add(nodes[49].item);

        FocusObject();

        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        HelpPrefab();
        HandBoardPrefab(nodes[49].item.gameObject);

        //Wait user tap
        yield return new WaitForSeconds(0.5f);
        Configuration.instance.pause = false;
        if (TutorialCubesList.Count > 0)
        {
            while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
            {
                yield return null;
            }
        }
        Configuration.instance.pause = true;
        yield return StartCoroutine(StartCloseHelp());


        while (flyingItems > 0 || huntering || destroyingItems > 0 || merging || droppingItems > 0)
        {
            yield return null;
        }

        Configuration.instance.MessageBoardInfo = "Tap the Bomb!";
        yield return new WaitForSeconds(0.5f);
        foreach (var itm in GetListItems())
        {
            if (itm != null && itm.type == ITEM_TYPE.ITEM_BOMB)
            {
                TutorialCubesList.Add(itm);
                DiffusePrefab(GameObject.Find("DiffuseLayer"));
                itm.gameObject.layer = 5;
                itm.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";

                HandBoardPrefab(itm.gameObject);
                HelpPrefab();

                //Wait user tap
                yield return new WaitForSeconds(0.5f);
                Configuration.instance.pause = false;
                if (TutorialCubesList.Count > 0)
                {
                    while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
                    {
                        yield return null;
                    }
                }
                break;
            }
            else
            {
                Configuration.instance.pause = false;
            }
        }


        yield return StartCoroutine(StartCloseHelp());
        helpBoard = false;
    }

    //ShowHelpRocket
    IEnumerator ShowHelpRocket()
    {
        helpBoard = true;
        Configuration.instance.pause = true;
        Configuration.instance.MessageBoardInfo = "Tap 7 same colored cubes to make a ROCKET!";

        TutorialCubesList.Add(nodes[30].item);
        TutorialCubesList.Add(nodes[39].item);
        TutorialCubesList.Add(nodes[40].item);
        TutorialCubesList.Add(nodes[41].item);
        TutorialCubesList.Add(nodes[48].item);
        TutorialCubesList.Add(nodes[49].item);
        TutorialCubesList.Add(nodes[50].item);


        FocusObject();

        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        HelpPrefab();
        HandBoardPrefab(nodes[40].item.gameObject);


        //Wait user tap
        yield return new WaitForSeconds(0.5f);
        Configuration.instance.pause = false;
        if (TutorialCubesList.Count > 0)
        {
            while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
            {
                yield return null;
            }
        }
        Configuration.instance.pause = true;
        yield return StartCoroutine(StartCloseHelp());

        while (flyingItems > 0 || huntering || destroyingItems > 0 || merging || droppingItems > 0)
        {
            yield return null;
        }

        Configuration.instance.MessageBoardInfo = "OK! Now Tap the ROCKET!";
        yield return new WaitForSeconds(0.5f);
        foreach (var itm in GetListItems())
        {
            if (itm != null && (itm.type == ITEM_TYPE.ITEM_COLUMN || itm.type == ITEM_TYPE.ITEM_ROW))
            {
                TutorialCubesList.Add(itm);
                DiffusePrefab(GameObject.Find("DiffuseLayer"));
                itm.gameObject.layer = 5;
                itm.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                HandBoardPrefab(itm.gameObject);
                HelpPrefab();

                //Wait user tap
                yield return new WaitForSeconds(0.5f);
                Configuration.instance.pause = false;
                if (TutorialCubesList.Count > 0)
                {
                    while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
                    {
                        yield return null;
                    }
                }
                break;
            }
            else
            {
                Configuration.instance.pause = false;
            }
        }



        yield return StartCoroutine(StartCloseHelp());
        helpBoard = false;
    }

    //ShowHelpXbreaker
    IEnumerator ShowHelpXbreaker()
    {
        helpBoard = true;
        Configuration.instance.pause = true;
        Configuration.instance.MessageBoardInfo = "Tap 8 same colored cubes to make a X BREAKER!";

        TutorialCubesList.Add(nodes[30].item);
        TutorialCubesList.Add(nodes[31].item);
        TutorialCubesList.Add(nodes[32].item);
        TutorialCubesList.Add(nodes[38].item);
        TutorialCubesList.Add(nodes[39].item);
        TutorialCubesList.Add(nodes[40].item);
        TutorialCubesList.Add(nodes[41].item);
        TutorialCubesList.Add(nodes[42].item);


        FocusObject();

        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        HelpPrefab();
        HandBoardPrefab(nodes[40].item.gameObject);


        //Wait user tap
        yield return new WaitForSeconds(0.5f);
        Configuration.instance.pause = false;
        if (TutorialCubesList.Count > 0)
        {
            while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
            {
                yield return null;
            }
        }
        Configuration.instance.pause = true;
        yield return StartCoroutine(StartCloseHelp());

        while (flyingItems > 0 || huntering || destroyingItems > 0 || merging || droppingItems > 0)
        {
            yield return null;
        }


        Configuration.instance.MessageBoardInfo = "Tap the X BREAKER!";
        yield return new WaitForSeconds(0.5f);
        foreach (var itm in GetListItems())
        {
            if (itm != null && itm.type == ITEM_TYPE.ITEM_CROSS)
            {
                TutorialCubesList.Add(itm);
                DiffusePrefab(GameObject.Find("DiffuseLayer"));
                itm.gameObject.layer = 5;
                itm.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                HandBoardPrefab(itm.gameObject);
                HelpPrefab();
                //Wait user tap
                yield return new WaitForSeconds(0.5f);
                Configuration.instance.pause = false;
                if (TutorialCubesList.Count > 0)
                {
                    while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
                    {
                        yield return null;
                    }
                }
                break;
            }
            else
            {
                Configuration.instance.pause = false;
            }
        }



        yield return StartCoroutine(StartCloseHelp());
        helpBoard = false;
    }

    //ShowHelpBigBomb
    IEnumerator ShowHelpBigBomb()
    {
        helpBoard = true;
        Configuration.instance.pause = true;
        Configuration.instance.MessageBoardInfo = "Tap 9 same colored cubes to make a BIG BOMB!";

        TutorialCubesList.Add(nodes[30].item);
        TutorialCubesList.Add(nodes[31].item);
        TutorialCubesList.Add(nodes[32].item);
        TutorialCubesList.Add(nodes[39].item);
        TutorialCubesList.Add(nodes[40].item);
        TutorialCubesList.Add(nodes[41].item);
        TutorialCubesList.Add(nodes[48].item);
        TutorialCubesList.Add(nodes[49].item);
        TutorialCubesList.Add(nodes[50].item);


        FocusObject();

        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        HelpPrefab();
        HandBoardPrefab(nodes[40].item.gameObject);


        //Wait user tap
        yield return new WaitForSeconds(0.5f);
        Configuration.instance.pause = false;
        if (TutorialCubesList.Count > 0)
        {
            while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
            {
                yield return null;
            }
        }
        Configuration.instance.pause = true;
        yield return StartCoroutine(StartCloseHelp());


        while (flyingItems > 0 || huntering || destroyingItems > 0 || merging || droppingItems > 0)
        {
            yield return null;
        }


        Configuration.instance.MessageBoardInfo = "Tap the BIG BOMB!";
        yield return new WaitForSeconds(0.5f);
        foreach (var itm in GetListItems())
        {
            if (itm != null && itm.type == ITEM_TYPE.ITEM_MEDBOMB)
            {
                TutorialCubesList.Add(itm);
                DiffusePrefab(GameObject.Find("DiffuseLayer"));
                itm.gameObject.layer = 5;
                itm.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                HandBoardPrefab(itm.gameObject);
                HelpPrefab();

                //Wait user tap
                yield return new WaitForSeconds(0.5f);
                Configuration.instance.pause = false;
                if (TutorialCubesList.Count > 0)
                {
                    while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
                    {
                        yield return null;
                    }
                }
                break;
            }
            else
            {
                Configuration.instance.pause = false;
            }
        }


        yield return StartCoroutine(StartCloseHelp());
        helpBoard = false;
    }

    //ShowHelpColorHunterAndCollectToy
    IEnumerator ShowHelpColorHunterAndCollectToy()
    {
        Configuration.instance.MessageBoardInfo = "Collect the Toys";
        lockSwapHelp = true;
        Configuration.instance.pause = true;
        yield return new WaitForSeconds(0.5f);
        foreach (var itm in GetListItems())
        {
            if (itm != null && itm.IsCollectible())
            {
                TutorialCubesList.Add(itm);
                //HandBoardPrefab(itm.gameObject);

            }
        }
        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        HelpPrefab();
        FocusObject();
        if (!m_canvas) m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var Target2 = GameObject.Find("GOAL");
        ArrowPrefab(Target2, -60);

        yield return new WaitForSeconds(0.5f);
        Configuration.instance.pause = false;
        huntering = true;
        while (!Input.GetMouseButtonUp(0) && !Configuration.instance.pause)
        {
            yield return null;
        }
        huntering = false;
        Configuration.instance.pause = true;
        yield return StartCoroutine(StartCloseHelp());
        helpBoard = true;
        //show cubes
        Configuration.instance.MessageBoardInfo = "Tap 10 same colored cubes to make a COLOR HUNTER!";
        TutorialCubesList.Add(nodes[29].item);
        TutorialCubesList.Add(nodes[30].item);
        TutorialCubesList.Add(nodes[31].item);
        TutorialCubesList.Add(nodes[32].item);
        TutorialCubesList.Add(nodes[33].item);
        TutorialCubesList.Add(nodes[38].item);
        TutorialCubesList.Add(nodes[39].item);
        TutorialCubesList.Add(nodes[40].item);
        TutorialCubesList.Add(nodes[41].item);
        TutorialCubesList.Add(nodes[42].item);

        FocusObject();
        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        HelpPrefab();
        HandBoardPrefab(nodes[40].item.gameObject);

        //Wait user tap
        yield return new WaitForSeconds(0.5f);
        Configuration.instance.pause = false;
        if (TutorialCubesList.Count > 0)
        {
            while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
            {
                yield return null;
            }
        }
        Configuration.instance.pause = true;
        yield return StartCoroutine(StartCloseHelp());

        while (flyingItems > 0 || huntering || destroyingItems > 0 || merging || droppingItems > 0)
        {
            yield return null;
        }

        Configuration.instance.MessageBoardInfo = "Great! Tap the COLOR HUNTER!";

        yield return new WaitForSeconds(0.5f);
        foreach (var itm in GetListItems())
        {
            if (itm != null && (itm.type == ITEM_TYPE.ITEM_COLORHUNTER1 ||
                itm.type == ITEM_TYPE.ITEM_COLORHUNTER2 ||
                itm.type == ITEM_TYPE.ITEM_COLORHUNTER3 ||
                itm.type == ITEM_TYPE.ITEM_COLORHUNTER4 ||
                itm.type == ITEM_TYPE.ITEM_COLORHUNTER5 ||
                itm.type == ITEM_TYPE.ITEM_COLORHUNTER6))
            {
                TutorialCubesList.Add(itm);
                DiffusePrefab(GameObject.Find("DiffuseLayer"));
                itm.gameObject.layer = 5;
                itm.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                HandBoardPrefab(itm.gameObject);
                HelpPrefab();

                //Wait user tap
                yield return new WaitForSeconds(0.5f);
                Configuration.instance.pause = false;
                if (TutorialCubesList.Count > 0)
                {
                    while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
                    {
                        yield return null;
                    }
                }
                break;
            }
            else
            {
                Configuration.instance.pause = false;
            }
        }


        yield return StartCoroutine(StartCloseHelp());
        helpBoard = false;

    }

    //ShowHelpMergeBoosters
    IEnumerator ShowHelpMergeBoosters()
    {
        helpBoard = true;
        Configuration.instance.pause = true;
        yield return new WaitForSeconds(1.0f);

        Configuration.instance.MessageBoardInfo = "Tap connected boosters and watch what happens!";
        foreach (var item in GetListItems())
        {
            if (item != null && (item.type == ITEM_TYPE.ITEM_COLUMN || item.type == ITEM_TYPE.ITEM_ROW))
            {
                TutorialCubesList.Add(item);
                TutorialCubesList.Add(item.neighborNodeRight.item);
                item.neighborNodeRight.item.ChangeToHunterBreaker();
            }
        }

        yield return new WaitForSeconds(0.5f);

        FocusObject();

        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        HelpPrefab();
        HandBoardPrefab(TutorialCubesList[1].gameObject);


        //Wait user tap
        yield return new WaitForSeconds(0.5f);
        Configuration.instance.pause = false;
        if (TutorialCubesList.Count > 0)
        {
            while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
            {
                yield return null;
            }
        }

        yield return StartCoroutine(StartCloseHelp());
        helpBoard = false;

    }

    //ShowHelpSingleBooster
    IEnumerator ShowHelpSingleBooster()
    {
        listenersinglebreaker = false;
        if (CoreData.instance.singleBreaker <= 0)
        {
            yield break;
        }
        yield return new WaitForEndOfFrame();
        if (PlayerPrefs.GetInt("SingleBreakerHelpShowed") == 1)
        {
            yield return StartCoroutine(StartCloseHelp());
            yield break;
        }

        while (flyingItems > 0 || huntering || destroyingItems > 0 || merging || droppingItems > 0)
        {
            yield return null;
        }
        Configuration.instance.pause = true;
        yield return new WaitForSeconds(0.5f);
        //single breaker and merge booster 
        if (RowItems(7).Contains(TutorialCubesList[0]))
        {
            TapCube = TutorialCubesList[0].neighborNodeBottom.item;
        }
        else if (RowItems(7).Contains(TutorialCubesList[1]))
        {
            TapCube = TutorialCubesList[1].neighborNodeBottom.item;
        }

        TutorialCubesList.Add(TapCube);
        //Booster Show
        Configuration.instance.MessageBoardInfo = "Tap the SINGLE BREAKER!";
        var boosterobje = GameObject.Find("Single Booster");

        if (!m_canvas) m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        boosterobje.transform.SetParent(m_canvas.transform, false);
        boosterobje.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);


        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        boosterobje.gameObject.layer = 5;
        HandPrefab(boosterobje.gameObject);
        HelpPrefab(GameObject.Find("TOPCENTER"));
        while (booster != BOOSTER_TYPE.SINGLE_BREAKER)
        {
            yield return null;
        }

        CoreData.instance.singleBreaker++;

        CloseHelpWindows();

        Configuration.instance.MessageBoardInfo = "Tap the Cube and Blast!";
        helpBoard = true;
        Configuration.instance.pause = false;
        yield return new WaitForSeconds(0.2f);



        //  int seed = UnityEngine.Random.Range(1, TutorialCubesList.Count);

        if (TutorialCubesList[0].neighborNodeBottom.item != null)
        {
            DiffusePrefab(GameObject.Find("DiffuseLayer"));
            //FocusObject();
            TapCube.gameObject.layer = 5;
            TapCube.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
            if (TapCube.ShapeSprite != null)
            {
                TapCube.ShapeSprite.gameObject.layer = 5;
                TapCube.ShapeSprite.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
            }

            HandBoardPrefab(TapCube.gameObject);
            HelpPrefab(GameObject.Find("TOPCENTER"));

            //Wait user tap
            yield return new WaitForSeconds(0.5f);
            Configuration.instance.pause = false;
            if (TutorialCubesList.Count > 0)
            {
                while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
                {
                    yield return null;
                }
            }

        }
        yield return StartCoroutine(StartCloseHelp());
        helpBoard = false;
        PlayerPrefs.SetInt("SingleBreakerHelpShowed", 1);
        PlayerPrefs.Save();
    }

    //ShowHelpSingleBooster
    IEnumerator ShowHelpAutoSingleBooster()
    {
        listenersinglebreaker = false;
        if (CoreData.instance.singleBreaker <= 0)
        {
            yield break;
        }
        yield return new WaitForEndOfFrame();
        if (PlayerPrefs.GetInt("SingleBreakerHelpShowed") == 1)
        {
            yield return StartCoroutine(StartCloseHelp());
            yield break;
        }

        while (flyingItems > 0 || huntering || destroyingItems > 0 || merging || droppingItems > 0)
        {
            yield return null;
        }
        Configuration.instance.pause = true;
        yield return new WaitForSeconds(0.5f);
        //single breaker and merge booster 
        if (RowItems(7).Contains(TutorialCubesList[0]))
        {
            TapCube = TutorialCubesList[0].neighborNodeBottom.item;
        }
        else if (RowItems(7).Contains(TutorialCubesList[1]))
        {
            TapCube = TutorialCubesList[1].neighborNodeBottom.item;
        }

        TutorialCubesList.Add(TapCube);
        //Booster Show
        Configuration.instance.MessageBoardInfo = "Tap the SINGLE BREAKER!";
        var boosterobje = GameObject.Find("Single Booster");

        if (!m_canvas) m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        boosterobje.transform.SetParent(m_canvas.transform, false);
        boosterobje.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);


        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        boosterobje.gameObject.layer = 5;
        HandPrefab(boosterobje.gameObject);
        HelpPrefab(GameObject.Find("TOPCENTER"));
        while (booster != BOOSTER_TYPE.SINGLE_BREAKER)
        {
            yield return null;
        }

        CoreData.instance.singleBreaker++;

        CloseHelpWindows();

        Configuration.instance.MessageBoardInfo = "Tap the Cube and Blast!";
        helpBoard = true;
        Configuration.instance.pause = false;
        yield return new WaitForSeconds(0.2f);



        //  int seed = UnityEngine.Random.Range(1, TutorialCubesList.Count);

        if (TutorialCubesList[0].neighborNodeBottom.item != null)
        {
            DiffusePrefab(GameObject.Find("DiffuseLayer"));
            //FocusObject();
            TapCube.gameObject.layer = 5;
            TapCube.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
            if (TapCube.ShapeSprite != null)
            {
                TapCube.ShapeSprite.gameObject.layer = 5;
                TapCube.ShapeSprite.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
            }

            HandBoardPrefab(TapCube.gameObject);
            HelpPrefab(GameObject.Find("TOPCENTER"));
            //Wait user tap
            yield return new WaitForSeconds(0.5f);
            Configuration.instance.pause = false;
            if (TutorialCubesList.Count > 0)
            {
                while (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
                {
                    yield return null;
                }
            }

        }
        yield return StartCoroutine(StartCloseHelp());
        helpBoard = false;
        PlayerPrefs.SetInt("SingleBreakerHelpShowed", 1);
        PlayerPrefs.Save();
    }

    //ShowHelpBoosters
    public IEnumerator ShowHelpBoosters(string BoosterName, string BoosterInfo)
    {
        yield return new WaitForEndOfFrame();
        //Booster Show
        Configuration.instance.MessageBoardInfo = BoosterInfo;
        var boosterobje4 = GameObject.Find(BoosterName);
        Configuration.instance.pause = true;

        if (!m_canvas) m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        boosterobje4.transform.SetParent(m_canvas.transform, false);

        DiffusePrefab(GameObject.Find("DiffuseLayer"));
        boosterobje4.gameObject.layer = 5;

        ArrowPrefab(boosterobje4.gameObject, 140);


        HelpPrefab(GameObject.Find("TOPCENTER"), 0, true);
        Configuration.instance.pause = true;
        //while (!Input.GetMouseButtonUp(0))
        //{
        //    yield return null;
        //}

        //yield return StartCoroutine(StartCloseHelp());
        //Configuration.instance.pause = false;
        helpBoard = false;
    }

    //ShowHelpMergeBoosters
    IEnumerator ShowHelpItem(ITEM_TYPE itemtype = ITEM_TYPE.NONE, bool brekable = false, bool bubble = false, bool choco = false, bool cage = false, bool ball = false, bool lego = false, string infoText = null)
    {
        //Booster Show
        Configuration.instance.MessageBoardInfo = infoText;// "Tap the cubes next to a ice to collect it!";
        Configuration.instance.pause = true;

        if (itemtype != ITEM_TYPE.NONE)
        {
            foreach (var item in GetListItems())
            {
                if (item != null && item.type == itemtype)
                {
                    TutorialCubesList.Add(item);

                }
            }

        }
        if (brekable)
        {
            foreach (var item in GetListItems())
            {
                if (item != null && item.type == ITEM_TYPE.BREAKABLE)
                {
                    TutorialCubesList.Add(item);

                }
            }

        }

        if (bubble)
        {
            foreach (var item in GetListItems())
            {
                if (item != null && item.node.waffle != null && (
                    item.node.waffle.type == WAFFLE_TYPE.WAFFLE_1 ||
                     item.node.waffle.type == WAFFLE_TYPE.WAFFLE_2 ||
                      item.node.waffle.type == WAFFLE_TYPE.WAFFLE_3
                    ))
                {
                    TutorialCubesList.Add(item);
                }
            }
        }

        if (choco)
        {
            foreach (var item in GetListItems())
            {
                if (item != null &&
                    (item.type == ITEM_TYPE.MINE_1_LAYER ||
                    item.type == ITEM_TYPE.MINE_2_LAYER ||
                    item.type == ITEM_TYPE.MINE_3_LAYER ||
                    item.type == ITEM_TYPE.MINE_4_LAYER ||
                    item.type == ITEM_TYPE.MINE_5_LAYER ||
                    item.type == ITEM_TYPE.MINE_6_LAYER
                    ))
                {
                    TutorialCubesList.Add(item);
                }
            }
        }
        if (cage)
        {
            foreach (var item in GetListItems())
            {
                if (item != null && item.node.cage != null)
                {
                    TutorialCubesList.Add(item);
                }
            }
        }

        if (ball)
        {
            foreach (var item in GetListItems())
            {
                if (item != null &&
                    (item.type == ITEM_TYPE.ROCKET_1 ||
                    item.type == ITEM_TYPE.ROCKET_2 ||
                    item.type == ITEM_TYPE.ROCKET_3 ||
                    item.type == ITEM_TYPE.ROCKET_4 ||
                    item.type == ITEM_TYPE.ROCKET_5 ||
                    item.type == ITEM_TYPE.ROCKET_6
                    ))
                {
                    TutorialCubesList.Add(item);
                }
            }
        }

        if (lego)
        {
            foreach (var item in GetListItems())
            {
                if (item != null &&
                    (item.type == ITEM_TYPE.ROCK_CANDY_1 ||
                    item.type == ITEM_TYPE.ROCK_CANDY_2 ||
                    item.type == ITEM_TYPE.ROCK_CANDY_3 ||
                    item.type == ITEM_TYPE.ROCK_CANDY_4 ||
                    item.type == ITEM_TYPE.ROCK_CANDY_5 ||
                    item.type == ITEM_TYPE.ROCK_CANDY_6
                    ))
                {
                    TutorialCubesList.Add(item);
                }
            }
        }

        yield return new WaitForSeconds(0.5f);
        if (TutorialCubesList.Count > 0)
        {
            DiffusePrefab(GameObject.Find("DiffuseLayer"));

            FocusObject();


            //while (!Input.GetMouseButtonUp(0))
            //{
            //    yield return null;
            //}
        }

        if (lego)
        {
            HelpPrefab(null, 710, true);
            Configuration.instance.pause = true;
        }
        else if (choco)
        {
            HelpPrefab(null, 650, true);
            Configuration.instance.pause = true;
        }
        else
        {

            HelpPrefab(null, 0, true);
            Configuration.instance.pause = true;


        }
        //Configuration.instance.pause = false;
        //yield return StartCoroutine(StartCloseHelp());
    }



    public void FocusObject()
    {
        foreach (var item in TutorialCubesList)
        {
            if (item != null)
            {
                item.gameObject.layer = 5;
                item.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                if (item.ShapeSprite != null)
                {
                    item.ShapeSprite.gameObject.layer = 5;
                    item.ShapeSprite.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                }
                if (item.node.waffle != null)
                {
                    item.node.waffle.gameObject.layer = 5;
                    item.node.waffle.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                    item.node.waffle.GetComponent<SpriteRenderer>().sortingOrder = 10;
                }
                if (item.node.cage != null)
                {
                    item.node.cage.gameObject.layer = 5;
                    item.node.cage.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                    item.node.cage.GetComponent<SpriteRenderer>().sortingOrder = 10;
                }

            }
        }
    }
    public void DeFocusObject()
    {
        foreach (var item in TutorialCubesList)
        {
            if (item != null)
            {
                item.gameObject.layer = 0;
                item.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                if (item.ShapeSprite != null)
                {
                    item.ShapeSprite.gameObject.layer = 0;
                    item.ShapeSprite.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                }
                if (item.node.waffle != null)
                {
                    item.node.waffle.gameObject.layer = 0;
                    item.node.waffle.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                    item.node.waffle.GetComponent<SpriteRenderer>().sortingOrder = 10;
                }
                if (item.node.cage != null)
                {
                    item.node.cage.gameObject.layer = 0;
                    item.node.cage.GetComponent<SpriteRenderer>().sortingLayerName = "UILayer";
                    item.node.cage.GetComponent<SpriteRenderer>().sortingOrder = 10;
                }

            }
        }
    }


    public void CloseHelp()
    {
        StartCoroutine(StartCloseHelp());
        Configuration.instance.pause = false;

    }
    IEnumerator StartCloseHelp()
    {
        DeFocusObject();
        //TutorialCubesList.Clear();
        if (GameObject.Find("HelpPopup(Clone)"))
        {
            Destroy(GameObject.Find("HelpPopup(Clone)"));
        }

        if (GameObject.Find("Diffuse(Clone)"))
        {
            Destroy(GameObject.Find("Diffuse(Clone)"));
        }
        if (GameObject.Find("Arrow(Clone)"))
        {
            Destroy(GameObject.Find("Arrow(Clone)"));

        }
        if (GameObject.Find("Hand(Clone)"))
        {
            Destroy(GameObject.Find("Hand(Clone)"));
        }
        if (GameObject.Find("HandBoard(Clone)"))
        {
            Destroy(GameObject.Find("HandBoard(Clone)"));
        }

        if (GameObject.Find("SkipHelpButon(Clone)"))
        {
            Destroy(GameObject.Find("SkipHelpButon(Clone)"));
        }


        foreach (var itm in TutorialCubesList)
        {
            if (itm != null && !itm.IsBooster())
            {
                itm.gameObject.layer = 0;
                itm.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
                if (itm.ShapeSprite != null)
                {
                    itm.ShapeSprite.gameObject.layer = 0;
                    itm.ShapeSprite.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
                }
                if (itm.node.waffle != null)
                {
                    itm.node.waffle.gameObject.layer = 0;
                    itm.node.waffle.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
                }
                if (itm.node.cage != null)
                {
                    itm.node.cage.gameObject.layer = 0;
                    itm.node.cage.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
                }

            }
        }
        yield return new WaitForSeconds(0.2f);
        TutorialCubesList.Clear();
        //helpBoard = false;
    }
    public void CloseHelpWindows()
    {
        //TutorialCubesList.Clear();
        if (GameObject.Find("HelpPopup(Clone)"))
        {
            Destroy(GameObject.Find("HelpPopup(Clone)"));
        }
        if (GameObject.Find("Diffuse(Clone)"))
        {
            Destroy(GameObject.Find("Diffuse(Clone)"));
        }
        if (GameObject.Find("Arrow(Clone)"))
        {
            Destroy(GameObject.Find("Arrow(Clone)"));

        }
        if (GameObject.Find("Hand(Clone)"))
        {
            Destroy(GameObject.Find("Hand(Clone)"));
        }
        if (GameObject.Find("HandBoard(Clone)"))
        {
            Destroy(GameObject.Find("HandBoard(Clone)"));
        }

    }
    public void HelpPrefab(GameObject Target = null, float positon_y = 0, bool button = false)
    {
        if (!m_canvas) m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var helpprefab = Instantiate(Resources.Load("Prefabs/HELP/HelpPopup")) as GameObject;

        //if(StageLoader.instance.Stage <=7)
        //{
        //    var helpprefabSkip = Instantiate(Resources.Load("Prefabs/HELP/SkipHelpButon")) as GameObject;
        //    helpprefabSkip.transform.SetParent(m_canvas.transform, false);
        //}

        if (Target != null)
        {
            helpprefab.transform.SetParent(Target.transform, false);
        }
        else
        {
            helpprefab.transform.SetParent(m_canvas.transform, false);
        }



        if (positon_y != 0)
            helpprefab.transform.localPosition = new Vector3(0, positon_y, 0);


        helpprefab.GetComponent<HelpPopup>().ContinueButton.SetActive(button);


    }
    public void DiffusePrefab(GameObject Target = null)
    {
        if (!m_canvas) m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var Diffuse = Instantiate(Resources.Load("Prefabs/HELP/Diffuse")) as GameObject;
        if (Target != null)
        {
            Diffuse.transform.SetParent(Target.transform, false);
        }
        else
        {
            Diffuse.transform.SetParent(m_canvas.transform, false);
        }


    }
    public void ArrowPrefab(GameObject Target = null, float angle = 0f)
    {
        //if (!m_canvas) m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var prefab = Instantiate(Resources.Load("Prefabs/HELP/Arrow")) as GameObject;
        prefab.transform.localEulerAngles = new Vector3(0, 0, angle);
        prefab.transform.SetParent(Target.transform, false);
        //if (Target != null)
        //    prefab.transform.localPosition = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);

    }
    public void HandPrefab(GameObject Target = null, float angle = 0f)
    {

        var prefab = Instantiate(Resources.Load("Prefabs/HELP/Hand")) as GameObject;
        prefab.transform.localEulerAngles = new Vector3(0, 0, angle);
        prefab.transform.SetParent(Target.transform, false);

    }
    public void HandBoardPrefab(GameObject Target = null, float angle = 0f)
    {
        var prefab = Instantiate(Resources.Load("Prefabs/HELP/HandBoard")) as GameObject;
        prefab.transform.localEulerAngles = new Vector3(0, 0, angle);
        prefab.transform.SetParent(Target.transform, false);
    }
    public void HelpGoster()
    {
     Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var nesne = Instantiate(Resources.Load(Configuration.Help())) as GameObject;
            nesne.transform.SetParent(m_canvas.transform, false);
   Debug.LogError("Help clicked");
     
    }
    #endregion

    #region ADDBOARDBOOSTER
    public void AddBooster()
    {
        AudioManager.instance.ButtonClickAudio();
        //for test
        //StartCoroutine(AddBoosters());
        if (RewardBoosterNum > 0)
        {
            MovesReward = false;
            //sameerreward
            //admanager.instance.ShowRewardedVideAdGeneric(1);
            //AdsManager.instance.ShowReward();
        }
        /////
    }
    IEnumerator AddBoosters()
    {


        var items = GetListItems();
        var cookies = new List<Item>();
        var boosters = new List<Item>();
        Item cookie1 = null;

        foreach (var item in items)
        {
            if (item != null && item.IsBooster() && item.Movable())
            {
                boosters.Add(item);
            }
        }

        foreach (var item in items)
        {
            if (item != null && item.IsCookie() && item.Movable())
            {
                cookies.Add(item);
            }
        }

        int seed = UnityEngine.Random.Range(0, 3);


        if (seed == 0)
        {
            cookie1 = cookies[UnityEngine.Random.Range(0, cookies.Count)];
        }
        else if (boosters.Count > 0)
        {
            foreach (var cookie in boosters)
            {
                if (cookie != null)
                {
                    if (cookie.komsuNodeLeft != null && !cookie.komsuNodeLeft.IsBooster())
                    {
                        cookie1 = cookie.komsuNodeLeft;
                        goto finded;
                    }
                    else if (cookie.komsuNodeRight != null && !cookie.komsuNodeRight.IsBooster())
                    {
                        cookie1 = cookie.komsuNodeRight;
                        goto finded;
                    }
                    else if (cookie.komsuNodeTop != null && !cookie.komsuNodeTop.IsBooster())
                    {
                        cookie1 = cookie.komsuNodeTop;
                        goto finded;
                    }
                    else if (cookie.komsuNodeBottom != null && !cookie.komsuNodeBottom.IsBooster())
                    {
                        cookie1 = cookie.komsuNodeBottom;
                        goto finded;
                    }
                }
            }
        }
        else
        {
            cookie1 = cookies[UnityEngine.Random.Range(0, cookies.Count)];
            goto finded;
        }



    finded:


        yield return new WaitForSeconds(0.5f);



        int boosternum = UnityEngine.Random.Range(0, 2);
        if (boosternum == 0)
        {
            cookie1.ChangeToColRowBreaker();
            GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

            if (explosion != null)
            {
                explosion.transform.position = cookie1.transform.position;
            }
            AudioManager.instance.SingleBoosterAudio();
            AudioManager.instance.Star3Audio();

        }
        else if (boosternum == 1)
        {
            cookie1.ChangeToBombBreaker();
            GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

            if (explosion != null)
            {
                explosion.transform.position = cookie1.transform.position;
            }
            AudioManager.instance.SingleBoosterAudio();
            AudioManager.instance.Star3Audio();
        }

    }
    #endregion

    #region TOUCH

    public enum StateHelp
    {
        None,
        Dragging
    }

    public StateHelp _StateHelp = StateHelp.None;
    void Update()
    {
        if (Configuration.instance.Tutorial)
        {
            if (helpBoard && _StateHelp == StateHelp.None && Input.GetMouseButtonDown(0))
            {
                Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (hit != null)
                {
                    _StateHelp = StateHelp.Dragging;
                    var item = hit.gameObject.GetComponent<Item>();

                    if (!TutorialCubesList.Contains(item) && !Configuration.instance.pause)
                    {
                        lockSwapHelp = true;
                    }
                    else
                    {
                        lockSwapHelp = false;
                    }
                }

            }

            if (!helpBoard && lockSwapHelp && Input.GetMouseButton(0))
            {
                lockSwapHelp = false;
            }
            if (helpBoard && _StateHelp == StateHelp.Dragging && Input.GetMouseButtonUp(0))
            {
                _StateHelp = StateHelp.None;
                Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (hit != null)
                {
                    var item = hit.gameObject.GetComponent<Item>();
                    touchedItem = item;
                    if (!TutorialCubesList.Contains(touchedItem) && !Configuration.instance.pause)
                    {
                        lockSwapHelp = true;
                        FocusObject();

                    }
                    else
                    {
                        lockSwapHelp = false;

                    }

                }

            }

        }
 


        if (moveLeft > 0 && !GameOver)
        {
            if (state == GAME_STATE.WAITING_USER_SWAP && !lockSwap && !Configuration.instance.pause && !lockSwapHelp && !huntering)
            {
                if (Configuration.instance.touchIsSwallowed == true)
                {
                    return;
                }
                // no booster
                if (booster == BOOSTER_TYPE.NONE)
                {
                    // mouse down
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        worldPos.z = 0; // Ensure it's visible on correct Z layer
/*
GameObject rippleEffect=                        Instantiate(touchRippleEffect, worldPos, Quaternion.identity);

                        Destroy(rippleEffect,5f);
                        
                        */

                        GameObject ripple = GetRipple();
                        ripple.transform.position = worldPos;
                        ripple.transform.rotation = Quaternion.identity;

                        ParticleSystem ps = ripple.GetComponent<ParticleSystem>();
                        if (ps != null)
                        {
                            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                            ps.Play();
                            StartCoroutine(DeactivateAfterParticle(ps, ripple));
                        }

                        // hit the collier
                        Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                        if (hit != null)
                        {
                            var item = hit.gameObject.GetComponent<Item>();

                            if (item != null)
                            {
                                item.drag = false;
                            }
                        }
                    }
                    // mouse up
                    else if (Input.GetMouseButtonUp(0))
                    {
                        Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                        if (hit != null)
                        {

                            //CameraSize.instance.MinElastic = true;

                            Item item = hit.gameObject.GetComponent<Item>();

                            if (item != null)
                            {
                                item.drag = true;
                                item.mousePostion = item.GetMousePosition();
                                item.deltaPosition = Vector3.zero;
                                movingGingerbread = false;
                                generatingGingerbread = false;
                                skipGenerateGingerbread = false;
                                balanced = false;

                            }
                        }

                    }
                }
                // use booster
                else
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                        if (hit != null)
                        {
                            var item = hit.gameObject.GetComponent<Item>();
                            if (item != null)
                            {
                                DestroyBoosterItems(item);
                            }
                        }

                    }

                }
                if (!balanced)
                {
                    ResetSize();
                }


            }
            else
            {
                StartCoroutine(ResetBoard());
            }
        }

        #region BackButton

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.ButtonClickAudio();

            Transition.LoadLevel("Map", 0.1f, Color.black);
            //ADS
            //sameer
            //AdsManager.instance.ShowInter();
            Configuration.instance.WinLevel = 0;
            //admanager.instance.ShowGenericVideoAd();
        }

        #endregion

    }
    private IEnumerator DeactivateAfterParticle(ParticleSystem ps, GameObject ripple)
    {
        yield return new WaitUntil(() => !ps.IsAlive(true));
        ripple.SetActive(false);
    }
    public void GameEnd()
    {
        GameOver = true;
        Timer.timerIsRunning = false;
        NextButon.SetActive(false);
        AudioManager.instance.GetComponent<AudioSource>().Stop();
        BackgroundMusic.instance.StopMusic();

        Configuration.instance.playing = false;
        state = GAME_STATE.OPENING_POPUP;

        //sameerSaveLevelInfo();
        AudioManager.instance.PopupWinAudio();
        winPopup.OpenPopup();
    }

    IEnumerator ResetBoard()
    {
        bool timeadded = false;
        float addtime = 5f;

        CallReset++;
        if (CallReset > 1)
        {
            CallReset--;
            yield break;
        }

        Destroyed = false;
        if (huntering)
        {

            CallResetBoardTime += addtime;
            timeadded = true;
        }
        yield return new WaitForSeconds(CallResetBoardTime);
        if (timeadded)
        {
            CallResetBoardTime -= addtime;
        }
        if ((state == GAME_STATE.WAITING_USER_SWAP && !lockSwap && !Configuration.instance.pause && !lockSwapHelp && !huntering)
           //|| huntering
           || Destroyed
           //|| destroyingItems >0
           )
        {
            //Destroyed = false;
            CallReset--;
            yield break;
        }
        if (lockSwap)
        {
            ResetBoardApply = true;
            lockSwap = false;
        }


        state = GAME_STATE.WAITING_USER_SWAP;
        huntering = false;
        Configuration.instance.pause = false;
        lockSwapHelp = false;

        // Destroyed = false;
        CallReset--;

        Debug.Log("RESET BOARD!!!");
    }

    public void BoosterArrount(Item item)
    {
        if (item.IsBombBreaker(item.type))
        {
            ArrountEffect(ItemAround(item.node), item);
        }
        else if (item.IsRowBreaker(item.type))
        {
            ArrountEffect(RowItems(item.node.i), item);
        }
        else if (item.IsColumnBreaker(item.type))
        {
            ArrountEffect(ColumnItems(item.node.j), item);
        }
        else if (item.IsXBreaker(item.type))
        {
            ArrountEffect(XCrossItems(item.node), item);
        }
        else if (item.IsMedBombBreaker(item.type))
        {
            ArrountEffect(ItemAroundMed(item.node), item);
        }
        else if (item.IsBigBombBreaker(item.type))
        {
            ArrountEffect(ItemAroundBig(item.node), item);
        }
        else if (item.IsColorHunterBreaker(item.type))
        {
            var arrount = GetListItems();
            foreach (var itm in arrount)
            {
                if (itm != null && itm.color == item.destroycolor && itm != item && !itm.IsBooster())
                {
                    var matchs = itm.gameObject;
                    matchs.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
                    matchs.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
                    matchs.GetComponent<SpriteRenderer>().sortingOrder = 101;
                }

            }
        }

    }

    public void ArrountEffect(List<Item> arrountList, Item item)
    {
        foreach (var itm in arrountList)
        {
            if (itm != null && itm != item && !itm.IsBooster())
            {
                var matchs = itm.gameObject;
                matchs.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
                matchs.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
                matchs.GetComponent<SpriteRenderer>().sortingOrder = 101;
            }
        }

    }

    public void ResetSize()
    {
        if (boosterdestroying)
        {
            return;
        }

        List<Item> kutulist = GetListItems();


        foreach (var item in kutulist)
        {
            if (item != null)
            {
                item.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                item.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                item.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10 - item.node.i;
                item.CheckStat();


            }
        }

    }

    #endregion

    #region Board

    void GenerateBoard()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                GameObject node = Instantiate(Resources.Load(Configuration.NodePrefab())) as GameObject;
                node.transform.SetParent(gameObject.transform, false);
                node.name = "Node " + order;
                node.GetComponent<Node>().grid = this;
                node.GetComponent<Node>().i = i;
                node.GetComponent<Node>().j = j;

                nodes.Add(node.GetComponent<Node>());
            } // end for j
        } // end for i

        GenerateTileLayer();
        GenerateTileBorder();

        GeneratebreakableLayer();

        GenerateItemLayer();

        GenerateCageLayer();

        GenerateCollectibleBoxByColumn();
        GenerateCollectibleBoxByNode();

        ResetSize();


    }

    void GenerateTileLayer()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                var tileLayerData = StageLoader.instance.tileLayerData;

                GameObject tile = null;

                switch (tileLayerData[order])
                {
                    case TILE_TYPE.NONE:
                        tile = Instantiate(Resources.Load(Configuration.NoneTilePrefab())) as GameObject;
                        break;
                    case TILE_TYPE.PASS_THROUGH:
                        tile = Instantiate(Resources.Load(Configuration.NoneTilePrefab())) as GameObject;
                        break;
                    case TILE_TYPE.LIGHT_TILE:
                        tile = Instantiate(Resources.Load(Configuration.LightTilePrefab())) as GameObject;
                        break;
                    case TILE_TYPE.DARD_TILE:
                        tile = Instantiate(Resources.Load(Configuration.DarkTilePrefab())) as GameObject;
                        break;
                }

                if (tile)
                {
                    tile.transform.SetParent(nodes[order].gameObject.transform);
                    tile.name = "Tile";
                    tile.transform.localPosition = NodeLocalPosition(i, j);
                    tile.GetComponent<Tile>().type = tileLayerData[order];
                    tile.GetComponent<Tile>().node = nodes[order];

                    nodes[order].tile = tile.GetComponent<Tile>();
                }

            } // end for j
        } // end for i
    }

    void GenerateTileBorder()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                nodes[order].tile.SetBorder();
            }
        }
    }

    // waffle
    void GeneratebreakableLayer()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                var breakableLayerData = StageLoader.instance.breakableLayerData;

                GameObject waffle = null;
                int stage = StageLoader.instance.Stage;
                int changeLevelsChoco = StageLoader.instance.ChocolateChangeLevels;
                int changeLevelsBubbles = StageLoader.instance.BubblesChangeLevels;



                switch (breakableLayerData[order])
                {
                    case WAFFLE_TYPE.WAFFLE_1:
                        if (stage % (3 * changeLevelsBubbles) >= 2 * changeLevelsBubbles)
                        {
                            waffle = Instantiate(Resources.Load(Configuration.Waffle1b())) as GameObject;
                        }
                        else if (stage % (3 * changeLevelsBubbles) >= changeLevelsBubbles)
                        {
                            waffle = Instantiate(Resources.Load(Configuration.Waffle1a())) as GameObject;
                        }
                        else
                        {
                            waffle = Instantiate(Resources.Load(Configuration.Waffle1())) as GameObject;
                        }

                        // waffle = Instantiate(Resources.Load(Configuration.Waffle1())) as GameObject;
                        break;
                    case WAFFLE_TYPE.WAFFLE_2:
                        if (stage % (3 * changeLevelsBubbles) >= 2 * changeLevelsBubbles)
                        {
                            waffle = Instantiate(Resources.Load(Configuration.Waffle2b())) as GameObject;
                        }
                        else if (stage % (3 * changeLevelsBubbles) >= changeLevelsBubbles)
                        {
                            waffle = Instantiate(Resources.Load(Configuration.Waffle2a())) as GameObject;
                        }
                        else
                        {
                            waffle = Instantiate(Resources.Load(Configuration.Waffle2())) as GameObject;
                        }
                        //waffle = Instantiate(Resources.Load(Configuration.Waffle2())) as GameObject;
                        break;
                    case WAFFLE_TYPE.WAFFLE_3:
                        if (stage % (3 * changeLevelsBubbles) >= 2 * changeLevelsBubbles)
                        {
                            waffle = Instantiate(Resources.Load(Configuration.Waffle3b())) as GameObject;
                        }
                        else if (stage % (3 * changeLevelsBubbles) >= changeLevelsBubbles)
                        {
                            waffle = Instantiate(Resources.Load(Configuration.Waffle3a())) as GameObject;
                        }
                        else
                        {
                            waffle = Instantiate(Resources.Load(Configuration.Waffle3())) as GameObject;
                        }
                        // waffle = Instantiate(Resources.Load(Configuration.Waffle3())) as GameObject;
                        break;
                }

                if (waffle)
                {
                    waffle.transform.SetParent(nodes[order].gameObject.transform);
                    waffle.name = "Waffle";
                    waffle.transform.localPosition = NodeLocalPosition(i, j);
                    waffle.GetComponent<Waffle>().type = breakableLayerData[order];
                    waffle.GetComponent<Waffle>().node = nodes[order];

                    nodes[order].waffle = waffle.GetComponent<Waffle>();
                }
            }
        }
    }

    void GenerateItemLayer()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                var itemLayerData = StageLoader.instance.itemLayerData;

                if (nodes[order].CanStoreItem())
                {
                    nodes[order].GenerateItem(itemLayerData[order]);

                    // add mask
                    var mask = Instantiate(Resources.Load(Configuration.Mask())) as GameObject;
                    mask.transform.SetParent(nodes[order].transform);
                    mask.transform.localPosition = NodeLocalPosition(i, j);
                    mask.name = "Mask";
                }

            }
        }
    }

    void GenerateCageLayer()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                var lockLayerData = StageLoader.instance.lockLayerData;

                GameObject locK = null;

                switch (lockLayerData[order])
                {
                    case LOCK_TYPE.LOCK_1:
                        locK = Instantiate(Resources.Load(Configuration.Lock1())) as GameObject;
                        break;
                }

                if (locK)
                {
                    locK.transform.SetParent(nodes[order].gameObject.transform);
                    locK.name = "Lock";
                    locK.transform.localPosition = NodeLocalPosition(i, j);
                    locK.GetComponent<Cage>().type = lockLayerData[order];
                    locK.GetComponent<Cage>().node = nodes[order];

                    nodes[order].cage = locK.GetComponent<Cage>();
                }
            }
        }
    }

    void GenerateCollectibleBoxByColumn()
    {
        if (StageLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
        {
            return;
        }

        var row = StageLoader.instance.row;

        foreach (var column in StageLoader.instance.collectibleCollectColumnMarkers)
        {
            var node = GetNode(row - 1, column);

            if (node != null && node.CanStoreItem() == true)
            {
                var box = Instantiate(Resources.Load(Configuration.CollectibleBox())) as GameObject;

                if (box)
                {
                    box.transform.SetParent(node.gameObject.transform);
                    box.name = "Box";
                    box.transform.localPosition = NodeLocalPosition(node.i, node.j) + new Vector3(0, -1 * NodeSize() + 0.2f, 0);
                }
            }
        }
    }

    void GenerateCollectibleBoxByNode()
    {
        if (StageLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
        {
            return;
        }

        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                if (StageLoader.instance.collectibleCollectNodeMarkers.Contains(order))
                {
                    var node = GetNode(i, j);

                    if (node != null)
                    {
                        var box = Instantiate(Resources.Load(Configuration.CollectibleBox())) as GameObject;

                        if (box)
                        {
                            box.transform.SetParent(node.gameObject.transform);
                            box.name = "Box";
                            box.transform.localPosition = NodeLocalPosition(node.i, node.j) + new Vector3(0, -1 * NodeSize() + 0.2f, 0);
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region Begin

    void BeginBooster()
    {
        if (Configuration.instance.beginFiveMoves == true)
        {
            //Configuration.instance.beginFiveMoves = false;

            CoreData.instance.SaveBeginFiveMoves(CoreData.instance.GetBeginFiveMoves() - 1);

            //Achievement
            Configuration.SaveAchievement("ach_use10beginXbreaker", 1);
            //Achievement
            Configuration.SaveAchievement("ach_beginboosters", 1);


            //Add Xcross
            var items = GetListItems();
            var cookies = new List<Item>();

            foreach (var item in items)
            {
                if (item != null && item.IsCookie() && item.Movable())
                {
                    cookies.Add(item);
                }
            }

            var cookie = cookies[UnityEngine.Random.Range(0, cookies.Count)];

            cookie.ChangeToXBreaker();
            AudioManager.instance.SingleBoosterAudio();
            AudioManager.instance.Star1Audio();
            GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

            if (explosion != null)
            {
                explosion.transform.position = cookie.transform.position;
            }

           

        }

        if (Configuration.instance.beginRainbow == true)
        {

            Configuration.instance.beginRainbow = false;

            //Achievement
            Configuration.SaveAchievement("ach_use10beginColorHunter", 1);

            //Achievement
            Configuration.SaveAchievement("ach_beginboosters", 1);

            CoreData.instance.SaveBeginRainbow(CoreData.instance.GetBeginRainbow() - 1);

            AddBoostersColorHunter(1);
          


        }

        if (Configuration.instance.beginBombBreaker == true)
        {
            //Achievement
            Configuration.SaveAchievement("ach_use10beginBigBomb", 1);

            //Achievement
            Configuration.SaveAchievement("ach_beginboosters", 1);

            Configuration.instance.beginBombBreaker = false;

            CoreData.instance.SaveBeginBombBreaker(CoreData.instance.GetBeginBombBreaker() - 1);

            var items = GetListItems();
            var cookies = new List<Item>();

            foreach (var item in items)
            {
                if (item != null && item.IsCookie() && item.Movable())
                {
                    cookies.Add(item);
                }
            }

            var cookie = cookies[UnityEngine.Random.Range(0, cookies.Count)];

            cookie.ChangeToMedBomb();
            AudioManager.instance.SingleBoosterAudio();
            AudioManager.instance.Star1Audio();
            GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

            if (explosion != null)
            {
                explosion.transform.position = cookie.transform.position;
            }

           

        }

    }

    //Add ColorHunter
    public void AddBoostersColorHunter(int count)
    {
        var items = GetListItems();
        var cookies = new List<Item>();



        foreach (var item in items)
        {
            if (item != null && item.IsCookie() && item.Movable())
            {
                cookies.Add(item);
            }
        }

        for (int i = 0; i < count; i++)
        {
            int c4 = UnityEngine.Random.Range(0, cookies.Count);
            var cookie1 = cookies[c4];

            cookie1.ChangeToHunterBreaker();
            AddExplosion(cookie1);
            cookies.RemoveAt(c4);
        }
    }

    public void AddExplosion(Item cookie1)
    {
        GameObject explosion2 = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);
        if (explosion2 != null)
        {
            explosion2.transform.position = cookie1.transform.position;
        }

        AudioManager.instance.SingleBoosterAudio();
        AudioManager.instance.Star3Audio();
    }
    #endregion

    #region Utility
    Vector3 CalculateFirstNodePosition()
    {
        var width = NodeSize();
        var height = NodeSize();
        var column = StageLoader.instance.column;
        var row = StageLoader.instance.row;

        var offset = new Vector3(0, -1, 0);

        return (new Vector3(-((column - 1) * width / 2), (row - 1) * height / 2, 0) + offset);
    }

    public float NodeSize()
    {
        return 0.96f;
    }

    public Vector3 NodeLocalPosition(int i, int j)
    {
        var width = NodeSize();
        var height = NodeSize();

        if (firstNodePosition == Vector3.zero)
        {
            firstNodePosition = CalculateFirstNodePosition();
        }

        var x = firstNodePosition.x + j * width;
        var y = firstNodePosition.y - i * height;

        return new Vector3(x, y, 0);
    }

    public int NodeOrder(int i, int j)
    {
        return (i * StageLoader.instance.column + j);
    }

    public Node GetNode(int row, int column)
    {
        if (row < 0 || row >= StageLoader.instance.row || column < 0 || column >= StageLoader.instance.column)
        {
            return null;
        }
        return nodes[row * StageLoader.instance.column + column];
    }

    Vector3 ColumnFirstItemPosition(int i, int j)
    {
        Node node = GetNode(i, j);

        if (node != null)
        {
            var item = node.item;

            if (item != null)
            {
                return item.gameObject.transform.position;
            }
            else
            {
                return ColumnFirstItemPosition(i + 1, j);
            }
        }
        else
        {
            return Vector3.zero;
        }
    }

    // return a list of items

    public List<Item> GetListItems()
    {
        var items = new List<Item>();

        foreach (var node in nodes)
        {
            if (node != null)
            {
                items.Add(node.item);
            }
        }
        return items;
    }

    #endregion

    #region Match

    // re-generate the board to make sure there is no "pre-matches"
    void GenerateNoMatches()
    {

        var combines = GetMatches();

        foreach (var combine in combines)
        {
            int i = 0;
            foreach (var item in combine)
            {
                if (item != null)
                {
                    // only re-generate color for random item
                    if (item.OriginCookieType() == ITEM_TYPE.ITEM_RAMDOM)
                    {
                        item.GenerateColor(item.color + i);
                        i++;
                    }
                }
            }
        }
        combines = GetMatches();
    }

    // return the list of matches on the board
    public List<List<Item>> GetMatches(FIND_DIRECTION direction = FIND_DIRECTION.NONE, int matches = 2)
    {
        var combines = new List<List<Item>>();

        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (GetNode(i, j) != null)
                {
                    List<Item> combine = GetNode(i, j).FindMatches(direction, matches);

                    // combine can be null
                    if (combine != null)
                    {
                        if (combine.Count >= matches)
                        {
                            combines.Add(combine);
                        }
                    }
                }
            }
        }

        return combines;
    }

    public void FindMatches()
    {
        StartCoroutine(DestroyMatches());
    }

    // destroy the matches on the board
    IEnumerator DestroyMatches()
    {
        if (ResetBoardApply)
        {

            lockSwap = false;
        }
        else
        {
            lockSwap = true;
        }


        matching++;
        bool movecheck;
        var combines = GetMatches();


        movecheck = true;
        foreach (var combine in combines)
        {
            foreach (var item in combine)
            {
                if (combine.Contains(item.swapItem))
                {
                    if (movecheck)
                    {
                        moveLeft--;
                        UITop.DecreaseMoves(true);
                        movecheck = false;
                    }

                    foreach (var itm in combine)
                    {
                        itm.Destroy();
                    }
                }
            }
        }

        while (destroyingItems > 0)
        {
            lockSwap = true;
            yield return new WaitForEndOfFrame();

        }

        // IMPORTANT: as describe in document Destroy is always delayed (but executed within the same frame).
        // So There is case destroyingItems = 0 BUT the item still exist that causes the GenerateNewItems function goes wrong
        yield return new WaitForEndOfFrame();
        lockSwap = false;

        // new items
        if (!merging && !huntering)
            Drop();


        while (droppingItems > 0)
        {
            //lockSwap = true;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
        //  lockSwap = false;

        if (CollectCollectible() == false)
        {

        }
        if (huntering || boosterdestroying)
        {
            dropTime = 2;
        }
        else
        {
            dropTime++;
        }


        while (flyingItems > 0)
        {
            yield return new WaitForEndOfFrame();
        }


        yield return new WaitForEndOfFrame();

        if (matching > 1)
        {
            matching--;
            yield break;
        }

        // check if sameerlevel complete
        if (state == GAME_STATE.WAITING_USER_SWAP)
        {
            if (moveLeft > 0)
            {
                if (IsLevelCompleted() && !huntering && !GameOver)
                {
                    Timer.timerIsRunning = false;
                    GameOver = true;
                 //Asad   StartCoroutine(PreWinAutoPlay());
                    int sound = UnityEngine.Random.Range(0, 11);
                    AudioManager.instance.GirlWowSound(sound);
                    // AudioManager.instance.GingerbreadExplodeAudio();

                    //  BackMusic.SetActive(false);

                    PlayerPrefs.SetInt("LevelWin", 0);
                    PlayerPrefs.SetInt("BaseScore", PlayerPrefs.GetInt("BaseScore")+ score);

                    //next level loaded
                    StageLoader.instance.LoadLevel(Configuration.instance.LevelNumber());
                  
                   
                    Transition.LoadLevel("Play", 0.2f, Color.black);
                }
                else
                {

                    yield return new WaitForSeconds(0.2f);

                    FindMatches();
                    //   yield return new WaitForSeconds(0.2f);
                    if (GenerateGingerbread() == true)
                    {
                        yield return new WaitForSeconds(0.2f);

                        FindMatches();
                    }
                    //  yield return new WaitForSeconds(0.2f);                    
                    StartCoroutine(CheckHint());

                }
            }
            else if (moveLeft == 0 && !huntering)
            {
                if (IsLevelCompleted() && !huntering && !GameOver)
                {
                    Timer.timerIsRunning = false;
                    GameOver = true;
                    SaveLevelInfo();
                    AudioManager.instance.PopupWinAudio();
                    int sound = UnityEngine.Random.Range(0, 11);
                    AudioManager.instance.GirlWowSound(sound);
                    // show win popup
                    state = GAME_STATE.OPENING_POPUP;
                    PlayerPrefs.SetInt("LevelWin", 1);
                    winPopup.OpenPopup();
                  
                }
                else if (!GameOver)
                {
                    // show lose popup
                    state = GAME_STATE.OPENING_POPUP;
                    StartCoroutine(Loser());
                }
            }
        }

        matching--;

        // if dropTime >= 3 we should show some text like: grate, amazing, etc.
        //if (dropTime >= Configuration.instance.encouragingPopup && state == GAME_STATE.WAITING_USER_SWAP && showingInspiringPopup == false && combines.Count >= 6 && !huntering)
        //{
        //    ShowInspiringPopup();
        //}

        // when finish function we can swap again
        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.2f);
        lockSwap = false;

        // dropTime = 1;
    }

    //skillzpatchof3
    int skillz;
    void nextLevel()
    {
        //Configuration.instance.CurrentEpisode = 1;
        if (PlayerPrefs.GetInt("SkillzLevel") == 0)
        {
            PlayerPrefs.SetInt("SkillzLevel", 1);
        }
        else if (PlayerPrefs.GetInt("SkillzLevel") == 1)
        {
            skillz = Configuration.instance.LeveltoPlay;
            PlayerPrefs.SetInt("SkillzLevel", 2);
            PlayerPrefs.SetInt("SkilzzEnd", 1);
        }
        print("Skilzz Level:" + skillz);
        StageLoader.instance.Stage = skillz;
        StageLoader.instance.LoadLevel(skillz);

        if (StageLoader.instance.StageReady)
        {
            Transition.LoadLevel("Play", 0.1f, Color.black);
        }
    }

    IEnumerator Loser()
    {
        PlayerPrefs.SetInt("LevelWin", 0);
        Timer.timerIsRunning = false;
        Configuration.instance.playing = false;
        yield return new WaitForSeconds(0.3f);
        winPopup.OpenPopup();
    }

    #endregion

    #region Drop

    void Drop()
    {
        SetDropTargets();
        GenerateNewItems(true, Vector3.zero);
        Move();
        DropItems();
        dropTime = 0;
    }

    // set drop target to the remain items
    void SetDropTargets()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int j = 0; j < column; j++)
        {
            //need to enumerate rows from bottom to top
            for (int i = row - 1; i >= 0; i--)
            {
                Node node = GetNode(i, j);

                if (node != null)
                {
                    Item item = node.item;

                    if (item != null)
                    {
                        // start calculating new target for the node
                        if (item.Movable())
                        {
                            Node target = node.BottomNeighbor();

                            if (target != null && target.CanGoThrough())
                            {
                                if (target.item == null)
                                {
                                    // check rows below at this time GetNode(i + 1, j) = target
                                    for (int k = i + 2; k < row; k++)
                                    {
                                        if (GetNode(k, j) != null)
                                        {
                                            if (GetNode(k, j).item == null)
                                            {
                                                if (GetNode(k, j).CanStoreItem() == true)
                                                {
                                                    target = GetNode(k, j);
                                                }
                                            }

                                            // if a node can not go through we do not need to check bellow
                                            if (GetNode(k, j).CanGoThrough() == false)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                if (GetNode(k, j).item != null)
                                                {
                                                    if (GetNode(k, j).item.Movable() == false)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                // after have the target we swap items on nodes
                                if (target.item == null && target.CanStoreItem() == true)
                                {
                                    target.item = item;
                                    target.item.gameObject.transform.SetParent(target.gameObject.transform);
                                    target.item.node = target;

                                    node.item = null;
                                }
                            } // end if target != null
                        } // end item dropable
                    } // end item != null
                } // end node != null
            } // end for i
        } // end for j
    }

    // after destroy and drop items then we generate new items
    void GenerateNewItems(bool IsDrop, Vector3 pos)
    {
        //ali
        //return;
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        var marshmallowGenerated = false;

        for (int j = 0; j < column; j++)
        {
            var space = -1;

            var itemPos = Vector3.zero;

            for (int i = row - 1; i >= 0; i--)
            {
                if (GetNode(i, j) != null)
                {
                    if (GetNode(i, j).item == null && GetNode(i, j).CanGenerateNewItem() == true)
                    {
                        // if target is collectible the new item can be a collectible
                        var collectible = false;

                        // collectible is only generated on the highest row
                        if (i == 0)
                        {
                            // check if need to generate new collectible
                            if (CheckGenerateCollectible() != null &&
                                CheckGenerateCollectible().Count > 0 &&
                                (StageLoader.instance.collectibleGenerateMarkers.Contains(j) || StageLoader.instance.collectibleGenerateMarkers.Count == 0))
                            {
                                collectible = true;
                            }
                        }

                        // check if need to generate a new marshmallow
                        var marshmallow = false;

                        if (CheckGenerateMarshmallow() == true)
                        {
                            marshmallow = true;
                        }

                        if (pos != Vector3.zero)
                        {
                            itemPos = pos + Vector3.up * NodeSize();
                        }
                        else
                        {
                            // calculate position of the new item
                            if (i > space)
                            {
                                space = i;
                            }

                            // can pass through node
                            var pass = 0;

                            for (int k = 0; k < row; k++)
                            {
                                var node = GetNode(k, j);

                                if (node != null && node.tile != null && node.tile.type == TILE_TYPE.PASS_THROUGH)
                                {
                                    pass++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            itemPos = NodeLocalPosition(i, j) + Vector3.up * (space - pass + 1) * NodeSize();
                        }

                        //print("COOKIE: Generate new item");

                        // if target is collectible then generate a new collectible item
                        if (collectible == true && UnityEngine.Random.Range(0, 2) == 1)
                        {
                            GetNode(i, j).GenerateItem(CheckGenerateCollectible()[UnityEngine.Random.Range(0, CheckGenerateCollectible().Count)]);
                        }
                        // generate a marshmallow
                        else if (marshmallow == true && UnityEngine.Random.Range(0, 2) == 1 && marshmallowGenerated == false)
                        {
                            marshmallowGenerated = true;

                            GetNode(i, j).GenerateItem(ITEM_TYPE.BREAKABLE);
                        }
                        // generate a new random cookie
                        else
                        {
                            GetNode(i, j).GenerateItem(ITEM_TYPE.ITEM_RAMDOM);
                        }

                        // set position

                        var newItem = GetNode(i, j).item;

                        if (newItem != null)
                        {
                            if (IsDrop == true)
                            {
                                newItem.gameObject.transform.position = itemPos;
                            }
                            else
                            {
                                newItem.gameObject.transform.position = NodeLocalPosition(i, j);
                            }
                        }
                    }
                }
            }
        }
    }

    // move item to neighbor empty node
    void Move()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = row - 1; i >= 0; i--)
        {
            //need to enumerate rows from bottom to top
            for (int j = 0; j < column; j++)
            {
                Node node = GetNode(i, j);

                if (node != null)
                {
                    if (node.item == null && node.CanStoreItem())
                    {
                        Node source = node.GetSourceNode();

                        if (source != null)
                        {

                            var pos = ColumnFirstItemPosition(0, source.j);


                            List<Vector3> path = node.GetMovePath();

                            if (source.transform.position != NodeLocalPosition(source.i, source.j))
                            {
                                // if source item is just generated
                                path.Add(NodeLocalPosition(source.i, source.j));
                            }

                            node.item = source.item;
                            node.item.gameObject.transform.SetParent(node.gameObject.transform);
                            node.item.node = node;

                            source.item = null;

                            if (path.Count > 1)
                            {
                                path.Reverse();

                                node.item.dropPath = path;
                            }

                            SetDropTargets();

                            GenerateNewItems(true, pos);

                        } // end if source node != null
                    }
                } // end if node != null
            } // for j
        } // for i
    }

    // drop item to new position
    void DropItems()
    {
        //print("COOKIE: Drop items");

        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int j = 0; j < column; j++)
        {
            for (int i = row - 1; i >= 0; i--)
            {
                if (GetNode(i, j) != null)
                {
                    if (GetNode(i, j).item != null)
                    {
                        GetNode(i, j).item.Drop();

                    }
                }
            }
        }

    }

    #endregion

    #region Item

    // this function check all the items and set them to be bomb-breaker/x-breaker
    public void SetBombBreakerOrXBreakerCombine(List<List<Item>> lists)
    {
        foreach (List<Item> list in lists)
        {
            foreach (Item item in list)
            {
                if (item != null && item.node != null)
                {
                    //print(item.node.name);

                    if (item.node.FindMatches(FIND_DIRECTION.COLUMN).Count > 2)
                    {
                        if (item.next == ITEM_TYPE.NONE)
                        {
                            var match = item.node.FindMatches(FIND_DIRECTION.COLUMN);

                            Node top = item.node.TopNeighbor();
                            Node bottom = item.node.BottomNeighbor();

                            if (top != null && bottom != null)
                            {
                                // - - o -
                                // o o - o
                                // - - o -
                                if (top.item != null && bottom.item != null)
                                {
                                    if (match.Contains(top.item) && match.Contains(bottom.item))
                                    {
                                        //print("T shape");
                                        item.next = item.GetXBreaker(item.type);
                                        return;
                                    }
                                }

                                var topTop = top.TopNeighbor();
                                var bottomBottom = bottom.BottomNeighbor();

                                // - - o
                                // - - o
                                // o o - o
                                if (topTop != null)
                                {
                                    if (top.item != null && topTop.item != null)
                                    {
                                        if (match.Contains(top.item) && match.Contains(topTop.item))
                                        {
                                            var left = item.node.LeftNeighbor();
                                            var right = item.node.RightNeighbor();

                                            if (left != null && right != null)
                                            {
                                                if (left.item != null && right.item != null)
                                                {
                                                    if (list.Contains(left.item) && list.Contains(right.item))
                                                    {
                                                        //print("T shape (top)");
                                                        item.next = item.GetXBreaker(item.type);
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                // o o - o
                                // - - o
                                // - - o
                                if (bottomBottom != null)
                                {
                                    if (bottom.item != null && bottomBottom.item != null)
                                    {
                                        if (match.Contains(bottom.item) && match.Contains(bottomBottom.item))
                                        {
                                            var left = item.node.LeftNeighbor();
                                            var right = item.node.RightNeighbor();

                                            if (left != null && right != null)
                                            {
                                                if (left.item != null && right.item != null)
                                                {
                                                    if (list.Contains(left.item) && list.Contains(right.item))
                                                    {
                                                        //print("T shape (bottom)");
                                                        item.next = item.GetXBreaker(item.type);
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            } // end check T shape

                            // L shape = bomb breaker
                            item.next = item.GetBombBreaker(item.type);

                        } // item.next = none
                    } // count > 2
                }
            }
        }
    }

    public void SetColRowBreakerCombine(List<Item> combine)
    {
        bool isSwap = false;

        foreach (Item item in combine)
        {
            if (item.next != ITEM_TYPE.NONE)
            {
                isSwap = true;

                break;
            }
        }

        // next type is normal (drop then match) get first item in the combine
        if (!isSwap)
        {
            Item first = null;

            foreach (Item item in combine)
            {
                if (first == null)
                {
                    first = item;
                }
                else
                {
                    if (item.node.OrderOnBoard() < first.node.OrderOnBoard())
                    {
                        first = item;
                    }
                }
            }

            foreach (Item item in combine)
            {
                if (first.node.RightNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.RightNeighbor().OrderOnBoard())
                    {
                        first.next = first.GetColumnBreaker(first.type);
                        break;
                    }
                }

                if (first.node.BottomNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.BottomNeighbor().OrderOnBoard())
                    {
                        first.next = first.GetRowBreaker(first.type);
                        break;
                    }
                }
            }
        } // not swap
    }

    public void SetRainbowCombine(List<Item> combine)
    {

        bool isSwap = false;

        foreach (Item item in combine)
        {
            if (item.next != ITEM_TYPE.NONE)
            {
                isSwap = true;

                break;
            }
        }

        if (!isSwap)
        {
            Item first = null;

            foreach (Item item in combine)
            {
                if (first == null)
                {
                    first = item;
                }
                else
                {
                    if (item.node.OrderOnBoard() < first.node.OrderOnBoard())
                    {
                        first = item;
                    }
                }
            }

            foreach (Item item in combine)
            {
                if (first.node.RightNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.RightNeighbor().OrderOnBoard())
                    {
                        combine[2].next = ITEM_TYPE.ITEM_COLORCONE;
                        break;
                    }
                }

                if (first.node.BottomNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.BottomNeighbor().OrderOnBoard())
                    {
                        first.next = ITEM_TYPE.ITEM_COLORCONE;
                        break;
                    }
                }
            }
        }
    }
    public void SetBigBombCombine(List<Item> combine)
    {

        bool isSwap = false;

        foreach (Item item in combine)
        {
            if (item.next != ITEM_TYPE.NONE)
            {
                isSwap = true;

                break;
            }
        }

        if (!isSwap)
        {
            Item first = null;

            foreach (Item item in combine)
            {
                if (first == null)
                {
                    first = item;
                }
                else
                {
                    if (item.node.OrderOnBoard() < first.node.OrderOnBoard())
                    {
                        first = item;
                    }
                }
            }

            foreach (Item item in combine)
            {
                if (first.node.RightNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.RightNeighbor().OrderOnBoard())
                    {
                        combine[2].next = ITEM_TYPE.ITEM_BIGBOMB;
                        break;
                    }
                }

                if (first.node.BottomNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.BottomNeighbor().OrderOnBoard())
                    {
                        first.next = ITEM_TYPE.ITEM_BIGBOMB;
                        break;
                    }
                }
            }
        }
    }
    public void SetMedBombCombine(List<Item> combine)
    {

        bool isSwap = false;

        foreach (Item item in combine)
        {
            if (item.next != ITEM_TYPE.NONE)
            {
                isSwap = true;

                break;
            }
        }

        if (!isSwap)
        {
            Item first = null;

            foreach (Item item in combine)
            {
                if (first == null)
                {
                    first = item;
                }
                else
                {
                    if (item.node.OrderOnBoard() < first.node.OrderOnBoard())
                    {
                        first = item;
                    }
                }
            }

            foreach (Item item in combine)
            {
                if (first.node.RightNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.RightNeighbor().OrderOnBoard())
                    {
                        combine[2].next = ITEM_TYPE.ITEM_MEDBOMB;
                        break;
                    }
                }

                if (first.node.BottomNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.BottomNeighbor().OrderOnBoard())
                    {
                        first.next = ITEM_TYPE.ITEM_MEDBOMB;
                        break;
                    }
                }
            }
        }
    }

    // return 9 items around
    public List<Item> ItemAround(Node node)
    {
        List<Item> items = new List<Item>();

        for (int i = node.i - 1; i <= node.i + 1; i++)
        {
            for (int j = node.j - 1; j <= node.j + 1; j++)
            {
                if (GetNode(i, j) != null)
                {
                    items.Add(GetNode(i, j).item);
                }
            }
        }

        return items;
    }
    public List<Item> ItemAroundMed(Node node)
    {
        List<Item> items = new List<Item>();

        for (int i = node.i - 2; i <= node.i + 2; i++)
        {
            for (int j = node.j - 2; j <= node.j + 2; j++)
            {
                if (GetNode(i, j) != null)
                {
                    items.Add(GetNode(i, j).item);
                }
            }
        }

        return items;
    }
    public List<Item> ItemAroundBig(Node node)
    {
        List<Item> items = new List<Item>();

        for (int i = node.i - 3; i <= node.i + 3; i++)
        {
            for (int j = node.j - 3; j <= node.j + 3; j++)
            {
                if (GetNode(i, j) != null)
                {
                    items.Add(GetNode(i, j).item);
                }
            }
        }

        return items;
    }
    public List<Item> ItemAroundGiant(Node node)
    {
        List<Item> items = new List<Item>();

        for (int i = node.i - 4; i <= node.i + 4; i++)
        {
            for (int j = node.j - 4; j <= node.j + 4; j++)
            {
                if (GetNode(i, j) != null)
                {
                    items.Add(GetNode(i, j).item);
                }
            }
        }

        return items;
    }
    public List<Item> ItemAroundMegaGiant(Node node)
    {
        List<Item> items = new List<Item>();

        for (int i = node.i - 5; i <= node.i + 5; i++)
        {
            for (int j = node.j - 5; j <= node.j + 5; j++)
            {
                if (GetNode(i, j) != null)
                {
                    items.Add(GetNode(i, j).item);
                }
            }
        }

        return items;
    }

    public List<Item> XCrossItems(Node node)
    {
        var items = new List<Item>();

        var row = StageLoader.instance.row;

        for (int i = 0; i < row; i++)
        {
            if (i < node.i)
            {
                var crossLeft = GetNode(i, node.j - (node.i - i));
                var crossRight = GetNode(i, node.j + (node.i - i));

                if (crossLeft != null)
                {
                    if (crossLeft.item != null)
                    {
                        items.Add(crossLeft.item);
                    }
                }

                if (crossRight != null)
                {
                    if (crossRight.item != null)
                    {
                        items.Add(crossRight.item);
                    }
                }
            }
            else if (i == node.i)
            {
                if (node.item != null)
                {
                    items.Add(node.item);
                }
            }
            else if (i > node.i)
            {
                var crossLeft = GetNode(i, node.j - (i - node.i));
                var crossRight = GetNode(i, node.j + (i - node.i));

                if (crossLeft != null)
                {
                    if (crossLeft.item != null)
                    {
                        items.Add(crossLeft.item);
                    }
                }

                if (crossRight != null)
                {
                    if (crossRight.item != null)
                    {
                        items.Add(crossRight.item);
                    }
                }
            }
        }

        return items;
    }

    // return list of items in a column
    public List<Item> ColumnItems(int column)
    {
        var items = new List<Item>();

        var row = StageLoader.instance.row;

        for (int i = 0; i < row; i++)
        {
            if (GetNode(i, column) != null)
            {
                items.Add(GetNode(i, column).item);
            }
        }

        return items;
    }

    // return list of items in a row
    public List<Item> RowItems(int row)
    {
        var items = new List<Item>();

        var column = StageLoader.instance.column;

        for (int j = 0; j < column; j++)
        {
            if (GetNode(row, j) != null)
            {
                items.Add(GetNode(row, j).item);
            }
        }

        return items;
    }

    #endregion

    #region Destroy
    // destroy the whole board when swap 2 rainbow
    // destroy the whole board when swap 2 rainbow
    public void DoubleRainbowDestroy()
    {
        StartCoroutine(DestroyWholeBoard());
    }

    IEnumerator DestroyWholeBoard()
    {
        state = GAME_STATE.DESTROYING_ITEMS;
        var column = StageLoader.instance.column;

        for (int i = 0; i < column; i++)
        {
            List<Item> items = ColumnItems(i);

            foreach (var item in items)
            {
                if (item != null && item.Destroyable() == true)
                {
                    //item.type = item.GetCookie(item.type);

                    //GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

                    //  if (explosion != null) explosion.transform.position = item.transform.position;

                    item.Destroy();
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
        state = GAME_STATE.WAITING_USER_SWAP;
        FindMatches();
        huntering = false;
    }

    // destroy all items of changing list
    public void DestroyChangingList()
    {
        StartCoroutine(StartDestroyChangingList());
    }

    IEnumerator StartDestroyChangingList()
    {
        //print("Start destroy items in the list");

        var originalState = state;

        state = GAME_STATE.DESTROYING_ITEMS;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < changingList.Count; i++)
        {
            var item = changingList[i];

            if (item != null)
            {
                item.Destroy();
            }

            while (destroyingItems > 0)
            {
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForEndOfFrame();

            //           Drop();

            while (droppingItems > 0)
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForEndOfFrame();
        }

        changingList.Clear();

        // state = originalState;
        state = GAME_STATE.WAITING_USER_SWAP;
        FindMatches();
    }


    public void DestroySameColorList(Item boosterItem)
    {
        StartCoroutine(StartDestroySameColorList(boosterItem));
    }

    IEnumerator StartDestroySameColorList(Item boosterItem)
    {

        if (boosterItem == null || huntering)
        {
            yield break;
        }

        List<Item> items = GetListItems();

        foreach (Item item in items)
        {
            if (item != null)
            {
                if (item.color == boosterItem.destroycolor)
                {
                    sameColorList.Add(item);
                }
            }
        }

        //create new Clone booster
        GameObject CloneBooster = null;
        CloneBooster = new GameObject();
        CloneBooster.transform.position = boosterItem.transform.position;


        AudioManager.instance.RainbowBoosterAudio();

        var originalState = state;
        huntering = true;
        state = GAME_STATE.DESTROYING_ITEMS;


        for (int i = 0; i < sameColorList.Count; i++)
        {
            var item = sameColorList[i];

            if (item != null && item.destroying == false)
            {

                if (item != null && item.IsCookie())
                {
                    StartCoroutine(StartFlyTarget(CloneBooster, item.gameObject));
                    //item.Destroy();
                }

                yield return new WaitForSeconds(0.1f);
            }

        }

        yield return new WaitForSeconds(0.5f);
        BoosterArrount(boosterItem);
        yield return new WaitForSeconds(0.2f);
        GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

        if (explosion != null)
        {
            explosion.transform.position = boosterItem.gameObject.transform.position;
            explosion.transform.position = new Vector3(explosion.transform.position.x, explosion.transform.position.y, -12f);
        }
        AudioManager.instance.CageExplodeAudio();
        iTween.ScaleTo(boosterItem.gameObject, iTween.Hash(
          "scale", Vector3.zero,
          "onstart", "OnStartDestroy",
          "oncomplete", "OnCompleteDestroy",
          "easetype", iTween.EaseType.easeInBack,
          "time", Configuration.instance.destroyTime
          ));

        yield return new WaitForSeconds(0.5f);


        for (int i = 0; i < sameColorList.Count; i++)
        {
            var item = sameColorList[i];
            if (item != null && item.destroying == false)
            {
                //GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

                //if (explosion != null) explosion.transform.position = item.transform.position;
                if (item != null && item.IsCookie())
                {

                    item.Destroy();
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        sameColorList.Clear();
        Destroy(CloneBooster);
        merging = false;
        huntering = false;
        state = GAME_STATE.WAITING_USER_SWAP;
        yield return new WaitForSeconds(0.2f);

        FindMatches();
        if (!GameOver)
        {
            PlayerPrefs.SetInt("12", PlayerPrefs.GetInt("12") + 1);
            PlayerPrefs.Save();
            //Debug.Log("12: " + PlayerPrefs.GetInt("12"));
        }
        //Destroy(boosterItem);


    }

    public void DestroyHunterSameColorList()
    {
        StartCoroutine(StartDestroyHunterBoosterSameColorList());

    }

    IEnumerator StartDestroyHunterBoosterSameColorList()
    {
        //print("Start destroy items in the same color list");

        var originalState = state;

        state = GAME_STATE.DESTROYING_ITEMS;

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < sameColorList.Count; i++)
        {
            var item = sameColorList[i];

            if (item != null && item.destroying == false)
            {
                GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

                if (explosion != null) explosion.transform.position = item.transform.position;
                if (item != null && item.IsCookie())
                {
                    item.Destroy();
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        sameColorList.Clear();

        // state = originalState;
        state = GAME_STATE.WAITING_USER_SWAP;
        FindMatches();
        if (!GameOver)
        {
            PlayerPrefs.SetInt("12", PlayerPrefs.GetInt("12") + 1);
            PlayerPrefs.Save();
            //Debug.Log("12: " + PlayerPrefs.GetInt("12"));
        }
    }

    //BOMB ADD
    public void DestroySameColorListBomb(Item boosterItem)
    {
        StartCoroutine(StartDestroySameColorListBomb(boosterItem));
    }
    IEnumerator StartDestroySameColorListBomb(Item boosterItem)
    {
        if (boosterItem == null)
        {
            yield break;
        }
        //print("Start destroy items in the same color list");
        AudioManager.instance.RainbowBoosterAudio();

        var originalState = state;
        huntering = true;

        state = GAME_STATE.DESTROYING_ITEMS;

        //get sprite
        GameObject flyobje = null;
        flyobje = Resources.Load(Configuration.Item1Bomb()) as GameObject;
        var flySprite = flyobje.GetComponent<SpriteRenderer>().sprite;


        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < sameColorList.Count; i++)
        {
            var item = sameColorList[i];

            if (item != null && item.destroying == false)
            {

                if (item != null && item.IsCookie() && boosterItem.gameObject != null && flySprite != null)
                {
                    StartCoroutine(StartFlyTarget(boosterItem.gameObject, item.gameObject, item.ChangeToBombBreaker, flySprite));
                    yield return new WaitForSeconds(0.1f);
                }

            }

        }
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);

        StartCoroutine(StartBoosterList());
        sameColorList.Clear();
    }



    //ROW or COLUMN ADD
    public void DestroySameColorListRow(Item boosterItem)
    {
        StartCoroutine(StartDestroySameColorListRow(boosterItem));
    }
    IEnumerator StartDestroySameColorListRow(Item boosterItem)
    {
        if (boosterItem == null)
        {
            yield break;
        }
        //print("Start destroy items in the same color list");
        AudioManager.instance.RainbowBoosterAudio();
        var originalState = state;
        huntering = true;

        state = GAME_STATE.DESTROYING_ITEMS;

        //get sprite
        GameObject flyobje = null;
        flyobje = Resources.Load(Configuration.Item1Column()) as GameObject;
        var flySprite = flyobje.GetComponent<SpriteRenderer>().sprite;

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < sameColorList.Count; i++)
        {
            var item = sameColorList[i];

            if (item != null && item.destroying == false)
            {


                if (item != null && item.IsCookie() && boosterItem.gameObject != null && flySprite != null)
                {
                    StartCoroutine(StartFlyTarget(boosterItem.gameObject, item.gameObject, item.ChangeToColRowBreaker, flySprite));
                    yield return new WaitForSeconds(0.1f);
                }

            }

        }
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);


        StartCoroutine(StartBoosterList());
        sameColorList.Clear();
    }

    //CROSS ADD
    public void DestroySameColorListCross(Item boosterItem)
    {
        StartCoroutine(StartDestroySameColorListCross(boosterItem));
    }
    IEnumerator StartDestroySameColorListCross(Item boosterItem)
    {
        if (boosterItem == null)
        {
            yield break;
        }
        //print("Start destroy items in the same color list");
        AudioManager.instance.RainbowBoosterAudio();
        //var originalState = state;
        huntering = true;
        state = GAME_STATE.DESTROYING_ITEMS;


        //get sprite
        GameObject flyobje = null;
        flyobje = Resources.Load(Configuration.Item1Cross()) as GameObject;
        var flySprite = flyobje.GetComponent<SpriteRenderer>().sprite;
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < sameColorList.Count; i++)
        {
            var item = sameColorList[i];

            if (item != null && item.destroying == false)
            {

                if (item != null && item.IsCookie() && boosterItem.gameObject != null && flySprite != null)
                {
                    StartCoroutine(StartFlyTarget(boosterItem.gameObject, item.gameObject, item.ChangeToXBreaker, flySprite));
                    yield return new WaitForSeconds(0.1f);
                }

            }

        }
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);

        StartCoroutine(StartBoosterList());
        sameColorList.Clear();

    }

    IEnumerator StartBoosterList()
    {
        var items = GetListItems();
        //var boosters = new List<Item>();
        foreach (var item in items)
        {
            if (item != null && item.IsBooster())
            {
                if (boosterItems.Contains(item))
                {
                    continue;
                }
                boosterItems.Add(item);
            }
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < boosterItems.Count; i++)
        {
            if (boosterItems[i] != null)
            {
                // int a = UnityEngine.Random.Range(0, boosters.Count);
                boosterItems[i].Destroy();
                yield return new WaitForSeconds(0.3f);
            }

            //boosters.RemoveAt(i);

        }
        yield return new WaitForSeconds(0.5f);



        state = GAME_STATE.WAITING_USER_SWAP;
        merging = false;
        huntering = false;
        lockSwap = false;

        boosterItems.Clear();
        FindMatches();

    }

    public void StartFX(GameObject item)
    {
        GameObject explosion1 = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

        if (explosion1 != null)
        {
            explosion1.transform.position = item.transform.position;
        }
        AudioManager.instance.PopupBosTikirtiAudio();


    }


    public void DestroyNeighborItems(Item item)
    {
        DestroyMarshmallow(item);

        DestroyChocolate(item);

        DestroyRockCandy(item);
    }



    public void DestroyMarshmallow(Item item)
    {
        if (item.IsMarshmallow() == true ||
            item.IsCollectible() == true ||
            item.IsGingerbread() == true ||
            item.IsChocolate() == true ||
            item.IsRockCandy() == true)
        {
            return;
        }

        if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            return;
        }

        var marshmallows = new List<Item>();

        if (item.node.TopNeighbor() != null && item.node.TopNeighbor().item != null && item.node.TopNeighbor().item.IsMarshmallow() == true)
        {
            marshmallows.Add(item.node.TopNeighbor().item);
        }

        if (item.node.RightNeighbor() != null && item.node.RightNeighbor().item != null && item.node.RightNeighbor().item.IsMarshmallow() == true)
        {
            marshmallows.Add(item.node.RightNeighbor().item);
        }

        if (item.node.BottomNeighbor() != null && item.node.BottomNeighbor().item != null && item.node.BottomNeighbor().item.IsMarshmallow() == true)
        {
            marshmallows.Add(item.node.BottomNeighbor().item);
        }

        if (item.node.LeftNeighbor() != null && item.node.LeftNeighbor().item != null && item.node.LeftNeighbor().item.IsMarshmallow() == true)
        {
            marshmallows.Add(item.node.LeftNeighbor().item);
        }

        foreach (var marshmallow in marshmallows)
        {
            marshmallow.Destroy();
        }
    }

    public void DestroyChocolate(Item item)
    {
        if (item.IsMarshmallow() == true ||
            item.IsCollectible() == true ||
            item.IsGingerbread() == true ||
            item.IsChocolate() == true ||
            item.IsRockCandy() == true)
        {
            return;
        }

        if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            return;
        }

        var chocolates = new List<Item>();

        if (item.node.TopNeighbor() != null && item.node.TopNeighbor().item != null && item.node.TopNeighbor().item.IsChocolate() == true)
        {
            chocolates.Add(item.node.TopNeighbor().item);
        }

        if (item.node.RightNeighbor() != null && item.node.RightNeighbor().item != null && item.node.RightNeighbor().item.IsChocolate() == true)
        {
            chocolates.Add(item.node.RightNeighbor().item);
        }

        if (item.node.BottomNeighbor() != null && item.node.BottomNeighbor().item != null && item.node.BottomNeighbor().item.IsChocolate() == true)
        {
            chocolates.Add(item.node.BottomNeighbor().item);
        }

        if (item.node.LeftNeighbor() != null && item.node.LeftNeighbor().item != null && item.node.LeftNeighbor().item.IsChocolate() == true)
        {
            chocolates.Add(item.node.LeftNeighbor().item);
        }

        foreach (var chocolate in chocolates)
        {
            chocolate.Destroy();
        }
    }

    public void DestroyRockCandy(Item item)
    {
        if (item.IsMarshmallow() == true ||
            item.IsCollectible() == true ||
            item.IsGingerbread() == true ||
            item.IsChocolate() == true ||
            item.IsRockCandy() == true)
        {
            return;
        }

        if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            return;
        }

        var rocks = new List<Item>();

        if (item.node.TopNeighbor() != null && item.node.TopNeighbor().item != null && item.node.TopNeighbor().item.IsRockCandy() == true && item.node.TopNeighbor().item.color == item.color)
        {
            rocks.Add(item.node.TopNeighbor().item);
        }

        if (item.node.RightNeighbor() != null && item.node.RightNeighbor().item != null && item.node.RightNeighbor().item.IsRockCandy() == true && item.node.RightNeighbor().item.color == item.color)
        {
            rocks.Add(item.node.RightNeighbor().item);
        }

        if (item.node.BottomNeighbor() != null && item.node.BottomNeighbor().item != null && item.node.BottomNeighbor().item.IsRockCandy() == true && item.node.BottomNeighbor().item.color == item.color)
        {
            rocks.Add(item.node.BottomNeighbor().item);
        }

        if (item.node.LeftNeighbor() != null && item.node.LeftNeighbor().item != null && item.node.LeftNeighbor().item.IsRockCandy() == true && item.node.LeftNeighbor().item.color == item.color)
        {
            rocks.Add(item.node.LeftNeighbor().item);
        }

        foreach (var rock in rocks)
        {
            rock.Destroy();
        }
    }



    #endregion

    #region Collect

    // if item is the target to collect
    public void CollectItem(Item item)
    {
        GameObject flyingItem = null;
        var order = 0;

        // cookie
        if (item.IsCookie())
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.ITEM && StageLoader.instance.target1Color == item.color && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.ITEM && StageLoader.instance.target2Color == item.color && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.ITEM && StageLoader.instance.target3Color == item.color && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.ITEM && StageLoader.instance.target4Color == item.color && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Cookie";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = null;

                switch (item.color)
                {
                    case 1:
                        prefab = Resources.Load(Configuration.Item1()) as GameObject;
                        break;
                    case 2:
                        prefab = Resources.Load(Configuration.Item2()) as GameObject;
                        break;
                    case 3:
                        prefab = Resources.Load(Configuration.Item3()) as GameObject;
                        break;
                    case 4:
                        prefab = Resources.Load(Configuration.Item4()) as GameObject;
                        break;
                    case 5:
                        prefab = Resources.Load(Configuration.Item5()) as GameObject;
                        break;
                    case 6:
                        prefab = Resources.Load(Configuration.Item6()) as GameObject;
                        break;
                }

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // gingerbread
        else if (item.IsGingerbread())
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.ROCKET && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.ROCKET && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.ROCKET && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.ROCKET && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Gingerbread";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configuration.RocketGeneric()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // marshmallow
        else if (item.IsMarshmallow())
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.BREAKABLE && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.BREAKABLE && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.BREAKABLE && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.BREAKABLE && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Marshmallow";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                //breakable
                int stage = StageLoader.instance.Stage;
                int changeLevels = StageLoader.instance.BreakableChangeLevels;


                if (stage % (9 * changeLevels) >= 8 * changeLevels)
                {
                    GameObject prefab = Resources.Load(Configuration.Breakable8()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (9 * changeLevels) >= 7 * changeLevels)
                {
                    GameObject prefab = Resources.Load(Configuration.Breakable7()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (9 * changeLevels) >= 6 * changeLevels)
                {
                    GameObject prefab = Resources.Load(Configuration.Breakable6()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (9 * changeLevels) >= 5 * changeLevels)
                {
                    GameObject prefab = Resources.Load(Configuration.Breakable5()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (9 * changeLevels) >= 4 * changeLevels)
                {
                    GameObject prefab = Resources.Load(Configuration.Breakable4()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (9 * changeLevels) >= 3 * changeLevels)
                {
                    GameObject prefab = Resources.Load(Configuration.Breakable3()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (9 * changeLevels) >= 2 * changeLevels)
                {
                    GameObject prefab = Resources.Load(Configuration.Breakable2()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (9 * changeLevels) >= changeLevels)
                {
                    GameObject prefab = Resources.Load(Configuration.Breakable1()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else
                {
                    GameObject prefab = Resources.Load(Configuration.Breakable()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }



                //if (prefab != null)
                //{
                //    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                //}

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // chocolate
        else if (item.IsChocolate())
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.TOYMINE && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.TOYMINE && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.TOYMINE && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.TOYMINE && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Chocolate";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                int stage = StageLoader.instance.Stage;
                int changeLevelsChoco = StageLoader.instance.ChocolateChangeLevels;
                int changeLevelsBubbles = StageLoader.instance.BubblesChangeLevels;

                if (stage % (6 * changeLevelsChoco) >= 5 * changeLevelsChoco)
                {
                    GameObject prefab = Resources.Load(Configuration.ToyMine1e()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (6 * changeLevelsChoco) >= 4 * changeLevelsChoco)
                {
                    GameObject prefab = Resources.Load(Configuration.ToyMine1d()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (6 * changeLevelsChoco) >= 3 * changeLevelsChoco)
                {
                    GameObject prefab = Resources.Load(Configuration.ToyMine1c()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (6 * changeLevelsChoco) >= 2 * changeLevelsChoco)
                {
                    GameObject prefab = Resources.Load(Configuration.ToyMine1b()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (6 * changeLevelsChoco) >= changeLevelsChoco)
                {
                    GameObject prefab = Resources.Load(Configuration.ToyMine1a()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else
                {
                    GameObject prefab = Resources.Load(Configuration.ToyMine1()) as GameObject;
                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                //GameObject prefab = Resources.Load(Configuration.ToyMine1()) as GameObject;



                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // column_row_breaker
        else if (item.IsColumnBreaker(item.type) == true || item.IsRowBreaker(item.type) == true)
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Column Row Breaker";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configuration.ColumnRowBreaker()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // generic bomb breaker
        else if (item.IsBombBreaker(item.type) == true)
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.BOMB_BREAKER && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.BOMB_BREAKER && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.BOMB_BREAKER && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.BOMB_BREAKER && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Bomb Breaker";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configuration.GenericBombBreaker()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // generic x_breaker
        else if (item.IsXBreaker(item.type) == true)
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.X_BREAKER && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.X_BREAKER && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.X_BREAKER && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.X_BREAKER && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying X Breaker";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configuration.GenericXBreaker()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // rainbow
        else if (item.type == ITEM_TYPE.ITEM_COLORCONE)
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.COLORCONE && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.COLORCONE && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.COLORCONE && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.COLORCONE && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Rainbow";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configuration.ItemColorCone()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // med bomb
        else if (item.type == ITEM_TYPE.ITEM_MEDBOMB)
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.MEDBOMB && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.MEDBOMB && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.MEDBOMB && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.MEDBOMB && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Medbomb";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configuration.MedBomb()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // big bomb
        else if (item.type == ITEM_TYPE.ITEM_BIGBOMB)
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.BIGBOMB && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.BIGBOMB && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.BIGBOMB && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.BIGBOMB && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying BigBomb";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configuration.BigBomb()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // rock candy
        else if (item.IsRockCandy() == true)
        {
            if (StageLoader.instance.target1Type == TARGET_TYPE.ROCK_CANDY && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (StageLoader.instance.target2Type == TARGET_TYPE.ROCK_CANDY && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (StageLoader.instance.target3Type == TARGET_TYPE.ROCK_CANDY && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (StageLoader.instance.target4Type == TARGET_TYPE.ROCK_CANDY && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Rock Candy";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                int stage = StageLoader.instance.Stage;
                int changeLevels = StageLoader.instance.BreakableChangeLevels;
                int changeLevelsLego = StageLoader.instance.LegoChangeLevels;



                if (stage % (5 * changeLevelsLego) >= 4 * changeLevelsLego)
                {

                    GameObject prefab = Resources.Load(Configuration.LegoBoxGenericd()) as GameObject;

                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (5 * changeLevelsLego) >= 3 * changeLevelsLego)
                {

                    GameObject prefab = Resources.Load(Configuration.LegoBoxGenericc()) as GameObject;

                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (5 * changeLevelsLego) >= 2 * changeLevelsLego)
                {

                    GameObject prefab = Resources.Load(Configuration.LegoBoxGenericb()) as GameObject;

                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else if (stage % (5 * changeLevelsLego) >= changeLevelsLego)
                {

                    GameObject prefab = Resources.Load(Configuration.LegoBoxGenerica()) as GameObject;

                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }
                else
                {

                    GameObject prefab = Resources.Load(Configuration.LegoBoxGeneric()) as GameObject;

                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }
                }





                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
    }

    public void CollectWaffle(Waffle waffle)
    {
        GameObject flyingItem = null;
        var order = 0;

        if (StageLoader.instance.target1Type == TARGET_TYPE.WAFFLE && target1Left > 0)
        {
            target1Left--;
            flyingItem = new GameObject();
            order = 1;
        }
        else if (StageLoader.instance.target2Type == TARGET_TYPE.WAFFLE && target2Left > 0)
        {
            target2Left--;
            flyingItem = new GameObject();
            order = 2;
        }
        else if (StageLoader.instance.target3Type == TARGET_TYPE.WAFFLE && target3Left > 0)
        {
            target3Left--;
            flyingItem = new GameObject();
            order = 3;
        }
        else if (StageLoader.instance.target4Type == TARGET_TYPE.WAFFLE && target4Left > 0)
        {
            target4Left--;
            flyingItem = new GameObject();
            order = 4;
        }

        if (flyingItem != null)
        {
            flyingItem.transform.position = waffle.transform.position;
            flyingItem.name = "Flying Waffle";
            flyingItem.layer = LayerMask.NameToLayer("On Top UI");
            flyingItem.transform.localScale = new Vector3(0.75f, 0.75f, 0);

            SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

            GameObject prefab = Resources.Load(Configuration.Waffle1()) as GameObject; ;

            spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;

            StartCoroutine(CollectItemAnim(flyingItem, order));
        }
    }

    public void CollectCage(Cage cage)
    {
        GameObject flyingItem = null;

        var order = 0;

        if (StageLoader.instance.target1Type == TARGET_TYPE.LOCK && target1Left > 0)
        {
            target1Left--;
            flyingItem = new GameObject();
            order = 1;
        }
        else if (StageLoader.instance.target2Type == TARGET_TYPE.LOCK && target2Left > 0)
        {
            target2Left--;
            flyingItem = new GameObject();
            order = 2;
        }
        else if (StageLoader.instance.target3Type == TARGET_TYPE.LOCK && target3Left > 0)
        {
            target3Left--;
            flyingItem = new GameObject();
            order = 3;
        }
        else if (StageLoader.instance.target4Type == TARGET_TYPE.LOCK && target4Left > 0)
        {
            target4Left--;
            flyingItem = new GameObject();
            order = 4;
        }

        if (flyingItem != null)
        {
            flyingItem.transform.position = cage.transform.position;
            flyingItem.name = "Flying Cage";
            flyingItem.layer = LayerMask.NameToLayer("On Top UI");
            flyingItem.transform.localScale = new Vector3(0.75f, 0.75f, 0);

            SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

            GameObject prefab = Resources.Load(Configuration.Lock1()) as GameObject; ;

            spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;

            StartCoroutine(CollectItemAnim(flyingItem, order));
        }
    }

    // if the collectible
    bool CollectCollectible()
    {
        if (GameObject.Find("Flying Collectible"))
        {
            return false;
        }

        if (StageLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
        {
            return false;
        }

        var items = GetListItems();

        foreach (var item in items)
        {
            bool collectable = false;

            // check each item in last row and in column which can collect and item is collectible
            if (item != null && (item.node.i == StageLoader.instance.row - 1) && StageLoader.instance.collectibleCollectColumnMarkers.Contains(item.node.j) && item.IsCollectible() == true)
            {
                collectable = true;

            }

            // collectible marker by node
            if (item != null &&
                StageLoader.instance.collectibleCollectNodeMarkers.Contains(NodeOrder(item.node.i, item.node.j)) &&
                item.IsCollectible() == true)
            {
                collectable = true;
            }

            if (collectable == true)
            {
                GameObject flyingItem = null;
                var order = 0;

                if (StageLoader.instance.target1Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target1Color == item.color && target1Left > 0)
                {
                    target1Left--;
                    flyingItem = new GameObject();
                    order = 1;
                }
                else if (StageLoader.instance.target2Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target2Color == item.color && target2Left > 0)
                {
                    target2Left--;
                    flyingItem = new GameObject();
                    order = 2;
                }
                else if (StageLoader.instance.target3Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target3Color == item.color && target3Left > 0)
                {
                    target3Left--;
                    flyingItem = new GameObject();
                    order = 3;
                }
                else if (StageLoader.instance.target4Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target4Color == item.color && target4Left > 0)
                {
                    target4Left--;
                    flyingItem = new GameObject();
                    order = 4;
                }
                else if (StageLoader.instance.target1Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target1Color == item.color && target1Left == 0)
                {
                    flyingItem = new GameObject();
                    order = 1;
                }
                else if (StageLoader.instance.target2Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target2Color == item.color && target2Left == 0)
                {
                    flyingItem = new GameObject();
                    order = 2;
                }
                else if (StageLoader.instance.target3Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target3Color == item.color && target3Left == 0)
                {
                    flyingItem = new GameObject();
                    order = 3;
                }
                else if (StageLoader.instance.target4Type == TARGET_TYPE.COLLECTIBLE && StageLoader.instance.target4Color == item.color && target4Left == 0)
                {
                    flyingItem = new GameObject();
                    order = 4;
                }

                if (flyingItem != null)
                {
                    flyingItem.transform.position = item.transform.position;
                    flyingItem.name = "Flying Collectible";
                    flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                    SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                    GameObject prefab = null;

                    switch (item.color)
                    {
                        case 1:
                            prefab = Resources.Load(Configuration.Collectible1()) as GameObject;
                            break;
                        case 2:
                            prefab = Resources.Load(Configuration.Collectible2()) as GameObject;
                            break;
                        case 3:
                            prefab = Resources.Load(Configuration.Collectible3()) as GameObject;
                            break;
                        case 4:
                            prefab = Resources.Load(Configuration.Collectible4()) as GameObject;
                            break;
                        case 5:
                            prefab = Resources.Load(Configuration.Collectible5()) as GameObject;
                            break;
                        case 6:
                            prefab = Resources.Load(Configuration.Collectible6()) as GameObject;
                            break;
                        case 7:
                            prefab = Resources.Load(Configuration.Collectible7()) as GameObject;
                            break;
                        case 8:
                            prefab = Resources.Load(Configuration.Collectible7()) as GameObject;
                            break;
                        case 9:
                            prefab = Resources.Load(Configuration.Collectible9()) as GameObject;
                            break;
                        case 10:
                            prefab = Resources.Load(Configuration.Collectible10()) as GameObject;
                            break;
                        case 11:
                            prefab = Resources.Load(Configuration.Collectible11()) as GameObject;
                            break;
                        case 12:
                            prefab = Resources.Load(Configuration.Collectible12()) as GameObject;
                            break;
                        case 13:
                            prefab = Resources.Load(Configuration.Collectible13()) as GameObject;
                            break;
                        case 14:
                            prefab = Resources.Load(Configuration.Collectible14()) as GameObject;
                            break;
                        case 15:
                            prefab = Resources.Load(Configuration.Collectible15()) as GameObject;
                            break;
                        case 16:
                            prefab = Resources.Load(Configuration.Collectible16()) as GameObject;
                            break;
                        case 17:
                            prefab = Resources.Load(Configuration.Collectible17()) as GameObject;
                            break;
                        case 18:
                            prefab = Resources.Load(Configuration.Collectible18()) as GameObject;
                            break;
                        case 19:
                            prefab = Resources.Load(Configuration.Collectible19()) as GameObject;
                            break;
                        case 20:
                            prefab = Resources.Load(Configuration.Collectible20()) as GameObject;
                            break;
                    }

                    if (prefab != null)
                    {
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }

                    StartCoroutine(CollectItemAnim(flyingItem, order));

                    item.Destroy(true);

                    return true;
                }
            }
        }

        return false;
    }

    // item fly to target
    IEnumerator CollectItemAnim(GameObject item, int order)
    {
        yield return new WaitForFixedUpdate();

        GameObject target = null;

        switch (order)
        {
            case 1:
                target = target1;
                break;
            case 2:
                target = target2;
                break;
            case 3:
                target = target3;
                break;
            case 4:
                target = target4;
                break;
        }

        flyingItems++;

        AnimationCurve curveX = new AnimationCurve(new Keyframe(0, item.transform.localPosition.x), new Keyframe(0.4f, target.transform.position.x));
        AnimationCurve curveY = new AnimationCurve(new Keyframe(0, item.transform.localPosition.y), new Keyframe(0.4f, target.transform.position.y));
        curveX.AddKey(0.12f, item.transform.localPosition.x + UnityEngine.Random.Range(-1f, 2f));
        curveY.AddKey(0.12f, item.transform.localPosition.y + UnityEngine.Random.Range(-1f, 0f));

        float startTime = Time.time;
        float speed = 1.2f + flyingItems * Configuration.instance.swapTime;
        float distCovered = 0;
        while (distCovered < 0.4f)
        {
            distCovered = (Time.time - startTime) / speed;
            item.transform.localPosition = new Vector3(curveX.Evaluate(distCovered), curveY.Evaluate(distCovered), 0);

            yield return new WaitForFixedUpdate();
        }

        AudioManager.instance.CollectTargetAudio();

        switch (order)
        {
            case 1:
                UITarget.UpdateTargetAmount(1);
                break;
            case 2:
                UITarget.UpdateTargetAmount(2);
                break;
            case 3:
                UITarget.UpdateTargetAmount(3);
                break;
            case 4:
                UITarget.UpdateTargetAmount(4);
                break;
        }

        Destroy(item);

        flyingItems--;
    }


    public void FlyTarget(GameObject source, GameObject target, Action change = null, Sprite flyobjeSprite = null)
    {
        StartCoroutine(StartFlyTarget(source, target, change, flyobjeSprite));
    }

    IEnumerator StartFlyTarget(GameObject source, GameObject target, Action change = null, Sprite flyobjeSprite = null)
    {
        yield return new WaitForFixedUpdate();



        var prefabFX = Instantiate(Resources.Load(Configuration.StarGoldFX())) as GameObject;
        prefabFX.transform.position = source.transform.position;
        var prefab = Instantiate(Resources.Load(Configuration.StarGold())) as GameObject;
        if (flyobjeSprite != null)
        {

            prefab.GetComponent<SpriteRenderer>().sprite = flyobjeSprite;
            prefab.transform.position = source.transform.position;
        }
        else { Destroy(prefab); }

        yield return new WaitForFixedUpdate();

        if (prefabFX != null)
        {

            flyingItems++;

            AnimationCurve curveX = new AnimationCurve(new Keyframe(0, prefabFX.transform.localPosition.x), new Keyframe(0.4f, target.transform.position.x));
            AnimationCurve curveY = new AnimationCurve(new Keyframe(0, prefabFX.transform.localPosition.y), new Keyframe(0.4f, target.transform.position.y));
            curveX.AddKey(0.12f, prefabFX.transform.localPosition.x + UnityEngine.Random.Range(-1f, 2f));
            curveY.AddKey(0.12f, prefabFX.transform.localPosition.y + UnityEngine.Random.Range(-1f, 0f));

            float startTime = Time.time;
            float speed = 1.2f + flyingItems * 0.1f;
            float distCovered = 0;
            while (distCovered < 0.4f)
            {
                distCovered = (Time.time - startTime) / speed;
                if (flyobjeSprite != null && prefab != null)
                {

                    prefab.transform.localPosition = new Vector3(curveX.Evaluate(distCovered), curveY.Evaluate(distCovered), 0);
                }

                if (prefabFX != null)
                {
                    prefabFX.transform.localPosition = new Vector3(curveX.Evaluate(distCovered), curveY.Evaluate(distCovered), 0);
                }

                yield return new WaitForFixedUpdate();
            }


            if (flyobjeSprite != null)
            {
                Destroy(prefab);
                AudioManager.instance.CollectTargetAudio();
                //AudioManager.instance.Star1Audio();
            }
            else
            {

                AudioManager.instance.DropAudio();
            }

            if (change != null)
            {
                change();
                StartFX(target);
            }

            if (target.gameObject != null)
            {
                iTween.ShakeRotation(target.gameObject, iTween.Hash(
       "name", "HintAnimation",
       "amount", new Vector3(0f, 0f, 15f),
       "easetype", iTween.EaseType.easeOutBack,
       "time", 10f
   ));
            }


            flyingItems--;

        }


    }
    #endregion

    #region Popup

   void TimeupPopup()
    {
        StartCoroutine(StartTimeupPopup());
    }

    IEnumerator StartTimeupPopup()
    {
        state = GAME_STATE.OPENING_POPUP;

        yield return new WaitForSeconds(0.3f);

        AudioManager.instance.PopupTargetAudio();
        Debug.LogError("Time up pop up");
        timeupPopup.OpenPopup();

        yield return new WaitForSeconds(2.5f);

        var popup = GameObject.Find("TimeUp(Clone)");

        if (popup)
        {
            popup.GetComponent<Popup>().Close();
        }


        yield return new WaitForSeconds(0.7f);



        Configuration.instance.MenuScene = false;
        state = GAME_STATE.WAITING_USER_SWAP;
        StartCoroutine(CheckHint());
        
           if (Timer.instance.timeOut == true)
        {
            Timer.instance.timeOut = false;
            GameEnd();
        }

    }




    void TargetPopup()
    {
        StartCoroutine(StartTargetPopup());
    }

    IEnumerator StartTargetPopup()
    {
        state = GAME_STATE.OPENING_POPUP;

        yield return new WaitForSeconds(0.3f);

        AudioManager.instance.PopupTargetAudio();

        targetPopup.OpenPopup();

        yield return new WaitForSeconds(2.5f);

        var popup = GameObject.Find("Target(Clone)");

        if (popup)
        {
            popup.GetComponent<Popup>().Close();
        }
        if (Configuration.instance.Tutorial)
        {
            ShowHelp();
        }

        BeginBooster();
        yield return new WaitForSeconds(0.7f);


        if (Configuration.instance.RewardPlay)
        {
            Configuration.instance.RewardPlay = false;

            //Achievement
            Configuration.SaveAchievement("ach_Rewardbeginboosters", 1);

            var items = GetListItems();
            var cookies = new List<Item>();

            foreach (var item in items)
            {
                if (item != null && item.IsCookie() && item.Movable())
                {
                    cookies.Add(item);
                }
            }

            int c1 = UnityEngine.Random.Range(5, cookies.Count);
            //Debug.Log("c1: " + c1);
            var cookie1 = cookies[c1];

            int c2 = UnityEngine.Random.Range(3, c1);
            //Debug.Log("c2: " + c2);
            var cookie2 = cookies[c2];

            int c3 = UnityEngine.Random.Range(0, c2);
            //Debug.Log("c3: " + c3);
            var cookie3 = cookies[c3];


            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                cookie1.ChangeToColRowBreaker();
                AudioManager.instance.SingleBoosterAudio();
                AudioManager.instance.Star1Audio();
                GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

                if (explosion != null)
                {
                    explosion.transform.position = cookie1.transform.position;
                }
            }
            else
            {
                cookie1.ChangeToBombBreaker();
                AudioManager.instance.SingleBoosterAudio();
                AudioManager.instance.Star1Audio();
                GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

                if (explosion != null)
                {
                    explosion.transform.position = cookie1.transform.position;
                }
            }

            yield return new WaitForSeconds(0.5f);




            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                cookie2.ChangeToColRowBreaker();
                AudioManager.instance.SingleBoosterAudio();
                AudioManager.instance.Star2Audio();
                GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

                if (explosion != null)
                {
                    explosion.transform.position = cookie2.transform.position;
                }
            }
            else
            {
                cookie2.ChangeToBombBreaker();
                AudioManager.instance.SingleBoosterAudio();
                AudioManager.instance.Star2Audio();
                GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

                if (explosion != null)
                {
                    explosion.transform.position = cookie2.transform.position;
                }
            }

            yield return new WaitForSeconds(0.5f);


            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                cookie3.ChangeToColRowBreaker();
                AudioManager.instance.SingleBoosterAudio();
                AudioManager.instance.Star3Audio();
                GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

                if (explosion != null)
                {
                    explosion.transform.position = cookie3.transform.position;
                }
            }
            else
            {
                cookie3.ChangeToBombBreaker();
                AudioManager.instance.SingleBoosterAudio();
                AudioManager.instance.Star3Audio();
                GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

                if (explosion != null)
                {
                    explosion.transform.position = cookie3.transform.position;
                }
            }
         


        }

        Configuration.instance.MenuScene = false;
        state = GAME_STATE.WAITING_USER_SWAP;
        StartCoroutine(CheckHint());

    }

    IEnumerator Plus5MovesPopup()
    {
        Configuration.instance.beginFiveMoves = false;

        plus5MovesPopup.OpenPopup();

        yield return new WaitForSeconds(1.0f);

        var popup = GameObject.Find("Plus5MovesPopup(Clone)");

        if (popup)
        {
            popup.GetComponent<Popup>().Close();
        }
    }

    public void AddThreeBooster()
    {
        return;
        //khudcommentkiyaskillz
        StartCoroutine(StartAddThreeBooster());
    }

    IEnumerator StartAddThreeBooster()
    {
        var items = GetListItems();
        var cookies = new List<Item>();

        foreach (var item in items)
        {
            if (item != null && item.IsCookie() && item.Movable())
            {
                cookies.Add(item);
            }
        }

        int c1 = UnityEngine.Random.Range(5, cookies.Count);
        //Debug.Log("c1: " + c1);
        var cookie1 = cookies[c1];

        int c2 = UnityEngine.Random.Range(3, c1);
        //Debug.Log("c2: " + c2);
        var cookie2 = cookies[c2];

        int c3 = UnityEngine.Random.Range(0, c2);
        //Debug.Log("c3: " + c3);
        var cookie3 = cookies[c3];

        yield return new WaitForSeconds(1.0f);
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            cookie1.ChangeToColRowBreaker();
            AudioManager.instance.SingleBoosterAudio();
            AudioManager.instance.Star1Audio();
            GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

            if (explosion != null)
            {
                explosion.transform.position = cookie1.transform.position;
            }
        }
        else
        {
            cookie1.ChangeToBombBreaker();
            AudioManager.instance.SingleBoosterAudio();
            AudioManager.instance.Star1Audio();
            GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

            if (explosion != null)
            {
                explosion.transform.position = cookie1.transform.position;
            }
        }

        yield return new WaitForSeconds(0.5f);




        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            cookie2.ChangeToColRowBreaker();
            AudioManager.instance.SingleBoosterAudio();
            AudioManager.instance.Star2Audio();
            GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

            if (explosion != null)
            {
                explosion.transform.position = cookie2.transform.position;
            }
        }
        else
        {
            cookie2.ChangeToBombBreaker();
            AudioManager.instance.SingleBoosterAudio();
            AudioManager.instance.Star2Audio();
            GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

            if (explosion != null)
            {
                explosion.transform.position = cookie2.transform.position;
            }
        }

        yield return new WaitForSeconds(0.5f);


        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            cookie3.ChangeToColRowBreaker();
            AudioManager.instance.SingleBoosterAudio();
            AudioManager.instance.Star3Audio();
            GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

            if (explosion != null)
            {
                explosion.transform.position = cookie3.transform.position;
            }
        }
        else
        {
            cookie3.ChangeToBombBreaker();
            AudioManager.instance.SingleBoosterAudio();
            AudioManager.instance.Star3Audio();
            GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

            if (explosion != null)
            {
                explosion.transform.position = cookie3.transform.position;
            }
        }
       


    }

    bool check_Once = true;
    //bilalbhai
    public void ShowInspiringPopup(int i)
    {
        print("is coming here");
        //print("Excellent!, Amazing!, Great!, Nice!");
        //int sound = UnityEngine.Random.Range(0, 11);
        if (check_Once)
        {
            check_Once = false;
            int encouraging = UnityEngine.Random.Range(0, 4);
            switch (encouraging)
            {
                case 0:
                    StartCoroutine(InspiringPopup(awesomePopup, encouraging));
                    AudioManager.instance.awesomeAudio();
                    break;
                case 1:
                    StartCoroutine(InspiringPopup(fabPopup, encouraging));
                    AudioManager.instance.fabAudio();
                    break;
                case 2:
                    StartCoroutine(InspiringPopup(brilliantPopup, encouraging));
                    AudioManager.instance.brilliantAudio();
                    break;
                case 3:
                    StartCoroutine(InspiringPopup(excellentPopup, encouraging));
                    AudioManager.instance.exellentAudio();
                    break;
                case 4:
                    StartCoroutine(InspiringPopup(greatPopup, encouraging));
                    AudioManager.instance.greatAudio();
                    break;
            }
            Invoke("CheckFalse", 6f);
        }
    }
   
    void CheckFalse()
    {
        check_Once = true;
    }

    IEnumerator InspiringPopup(PopupOpener popupOpener, int encouraging)
    {
        // prevent multiple call
        if (showingInspiringPopup == false) showingInspiringPopup = true;
        else yield return null;

        popupOpener.OpenPopup();

        yield return new WaitForSeconds(1.0f);

        GameObject popup = null;

        switch (encouraging)
        {
            /*  case 0:
                  popup = GameObject.Find("AmazingPopup(Clone)");
                  break;*/
            case 1:
                popup = GameObject.Find("ExcellentPopup(Clone)");
                break;
            case 2:
                popup = GameObject.Find("GreatPopup(Clone)");
                break;
        }

        if (popup)
        {
            popup.GetComponent<Popup>().Close();
        }

        yield return new WaitForSeconds(1f);

        showingInspiringPopup = false;
    }

    #endregion

    #region Complete

    bool IsLevelCompleted()
    {
        if (target1Left == 0 && target2Left == 0 && target3Left == 0 && target4Left == 0)
        {
            return true;
        }

        return false;
    }

    // auto play the left moves when target is reached
    IEnumerator PreWinAutoPlay()
    {
        HideHint();

        // reset drop time
        dropTime = 1;

        state = GAME_STATE.OPENING_POPUP;

        yield return new WaitForSeconds(0.5f);

        //achievement
        //Configuration.SaveAchievement("ach_moveleft", moveLeft);

        // completed popup

        completedPopup.OpenPopup();

        BackgroundMusic.instance.StopMusic();
        Configuration.instance.playing = false;

        AudioManager.instance.PopupCompletedAudio();
        yield return new WaitForEndOfFrame();
        AudioManager.instance.PopupCompletedMusicAudio();

        yield return new WaitForSeconds(1.75f);

        if (GameObject.Find("CompletedPopup(Clone)"))
        {

            //var animator = GameObject.Find("LOGOANIOBJE").GetComponent<Animator>();
            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("logoani"))
            //    animator.Play("logoaniclose");

            GameObject.Find("CompletedPopup(Clone)").GetComponent<Popup>().Close();

            //NextButon.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            // Confeti Fırlat
            try
            {
                Canvas d_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                var confeti = Instantiate(Resources.Load("Prefabs/Play/Popup/Confeti")) as GameObject;
                confeti.transform.SetParent(d_canvas.transform, false);
            }
            catch { }

            if (Next) { yield break; }

            state = GAME_STATE.PRE_WIN_AUTO_PLAYING;

            var items = GetRandomItems(moveLeft);

            foreach (var item in items)
            {
                if (Next) { yield break; }

                item.SetRandomNextType();
                item.nextSound = false;

                //moveLeft--;
                UITop.DecreaseMoves(true);

                var prefab = Instantiate(Resources.Load(Configuration.StarGold())) as GameObject;
                prefab.transform.position = UITop.GetComponent<UITop>().movesText.gameObject.transform.position;

                var startPosition = prefab.transform.position;
                var endPosition = item.gameObject.transform.position;
                var bending = new Vector3(1, 1, 0);
                var timeToTravel = 0.05f;
                var timeStamp = Time.time;

                while (Time.time < timeStamp + timeToTravel)
                {
                    var currentPos = Vector3.Lerp(startPosition, endPosition, (Time.time - timeStamp) / timeToTravel);

                    currentPos.x += bending.x * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);
                    currentPos.y += bending.y * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);
                    currentPos.z += bending.z * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp) / timeToTravel) * Mathf.PI);

                    prefab.transform.position = currentPos;

                    yield return null;
                }

                Destroy(prefab);

                item.Destroy();

                yield return new WaitForSeconds(0.1f);
            }
            if (Next) { yield break; }

            yield return new WaitForSeconds(0.1f);

            while (GetAllSpecialItems().Count > 0 && !Next)
            {
                while (GetAllSpecialItems().Count > 0 && !Next)
                {
                    var specials = GetAllSpecialItems();

                    var item = specials[UnityEngine.Random.Range(0, specials.Count)];

                    //if (item.type == ITEM_TYPE.ITEM_COLORCONE)
                    //{
                    //    //item.DestroyItemsSameColor(StageLoader.instance.RandomColor());
                    //}

                    item.Destroy();

                    while (destroyingItems > 0)
                    {
                        yield return new WaitForSeconds(0.2f);
                    }

                    yield return new WaitForEndOfFrame();

                    // Drop();
                    yield return new WaitForSeconds(0.2f);

                    while (droppingItems > 0)
                    {
                        yield return new WaitForSeconds(0.2f);
                    }

                    yield return new WaitForEndOfFrame();
                }

                yield return StartCoroutine(DestroyMatches());
            }
            if (Next) { yield break; }
            while (destroyingItems > 0)
            {
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForEndOfFrame();
            if (Next) { yield break; }
            while (droppingItems > 0)
            {
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForEndOfFrame();

            if (!Next)
            {
                NextToWin();
            }
        }
    }
    //skillz last random item generation
    List<Item> GetRandomItems(int number)
    {
        var avaiableItems = new List<Item>();
        var returnItems = new List<Item>();

        foreach (var item in GetListItems())
        {
            if (item != null)
            {
                if (item.node != null)
                {
                    if (item.IsCookie())
                    {
                        avaiableItems.Add(item);
                    }
                }
            }
        }

        while (returnItems.Count < number && avaiableItems.Count > 0)
        {
            var item = avaiableItems[UnityEngine.Random.Range(0, avaiableItems.Count)];

            returnItems.Add(item);

            avaiableItems.Remove(item);
        }

        return returnItems;
    }

    List<Item> GetAllSpecialItems()
    {
        var specials = new List<Item>();

        foreach (var item in GetListItems())
        {
            if (item != null)
            {
                if (item.type == ITEM_TYPE.ITEM_COLORCONE || item.IsColumnBreaker(item.type) || item.IsRowBreaker(item.type) || item.IsBombBreaker(item.type) || item.IsXBreaker(item.type) || item.type == ITEM_TYPE.ITEM_MEDBOMB || item.type == ITEM_TYPE.ITEM_BIGBOMB)
                {
                    specials.Add(item);
                }
            }
        }

        return specials;
    }

    public void NextToWin()
    {
        StartCoroutine(StartNext());
    }

    IEnumerator StartNext()
    {
        Timer.timerIsRunning = false;
        Next = true;
        NextButon.SetActive(false);
        //sameermovesleftscore
        score += moveLeft * Configuration.instance.finishedScoreItem * 3;
        AudioManager.instance.GetComponent<AudioSource>().Stop();
        BackgroundMusic.instance.StopMusic();

        Configuration.instance.playing = false;

        yield return new WaitForSeconds(0.5f);
        state = GAME_STATE.OPENING_POPUP;

        SaveLevelInfo();

        AudioManager.instance.PopupWinAudio();

        PlayerPrefs.SetInt("LevelWin", 1);
        winPopup.OpenPopup();

        //if (PlayerPrefs.GetInt("FirstTime") == 0)
        //{
        //    PlayerPrefs.SetInt("FirstTime", 1);
        //    SkillzCrossPlatform.LaunchSkillz(new SkillzGameController());
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("LevelWin", 1);
        //    winPopup.OpenPopup();
        //}

        //if (GameObject.Find("NewLife"))
        //{
        //    //Life CALL
        //    if (Configuration.instance.LifeCall && PlayerPrefs.GetInt("isNotifEnabled") == 1)
        //    {
        //        if (NewLife.instance.lives < NewLife.instance.maxLives)
        //        {
        //            NotificationsSetup.NotificationLifeCall();
        //        }
        //        //else
        //        //{
        //        //    Notifications.CancelPendingLocalNotification("LifeCall");
        //        //}
        //    }
        //}


        //yield return new WaitForEndOfFrame();       
    }


    public void SaveLevelInfo()
    {
        //add 1 life
        if (GameObject.Find("NewLife"))
        {

            if (Configuration.instance.OneTimePlay)
            {
                Configuration.instance.OneTimePlay = false;

            }
            else
            {

                NewLife.instance.lives++;
            }

            if (NewLife.instance.lives > NewLife.instance.maxLives)
            {
                NewLife.instance.lives = NewLife.instance.maxLives;
            }

        }


        int openedLevel = CoreData.instance.GetOpendedLevel();

        if (openedLevel > Configuration.instance.maxLevel + 1)
        {
            if ((Configuration.instance.EpisodePlay && StageLoader.instance.Stage + 1448 == openedLevel) || Configuration.instance.RandomPlay || Configuration.instance.ArenaMode || Configuration.instance.GiftBox)
            {
                if (CoreData.instance.openedLevel <= 2)
                {
                    StageLoader.instance.Reward = false;
                }
                else
                {
                    StageLoader.instance.Reward = true;
                }
            }
            else
            {
                StageLoader.instance.Reward = false;
            }

        }
        else
        {
            if ((Configuration.instance.EpisodePlay && StageLoader.instance.Stage == openedLevel) || Configuration.instance.RandomPlay || Configuration.instance.ArenaMode || Configuration.instance.GiftBox)
            {
                if (CoreData.instance.openedLevel <= 2)
                {
                    StageLoader.instance.Reward = false;
                }
                else
                {
                    StageLoader.instance.Reward = true;
                }
            }
            else
            {
                StageLoader.instance.Reward = false;
            }

        }




        // level star
        if (score < StageLoader.instance.score_Star_1)
        {
            star = 0;
        }
        else if (StageLoader.instance.score_Star_1 <= score && score < StageLoader.instance.score_Star_2)
        {
            star = 1;
        }
        else if (StageLoader.instance.score_Star_2 <= score && score < StageLoader.instance.score_Star_3)
        {
            star = 2;
        }
        else if (score >= StageLoader.instance.score_Star_2)
        {
            star = 3;
        }

        // score and star

        if (openedLevel > Configuration.instance.maxLevel)
        {
            CoreData.instance.SaveLevelStatistics(StageLoader.instance.Stage + 1448, score, star);
        }
        else
        {
            CoreData.instance.SaveLevelStatistics(StageLoader.instance.Stage, score, star);
        }


        // add bonus stars


        int stars = CoreData.instance.GetPlayerStars();

        if (star == 1)
        {
            if (openedLevel > Configuration.instance.maxLevel + 1)
            {
                int currentStage = StageLoader.instance.Stage + 1448;

                int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
                if ((Configuration.instance.EpisodePlay && currentStage == openedLevel) || Configuration.instance.RandomPlay || Configuration.instance.ArenaMode || Configuration.instance.GiftBox)
                {
                    CoreData.instance.SavePlayerStars(stars + 1);
                    Configuration.instance.WinStarAmount = 1;
                }


                if (Configuration.instance.ArenaMode)
                {
                    int staramount = PlayerPrefs.GetInt("staramount");

                    var winamount = UnityEngine.Random.Range(Configuration.instance.ArenaWinCoinsMin, Configuration.instance.ArenaWinCoinsMax);

                    if (Configuration.instance.StarChallenge)
                    {

                        PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 1);
                        PlayerPrefs.Save();
                    }
                    PlayerPrefs.SetInt("staramount", staramount + winamount);
                    PlayerPrefs.SetInt("winamount", winamount);
                    PlayerPrefs.Save();
                }
                if (Configuration.instance.StarChallenge && (currentStage == openedLevel || Configuration.instance.EpisodemaxLevel <= openedLevel))
                {

                    PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 1);
                    PlayerPrefs.Save();
                }

            }
            else
            {
                int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
                if ((Configuration.instance.EpisodePlay && StageLoader.instance.Stage == openedLevel) || Configuration.instance.RandomPlay || Configuration.instance.ArenaMode || Configuration.instance.GiftBox)
                {
                    CoreData.instance.SavePlayerStars(stars + 1);
                    Configuration.instance.WinStarAmount = 1;
                }


                if (Configuration.instance.ArenaMode)
                {
                    int staramount = PlayerPrefs.GetInt("staramount");

                    var winamount = UnityEngine.Random.Range(Configuration.instance.ArenaWinCoinsMin, Configuration.instance.ArenaWinCoinsMax);

                    if (Configuration.instance.StarChallenge)
                    {

                        PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 1);
                        PlayerPrefs.Save();
                    }
                    PlayerPrefs.SetInt("staramount", staramount + winamount);
                    PlayerPrefs.SetInt("winamount", winamount);
                    PlayerPrefs.Save();
                }
                if (Configuration.instance.StarChallenge && (StageLoader.instance.Stage == openedLevel || Configuration.instance.EpisodemaxLevel <= openedLevel))
                {

                    PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 1);
                    PlayerPrefs.Save();
                }
            }

            //achievement
            Configuration.SaveAchievement("ach_Collect200stars", 1);
        }
        else if (star == 2)
        {
            if (openedLevel > Configuration.instance.maxLevel + 1)
            {
                int currentStage = StageLoader.instance.Stage + 1448;

                int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
                if ((Configuration.instance.EpisodePlay && currentStage == openedLevel) || Configuration.instance.RandomPlay || Configuration.instance.ArenaMode || Configuration.instance.GiftBox)
                {
                    CoreData.instance.SavePlayerStars(stars + 2);
                    Configuration.instance.WinStarAmount = 2;
                }

                if (Configuration.instance.ArenaMode)
                {
                    int staramount = PlayerPrefs.GetInt("staramount");
                    var winamount = UnityEngine.Random.Range(Configuration.instance.ArenaWinCoinsMin, Configuration.instance.ArenaWinCoinsMax);

                    if (Configuration.instance.StarChallenge)
                    {

                        PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 2);
                        PlayerPrefs.Save();
                    }
                    PlayerPrefs.SetInt("staramount", staramount + (2 * winamount));
                    PlayerPrefs.SetInt("winamount", (2 * winamount));
                    PlayerPrefs.Save();
                }
                if (Configuration.instance.StarChallenge && (currentStage == openedLevel || Configuration.instance.EpisodemaxLevel <= openedLevel))
                {


                    PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 2);
                    PlayerPrefs.Save();
                }

            }
            else
            {
                int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
                if ((Configuration.instance.EpisodePlay && StageLoader.instance.Stage == openedLevel) || Configuration.instance.RandomPlay || Configuration.instance.ArenaMode || Configuration.instance.GiftBox)
                {
                    CoreData.instance.SavePlayerStars(stars + 2);
                    Configuration.instance.WinStarAmount = 2;
                }

                if (Configuration.instance.ArenaMode)
                {
                    int staramount = PlayerPrefs.GetInt("staramount");
                    var winamount = UnityEngine.Random.Range(Configuration.instance.ArenaWinCoinsMin, Configuration.instance.ArenaWinCoinsMax);

                    if (Configuration.instance.StarChallenge)
                    {

                        PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 2);
                        PlayerPrefs.Save();
                    }
                    PlayerPrefs.SetInt("staramount", staramount + (2 * winamount));
                    PlayerPrefs.SetInt("winamount", (2 * winamount));
                    PlayerPrefs.Save();
                }
                if (Configuration.instance.StarChallenge && (StageLoader.instance.Stage == openedLevel || Configuration.instance.EpisodemaxLevel <= openedLevel))
                {


                    PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 2);
                    PlayerPrefs.Save();
                }
            }
            //achievement
            Configuration.SaveAchievement("ach_Collect200stars", 2);
        }
        else if (star == 3)
        {
            if (openedLevel > Configuration.instance.maxLevel + 1)
            {
                int currentStage = StageLoader.instance.Stage + 1448;

                int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
                if ((Configuration.instance.EpisodePlay && currentStage == openedLevel) || Configuration.instance.RandomPlay || Configuration.instance.ArenaMode || Configuration.instance.GiftBox)
                {
                    CoreData.instance.SavePlayerStars(stars + 3);
                    Configuration.instance.WinStarAmount = 3;
                }
                if (Configuration.instance.ArenaMode)
                {
                    int staramount = PlayerPrefs.GetInt("staramount");
                    var winamount = UnityEngine.Random.Range(Configuration.instance.ArenaWinCoinsMin, Configuration.instance.ArenaWinCoinsMax);

                    if (Configuration.instance.StarChallenge)
                    {

                        PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 3);
                        PlayerPrefs.Save();
                    }
                    PlayerPrefs.SetInt("staramount", staramount + (3 * winamount));
                    PlayerPrefs.SetInt("winamount", (3 * winamount));
                    PlayerPrefs.Save();
                }
                if (Configuration.instance.StarChallenge && (currentStage == openedLevel || Configuration.instance.EpisodemaxLevel <= openedLevel))
                {

                    PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 3);
                    PlayerPrefs.Save();
                }


            }
            else
            {

                int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
                if ((Configuration.instance.EpisodePlay && StageLoader.instance.Stage == openedLevel) || Configuration.instance.RandomPlay || Configuration.instance.ArenaMode || Configuration.instance.GiftBox)
                {
                    CoreData.instance.SavePlayerStars(stars + 3);
                    Configuration.instance.WinStarAmount = 3;
                }
                if (Configuration.instance.ArenaMode)
                {
                    int staramount = PlayerPrefs.GetInt("staramount");
                    var winamount = UnityEngine.Random.Range(Configuration.instance.ArenaWinCoinsMin, Configuration.instance.ArenaWinCoinsMax);

                    if (Configuration.instance.StarChallenge)
                    {

                        PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 3);
                        PlayerPrefs.Save();
                    }
                    PlayerPrefs.SetInt("staramount", staramount + (3 * winamount));
                    PlayerPrefs.SetInt("winamount", (3 * winamount));
                    PlayerPrefs.Save();
                }
                if (Configuration.instance.StarChallenge && (StageLoader.instance.Stage == openedLevel || Configuration.instance.EpisodemaxLevel <= openedLevel))
                {

                    PlayerPrefs.SetInt("StarChallengeNum", StarChallengeNum + 3);
                    PlayerPrefs.Save();
                }
            }
            //achievement
            Configuration.SaveAchievement("ach_Collect200stars", 3);
        }

        if (Configuration.instance.ArenaMode)
        {
            updatearenanumber();
        }
        // add bonus puans
        if (openedLevel > Configuration.instance.maxLevel + 1)
        {
            int currentStage = StageLoader.instance.Stage + 1448;

            if ((Configuration.instance.EpisodePlay && currentStage == openedLevel) || Configuration.instance.RandomPlay || Configuration.instance.ArenaMode || Configuration.instance.GiftBox)
            {
                int puan = CoreData.instance.GetPlayerPuan();

                CoreData.instance.SavePlayerPuan(puan + score);
                Configuration.instance.WinScoreAmount = score;
            }

        }
        else
        {
            if ((Configuration.instance.EpisodePlay && StageLoader.instance.Stage == openedLevel) || Configuration.instance.RandomPlay || Configuration.instance.ArenaMode || Configuration.instance.GiftBox)
            {
                int puan = CoreData.instance.GetPlayerPuan();

                CoreData.instance.SavePlayerPuan(puan + score);
                Configuration.instance.WinScoreAmount = score;
            }
        }



        // add toplam Score
        int toplamscore = CoreData.instance.GetToplamScore();

        CoreData.instance.SaveToplamScore(toplamscore + score);
        //achievement
        Configuration.SaveAchievement("ach_Collect200000scores", score);




        // add bonus coin

        int coin = CoreData.instance.GetPlayerCoin();

        if (star == 1)
        {
            CoreData.instance.SavePlayerCoin(coin + Configuration.instance.bonus1Star);
        }
        else if (star == 2)
        {
            CoreData.instance.SavePlayerCoin(coin + Configuration.instance.bonus2Star);
        }
        else if (star == 3)
        {
            CoreData.instance.SavePlayerCoin(coin + Configuration.instance.bonus3Star);
        }

        // open next level
        //if (!Configuration.instance.Amazon && Configuration.instance.GameServices)
        //{
        //    GameServices.ReportScore(openedLevel, EM_GameServicesConstants.Leaderboard_level_leaderboard);
        //}

        if (openedLevel > Configuration.instance.maxLevel)
        {
            if (StageLoader.instance.Stage + 1448 == openedLevel)
            {
                if (!Configuration.instance.RandomPlay && !Configuration.instance.ArenaMode && !Configuration.instance.GiftBox && openedLevel < Configuration.instance.EpisodemaxLevel)
                {
                    CoreData.instance.SaveOpendedLevel(openedLevel + 1);
                    Configuration.instance.SetCurrentEpisode();
                }
            }

        }
        else
        {
            if (StageLoader.instance.Stage == openedLevel)
            {
                if (openedLevel <= Configuration.instance.maxLevel)
                {

                    if (!Configuration.instance.RandomPlay && !Configuration.instance.ArenaMode && !Configuration.instance.GiftBox && openedLevel < Configuration.instance.EpisodemaxLevel)
                    {
                        CoreData.instance.SaveOpendedLevel(openedLevel + 1);
                        Configuration.instance.SetCurrentEpisode();
                    }
                }
            }

        }




        // Achiments
        #region comment
        // open next level
        //if (!Configuration.instance.Amazon && Configuration.instance.GameServices)
        //{

        //    if (openedLevel == 10)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_10);
        //        //  CoreData.instance.SavePlayerCoin(coin += 5);
        //        AudioManager.instance.giftbuton();

        //    }

        //    if (openedLevel == 30)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_30);
        //        // CoreData.instance.SavePlayerCoin(coin += 5);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 50)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_55);
        //        //CoreData.instance.SavePlayerCoin(coin += 5);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 70)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_70);
        //        //  CoreData.instance.SavePlayerCoin(coin += 5);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 80)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_85);
        //        // CoreData.instance.SavePlayerCoin(coin += 5);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 100)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_100);
        //        // CoreData.instance.SavePlayerCoin(coin += 25);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 130)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_130);
        //        //  CoreData.instance.SavePlayerCoin(coin += 5);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 150)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_155);
        //        //   CoreData.instance.SavePlayerCoin(coin += 5);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 180)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_185);
        //        //CoreData.instance.SavePlayerCoin(coin += 5);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 200)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_200);
        //        //  CoreData.instance.SavePlayerCoin(coin += 35);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 220)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_222);
        //        // CoreData.instance.SavePlayerCoin(coin += 5);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 250)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_250);
        //        // CoreData.instance.SavePlayerCoin(coin += 5);
        //        AudioManager.instance.giftbuton();
        //    }
        //    if (openedLevel == 270)
        //    {
        //        GameServices.UnlockAchievement(EM_GameServicesConstants.Achievement_Level_270);
        //        //  CoreData.instance.SavePlayerCoin(coin += 30);
        //        AudioManager.instance.giftbuton();
        //    }
        //}
        #endregion
        if (Configuration.instance.GiftBox)
        {
            Configuration.instance.GiftBoxWinNum++;
        }

    }
    bool InitCheck()
    {
        //bool isInit = Notifications.IsInitialized();
        return false;
    }
    public void updatearenanumber()
    {
        for (int i = 1; i < 12; i++)
        {
            int coin = CoreData.instance.GetPlayerCoin();
            int staramount = PlayerPrefs.GetInt("staramount");
            int GiftAmount = Configuration.instance.GiftAmountText[i];
            int GerekliStar = Configuration.instance.GerekliStarText[i];
            if (staramount >= GerekliStar)
            {
                PlayerPrefs.SetInt("arenanumber", i);
                PlayerPrefs.Save();
            }
            int arenanumber = PlayerPrefs.GetInt("arenanumber");

        }
    }




    #endregion  

    #region Hint

    public void Hint()
    {
        StartCoroutine(CheckHint());
    }

    public IEnumerator CheckHint()
    {

        checkHintCall++;

        if (checkHintCall > 1)
        {
            checkHintCall--;


            yield break;
        }

        if (Configuration.instance.showHint == false)
        {
            yield break;
        }

        if (moveLeft <= 0)
        {
            yield break;
        }

        while (flyingItems > 0)
        {
            yield return null;
        }

        HideHint();

        while (state != GAME_STATE.WAITING_USER_SWAP)
        {

            yield return new WaitForEndOfFrame();
        }

        while (lockSwap == true)
        {

            yield return new WaitForEndOfFrame();
        }



        yield return new WaitForEndOfFrame();

        if (GetHintByRainbowItem() == true || GetHintByBreaker() == true || GetHintByColor() == true)
        {
            StartCoroutine(ShowHint());

            checkHintCall--;

            yield break;
        }
        // if reach this code that mean there is no matches
        else if (flyingItems == 0)
        {
            // prevent multiple call
            if (!GameObject.Find("NoMatchesdPopup(Clone)"))
            {
                yield return new WaitForSeconds(2.0f);
                state = GAME_STATE.NO_MATCHES_REGENERATING;

                lockSwap = true;

                AudioManager.instance.PopupNoMatchesAudio();

                // show and close popup
                noMatchesPopup.OpenPopup();

                yield return new WaitForEndOfFrame();

                if (GameObject.Find("NoMatchesdPopup(Clone)"))
                {
                    GameObject.Find("NoMatchesdPopup(Clone)").GetComponent<Popup>().Close();
                }

                yield return new WaitForEndOfFrame();

                NoMoveRegenerate();
                yield return new WaitForEndOfFrame();

                while (GetHintByColor() == false)
                {
                    NoMoveRegenerate();

                    yield return new WaitForEndOfFrame();
                }



                state = GAME_STATE.WAITING_USER_SWAP;

                FindMatches();
            }

            checkHintCall--;
        }
    }

    public IEnumerator ShowHint()
    {

        showHintCall++;

        if (showHintCall > 1)
        {

            showHintCall--;

            yield break;
        }

        if (Configuration.instance.showHint == false)
        {
            yield break;
        }

        yield return new WaitForSeconds(Configuration.instance.hintDelay);

        while (state != GAME_STATE.WAITING_USER_SWAP)
        {
            yield return new WaitForSeconds(0.1f);
        }

        while (lockSwap == true)
        {
            yield return new WaitForSeconds(0.1f);
        }


        foreach (var item in GetListItems())
        {
            if (item != null)
            {
                if (!hintItems.Contains(item))
                {
                    iTween.StopByName(item.gameObject, "HintAnimation");
                    item.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
            }
        }

        foreach (var item in hintItems)
        {
            if (item != null)
            {

                iTween.ShakeRotation(item.gameObject, iTween.Hash(
                     "name", "HintAnimation",
                     "amount", new Vector3(0f, 0f, 50f),
                     "easetype", iTween.EaseType.easeOutBack,
                     //"looptype", iTween.LoopType.pingPong,
                     "oncomplete", "OnCompleteShowHint",
                     "oncompletetarget", gameObject,
                     "oncompleteparams", new Hashtable() { { "item", item } },
                     "time", 1f
                 ));
            }
        }

        if (hintItems.Count > 0)
        {
            yield return new WaitForSeconds(1.5f);
        }

        showHintCall--;

        StartCoroutine(CheckHint());
    }

    public void OnCompleteShowHint(Hashtable args)
    {
        var item = (Item)args["item"];


        iTween.RotateTo(item.gameObject, iTween.Hash(
            "rotation", Vector3.zero,
            "time", 0.2f
        ));
    }

    public void HideHint()
    {

        foreach (var item in hintItems)
        {
            if (item != null)
            {

                iTween.StopByName(item.gameObject, "HintAnimation");

                iTween.RotateTo(item.gameObject, iTween.Hash(
                    "rotation", Vector3.zero,
                    "time", 0.2f
                ));
            }
        }

        hintItems.Clear();
    }

    List<int> Shuffle(List<int> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }

    void CheckHintNode(Node node, int color, bool needMove = false)
    {
        if (node != null)
        {
            if (node.item != null && node.item.color == color)
            {
                if (node.item.Movable() && node.item.Matchable())
                {
                    hintItems.Add(node.item);
                }
                else
                {
                    if (node.item.Matchable())
                    {
                        hintItems.Add(node.item);
                    }
                }
            }
        }
    }

    public void NoMoveRegenerate()
    {


        foreach (var item in GetListItems())
        {
            if (item != null)
            {
                if (item.Movable() && item.IsCookie())
                {
                    item.color = StageLoader.instance.RandomColor();

                    item.ChangeSpriteAndType(item.color);
                    item.CheckStat();
                }
            }
        }
    }

    bool GetHintByColor()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        foreach (int color in Shuffle(StageLoader.instance.usingColors))
        {
            for (int j = 0; j < column; j++)
            {
                for (int i = 0; i < row; i++)
                {
                    Node node = GetNode(i, j);

                    if (node != null)
                    {
                        if (node.item == null || !(node.item.Movable()))
                        {
                            continue;
                        }

                        // current node is x
                        // o-o-x
                        //     o
                        CheckHintNode(GetNode(i, j), color, true);
                        CheckHintNode(GetNode(i + 1, j), color);
                        if (hintItems.Count == 2)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        //     o
                        // o-o x
                        CheckHintNode(GetNode(i - 1, j), color, true);
                        CheckHintNode(GetNode(i, j), color);
                        if (hintItems.Count == 2)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // x o o
                        // o
                        CheckHintNode(GetNode(i, j), color, true);
                        CheckHintNode(GetNode(i, j + 1), color);
                        if (hintItems.Count == 2)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // o
                        // x o o
                        CheckHintNode(GetNode(i, j), color, true);
                        CheckHintNode(GetNode(i, j - 1), color);
                        if (hintItems.Count == 2)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }
                    }
                } // end for row
            }
        } // end foreach color

        return false;
    }

    bool GetHintByRainbowItem()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Node node = GetNode(i, j);
                if (node != null)
                {
                    if (node.item == null || !(node.item.Movable()))
                    {
                        continue;
                    }

                    if (node.item.type == ITEM_TYPE.ITEM_COLORCONE)
                    {
                        Node neighbor = null;

                        neighbor = node.LeftNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable())
                            {
                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.RightNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable())
                            {
                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.TopNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable())
                            {
                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.BottomNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable())
                            {
                                hintItems.Add(node.item);

                                return true;
                            }
                        }
                    } // end if item is rainbow
                }
            }
        }

        return false;
    }

    bool GetHintByBreaker()
    {
        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Node node = GetNode(i, j);
                if (node != null)
                {
                    if (node.item == null || !(node.item.Movable()))
                    {
                        continue;
                    }

                    if (node.item.IsBreaker(node.item.type))
                    {
                        Node neighbor = null;

                        neighbor = node.LeftNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable() && neighbor.item.IsBreaker(neighbor.item.type))
                            {
                                hintItems.Add(neighbor.item);

                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.RightNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable() && neighbor.item.IsBreaker(neighbor.item.type))
                            {
                                hintItems.Add(neighbor.item);

                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.TopNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable() && neighbor.item.IsBreaker(neighbor.item.type))
                            {
                                hintItems.Add(neighbor.item);

                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.BottomNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable() && neighbor.item.IsBreaker(neighbor.item.type))
                            {
                                hintItems.Add(neighbor.item);

                                hintItems.Add(node.item);

                                return true;
                            }
                        }
                    } // end if
                }
            }
        }

        return false;
    }

    #endregion

    #region Gingerbread

    bool GenerateGingerbread()
    {
        if (IsGingerbreadTarget() == false)
        {
            return false;
        }

        if (skipGenerateGingerbread == true)
        {
            return false;
        }

        // calculate the total gingerbread need to generate
        var needGenerate = 0;

        for (int i = 1; i <= 4; i++)
        {
            switch (i)
            {
                case 1:
                    if (StageLoader.instance.target1Type == TARGET_TYPE.ROCKET)
                    {
                        needGenerate += target1Left;
                    }
                    break;
                case 2:
                    if (StageLoader.instance.target2Type == TARGET_TYPE.ROCKET)
                    {
                        needGenerate += target2Left;
                    }
                    break;
                case 3:
                    if (StageLoader.instance.target3Type == TARGET_TYPE.ROCKET)
                    {
                        needGenerate += target3Left;
                    }
                    break;
                case 4:
                    if (StageLoader.instance.target4Type == TARGET_TYPE.ROCKET)
                    {
                        needGenerate += target4Left;
                    }
                    break;
            }
        }

        if (needGenerate <= 0)
        {
            return false;
        }

        // check gingerbread on board
        var amount = GingerbreadOnBoard().Count;

        if (amount >= StageLoader.instance.maxRockettoys)
        {
            return false;
        }

        // prevent multiple call
        if (generatingGingerbread == true)
        {
            return false;
        }

        // skip generate randomly skillz
        if (UnityEngine.Random.Range(0, 2) == 0 && skipGingerbreadCount < 2)
        {
            skipGingerbreadCount++;
            return false;
        }
        skipGingerbreadCount = 0;

        generatingGingerbread = true;

        // get node to generate gingerbread
        var row = StageLoader.instance.row - 1;
        var column = StageLoader.instance.rocketToysMarkers[UnityEngine.Random.Range(0, StageLoader.instance.rocketToysMarkers.Count)];

        var node = GetNode(row, column);

        //print(node.name);

        if (node != null && node.item != null && node.item.IsCookie())
        {
            node.item.ChangeToGingerbread(StageLoader.instance.RandomRockets());
            return true;
        }

        return false;
    }
    bool IsGingerbreadTarget()
    {
        if (StageLoader.instance.target1Type == TARGET_TYPE.ROCKET ||
            StageLoader.instance.target2Type == TARGET_TYPE.ROCKET ||
            StageLoader.instance.target3Type == TARGET_TYPE.ROCKET ||
            StageLoader.instance.target4Type == TARGET_TYPE.ROCKET)
        {
            return true;
        }

        return false;
    }
    List<Item> GingerbreadOnBoard()
    {
        var list = new List<Item>();

        var items = GetListItems();

        foreach (var item in items)
        {
            if (item != null && item.IsGingerbread())
            {
                list.Add(item);
            }
        }

        return list;
    }
    bool MoveGingerbread()
    {
        if (IsGingerbreadTarget() == false)
        {
            return false;
        }

        // prevent multiple call
        if (movingGingerbread == true)
        {
            return false;
        }
        movingGingerbread = true;

        var isMoved = false;

        //print("Move gingerbread");

        foreach (var gingerbread in GingerbreadOnBoard())
        {
            if (gingerbread != null)
            {
                var upper = GetUpperItem(gingerbread.node);

                if (upper != null && upper.node != null && upper.IsGingerbread() == false && gingerbread.node.cage == null)
                {
                    var gingerbreadPosition = NodeLocalPosition(upper.node.i, upper.node.j);
                    var upperItemPosition = NodeLocalPosition(gingerbread.node.i, gingerbread.node.j);

                    gingerbread.neighborNode = upper.node;
                    gingerbread.swapItem = upper;

                    touchedItem = gingerbread;
                    swappedItem = upper;



                    gingerbread.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;


                    // animation
                    iTween.MoveTo(gingerbread.gameObject, iTween.Hash(
                        "position", gingerbreadPosition,
                        "easetype", iTween.EaseType.linear,
                        "time", Configuration.instance.swapTime
                    ));

                    iTween.MoveTo(upper.gameObject, iTween.Hash(
                        "position", upperItemPosition,
                        "easetype", iTween.EaseType.linear,
                        "time", Configuration.instance.swapTime
                    ));
                }
                else if (upper == null || upper.node == null)
                {
                    AudioManager.instance.GingerbreadExplodeAudio();

                    gingerbread.color = StageLoader.instance.RandomColor();

                    gingerbread.ChangeSpriteAndType(gingerbread.color);

                    // after changing a gingerbread to a cookie. skip generate one turn on generate call right after this function
                    skipGenerateGingerbread = true;
                }

                isMoved = true;
            }
        }

        return isMoved;
    }
    public Item GetUpperItem(Node node)
    {
        var top = node.TopNeighbor();

        if (top == null)
        {
            return null;
        }
        else
        {
            if (top.tile.type == TILE_TYPE.NONE || top.tile.type == TILE_TYPE.PASS_THROUGH)
            {
                return GetUpperItem(top);
            }
            else if (top.item != null && top.item.Movable())
            {
                return top.item;
            }
            else
            {
                return node.item;
            }
        }
    }

    #endregion

    #region Booster

    void DestroyBoosterItems(Item boosterItem)
    {
        if (boosterItem == null)
        {
            return;
        }

        if (boosterItem.Destroyable() && booster != BOOSTER_TYPE.OVEN_BREAKER)
        {
            if (booster == BOOSTER_TYPE.RAINBOW_BREAKER && boosterItem.IsCookie() == false)
            {
                return;
            }

            lockSwap = true;

            switch (booster)
            {
                case BOOSTER_TYPE.SINGLE_BREAKER:
                    DestroySingleBooster(boosterItem);
                    break;
                case BOOSTER_TYPE.ROW_BREAKER:
                    StartCoroutine(DestroyRowBooster(boosterItem));
                    break;
                case BOOSTER_TYPE.COLUMN_BREAKER:
                    StartCoroutine(DestroyColumnBooster(boosterItem));
                    break;
                case BOOSTER_TYPE.RAINBOW_BREAKER:
                    StartCoroutine(DestroyRainbowBooster(boosterItem));
                    break;
            }

            Booster.instance.BoosterComplete();
        }

        if (boosterItem.Movable() && booster == BOOSTER_TYPE.OVEN_BREAKER)
        {
            StartCoroutine(DestroyOvenBooster(boosterItem));
        }
    }

    void DestroySingleBooster(Item boosterItem)
    {
        HideHint();

        AudioManager.instance.SingleBoosterAudio();

        if (boosterItem.type == ITEM_TYPE.ITEM_COLORCONE)
        {
            // boosterItem.DestroyItemsSameColor(StageLoader.instance.RandomColor());
        }

        boosterItem.Destroy();

        FindMatches();
    }

    IEnumerator DestroyRowBooster(Item boosterItem)
    {
        HideHint();

        AudioManager.instance.RowBoosterAudio();

        // animation

        // destroy a row
        var items = new List<Item>();

        items = RowItems(boosterItem.node.i);

        foreach (var item in items)
        {
            // this item maybe destroyed in other call
            if (item != null)
            {
                if (item.type == ITEM_TYPE.ITEM_COLORCONE)
                {
                    //item.DestroyItemsSameColor(StageLoader.instance.RandomColor());
                }

                item.Destroy();
            }

            yield return new WaitForSeconds(0.1f);
        }

        FindMatches();
    }

    IEnumerator DestroyColumnBooster(Item boosterItem)
    {
        HideHint();

        AudioManager.instance.ColumnBoosterAudio();

        // animation

        // destroy a row
        var items = new List<Item>();

        items = ColumnItems(boosterItem.node.j);

        foreach (var item in items)
        {
            // this item maybe destroyed in other call
            if (item != null)
            {
                if (item.type == ITEM_TYPE.ITEM_COLORCONE)
                {
                    //item.DestroyItemsSameColor(StageLoader.instance.RandomColor());
                }

                item.Destroy();
            }

            yield return new WaitForSeconds(0.1f);
        }

        FindMatches();
    }

    IEnumerator DestroyRainbowBooster(Item boosterItem)
    {
        HideHint();

        AudioManager.instance.RainbowBoosterAudio();

        boosterItem.DestroyHunterItemsSameColor(boosterItem.color);

        yield return new WaitForFixedUpdate();
    }

    IEnumerator DestroyOvenBooster(Item boosterItem)
    {
        HideHint();

        if (ovenTouchItem == null)
        {
            ovenTouchItem = boosterItem;

            AudioManager.instance.ButtonClickAudio();
            lockSwap = true;

            boosterItem.node.AddOvenBoosterActive();

            AudioManager.instance.OvenBoosterAudio();

            AudioManager.instance.ButtonClickAudio();


            NoMoveRegenerate();
            yield return new WaitForEndOfFrame();

            while (GetHintByColor() == false)
            {
                NoMoveRegenerate();

                yield return new WaitForEndOfFrame();
            }



            state = GAME_STATE.WAITING_USER_SWAP;

            yield return new WaitForSeconds(0.1f);
            ovenTouchItem.node.RemoveOvenBoosterActive();
            boosterItem.node.RemoveOvenBoosterActive();
            Booster.instance.BoosterComplete();

            yield return new WaitForSeconds(0.1f);

            FindMatches();
        }
        else
        {
            // the same item
            if (ovenTouchItem.node.OrderOnBoard() == boosterItem.node.OrderOnBoard())
            {
                // remove active
                ovenTouchItem.node.RemoveOvenBoosterActive();

                ovenTouchItem = null;

                AudioManager.instance.ButtonClickAudio();
            }
            // swap
            else
            {
                lockSwap = true;

                boosterItem.node.AddOvenBoosterActive();

                AudioManager.instance.OvenBoosterAudio();

                AudioManager.instance.ButtonClickAudio();


                NoMoveRegenerate();
                yield return new WaitForEndOfFrame();

                while (GetHintByColor() == false)
                {
                    NoMoveRegenerate();

                    yield return new WaitForEndOfFrame();
                }



                state = GAME_STATE.WAITING_USER_SWAP;

                /*  // animation
                  iTween.MoveTo(ovenTouchItem.gameObject, iTween.Hash(
                      "position", boosterItem.gameObject.transform.position,                    
                      "easetype", iTween.EaseType.linear,
                      "time", Configuration.instance.swapTime
                  ));

                  iTween.MoveTo(boosterItem.gameObject, iTween.Hash(
                      "position", ovenTouchItem.gameObject.transform.position,
                      "easetype", iTween.EaseType.linear,
                      "time", Configuration.instance.swapTime
                  ));

                  yield return new WaitForSeconds(Configuration.instance.swapTime);

                  ovenTouchItem.node.RemoveOvenBoosterActive();
                  boosterItem.node.RemoveOvenBoosterActive();

                  var ovenTouchNode = ovenTouchItem.node;
                  var boosterItemNode = boosterItem.node;

                  // swap item
                  ovenTouchNode.item = boosterItem;
                  boosterItemNode.item = ovenTouchItem;

                  // swap node
                  ovenTouchItem.node = boosterItemNode;
                  boosterItem.node = ovenTouchNode;

                  // swap on hierarchy
                  ovenTouchItem.gameObject.transform.SetParent(boosterItemNode.gameObject.transform);
                  boosterItem.gameObject.transform.SetParent(ovenTouchNode.gameObject.transform);

                  yield return new WaitForEndOfFrame();

                  ovenTouchItem = null;*/
                yield return new WaitForSeconds(0.1f);
                ovenTouchItem.node.RemoveOvenBoosterActive();
                boosterItem.node.RemoveOvenBoosterActive();
                Booster.instance.BoosterComplete();

                yield return new WaitForSeconds(0.1f);

                FindMatches();
            }
        }

        yield return new WaitForFixedUpdate();
    }

    #endregion 

    #region Collectible

    List<ITEM_TYPE> CheckGenerateCollectible()
    {
        if (StageLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE &&
            StageLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
        {
            return null;
        }

        var collectibles = new List<ITEM_TYPE>();

        if (CollectibleOnBoard() >= StageLoader.instance.collectibleMaxOnBoard)
        {
            return null;
        }

        for (int i = 0; i <= 4; i++)
        {
            TARGET_TYPE targetType = TARGET_TYPE.NONE;
            int targetColor = 0;
            int collectibleOnBoard = 0;
            int targetLeft = 0;

            switch (i)
            {
                case 1:
                    targetType = StageLoader.instance.target1Type;
                    targetColor = StageLoader.instance.target1Color;
                    collectibleOnBoard = CollectibleOnBoard(StageLoader.instance.target1Color);
                    targetLeft = target1Left;
                    break;
                case 2:
                    targetType = StageLoader.instance.target2Type;
                    targetColor = StageLoader.instance.target2Color;
                    collectibleOnBoard = CollectibleOnBoard(StageLoader.instance.target2Color);
                    targetLeft = target2Left;
                    break;
                case 3:
                    targetType = StageLoader.instance.target3Type;
                    targetColor = StageLoader.instance.target3Color;
                    collectibleOnBoard = CollectibleOnBoard(StageLoader.instance.target3Color);
                    targetLeft = target3Left;
                    break;
                case 4:
                    targetType = StageLoader.instance.target4Type;
                    targetColor = StageLoader.instance.target4Color;
                    collectibleOnBoard = CollectibleOnBoard(StageLoader.instance.target4Color);
                    targetLeft = target4Left;
                    break;
            }

            if (targetType == TARGET_TYPE.COLLECTIBLE && collectibleOnBoard < targetLeft)
            {
                for (int k = 0; k < targetLeft - collectibleOnBoard; k++)
                {
                    collectibles.Add(ColorToCollectible(targetColor));
                }
            }

        }

        return collectibles;
    }

    ITEM_TYPE ColorToCollectible(int color)
    {
        switch (color)
        {
            case 1:
                return ITEM_TYPE.COLLECTIBLE_1;
            case 2:
                return ITEM_TYPE.COLLECTIBLE_2;
            case 3:
                return ITEM_TYPE.COLLECTIBLE_3;
            case 4:
                return ITEM_TYPE.COLLECTIBLE_4;
            case 5:
                return ITEM_TYPE.COLLECTIBLE_5;
            case 6:
                return ITEM_TYPE.COLLECTIBLE_6;
            case 7:
                return ITEM_TYPE.COLLECTIBLE_7;
            case 8:
                return ITEM_TYPE.COLLECTIBLE_8;
            case 9:
                return ITEM_TYPE.COLLECTIBLE_9;
            case 10:
                return ITEM_TYPE.COLLECTIBLE_10;
            case 11:
                return ITEM_TYPE.COLLECTIBLE_11;
            case 12:
                return ITEM_TYPE.COLLECTIBLE_12;
            case 13:
                return ITEM_TYPE.COLLECTIBLE_13;
            case 14:
                return ITEM_TYPE.COLLECTIBLE_14;
            case 15:
                return ITEM_TYPE.COLLECTIBLE_15;
            case 16:
                return ITEM_TYPE.COLLECTIBLE_16;
            case 17:
                return ITEM_TYPE.COLLECTIBLE_17;
            case 18:
                return ITEM_TYPE.COLLECTIBLE_18;
            case 19:
                return ITEM_TYPE.COLLECTIBLE_19;
            case 20:
                return ITEM_TYPE.COLLECTIBLE_20;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    int CollectibleOnBoard(int color = 0)
    {
        int amount = 0;

        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var node = GetNode(i, j);

                if (node != null && node.item != null && node.item.IsCollectible() == true)
                {
                    if (color == 0)
                    {
                        amount++;
                    }
                    else
                    {
                        if (node.item.color == color)
                        {
                            amount++;
                        }
                    }
                }
            }
        }

        return amount;
    }

    #endregion

    #region Marshmallow

    bool CheckGenerateMarshmallow()
    {
        if (StageLoader.instance.target1Type != TARGET_TYPE.BREAKABLE &&
            StageLoader.instance.target2Type != TARGET_TYPE.BREAKABLE &&
            StageLoader.instance.target3Type != TARGET_TYPE.BREAKABLE &&
            StageLoader.instance.target4Type != TARGET_TYPE.BREAKABLE)
        {
            return false;
        }

        var needGenerate = 0;

        for (int i = 1; i <= 4; i++)
        {
            switch (i)
            {
                case 1:
                    if (StageLoader.instance.target1Type == TARGET_TYPE.BREAKABLE)
                    {
                        needGenerate += target1Left;
                    }
                    break;
                case 2:
                    if (StageLoader.instance.target2Type == TARGET_TYPE.BREAKABLE)
                    {
                        needGenerate += target2Left;
                    }
                    break;
                case 3:
                    if (StageLoader.instance.target3Type == TARGET_TYPE.BREAKABLE)
                    {
                        needGenerate += target3Left;
                    }
                    break;
                case 4:
                    if (StageLoader.instance.target4Type == TARGET_TYPE.BREAKABLE)
                    {
                        needGenerate += target4Left;
                    }
                    break;
            }
        }

        if (needGenerate + StageLoader.instance.marshmallowMoreThanTarget <= MarshmallowOnBoard())
        {
            return false;
        }

        return true;
    }

    int MarshmallowOnBoard()
    {
        int amount = 0;

        var row = StageLoader.instance.row;
        var column = StageLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var node = GetNode(i, j);

                if (node != null && node.item != null && node.item.IsMarshmallow() == true)
                {
                    amount++;
                }
            }
        }

        return amount;
    }

    #endregion

    #region ADSCALLBACK

    // ADS

    //sameerrewardedcomplete
    public void RewardedAdCompletedHandler()
    {

        if (!MovesReward && !GameOver)
        {
            if (RewardBoosterNum > 0)
            {
                RewardBoosterNum--;
                RewardBoosterNumText.text = "" + RewardBoosterNum;
          

            }

            StartCoroutine(AddBoosters());
        }
        //achievement
        Configuration.SaveAchievement("ach_watchAds", 1);
    }



    void OnApplicationQuit()
    {
        //print("Configuration: On application quit / Exit date time: " + DateTime.Now.ToString() + " / Life: " + life + " / Timer: " + timer);

       
    }




    #endregion

}
