using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;



public class Booster : MonoBehaviour 
{
    public static Booster instance = null;

    [Header("Board")]
    public itemGrid board;
   
    [Header("Booster")]
    public GameObject singleBooster;
    public GameObject rowBooster;
    public GameObject columnBooster;
    public GameObject rainbowBooster;
    public GameObject ovenBooster;

    public GameObject locksingleBooster;
    public GameObject lockrowBooster;
    public GameObject lockcolumnBooster;
    public GameObject lockrainbowBooster;
    public GameObject lockovenBooster;

    [Header("Active")]
    public GameObject singleActive;
    public GameObject rowActive;
    public GameObject columnActive;
    public GameObject rainbowActive;
    public GameObject ovenActive;

    [Header("Amount")]
    public Text singleAmount;
    public Text rowAmount;
    public Text columnAmount;
    public Text rainbowAmount;
    public Text ovenAmount;

    [Header("Popup")]
    public PopupOpener singleBoosterPopup;
    public PopupOpener rowBoosterPopup;
    public PopupOpener columnBoosterPopup;
    public PopupOpener rainbowBoosterPopup;
    public PopupOpener ovenBoosterPopup;

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

    void Start () 
    {
        singleBooster.SetActive(true);
        rowBooster.SetActive(true);
        columnBooster.SetActive(true);
        rainbowBooster.SetActive(true);
        ovenBooster.SetActive(true);
        Debug.LogError("Booster update");
        // single breaker
        if (StageLoader.instance.Stage >= 8)
        {
            locksingleBooster.SetActive(false);
            singleBooster.SetActive(true);
            singleAmount.text = CoreData.instance.GetSingleBreaker().ToString();
        }
        
        // row breaker
        if (StageLoader.instance.Stage >= 11)
        {
            lockrowBooster.SetActive(false);
            rowBooster.SetActive(true);
            rowAmount.text = CoreData.instance.GetRowBreaker().ToString();
        }

        // column breaker
        if (StageLoader.instance.Stage >= 13)
        {
            lockcolumnBooster.SetActive(false);
            columnBooster.SetActive(true);
            columnAmount.text = CoreData.instance.GetColumnBreaker().ToString();
        }

        // rainbow breaker
        if (StageLoader.instance.Stage >= 20)
        {
            lockrainbowBooster.SetActive(false);
            rainbowBooster.SetActive(true);
            rainbowAmount.text = CoreData.instance.GetRainbowBreaker().ToString();
        }

        // oven breaker
        if (StageLoader.instance.Stage >= 23)
        {
            lockovenBooster.SetActive(false);
            ovenBooster.SetActive(true);
            ovenAmount.text = CoreData.instance.GetOvenBreaker().ToString();
        }
        rainbowAmount.text = CoreData.instance.GetRainbowBreaker().ToString();
        columnAmount.text = CoreData.instance.GetColumnBreaker().ToString();
        rowAmount.text = CoreData.instance.GetRowBreaker().ToString();
        singleAmount.text = CoreData.instance.GetSingleBreaker().ToString();

        ovenAmount.text = CoreData.instance.GetOvenBreaker().ToString();

    }


    
    #region Single

    public void SingleBoosterClick()
    {

        Debug.Log ("Click on single booster");

        if (board.state != GAME_STATE.WAITING_USER_SWAP || board.lockSwap == true)
        {
            return;
        }
       
         itemGrid.instance.CloseHelpWindows();
         Configuration.instance.pause = false;
        
        AudioManager.instance.ButtonClickAudio();       

        board.dropTime = 1;       

        // check amount
        
        if (CoreData.instance.GetSingleBreaker() <= 0)
        {
            // show booster popup
             ShowPopup(BOOSTER_TYPE.SINGLE_BREAKER);

            return;
        }

        if (board.booster == BOOSTER_TYPE.NONE)
        {
            ActiveBooster(BOOSTER_TYPE.SINGLE_BREAKER);

          

        }
        else
        {
            CancelBooster(BOOSTER_TYPE.SINGLE_BREAKER);
        }

       
    }

    #endregion

    #region Row

    public void RowBoosterClick()
    {
        if (board.state != GAME_STATE.WAITING_USER_SWAP || board.lockSwap == true)
        {
            return;
        }
        
            itemGrid.instance.CloseHelp();
        
       
        AudioManager.instance.ButtonClickAudio();
       

        board.dropTime = 1;
        

        // check amount

        if (CoreData.instance.GetRowBreaker() <= 0)
        {
            // show booster popup
       //     ShowPopup(BOOSTER_TYPE.ROW_BREAKER);

            return;
        }

        if (board.booster == BOOSTER_TYPE.NONE)
        {
            ActiveBooster(BOOSTER_TYPE.ROW_BREAKER);


           
        }
        else
        {
            CancelBooster(BOOSTER_TYPE.ROW_BREAKER);
        }

    }

    #endregion

    #region Column

