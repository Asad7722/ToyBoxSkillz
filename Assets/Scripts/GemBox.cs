using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GemBox : MonoBehaviour
{
    public static GemBox instance = null;
    [Header("Game Objects for some elements")]
    public Button PaidTurnButton;               // This button is showed when you can turn the wheel for coins
    public Button FreeTurnButton;               // This button is showed when you can turn the wheel for free        
    public GameObject NextTurnTimerWrapper;
    public Text NextFreeTurnTimerText;          // Text element that contains remaining time to next free turn   
    public Text GemBoxInfoText;
    public Text GemBoxBonusAmountText;
    public Text GemboxGemsAmountText;
    public Text GemboxGemsMapAmountText;
    public GameObject GemBoxPopup;
    public GameObject GemboxGemsMapAmount;
    public GAME_STATE state;
    public PopupOpener GemboxfabPopup;
       
    [Header("How much currency one paid turn costs")]
    public int TurnCost = 0;                   // How much coins user waste when turn whe wheel

    private bool _isStarted;                    // Flag that the wheel is spinning
          
    private int PlayerCoins;        // Started coins amount. In your project it should be picked up from CoinsManager or from PlayerPrefs and so on
    private int _previousCoinsAmount;


    // Remaining time to next free turn
    private int _timerRemainingDays;
    private int _timerRemainingHours;
    private int _timerRemainingMinutes;
    private int _timerRemainingSeconds;

    private DateTime _nextFreeTurnTime;

    // Key name for storing in PlayerPrefs
    private const string OPEN_FREE_BOX1 = "GemBoxLastDeactiveTimeTicks";

    // Set TRUE if you want to let players to turn the wheel for coins while free turn is not available yet
    [Header("Can players turn the wheel for currency?")]
    public bool IsPaidTurnEnabled = true;

    // Set TRUE if you want to let players to turn the wheel for FREE from time to time
    [Header("Can players turn the wheel for FREE from time to time?")]
    public bool IsFreeTurnEnabled = true;
  

    // Flag that player can turn the wheel for free right now
    private bool _isFreeTurnAvailable;

   // private GemBoxSector _finalSector;
    
 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
     /*   else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);*/
      
       

    }
  
    void Start()
    {
     
        if (Configuration.instance.GemboxActive)
        {
            if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
            {
                int openedLevel = CoreData.instance.GetOpendedLevel();
                int totalusedgame = PlayerPrefs.GetInt("totalusedgame", 0);
                int firststart = Configuration.instance.BeginLevelGembox;
                StartCoroutine(Showanimasyon());
                if (openedLevel >= firststart)
                {
                    GemboxGemsAmountText.text = "" + PlayerPrefs.GetInt("GemBoxAmount");
                }
                else
                {
                    GemboxGemsAmountText.text = "" + PlayerPrefs.GetInt("GemBoxAmount");
                }               
            }
        }
        else
        {
            GemboxGemsAmountText.text = "" + PlayerPrefs.GetInt("GemBoxAmount");
        }

    }
    public void GemboxControl()
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        int totalusedgame = PlayerPrefs.GetInt("totalusedgame", 0);
        int firststart = Configuration.instance.BeginLevelGembox;
        if (Configuration.instance.GemboxActive)
        {
            if (openedLevel >= firststart)
            {
                GemboxGemsMapAmount.SetActive(true);
            }
            else
            {
                GemboxGemsMapAmount.SetActive(false);
            }
        }
       updateGemboxAmount();           
    }

    IEnumerator Showanimasyon()
    {
        int CurrentGems = PlayerPrefs.GetInt("GemBoxAmount", 0);
        int GemBoxBonus = Configuration.instance.gemboxEveryLevelBonusAmount;
        int GemBoxMaxAmount = Configuration.instance.gemboxMaxAmount;        
          
        yield return new WaitForSeconds(2f);
        if (CurrentGems < GemBoxMaxAmount)
        {
            GemBoxBonusAmountText.text = "+" + GemBoxBonus ;

        }
        else
        {
            GemBoxBonusAmountText.text = "FULL!";
        }        

    }
    private void TurnWheelForFree()
    {
        TurnWheel(true);
        AudioManager.instance.GingerbreadExplodeAudio();
    }

    private void TurnWheelForCoins()
    {
        TurnWheel(false);
    }

    public void updateGemboxAmount()
    {
        int CurrentGems = PlayerPrefs.GetInt("GemBoxAmount", 0);
        GemboxGemsMapAmountText.text =""+ CurrentGems;
    }
    private void TurnWheel(bool isFree)
    {   
       PlayerPrefs.SetString(OPEN_FREE_BOX1, DateTime.Now.Ticks.ToString());
            // Restart timer to next free turn
     //  SetNextFreeTime();
      
    }

    public void CheckGemboxGems()
    {
        GemboxGemsAmountText.text = "" + PlayerPrefs.GetInt("GemBoxAmount", 0);
    }
    public void TurnWheelButtonClick()
    {
        if (_isFreeTurnAvailable)
         {
        PlayerPrefs.SetString(OPEN_FREE_BOX1, DateTime.Now.Ticks.ToString());
        // Restart timer to next free turn
            CloseGemBoxPopup();
            sifirla();
        }
    }
    public void TurnWheelbuyButtonClick()
    {
     TurnWheelForCoins(); 
             
    }
    public void OpenGemBoxPopup()
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        int totalusedgame = PlayerPrefs.GetInt("totalusedgame", 0);
        int firststart = Configuration.instance.BeginLevelGembox;
        AudioManager.instance.ButtonClickAudio();
        if (openedLevel >= firststart)
        {
            GemboxfabPopup.OpenPopup();
        }
    }

    public void CloseGemBoxPopup()
    {
        GemBoxPopup.SetActive(false);
        AudioManager.instance.ButtonClickAudio();
        updateGemboxAmount();
    }
    public void enablegembox()
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        int totalusedgame = PlayerPrefs.GetInt("totalusedgame", 0);
        int firststart = Configuration.instance.BeginLevelGembox;
        if (openedLevel >= firststart)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }
    public void disablegembox()
    {
        gameObject.SetActive(false);
        Configuration.instance.GemboxActive = false;
        Configuration.instance.GemboxDeactive = true;
        
    }
    public void sifirla()
    {
        PlayerPrefs.SetInt("GemBoxAmount", 0);
        PlayerPrefs.SetInt("usedgame", 0);
        PlayerPrefs.Save();
      
    }

    private void ShowTurnButtons()
    {
        int PlayerCoins = CoreData.instance.GetPlayerCoin();
        int CurrentGems = PlayerPrefs.GetInt("GemBoxAmount", 0);
        int gemboxminamount = Configuration.instance.gemboxMinAmount;
        if (_isFreeTurnAvailable)               // If have free turn
        {
            disablegembox();         
        }
        else
        {                                // If haven't free turn
            
            if (!IsPaidTurnEnabled)             // If our settings allow only free turns
            {
                ShowFreeTurnButton();
                DisableFreeTurnButton();        // Make button inactive while spinning or timer to next free turn
                Language.StartGlobalTranslate(GemBoxInfoText, gemboxminamount, "");

            }
            else
            {                           // If player can turn for coins
                ShowPaidTurnButton();                
                if (_isStarted || PlayerCoins < TurnCost || CurrentGems < gemboxminamount)
                {
                    DisablePaidTurnButton(); // Make button non interactable if user has not enough money for the turn of if wheel is turning right now
                    Language.StartGlobalTranslate(GemBoxInfoText, gemboxminamount, "");
                }
                else
                {
                    EnablePaidTurnButton();           // Can make paid turn right now
                    Language.StartGlobalTranslate(GemBoxInfoText, 0, "");

                }
            }
        }
    } 
    private void Update()
    {

        if (Configuration.instance.GemboxActive)
        {
            if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
            {
                int CurrentGems = PlayerPrefs.GetInt("GemBoxAmount", 0);
                GemboxGemsAmountText.text = "" + CurrentGems;
            }
        }
            }

    /// <summary>
    /// Sample callback for giving reward (in editor each sector have Reward Callback field pointed to this method)
    /// </summary>
    /// <param name="awardCoins">Coins for user</param>
    
     

    // Change remaining time to next free turn every 1 second
    private void UpdateFreeTurnTimer()
    {
        // Don't count the time if we have free turn already
        if (_isFreeTurnAvailable)
            return;

        // Update the remaining time values
        _timerRemainingDays = (int)(_nextFreeTurnTime - DateTime.Now).Days;
        _timerRemainingHours = (int)(_nextFreeTurnTime - DateTime.Now).Hours;
        _timerRemainingMinutes = (int)(_nextFreeTurnTime - DateTime.Now).Minutes;
        _timerRemainingSeconds = (int)(_nextFreeTurnTime - DateTime.Now).Seconds;

        // If the timer has ended      
        int reanable = Configuration.instance.reenable;
        int usedgame = PlayerPrefs.GetInt("usedgame", 0);
        if (_timerRemainingDays <= 0 && _timerRemainingHours <= 0 && _timerRemainingMinutes <= 0 && _timerRemainingSeconds <= 0)
        {                       
            NextFreeTurnTimerText.text = "Remaining Time: " + (reanable - usedgame);
            // Now we have a free turn
            _isFreeTurnAvailable = true;
        }
        else
        {
            // Show the remaining time
            if (_timerRemainingDays <= 0)
            {
                NextFreeTurnTimerText.text = String.Format("{0:00}h :{1:00}m", _timerRemainingHours, _timerRemainingMinutes);
            }
            if (_timerRemainingDays <= 0 && _timerRemainingHours <= 0)
            {
                NextFreeTurnTimerText.text = String.Format("{0:00}m :{1:00}s", _timerRemainingMinutes, _timerRemainingSeconds);
            }
           if (_timerRemainingDays > 0)
            {
                NextFreeTurnTimerText.text = String.Format("{0:00}d :{1:00}h", _timerRemainingDays, _timerRemainingHours);
            }
            // We don't have a free turn yet
            _isFreeTurnAvailable = false;
        }
    }

    private void EnableButton(Button button)
    {
        button.interactable = true;
        //button.GetComponent<Image> ().color = new Color(255, 255, 255, 1f);
    }

    private void DisableButton(Button button)
    {
        button.interactable = false;
        //button.GetComponent<Image> ().color = new Color(255, 255, 255, 0.5f);
    }

    // Function for more readable calls
    private void EnableFreeTurnButton() { EnableButton(FreeTurnButton); }
    private void DisableFreeTurnButton() { DisableButton(FreeTurnButton); }
    private void EnablePaidTurnButton() { EnableButton(PaidTurnButton); }
    private void DisablePaidTurnButton() { DisableButton(PaidTurnButton); }

    private void ShowFreeTurnButton()
    {
        FreeTurnButton.gameObject.SetActive(true);
        PaidTurnButton.gameObject.SetActive(true);
    }

    private void ShowPaidTurnButton()
    {
        PaidTurnButton.gameObject.SetActive(true);
        FreeTurnButton.gameObject.SetActive(false);
    }

    public void ResetTimer()
    {
        PlayerPrefs.DeleteKey(OPEN_FREE_BOX1);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GemBox))]
public class GemBoxEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GemBox myScript = (GemBox)target;
        if (GUILayout.Button("Reset Timer"))
        {
            myScript.ResetTimer();
        }
    }
}
#endif