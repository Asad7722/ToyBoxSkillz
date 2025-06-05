using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static event Action<bool> OnSoundStatusChangedEvent;
    public static event Action<bool> OnMusicStatusChangedEvent;
    public static event Action<bool> OnNotifStatusChangedEvent;

    public bool isSoundEnabled = true;
    public bool isMusicEnabled = true;
    public bool isNotifEnabled = true;

    public AudioSource audioSource;
    public AudioClip SFX_ButtonClick;
    public AudioClip SFX_BlockPlace;
    public AudioClip SFX_GameOver;



    [Header("Play")]
    public AudioClip tap;
    public AudioClip tapBck;
    public AudioClip[] itemCrush;
    public AudioClip[] drop;
    public AudioClip[] collectTarget;
    public AudioClip bomb;
    public AudioClip bombExplode;
    public AudioClip medbombExplode;
    public AudioClip bigbombExplode;
    public AudioClip colRowBreaker;
    public AudioClip colRowBreakerExplode;
    public AudioClip rainbow;
    public AudioClip rainbowExplode;
    public AudioClip UIPopupLevelSkipped;
    public AudioClip[] gingerbread;
    public AudioClip gingerbreadExplode;
    public AudioClip waffleExplode;
    public AudioClip cageExplode;
    public AudioClip marshmallowExplode;
    public AudioClip collectibleExplode;
    public AudioClip chocolateExplode;
    public AudioClip rockCandyExplode;
    public AudioClip coinPay;
    public AudioClip coinAdd;
    public AudioClip amazing;
    public AudioClip exellent;
    public AudioClip fab;
    public AudioClip brilliant;
    public AudioClip awesome;
    public AudioClip great;
    public AudioClip star1;
    public AudioClip star2;
    public AudioClip star3;
    public AudioClip[] girlWowSound;
    public AudioClip[] girlTickSound;

    // UI
    [Header("UI")]
    public AudioClip Click;
    public AudioClip Target;
    public AudioClip completed;
    public AudioClip Win;
    public AudioClip Lose;
    public AudioClip NoMatch;
    public AudioClip Gift;
    public AudioClip giftgiris;
    public AudioClip giftvar;
    public AudioClip[] completedMusic;
    public AudioClip bostikirti;
    public AudioClip dolutikirti;
    public AudioClip swosh;
    public AudioClip bonuscoins;

    [Header("Booster")]
    public AudioClip singleBooster;
    public AudioClip rowBooster;
    public AudioClip columnBooster;
    public AudioClip rainbowBooster;
    public AudioClip ovenBooster;

    [Header("Check")]
    public bool playingCookieCrush;
    public bool playingBomb;
    public bool playingBombExplode;
    public bool playingColRowBreaker;
    public bool playingColRowBreakerExplode;
    public bool playingRainbow;
    public bool playingRainbowExplode;
    public bool playingDrop;
    public bool playingWaffleExplode;
    public bool playingCageExplode;
    public bool playingMarshmallowExplode;
    public bool playingChocolateExplode;
    public bool playingRockCandyExplode;

    float delay = 0.01f;


    [Header("Appreciations")]
    public AudioClip[] Fantastic;
    public AudioClip[] Sweet;


    private static AudioManager _instance;

    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        if (_instance != null)
        {
            if (_instance.gameObject != gameObject)
            {
                Destroy(gameObject);
                return;
            }
        }
        DontDestroyOnLoad(gameObject);
        _instance = GameObject.FindObjectOfType<AudioManager>();
    }

    /// <summary>
    /// Raises the enable event.
    /// </summary>
    /// 
    public static int sound_1, sound_2, sound_3, sound_4, sound_5;
    private void Start()
    {
        sound_1 = UnityEngine.Random.Range(0, 10);
        sound_2 = UnityEngine.Random.Range(0, 4);
        sound_3 = UnityEngine.Random.Range(0, 5);
        sound_4 = UnityEngine.Random.Range(0, 8);
        sound_5 = UnityEngine.Random.Range(5, 9);
    }
    void OnEnable()
    {
        initAudioStatus();
    }

    /// <summary>
    /// Inits the audio status.
    /// </summary>
    public void initAudioStatus()
    {
        isSoundEnabled = (PlayerPrefs.GetInt("isSoundEnabled", 0) == 0) ? true : false;
        isMusicEnabled = (PlayerPrefs.GetInt("isMusicEnabled", 0) == 0) ? true : false;
        isNotifEnabled = (PlayerPrefs.GetInt("isNotifEnabled", 0) == 0) ? true : false;

        if ((!isSoundEnabled) && (OnSoundStatusChangedEvent != null))
        {
            OnSoundStatusChangedEvent.Invoke(isSoundEnabled);
        }
        if ((!isMusicEnabled) && (OnMusicStatusChangedEvent != null))
        {
            OnMusicStatusChangedEvent.Invoke(isMusicEnabled);
        }
        if ((!isNotifEnabled) && (OnNotifStatusChangedEvent != null))
        {
            OnNotifStatusChangedEvent.Invoke(isNotifEnabled);
        }

    }

    /// <summary>
    /// Toggles the sound status.
    /// </summary>
    /// <param name="state">If set to <c>true</c> state.</param>
    public void ToggleSoundStatus(bool state)
    {
        isSoundEnabled = state;
        PlayerPrefs.SetInt("isSoundEnabled", (isSoundEnabled) ? 0 : 1);


        if (OnSoundStatusChangedEvent != null)
        {

            OnSoundStatusChangedEvent.Invoke(isSoundEnabled);

        }
    }

    /// <summary>
    /// Toggles the music status.
    /// </summary>
    /// <param name="state">If set to <c>true</c> state.</param>
    public void ToggleMusicStatus(bool state)
    {
        isMusicEnabled = state;
        PlayerPrefs.SetInt("isMusicEnabled", (isMusicEnabled) ? 0 : 1);


        if (OnMusicStatusChangedEvent != null)
        {

            OnMusicStatusChangedEvent.Invoke(isMusicEnabled);
        }
    }

    public void ToggleNotifStatus(bool state)
    {
        isNotifEnabled = state;
        PlayerPrefs.SetInt("isNotifEnabled", (isNotifEnabled) ? 1 : 0);


        if (OnNotifStatusChangedEvent != null)
        {

            OnNotifStatusChangedEvent.Invoke(isNotifEnabled);
        }
        if (PlayerPrefs.GetInt("isNotifEnabled") == 0)
        {
            //Notifications.CancelAllPendingLocalNotifications();
            //Notifications.ClearAllDeliveredNotifications();
            Debug.Log("isNotifEnables= " + PlayerPrefs.GetInt("isNotifEnabled"));
            Debug.Log("Notifications.CancelAllPendingLocalNotifications()");
        }
        else
        {
            Debug.Log("isNotifEnables= " + PlayerPrefs.GetInt("isNotifEnabled"));
            //if (Configuration.instance.Bildirim)
            //{                
            //    bool isInit = Notifications.IsInitialized();
            //    if (!isInit)
            //    {
            //        Notifications.Init();
            //        Debug.Log("Notification module is already initalized.");
            //    }
            //}

        }
    }

    /// <summary>
    /// Plaies the button click sound.
    /// </summary>
    public void PlayButtonClickSound()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(SFX_ButtonClick);
        }
    }
    public void ButtonClickAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(Click);
        }

    }
    /// <summary>
    /// Plaies the one shot clip.
    /// </summary>
    /// <param name="clip">Clip.</param>
    public void PlayOneShotClip(AudioClip clip)
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(clip);
        }
    }


    #region Play

    public void SwapBackAudio()

    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(tapBck);
        }
    }

    public void SwapAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(tap);
        }
    }

    public void CookieCrushAudio()
    {
        if (playingCookieCrush == false)
        {
            if (isSoundEnabled)
            {
                playingCookieCrush = true;
                //skillz
                //int sound = UnityEngine.Random.Range(0, 10);
                if (sound_1 < 3)
                {
                    audioSource.PlayOneShot(itemCrush[sound_1]);
                    if (Configuration.instance.playing)
                        AudioManager.instance.GirlTickSound(sound_1);
                }
                else
                {
                    //  Debug.Log("esgectim");                  
                    audioSource.PlayOneShot(itemCrush[sound_1]);
                }

                StartCoroutine(ResetCookieCrushAudio());
            }
        }
    }



    public void FantasticSound(int i)
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(Fantastic[i]);
        }
    }
    public void GoodSound(int i)
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(Sweet[i]);
        }
    }


    IEnumerator ResetCookieCrushAudio()
    {
        if (isSoundEnabled)
        {
            yield return new WaitForSeconds(delay);

            playingCookieCrush = false;
        }
    }

    public void BombBreakerAudio()
    {

        if (playingBomb == false)
        {
            if (isSoundEnabled)
            {
                playingBomb = true;

                audioSource.PlayOneShot(bomb);

                StartCoroutine(ResetBombAudio());
            }
        }
    }
    public void BigBombBreakerAudio()
    {
        try { CameraSize.instance.BigShake = true; }
        catch { }
        if (playingBomb == false)
        {
            if (isSoundEnabled)
            {
                playingBomb = true;

                audioSource.PlayOneShot(bigbombExplode);

                StartCoroutine(ResetBombAudio());
            }
        }
    }
    public void MedBombBreakerAudio()
    {
        try { CameraSize.instance.MedShake = true; }
        catch { }
        if (playingBomb == false)
        {

            if (isSoundEnabled)
            {
                playingBomb = true;

                audioSource.PlayOneShot(medbombExplode);

                StartCoroutine(ResetBombAudio());
            }
        }
    }

    IEnumerator ResetBombAudio()
    {
        yield return new WaitForSeconds(delay);

        playingBomb = false;
    }

    public void BombExplodeAudio()
    {
        try { CameraSize.instance.Shake = true; }
        catch { }
        if (playingBombExplode == false)
        {
            if (isSoundEnabled)
            {
                playingBombExplode = true;

                audioSource.PlayOneShot(bombExplode);

                StartCoroutine(ResetBombExplodeAudio());
            }
        }
    }

    IEnumerator ResetBombExplodeAudio()
    {
        yield return new WaitForSeconds(delay);

        playingBombExplode = false;
    }

    public void ColRowBreakerAudio()

    {

        if (playingColRowBreaker == false)
        {
            if (isSoundEnabled)
            {
                playingColRowBreaker = true;

                audioSource.PlayOneShot(colRowBreaker);

                StartCoroutine(ResetColRowBreakerAudio());
            }
        }
    }

    IEnumerator ResetColRowBreakerAudio()
    {
        yield return new WaitForSeconds(delay);

        playingColRowBreaker = false;
    }

    public void ColRowBreakerExplodeAudio()
    {
        try { CameraSize.instance.MinShake = true; }
        catch { }
        if (playingColRowBreakerExplode == false)
        {
            if (isSoundEnabled)
            {
                playingColRowBreakerExplode = true;

                audioSource.PlayOneShot(colRowBreakerExplode);
                StartCoroutine(ResetColRowBreakerExplodeAudio());
            }
        }
    }

    IEnumerator ResetColRowBreakerExplodeAudio()
    {
        yield return new WaitForSeconds(delay);

        playingColRowBreakerExplode = false;
    }

    public void RainbowAudio()
    {
        if (playingRainbow == false)
        {
            if (isSoundEnabled)
            {
                playingRainbow = true;

                audioSource.PlayOneShot(rainbow);

                StartCoroutine(ResetRainbowAudio());
            }
        }
    }

    IEnumerator ResetRainbowAudio()
    {
        yield return new WaitForSeconds(delay);

        playingRainbow = false;
    }

    public void RainbowExplodeAudio()
    {
        if (playingRainbowExplode == false)
        {
            if (isSoundEnabled)
            {
                playingRainbowExplode = true;

                audioSource.PlayOneShot(rainbowExplode);

                StartCoroutine(ResetRainbowExplodeAudio());
            }
        }
    }

    IEnumerator ResetRainbowExplodeAudio()
    {
        yield return new WaitForSeconds(delay);

        playingRainbowExplode = false;
    }

    public void DropAudio()
    {
        if (playingDrop == false)
        {
            if (isSoundEnabled)
            {
                playingDrop = true;
                //skillz
                //int sound = UnityEngine.Random.Range(0, 4);
                audioSource.PlayOneShot(drop[sound_2]);

                StartCoroutine(ResetDropAudio());
            }
        }
    }

    IEnumerator ResetDropAudio()
    {
        yield return new WaitForSeconds(delay);

        playingDrop = false;
    }

    public void WaffleExplodeAudio()
    {
        if (playingWaffleExplode == false)
        {
            if (isSoundEnabled)
            {
                playingWaffleExplode = true;

                audioSource.PlayOneShot(waffleExplode);

                StartCoroutine(ResetWaffleExplodeAudio());
            }
        }
    }

    IEnumerator ResetWaffleExplodeAudio()
    {
        yield return new WaitForSeconds(delay);

        playingWaffleExplode = false;
    }

    public void CollectTargetAudio()
    {
        if (isSoundEnabled)
        {
            //int sound = UnityEngine.Random.Range(0, 5);
            // audioSource.PlayOneShot(itemCrush[sound]);
            audioSource.PlayOneShot(collectTarget[sound_3]);
        }

    }

    public void CageExplodeAudio()
    {
        if (playingCageExplode == false)
        {
            if (isSoundEnabled)
            {
                playingCageExplode = true;

                audioSource.PlayOneShot(cageExplode);

                StartCoroutine(ResetCageExplodeAudio());
            }
        }
    }

    IEnumerator ResetCageExplodeAudio()
    {
        yield return new WaitForSeconds(delay);

        playingCageExplode = false;
    }

    public void GingerbreadExplodeAudio()
    {
        if (isSoundEnabled)
        {
            //skillz
            //int sound = UnityEngine.Random.Range(0, 8);
            audioSource.PlayOneShot(gingerbread[sound_4]);
        }
    }
    //sameerfishy
    public void GirlWowSound(int sound)
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(girlWowSound[sound_1]);
        }
    }
    public void GirlTickSound(int sound)
    {
        if (isSoundEnabled)
        {
            //audioSource.PlayOneShot(girlTickSound[sound]);
        }
    }

    public void GingerbreadAudio()
    {
        if (isSoundEnabled)
        {
            //skillz
            //int sound = UnityEngine.Random.Range(0, 8);
            audioSource.PlayOneShot(gingerbread[sound_4]);
        }
    }

    public void MarshmallowExplodeAudio()
    {
        if (playingMarshmallowExplode == false)
        {
            if (isSoundEnabled)
            {
                playingMarshmallowExplode = true;

                audioSource.PlayOneShot(marshmallowExplode);

                StartCoroutine(ResetMarshmallowExplodeAudio());
            }
        }
    }

    IEnumerator ResetMarshmallowExplodeAudio()
    {
        yield return new WaitForSeconds(delay);

        playingMarshmallowExplode = false;
    }

    public void ChocolateExplodeAudio()
    {
        if (playingChocolateExplode == false)
        {
            if (isSoundEnabled)
            {

                playingChocolateExplode = true;

                audioSource.PlayOneShot(chocolateExplode);

                StartCoroutine(ResetChocolateExplodeAudio());
            }
        }
    }

    IEnumerator ResetChocolateExplodeAudio()
    {
        yield return new WaitForSeconds(delay);

        playingChocolateExplode = false;
    }

    public void CollectibleExplodeAudio()
    {
        if (isSoundEnabled)
        {
            //skillz
            //int sound = UnityEngine.Random.Range(0, 8);
            audioSource.PlayOneShot(gingerbread[sound_4]);
        }
    }

    public void RockCandyExplodeAudio()
    {
        if (playingRockCandyExplode == false)
        {
            if (isSoundEnabled)
            {
                playingRockCandyExplode = true;

                audioSource.PlayOneShot(rockCandyExplode);

                StartCoroutine(ResetRockCandyExplodeAudio());
            }
        }
    }

    IEnumerator ResetRockCandyExplodeAudio()
    {
        yield return new WaitForSeconds(delay);

        playingRockCandyExplode = false;
    }

    #endregion

    #region UI


    public void PopupTargetAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(Target);
        }
    }

    public void PopupCompletedAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(completed);
        }
    }
    public void PopupCompletedMusicAudio()
    {
        if (isSoundEnabled)
        {
            int i = UnityEngine.Random.Range(0, 2);
            audioSource.PlayOneShot(completedMusic[i]);
        }
    }
    public void PopupBosTikirtiAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(bostikirti);
        }
    }
    public void PopupDolutikirtiAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(dolutikirti);
        }
    }
    public void PopupSwoshAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(swosh);
        }
    }
    public void PopupBonusCoinsAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(bonuscoins);
        }
    }
    public void PopupWinAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(Win);
        }
    }

    public void PopupLoseAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(Lose);
        }
    }

    public void CoinPayAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(coinPay);
        }
    }

    public void CoinAddAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(coinAdd);
        }
    }

    public void PopupLevelSkippedAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(UIPopupLevelSkipped);
        }
    }

    public void PopupNoMatchesAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(NoMatch);
        }
    }

    public void giftsound()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(Gift);
        }
    }
    public void giftbuton()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(giftvar);
        }
    }
    public void giftgirisAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(giftgiris);
        }
    }
    #endregion

    #region Booster

    public void SingleBoosterAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(singleBooster);
        }
    }

    public void RowBoosterAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(rowBooster);
        }
    }

    public void ColumnBoosterAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(columnBooster);
        }
    }

    public void RainbowBoosterAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(rainbowBooster);
        }
    }

    public void OvenBoosterAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(ovenBooster);
        }
    }

    // font
    public void amazingAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(amazing);
        }
    }

    public void exellentAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(exellent);
        }
    }

    public void greatAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(great);
        }
    }

    public void fabAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(fab);
        }
    }

    public void awesomeAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(awesome);
        }
    }

    public void brilliantAudio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(brilliant);
        }
    }

    // star
    public void Star1Audio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(star1);
        }
    }

    public void Star2Audio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(star2);
        }
    }

    public void Star3Audio()
    {
        if (isSoundEnabled)
        {
            audioSource.PlayOneShot(star3);
        }
    }

    #endregion
}