    public void ColumnBoosterClick()
    {
        if (board.state != GAME_STATE.WAITING_USER_SWAP || board.lockSwap == true)
        {
            return;
        }
        
            itemGrid.instance.CloseHelp();
       
        AudioManager.instance.ButtonClickAudio();
        
        board.dropTime = 1;      

        // check amount

        if (CoreData.instance.GetColumnBreaker() <= 0)
        {
            // show booster popup
        //    ShowPopup(BOOSTER_TYPE.COLUMN_BREAKER);

            return;
        }

        if (board.booster == BOOSTER_TYPE.NONE)
        {
            ActiveBooster(BOOSTER_TYPE.COLUMN_BREAKER);


        }
        else
        {
            CancelBooster(BOOSTER_TYPE.COLUMN_BREAKER);
        }
    }

    #endregion

    #region Rainbow

    public void RainbowBoosterClick()
    {
        if (board.state != GAME_STATE.WAITING_USER_SWAP || board.lockSwap == true)
        {
            return;
        }
        
            itemGrid.instance.CloseHelp();
        
        AudioManager.instance.ButtonClickAudio();
        

        board.dropTime = 1;
       
        // check amount

        if (CoreData.instance.GetRainbowBreaker() <= 0)
        {
            // show booster popup
        //    ShowPopup(BOOSTER_TYPE.RAINBOW_BREAKER);

            return;
        }

        if (board.booster == BOOSTER_TYPE.NONE)
        {
            ActiveBooster(BOOSTER_TYPE.RAINBOW_BREAKER);


        }
        else
        {
            CancelBooster(BOOSTER_TYPE.RAINBOW_BREAKER);
        }
    }

    #endregion

    #region Oven

    public void OvenBoosterClick()
    {
        if (board.state != GAME_STATE.WAITING_USER_SWAP || board.lockSwap == true)
        {
            return;
        }
        
        itemGrid.instance.CloseHelp();
        
        AudioManager.instance.ButtonClickAudio();
       

        board.dropTime = 0;       

        // check amount

        if (CoreData.instance.GetOvenBreaker() <= 0)
        {
            // show booster popup
        //    ShowPopup(BOOSTER_TYPE.OVEN_BREAKER);

            return;
        }

        if (board.booster == BOOSTER_TYPE.NONE)
        {
            ActiveBooster(BOOSTER_TYPE.OVEN_BREAKER);


        }
        else
        {
            CancelBooster(BOOSTER_TYPE.OVEN_BREAKER);
        }
    }

    #endregion

    #region Complete

    public void BoosterComplete()
    {
        if (board.booster == BOOSTER_TYPE.SINGLE_BREAKER)
        {
            CancelBooster(BOOSTER_TYPE.SINGLE_BREAKER);

            // reduce amount

            if (CoreData.instance.GetSingleBreaker() > 0)
            {
                var amount = CoreData.instance.GetSingleBreaker() - 1;
                CoreData.instance.SaveSingleBreaker(amount);

                // change text

                singleAmount.text = amount.ToString();
           
                //achievement
                Configuration.SaveAchievement("ach_use10singleBreaker",1);
            }

        }
        else if (board.booster == BOOSTER_TYPE.ROW_BREAKER)
        {
            CancelBooster(BOOSTER_TYPE.ROW_BREAKER);

            // reduce amount

            if (CoreData.instance.GetRowBreaker() > 0)
            {
                var amount = CoreData.instance.GetRowBreaker() - 1;
                CoreData.instance.SaveRowBreaker(amount);

                // change text

                rowAmount.text = amount.ToString();
               

                //achievement
                Configuration.SaveAchievement("ach_use10RowBreaker", 1);
            }
        }
        else if (board.booster == BOOSTER_TYPE.COLUMN_BREAKER)
        {
            CancelBooster(BOOSTER_TYPE.COLUMN_BREAKER);

            // reduce amount

            if (CoreData.instance.GetColumnBreaker() > 0)
            {
                var amount = CoreData.instance.GetColumnBreaker() - 1;
                CoreData.instance.SaveColumnBreaker(amount);

                // change text

                columnAmount.text = amount.ToString();
                
                //achievement
                Configuration.SaveAchievement("ach_use10ColumnBreaker", 1);
            }
        }
        else if (board.booster == BOOSTER_TYPE.RAINBOW_BREAKER)
        {
            CancelBooster(BOOSTER_TYPE.RAINBOW_BREAKER);

            // reduce amount

            if (CoreData.instance.GetRainbowBreaker() > 0)
            {
                var amount = CoreData.instance.GetRainbowBreaker() - 1;
                CoreData.instance.SaveRainbowBreaker(amount);

                // change text

                rainbowAmount.text = amount.ToString();
                
                //achievement
                Configuration.SaveAchievement("ach_use10ColorBreaker", 1);
            }
        }
        else if (board.booster == BOOSTER_TYPE.OVEN_BREAKER)
        {
            CancelBooster(BOOSTER_TYPE.OVEN_BREAKER);

            // reduce amount

            if (CoreData.instance.GetOvenBreaker() > 0)
            {
                var amount = CoreData.instance.GetOvenBreaker() - 1;
                CoreData.instance.SaveOvenBreaker(amount);

                // change text

                ovenAmount.text = amount.ToString();
                 
                //achievement
                Configuration.SaveAchievement("ach_use10BlenderBall", 1);
            }
        }
        board.ResetSize();
    }

