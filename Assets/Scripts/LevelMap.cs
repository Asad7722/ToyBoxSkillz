using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMap : MonoBehaviour
{
    public static LevelMap instance = null;
    public GameObject scrollContent;
    public float canvasHeight;
    public float pointCurrentEpisode;
    //public GameObject[] Unlocked;
    //public GameObject[] Locked;  
    //public Text EpisodeLabelText;
    public string[] EpisodesLabel;
    //public Text[] TitleText;
    //public Text[] EpisodeText;
    //public Button[] BG;
    //public Text[] EpisodeMBLabelText;
    //public GameObject[] Lock;   

    // Use this for initialization
    void Start()
    {
        canvasHeight = ((float)Screen.height / (float)Screen.width) * 720f;
        Invoke("FoundTargetButtonClick2",0.5f);
    }
    public void FoundTargetButtonClick2()
    {
        Debug.Log("Configuration.instance.CurruentEpisodeObje.GetComponent<RectTransform>().localPosition.y = " + Configuration.instance.CurruentEpisodeObje.GetComponent<RectTransform>().localPosition.y);
        float target = -1 * Configuration.instance.CurruentEpisodeObje.GetComponent<RectTransform>().localPosition.y;
        scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, target- 1548f, 0);
       
    }
    Vector3 TargetPosition()
    {      

        var currentPosition = Vector3.zero;
        var target = GameObject.Find("TargetEpisode") as GameObject;

        currentPosition = target.GetComponent<RectTransform>().localPosition;

        return currentPosition;
    }

    public void close()
    {
        gameObject.SetActive(false);
        AudioManager.instance.ButtonClickAudio();
    }

}
