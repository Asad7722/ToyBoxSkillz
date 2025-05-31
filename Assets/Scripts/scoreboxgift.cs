using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreboxgift : MonoBehaviour {
    public PopupOpener scoregiftPopup;

    // Use this for initialization

    public void scoregifts()
    {
        //if (!GameObject.Find("Puangift(Clone)"))
        //{
        //    scoregiftPopup.OpenPopup();
        //    Configuration.instance.GiftTiklanmadi = 0;
        //}
        Configuration.instance.OpenGiftBoxPopup("Score Box", REWARD_TYPE.ScoreGiftBox);
    }
    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }
}