    #endregion

    #region Popup

    public void ShowPopup(BOOSTER_TYPE check)
    {
        if (check == BOOSTER_TYPE.SINGLE_BREAKER)
        {
            board.state = GAME_STATE.OPENING_POPUP;

            singleBoosterPopup.OpenPopup();
        }
        else if (check == BOOSTER_TYPE.ROW_BREAKER)
        {
            board.state = GAME_STATE.OPENING_POPUP;

            rowBoosterPopup.OpenPopup();
        }
        else if (check == BOOSTER_TYPE.COLUMN_BREAKER)
        {
            board.state = GAME_STATE.OPENING_POPUP;

            columnBoosterPopup.OpenPopup();
        }
        else if (check == BOOSTER_TYPE.RAINBOW_BREAKER)
        {
            board.state = GAME_STATE.OPENING_POPUP;

            rainbowBoosterPopup.OpenPopup();
        }
        else if (check == BOOSTER_TYPE.OVEN_BREAKER)
        {
            board.state = GAME_STATE.OPENING_POPUP;

            ovenBoosterPopup.OpenPopup();
        }
    }

    #endregion

    #region Booster

    public void ActiveBooster(BOOSTER_TYPE check)
    {
        if (check == BOOSTER_TYPE.SINGLE_BREAKER)
        {
            board.booster = BOOSTER_TYPE.SINGLE_BREAKER;

            singleActive.SetActive(true);

            // interactable
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            //Info Popup
            StartCoroutine(ShowInfoBoosters("Single Booster", "SINGLE BREAKER! \n Destroys a cube!"));
        }
        else if (check == BOOSTER_TYPE.ROW_BREAKER)
        {
            board.booster = BOOSTER_TYPE.ROW_BREAKER;

            rowActive.SetActive(true);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            //Info Popup
            StartCoroutine(ShowInfoBoosters("Row Booster", "ROW BREAKER! \n Destroys a row!"));
        }
        else if (check == BOOSTER_TYPE.COLUMN_BREAKER)
        {
            board.booster = BOOSTER_TYPE.COLUMN_BREAKER;

            columnActive.SetActive(true);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            //Info Popup
            StartCoroutine(ShowInfoBoosters("Column Booster", "COLUMN BREAKER! \n Destroy a column!"));
        }
        else if (check == BOOSTER_TYPE.RAINBOW_BREAKER)
        {
            board.booster = BOOSTER_TYPE.RAINBOW_BREAKER;

            rainbowActive.SetActive(true);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;            
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            //Info Popup
            StartCoroutine(ShowInfoBoosters("Rainbow Booster", "COLOR BREAKER! \n It destroys cubes of the same color you choose!"));
        }
        else if (check == BOOSTER_TYPE.OVEN_BREAKER)
        {
            board.booster = BOOSTER_TYPE.OVEN_BREAKER;

            ovenActive.SetActive(true);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            //Info Popup
            StartCoroutine(ShowInfoBoosters("Oven Booster", "BLENDER BALL! \n shuffles all the cubes!"));
        }
    }

    public void CancelBooster(BOOSTER_TYPE check)
    {
        board.booster = BOOSTER_TYPE.NONE;
        //Close info popup
        itemGrid.instance.CloseHelpWindows();

        if (check == BOOSTER_TYPE.SINGLE_BREAKER)
        {
            singleActive.SetActive(false);

            // interactable
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
        }
        else if (check == BOOSTER_TYPE.ROW_BREAKER)
        {
            rowActive.SetActive(false);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
        }
        else if (check == BOOSTER_TYPE.COLUMN_BREAKER)
        {
            columnActive.SetActive(false);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
        }
        else if (check == BOOSTER_TYPE.RAINBOW_BREAKER)
        {
            rainbowActive.SetActive(false);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;            
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
        }
        else if (check == BOOSTER_TYPE.OVEN_BREAKER)
        {
            ovenActive.SetActive(false);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
        }
    }

    #endregion

    //ShowHelpBoosters
    public IEnumerator ShowInfoBoosters(string BoosterName, string BoosterInfo)
    {
        yield return new WaitForEndOfFrame();
        //Booster Show
        Configuration.instance.MessageBoardInfo = BoosterInfo;     


       itemGrid.instance.HelpPrefab(GameObject.Find("TOPCENTER"), 340, false);

        //while (!Input.GetMouseButtonUp(0))
        //{
        //    yield return null;
        //}

        //yield return StartCoroutine(StartCloseHelp());
        //Configuration.instance.pause = false;

    }
}
