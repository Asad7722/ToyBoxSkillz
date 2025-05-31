using System;
using System.Collections.Generic;
using UnityEngine;

public class ToyBoxGift : MonoBehaviour

{
    public PopupOpener ToyBoxgiftPopup;
    public GameObject gift1;
    public GameObject gift2;
    public GameObject gift3;
    public GameObject gift4;
    public GameObject gift5;
    public GameObject gift6;
    public bool gift1alindi;
    public bool gift2alindi;
    public bool gift3alindi;
    public bool gift4alindi;
    public bool gift5alindi;
    public bool gift6alindi;



    bool IsGiftsCompleted()
    {
        if (gift1alindi == false && gift2alindi == false && gift3alindi == false && gift4alindi == false && gift5alindi == false && gift6alindi == false)
        {
            return true;
        }

        return false;
    }

    public void levelgifts()
    {
     /*   if (!GameObject.Find("Levelgift(Clone)"))
        {
            levelgiftPopup.OpenPopup();
        }*/
    }
    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }
    public void ButtonPayAudio()
    {
        AudioManager.instance.giftsound();
    }
    public void g1()
    {
        Debug.Log("gift1 alindi");
        CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 15);
        //  GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();

        gift1.SetActive(false);
        gift1alindi = true;
        //   CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
        UpdatelevelgiftPopup();

    }
    public void g2()
    {
        Debug.Log("gift2 alindi");
        CoreData.instance.SaveColumnBreaker(CoreData.instance.columnBreaker += 1);

        gift2.SetActive(false);
        //    CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
        gift2alindi = true;
        UpdatelevelgiftPopup();


    }
    public void g3()
    {

        Debug.Log("gift3 alindi");
        CoreData.instance.SaveBeginRainbow(CoreData.instance.beginRainbow += 1);

        gift3.SetActive(false);
        //  CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
        gift3alindi = true;
        UpdatelevelgiftPopup();
    }
    public void g4()
    {

        Debug.Log("gift4 alindi");
        CoreData.instance.SaveSingleBreaker(CoreData.instance.singleBreaker += 1);
        gift4.SetActive(false);
        //  CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
        gift4alindi = true;
        UpdatelevelgiftPopup();
    }
    public void g5()
    {

        Debug.Log("gift5 alindi");
        CoreData.instance.SaveRainbowBreaker(CoreData.instance.rainbowBreaker += 1);

        gift5.SetActive(false);
        //  CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
        gift5alindi = true;
        UpdatelevelgiftPopup();
    }
    public void g6()
    {

        Debug.Log("gift6 alindi");
        CoreData.instance.SaveOvenBreaker(CoreData.instance.ovenBreaker += 1);
        gift6.SetActive(false);
        // CoreData.instance.SaveGiftAmount(CoreData.instance.giftAmount += 1);
        gift6alindi = true;
        UpdatelevelgiftPopup();

    }



    public void UpdatelevelgiftPopup()
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        PlayerPrefs.SetInt("levelgift alindi" + openedLevel, 1);
        if (gift1alindi && gift2alindi && gift3alindi && gift4alindi && gift5alindi && gift6alindi)
        {
            gameObject.SetActive(false);
            AudioManager.instance.CoinAddAudio();
        }
    }

}
