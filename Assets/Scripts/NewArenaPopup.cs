using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewArenaPopup : MonoBehaviour {

    public static newepisodepopup instance = null;  
    public Text ArenaNumberText;


    // Use this for initialization

    void Start()
    {
        int arenanumber = PlayerPrefs.GetInt("arenanumber");



        if (arenanumber < 11)
        {
            ArenaNumberText.text = "Arena " + arenanumber;
        }
        else
        {
            ArenaNumberText.text = "Arena Legendary";
        }
    }



    public void close()
    {
        gameObject.SetActive(false);
        AudioManager.instance.ButtonClickAudio();
    }
}
