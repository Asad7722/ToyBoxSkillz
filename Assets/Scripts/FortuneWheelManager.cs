using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;
using UnityEngine.Events;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class FortuneWheelManager : MonoBehaviour
{
    
    public GAME_STATE state;
    [Header("Game Objects for some elements")]
    public Button PaidTurnButton =null;               // This button is showed when you can turn the wheel for coins
    public Button FreeTurnButton = null;               // This button is showed when you can turn the wheel for free
    public GameObject Circle;                   // Rotatable GameObject on scene with reward objects
    public Text DeltaCoinsText;                 // Pop-up text with wasted or rewarded coins amount
    public Text CurrentCoinsText;               // Pop-up text with wasted or rewarded coins amount
    public GameObject NextTurnTimerWrapper;
    public Text NextFreeTurnTimerText;	    // Text element that contains remaining time to next free turn
    public PopupOpener GiftWheelWindow;

    [Header("How much currency one paid turn costs")]
    public int TurnCost = 10;                   // How much coins user waste when turn whe wheel

    private bool _isStarted;                    // Flag that the wheel is spinning

    [Header("Params for each sector")]
    public FortuneWheelSector[] Sectors;        // All sectors objects

    private float _finalAngle;                  // The final angle is needed to calculate the reward
    private float _startAngle;                  // The first time start angle equals 0 but the next time it equals the last final angle
    private float _currentLerpRotationTime;     // Needed for spinning animation
    private int PlayerCoins;        // Started coins amount. In your project it should be picked up from CoinsManager or from PlayerPrefs and so on
    private int _previousCoinsAmount;

    // Here you can set time between two free turns
    [Header("Time Between Two Free Turns")]
    public int TimerMaxHours;
    [RangeAttribute(0, 59)]
    public int TimerMaxMinutes;
    [RangeAttribute(0, 59)]
    public int TimerMaxSeconds = 10;

    // Remaining time to next free turn
    private int _timerRemainingHours;
    private int _timerRemainingMinutes;
    private int _timerRemainingSeconds;

    private DateTime _nextFreeTurnTime;

    // Key name for storing in PlayerPrefs
    private const string LAST_FREE_TURN_TIME_NAME = "LastFreeTurnTimeTicks";

    // Set TRUE if you want to let players to turn the wheel for coins while free turn is not available yet
    [Header("Can players turn the wheel for currency?")]
    public bool IsPaidTurnEnabled = true;

    // Set TRUE if you want to let players to turn the wheel for FREE from time to time
    [Header("Can players turn the wheel for FREE from time to time?")]
    public bool IsFreeTurnEnabled = true;

    // Flag that player can turn the wheel for free right now
    private bool _isFreeTurnAvailable;

    private FortuneWheelSector _finalSector;
    public GameObject WinWindow;
    public GameObject GiftWheel;
    public GameObject WheelInfo;
    public bool wheel = false;
    public bool reward = true;

    public static FortuneWheelManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        // _previousCoinsAmount = 1000;
        // Show our current coins amount
        // CurrentCoinsText.text = CoreData.instance.GetPlayerCoin().ToString();

        // Show sector reward value in text object if it's set
        foreach (var sector in Sectors)
        {
            if (sector.ValueTextObject != null)
                sector.ValueTextObject.GetComponent<Text>().text = sector.RewardValue.ToString();
        }

        if (IsFreeTurnEnabled)
        {
            PaidTurnButton.interactable = false;
            if (!PlayerPrefs.HasKey("First_Look_Wheel"))
            {
               

                _isFreeTurnAvailable = true;
                ShowFreeTurnButton();

            }
            else
            {
                // Start our timer to next free turn
                SetNextFreeTime();
                if (!PlayerPrefs.HasKey(LAST_FREE_TURN_TIME_NAME))
                {
                    PlayerPrefs.SetString(LAST_FREE_TURN_TIME_NAME, DateTime.Now.Ticks.ToString());
                }

            }


           
        }
        else
        {
            PaidTurnButton.interactable = true;
            NextTurnTimerWrapper.gameObject.SetActive(false);
        }
        if (state == GAME_STATE.OPENING_POPUP)
        {
            WinWindow.gameObject.SetActive(false);
            GiftWheel.SetActive(false);
        }
        reward = true;        
    }


    private void TurnWheelForFree()
    {
        TurnWheel(true);
        PlayerPrefs.SetInt("First_Look_Wheel", 1);
        AudioManager.instance.GingerbreadExplodeAudio();
    }

    private void TurnWheelForCoins()
    {
        TurnWheel(false);
    }

    private void TurnWheel(bool isFree,bool againturn=false)
    {
        //achievement
        Configuration.SaveAchievement("ach_giftWheel", 1);

        _currentLerpRotationTime = 0f;

        // All sectors angles
        int[] sectorsAngles = new int[Sectors.Length];

        // Fill the necessary angles (for example if we want to have 12 sectors we need to fill the angles with 30 degrees step)
        // It's recommended to use the EVEN sectors count (2, 4, 6, 8, 10, 12, etc)
        for (int i = 1; i <= Sectors.Length; i++)
        {
            sectorsAngles[i - 1] = 360 / Sectors.Length * i;
        }

        //int cumulativeProbability = Sectors.Sum(sector => sector.Probability);

        double rndNumber = UnityEngine.Random.Range(1, Sectors.Sum(sector => sector.Probability));

        // Calculate the propability of each sector with respect to other sectors
        int cumulativeProbability = 0;
        // Random final sector accordingly to probability
        int randomFinalAngle = sectorsAngles[0];
        _finalSector = Sectors[0];

        for (int i = 0; i < Sectors.Length; i++)
        {
            cumulativeProbability += Sectors[i].Probability;

            if (rndNumber <= cumulativeProbability)
            {
                // Choose final sector
                randomFinalAngle = sectorsAngles[i];
                _finalSector = Sectors[i];
                break;
            }
        }

        int fullTurnovers = 5;

        // Set up how many turnovers our wheel should make before stop
        _finalAngle = fullTurnovers * 360 + randomFinalAngle;

        // Stop the wheel
        _isStarted = true;

        int PlayerCoins = CoreData.instance.GetPlayerCoin();
        _previousCoinsAmount = PlayerCoins;

        // Decrease money for the turn if it is not free turn
        if (!isFree)
        {
            //PlayerCoins -= TurnCost;
            if(!againturn)
            {
                CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= TurnCost);
                if (state == GAME_STATE.OPENING_POPUP)
                {
                    GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
                }
                AudioManager.instance.CoinPayAudio();
                // Show wasted coins
                DeltaCoinsText.text = String.Format("-{0}", TurnCost);
                DeltaCoinsText.gameObject.SetActive(true);

                // Animations for coins
                StartCoroutine(HideCoinsDelta());
                StartCoroutine(UpdateCoinsAmount());
            }          
        }
        else
        {
            // At this step you can save current time value to your server database as last used free turn
            // We can't save long int to PlayerPrefs so store this value as string and convert to long later
            PlayerPrefs.SetString(LAST_FREE_TURN_TIME_NAME, DateTime.Now.Ticks.ToString());

            // Restart timer to next free turn
            SetNextFreeTime();
        }
    }

    public void TurnWheelButtonClick()
    {
        if (_isFreeTurnAvailable)
        {
            // Show it if it's ready
            // Show it if it's ready
            //sameer
            wheel = true;
            reward = false;
            ////admanager.instance.ShowRewardedVideAdGeneric(3);
            //AdsManager.instance.ShowReward();

            {

                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    // TurnWheelForFree();
                    WheelInfo.SetActive(true);
                    AudioManager.instance.ButtonClickAudio();
                }
                else
                {
                    if (reward)
                    {
                        TurnWheelForFree();
                        wheel = false;
                    }
                    else
                    {
                        Debug.Log("odul kapatıldı");
                        //WheelInfo.SetActive(true);
                        AudioManager.instance.ButtonClickAudio();
                    }
                }
            }

        }
        else
        {
            // If we have enabled paid turns
            if (IsPaidTurnEnabled)
            {
                int PlayerCoins = CoreData.instance.GetPlayerCoin();
                // If player have enough coins
                if (PlayerCoins >= TurnCost)
                {
                    TurnWheelForCoins();
                }
            }
        }
    }
    public void TurnWheelbuyButtonClick()
    {
        int PlayerCoins = CoreData.instance.GetPlayerCoin();
        // If player have enough coins
        if (PlayerCoins >= TurnCost)
        {
            TurnWheelForCoins();
        }
        else
        {
            MapScene.instance.page2();
        }
    }
    public void closeWheelInfo()
    {
        WheelInfo.SetActive(false);
        AudioManager.instance.ButtonClickAudio();
    }

    public void ShowGiftWheel()
    {
        // GiftWheel.SetActive(true);
        AudioManager.instance.ButtonClickAudio();
        GiftWheelWindow.OpenPopup();
        /*  Canvas m_canvas = GameObject.Find("Canvas Setting").GetComponent<Canvas>();
        var nesne = Instantiate(Resources.Load(Configuration.GiftWheelWindow())) as GameObject;
        nesne.transform.SetParent(m_canvas.transform, false);*/

    }
    public void HideGiftWheel()
    {
        GiftWheel.SetActive(false);
        AudioManager.instance.ButtonClickAudio();
    }

    public void BoosterDikeyfirca()
    {
        CoreData.instance.SaveColumnBreaker(CoreData.instance.columnBreaker += 1);
        try { BostersBox.instance.updateBoostersLabels(); }
        catch { }
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void BoosterBomb()
    {
        CoreData.instance.SaveBeginBombBreaker(CoreData.instance.beginBombBreaker += 1);
        try { BostersBox.instance.updateBoostersLabels(); }
        catch { }
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);

    }
    public void BoosterFive()
    {
        CoreData.instance.SaveBeginFiveMoves(CoreData.instance.beginFiveMoves += 1);
        try { BostersBox.instance.updateBoostersLabels(); }
        catch { }
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void BoosterMala()
    {
        CoreData.instance.SaveSingleBreaker(CoreData.instance.singleBreaker += 1);
        try { BostersBox.instance.updateBoostersLabels(); }
        catch { }
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void BoosterLife3()
    {
        if (NewLife.instance.Rewardlives >= 10 || NewLife.instance.unlimitedLifePurchased)
        {
            Configuration.OpenInfoPopupAutoClose("", "Life Box is Full", 1);
            TurnWheel(false,true);
            return;
        }       
        if (NewLife.instance.maxLives - NewLife.instance.lives < 3)
        {
            NewLife.instance.Rewardlives += 3 - (NewLife.instance.maxLives - NewLife.instance.lives);
            NewLife.instance.lives = NewLife.instance.maxLives;
        }
        else
        {
            NewLife.instance.lives += 3;
        }
      
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void BoosterYatayfirca()
    {
        CoreData.instance.SaveRowBreaker(CoreData.instance.rowBreaker += 1);
        try { BostersBox.instance.updateBoostersLabels(); }
        catch { }
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void BoosterRainbow()
    {
        CoreData.instance.SaveRainbowBreaker(CoreData.instance.rainbowBreaker += 1);
        try { BostersBox.instance.updateBoostersLabels(); }
        catch { }
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void BoosterLife2()
    {
        if (NewLife.instance.Rewardlives >= 10 || NewLife.instance.unlimitedLifePurchased)
        {
            Configuration.OpenInfoPopupAutoClose("", "Life Box is Full", 1);
            TurnWheel(false, true);
            return;
        }
        if (NewLife.instance.maxLives - NewLife.instance.lives < 2)
        {
            NewLife.instance.Rewardlives += 3 - (NewLife.instance.maxLives - NewLife.instance.lives);
            NewLife.instance.lives = NewLife.instance.maxLives;
        }
        else
        {
            NewLife.instance.lives += 2;
        }

        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void BoosterBeginRainbow()
    {
        CoreData.instance.SaveBeginRainbow(CoreData.instance.beginRainbow += 1);
        try { BostersBox.instance.updateBoostersLabels(); }
        catch { }
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void BoosterGems10()
    {
        CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 25);
        if (state == GAME_STATE.OPENING_POPUP)
        {
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
        }
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void BoosterLife1()
    {
        if (NewLife.instance.Rewardlives >= 10 || NewLife.instance.unlimitedLifePurchased)
        {
            Configuration.OpenInfoPopupAutoClose("", "Life Box is Full", 1);
            TurnWheel(false, true);
            return;
        }
        if (NewLife.instance.maxLives == NewLife.instance.lives)
        {
            NewLife.instance.Rewardlives += 1;           
        }
        else
        {
            NewLife.instance.lives += 1;
        }
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void BoosterEl()
    {
        CoreData.instance.SaveOvenBreaker(CoreData.instance.ovenBreaker += 1);
        try { BostersBox.instance.updateBoostersLabels(); }
        catch { }
        AudioManager.instance.giftbuton();
        WinWindow.gameObject.SetActive(true);
    }
    public void HideWinWindow()
    {
        WinWindow.gameObject.SetActive(false);
    }

    public void SetNextFreeTime()
    {
        // Reset the remaining time values
        _timerRemainingHours = TimerMaxHours;
        _timerRemainingMinutes = TimerMaxMinutes;
        _timerRemainingSeconds = TimerMaxSeconds;

        // Get last free turn time value from storage
        // We can't save long int to PlayerPrefs so store this value as string and convert to long
        _nextFreeTurnTime = new DateTime(Convert.ToInt64(PlayerPrefs.GetString(LAST_FREE_TURN_TIME_NAME, DateTime.Now.Ticks.ToString())))
            .AddHours(TimerMaxHours)
            .AddMinutes(TimerMaxMinutes)
            .AddSeconds(TimerMaxSeconds);

        _isFreeTurnAvailable = false;
        if (state == GAME_STATE.OPENING_POPUP || state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
            FreeTurnButton.gameObject.SetActive(false);


    }

    private void ShowTurnButtons()
    {
        if (_isFreeTurnAvailable)               // If have free turn
        {
            ShowFreeTurnButton();
            EnableFreeTurnButton();

        }
        else
        {                                // If haven't free turn
            int PlayerCoins = CoreData.instance.GetPlayerCoin();

            if (!IsPaidTurnEnabled)         // If our settings allow only free turns
            {
                ShowFreeTurnButton();
                DisableFreeTurnButton();        // Make button inactive while spinning or timer to next free turn

            }
            else
            {                           // If player can turn for coins
                ShowPaidTurnButton();

                if (_isStarted || PlayerCoins < TurnCost)
                    DisablePaidTurnButton();    // Make button non interactable if user has not enough money for the turn of if wheel is turning right now
                else
                    EnablePaidTurnButton(); // Can make paid turn right now
            }
        }
    }
   

    //sameerrewardedcomplete
    public void RewardedAdCompletedHandler()
    {
        //REWARD
        if (wheel)
        {
            TurnWheelForFree();
            if (state == GAME_STATE.OPENING_POPUP)
            {
                GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
            }

            wheel = false;
            reward = true;
        }

        //REWARD 
        //achievement
        Configuration.SaveAchievement("ach_watchAds", 1);
    }
    
    //private void HandleShowResult(ShowResult result)
    //{
    //    switch (result)
    //    {
    //        case ShowResult.Finished:
    //            Debug.Log("The ad was successfully shown.");
    //            //
    //            // YOUR CODE TO REWARD THE GAMER
    //            // Give coins etc.
    //            //REWARD
    //            if (wheel)
    //            {
    //                TurnWheelForFree();
    //                if (state == GAME_STATE.OPENING_POPUP)
    //                {
    //                    GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
    //                }

    //                wheel = false;
    //                reward = true;
    //            }

    //            //REWARD 
    //            break;
    //        case ShowResult.Skipped:
    //            Debug.Log("The ad was skipped before reaching the end.");
    //            break;
    //        case ShowResult.Failed:
    //            Debug.LogError("The ad failed to be shown.");
    //            break;
    //    }
    //}
    public void OnVideoComplete()
    {
        Debug.Log("<AMRSDK> OnVideoComplete");
        // reward user for watching a video
        //REWARD
        if (wheel)
        {
            TurnWheelForFree();
            if (state == GAME_STATE.OPENING_POPUP)
            {
                GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
            }

            wheel = false;
            reward = true;
        }

        //REWARD 
        Debug.Log("<AMRSDK> LoadReward");
        //AdsManager.LoadReward();
    }

    public void OnVideoDismiss()
    {
        Debug.Log("<AMRSDK> OnVideoDismiss");
        Debug.Log("<AMRSDK> LoadReward");
        //AdsManager.LoadReward();

    }
    private void Update()
    {
        if (!PlayerPrefs.HasKey("First_Look_Wheel"))
        {
            return;
        }

            // We need to show TURN FOR FREE button or TURN FOR COINS button
            ShowTurnButtons();

        // Show timer only if we enable free turns
        if (IsFreeTurnEnabled)
            UpdateFreeTurnTimer();

        if (!_isStarted)
            return;

        // Animation time
        float maxLerpRotationTime = 4f;

        // increment animation timer once per frame
        _currentLerpRotationTime += Time.deltaTime;

        // If the end of animation
        if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle)
        {
            _currentLerpRotationTime = maxLerpRotationTime;
            _isStarted = false;
            _startAngle = _finalAngle % 360;

            //GiveAwardByAngle ();
            _finalSector.RewardCallback.Invoke();
            StartCoroutine(HideCoinsDelta());
        }
        else
        {
            // Calculate current position using linear interpolation
            float t = _currentLerpRotationTime / maxLerpRotationTime;

            // This formulae allows to speed up at start and speed down at the end of rotation.
            // Try to change this values to customize the speed
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
            Circle.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    /// <summary>
    /// Sample callback for giving reward (in editor each sector have Reward Callback field pointed to this method)
    /// </summary>
    /// <param name="awardCoins">Coins for user</param>
    public void RewardCoins(int awardCoins)
    {
        int PlayerCoins = CoreData.instance.GetPlayerCoin();
        PlayerCoins += awardCoins;
        // Show animated delta coins
        DeltaCoinsText.text = String.Format("+{0}", awardCoins);
        DeltaCoinsText.gameObject.SetActive(true);
        StartCoroutine(UpdateCoinsAmount());
    }

    // Hide coins delta text after animation
    private IEnumerator HideCoinsDelta()
    {
        yield return new WaitForSeconds(1f);
        DeltaCoinsText.gameObject.SetActive(false);
    }

    // Animation for smooth increasing and decreasing the number of coins
    private IEnumerator UpdateCoinsAmount()
    {
        int PlayerCoins = CoreData.instance.GetPlayerCoin();
        const float seconds = 0.5f; // Animation duration
        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            CurrentCoinsText.text = Mathf.Floor(Mathf.Lerp(_previousCoinsAmount, PlayerCoins, (elapsedTime / seconds))).ToString();
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        _previousCoinsAmount = PlayerCoins;

        //CurrentCoinsText.text = PlayerCoins.ToString();
        try
        {
            MapScene.instance.UpdateCoinAmountLabel();
        }
        catch 
        {

           
        }
    }

    // Change remaining time to next free turn every 1 second
    private void UpdateFreeTurnTimer()
    {
        // Don't count the time if we have free turn already
        if (_isFreeTurnAvailable)
            return;

        // Update the remaining time values
        _timerRemainingHours = (int)(_nextFreeTurnTime - DateTime.Now).Hours;
        _timerRemainingMinutes = (int)(_nextFreeTurnTime - DateTime.Now).Minutes;
        _timerRemainingSeconds = (int)(_nextFreeTurnTime - DateTime.Now).Seconds;

        // If the timer has ended
        if (_timerRemainingHours <= 0 && _timerRemainingMinutes <= 0 && _timerRemainingSeconds <= 0)
        {
            Language.StartGlobalTranslateWord(NextFreeTurnTimerText, "Ready", 0, "");

            //NextFreeTurnTimerText.text = "Ready!";
            // Now we have a free turn
            _isFreeTurnAvailable = true;
        }
        else
        {
            // Show the remaining time
            NextFreeTurnTimerText.text = String.Format("{0:00}:{1:00}:{2:00}", _timerRemainingHours, _timerRemainingMinutes, _timerRemainingSeconds);
            // We don't have a free turn yet
            _isFreeTurnAvailable = false;
        }
    }

    private void EnableButton(Button button)
    {
        if (state == GAME_STATE.OPENING_POPUP || state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            button.interactable = true;
        }
        //button.GetComponent<Image> ().color = new Color(255, 255, 255, 1f);
    }

    private void DisableButton(Button button)
    {
        if (state == GAME_STATE.OPENING_POPUP || state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            button.interactable = false;
        }
        //button.GetComponent<Image> ().color = new Color(255, 255, 255, 0.5f);
    }

    // Function for more readable calls
    private void EnableFreeTurnButton() { EnableButton(FreeTurnButton); }
    private void DisableFreeTurnButton() { DisableButton(FreeTurnButton); }
    private void EnablePaidTurnButton() { EnableButton(PaidTurnButton); }
    private void DisablePaidTurnButton() { DisableButton(PaidTurnButton); }

    private void ShowFreeTurnButton()
    {
        if (state == GAME_STATE.OPENING_POPUP || state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            FreeTurnButton.gameObject.SetActive(true);
            PaidTurnButton.gameObject.SetActive(true);
        }
    }

    private void ShowPaidTurnButton()
    {
        if (state == GAME_STATE.OPENING_POPUP || state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            PaidTurnButton.gameObject.SetActive(true);
            FreeTurnButton.gameObject.SetActive(false);
        }
    }

    public void ResetTimer()
    {
        PlayerPrefs.DeleteKey(LAST_FREE_TURN_TIME_NAME);
    }
}

/**
 * One sector on the wheel
 */
[Serializable]
public class FortuneWheelSector : System.Object
{
    [Tooltip("Text object where value will be placed (not required)")]
    public GameObject ValueTextObject;

    [Tooltip("Value of reward")]
    public int RewardValue = 100;

    [Tooltip("Chance that this sector will be randomly selected")]
    [RangeAttribute(0, 100)]
    public int Probability = 100;

    [Tooltip("Method that will be invoked if this sector will be randomly selected")]
    public UnityEvent RewardCallback;
}

/**
 * Draw custom button in inspector
 */
#if UNITY_EDITOR
[CustomEditor(typeof(FortuneWheelManager))]
public class FortuneWheelManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FortuneWheelManager myScript = (FortuneWheelManager)target;
        if (GUILayout.Button("Reset Timer"))
        {
            myScript.ResetTimer();
        }
    }
}
#endif