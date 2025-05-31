using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    public AudioClip[] Music;
    public GAME_STATE state;
    public static BackgroundMusic instance = null;
    public bool changemusic=false;
    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        //Check whether the music is enable or not.
        if ((PlayerPrefs.GetInt("isMusicEnabled", 0) == 0))
        {
            MapMusic();
        }
    }


    /// <summary>
    /// Registers the event for music status change.
    /// </summary>
    /// 
    
    public void GamePlayMusic()
    {
        if ((PlayerPrefs.GetInt("isMusicEnabled", 0) == 0))
        {
            try
            {
                int playnumber = AudioManager.sound_5 ;//Random.Range(5, 9);
                GetComponent<AudioSource>().clip = Music[playnumber];
                GetComponent<AudioSource>().volume = 0.7f;
                GetComponent<AudioSource>().Play();

            }
            catch { }
        }
    }
    public void BeginMapMusic()
    {
        if ((PlayerPrefs.GetInt("isMusicEnabled", 0) == 0))
        {
            try
            {
                int playnumber = AudioManager.sound_3;//Random.Range(0, 5);
                GetComponent<AudioSource>().clip = Music[playnumber];
                GetComponent<AudioSource>().volume = 1.0f;
                GetComponent<AudioSource>().Play();
                changemusic = false;
            }
            catch { }


        }
    }
    public void MapMusic()
    {
        if ((PlayerPrefs.GetInt("isMusicEnabled", 0) == 0))
        {
              try
                {
                    int playnumber = AudioManager.sound_3; //Random.Range(0, 5);
                    GetComponent<AudioSource>().clip = Music[playnumber];
                    GetComponent<AudioSource>().volume = 1.0f;
                    GetComponent<AudioSource>().Play();
                }
                catch { }
           
        }
    }
    public void WinMusic()
    {
        if ((PlayerPrefs.GetInt("isMusicEnabled", 0) == 0))
        {
            try
            {
                //int playnumber = Random.Range(0, 5);
                GetComponent<AudioSource>().clip = Music[9];
                GetComponent<AudioSource>().volume = 1.0f;
                GetComponent<AudioSource>().Play();
            }
            catch { }

        }
    }
    IEnumerator MapmusicStart()
    {
        changemusic = true;
        yield return new WaitForSeconds(2.0f);
        int playnumber = AudioManager.sound_3; //Random.Range(0, 5);
        GetComponent<AudioSource>().clip = Music[playnumber];
        GetComponent<AudioSource>().Play();
        changemusic = false;
    }
    IEnumerator GamePlayMusicStart()
    {
        changemusic = true;
        yield return new WaitForSeconds(2.0f);      
        int playnumber = AudioManager.sound_5; //Random.Range(5, 9);
        GetComponent<AudioSource>().clip = Music[playnumber];
        GetComponent<AudioSource>().Play();
        changemusic = false;
    }
    public void StopMusic()
    {
        changemusic = true;
        GetComponent<AudioSource>().Stop();
    }
    void OnEnable()
    {
        AudioManager.OnMusicStatusChangedEvent += AudioManager_OnMusicStatusChangedEvent;
    }

    /// <summary>
    /// Unregisters the event for music status change.
    /// </summary>
    void OnDisable()
    {
        AudioManager.OnMusicStatusChangedEvent -= AudioManager_OnMusicStatusChangedEvent;
    }

    /// <summary>
    /// Update the background music status based on changes state.
    /// </summary>
    /// <param name="status">If set to <c>true</c> status.</param>
    void AudioManager_OnMusicStatusChangedEvent(bool status)
    {
        if (status)
        {
            if(Configuration.instance.CurrentScene == CURRENT_SCENE.PLAY)
            {
                GamePlayMusic();
            }
            else if (Configuration.instance.CurrentScene == CURRENT_SCENE.MAP)
            {
                MapMusic();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
