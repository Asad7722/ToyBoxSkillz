using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starboxgift : MonoBehaviour {

    public PopupOpener stargiftPopup;

    // Use this for initialization
 
    public void stargifts()
    {
        //if (!GameObject.Find("Stargift(Clone)"))
        //{
        //    stargiftPopup.OpenPopup();
        //    Configuration.instance.GiftTiklanmadi = 0;
        //}


        Configuration.instance.OpenGiftBoxPopup("Star Box", REWARD_TYPE.StarGiftBox);

    }
    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }
}
