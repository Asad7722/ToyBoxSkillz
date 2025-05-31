using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class newepisodepopup : MonoBehaviour {
    public static newepisodepopup instance = null;   
    public Text EpisodeLabelText;
    public Text EpisodeNumberText;


    // Use this for initialization

    void Start()
    {

        int episodenumber = PlayerPrefs.GetInt("EpisodeNumber");
        string EpisodesLabel = Configuration.instance.EpisodesLabel[episodenumber];
        EpisodeLabelText.text = "" + EpisodesLabel;
        EpisodeNumberText.text = "Episode " + episodenumber;

    }
  

    public void close()
    {
        gameObject.SetActive(false);
        AudioManager.instance.ButtonClickAudio();
    }
}
