using System;
using System.Collections.Generic;
using UnityEngine;




public class moregames : MonoBehaviour
{
    // public PopupOpener biberPopup;
    public static moregames instance;


    public void feedbackCrazy()
    {
        string content = "Please send us your advice: ";

        if (Configuration.instance.Amazon)
        {
            Application.OpenURL("mailto:info@bibergames.com?subject=ToyBoxCrazyBlastAmazon Feedback&body=" + content);
        }
        else
        {
            Application.OpenURL("mailto:info@bibergames.com?subject=ToyBoxCrazyBlast Feedback&body=" + content);
        }
    }
    public void feedbackTBB()
    {
        string content = "Please send us your advice: ";

#if UNITY_IOS
        Application.OpenURL("mailto:info@bibergames.com?subject=ToyBoxPartyTimeIos Feedback&body=" + content);
#elif UNITY_ANDROID
          if (Configuration.instance.Amazon)
        {
            Application.OpenURL("mailto:info@bibergames.com?subject=ToyBoxPartyTimeAmazon Feedback&body=" + content);
        }
        else
        {
            Application.OpenURL("mailto:info@bibergames.com?subject=ToyBoxPartyTime Feedback&body=" + content);
        }
        
#endif

    }
    public void drawgame()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/developer/aydin-karabudak/id1052609585";
#elif UNITY_ANDROID
        if(Configuration.instance.Amazon)
        {
            appstoreUrl = "https://www.amazon.com/biber-games-Brain-Dots-Draw/dp/B06Y11F71P";
        }
        else
        {
            appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.drawdotgame";
        }
        
#endif

        Application.OpenURL(appstoreUrl);
    }


    public void HOB()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://apps.apple.com/us/app/heroes-of-blast/id1581257524";
#elif UNITY_ANDROID
        if (Configuration.instance.Amazon)
        {
            appstoreUrl = "https://www.amazon.com/dp/B09GJ7XYDT";
        }
        else if (Configuration.instance.Udp)
        {
            appstoreUrl = "https://appgallery.huawei.com/app/C104686269";
        }
        else
        {
            appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.heroesofblast";
        }

#endif

        Application.OpenURL(appstoreUrl);
    }
    public void nab()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/developer/aydin-karabudak/id1052609585";
#elif UNITY_ANDROID
        appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.ttban";
#endif

        Application.OpenURL(appstoreUrl);
    }

    public void wow()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/app/wheel-online/id1067576582?mt=8";
#elif UNITY_ANDROID
        appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.carkifelek.en";
#endif

        Application.OpenURL(appstoreUrl);

    }
    public void crazyrush()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/developer/aydin-karabudak/id1052609585";
#elif UNITY_ANDROID
        appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.crazyrush";
#endif

        Application.OpenURL(appstoreUrl);

    }
    public void TBMB()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/developer/aydin-karabudak/id1052609585";
#elif UNITY_ANDROID
        if (Configuration.instance.Amazon)
        {
            appstoreUrl = "https://www.amazon.com/dp/B07CTN94S3";
        }
        else
        {
            appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxblastmagic";
        }
#endif

        Application.OpenURL(appstoreUrl);
        //if (PlayerPrefs.GetInt("TBMB Odul Verildi", 0) == 0)
        //{
        //    PlayerPrefs.SetInt("TBMB Odul Verildi", 1);
        //    PlayerPrefs.Save();
        //    CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 15);
        //    AudioManager.instance.CoinAddAudio();
        //    FirebaseAnalytics.SetCurrentScreen("Magic Blast Odul verildi", "MOREGAMES");
        //    try { GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel(); } catch { }
        //}
    }
    public void fcm()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/app/fruit-candy-monsters-juice/id1200514406?l=tr&ls=1&mt=8";
#elif UNITY_ANDROID
        if (Configuration.instance.Amazon)
        {
            appstoreUrl = "https://www.amazon.com/biber-games-Fruit-Candy-Monsters/dp/B06W9M713R";
        }
        else
        {
            appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.fruitcandymonsters";
        }
        
#endif


        Application.OpenURL(appstoreUrl);
        //if (PlayerPrefs.GetInt("FCM Odul Verildi", 0) == 0)
        //{
        //    PlayerPrefs.SetInt("FCM Odul Verildi", 1);
        //    PlayerPrefs.Save();
        //    CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 15);
        //    AudioManager.instance.CoinAddAudio();
        //    FirebaseAnalytics.SetCurrentScreen("FCM odul Verildi", "MOREGAMES");
        //    try { GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel(); } catch { }
        //}
    }

    public void bibergames()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/developer/aydin-karabudak/id1052609585";
#elif UNITY_ANDROID
        if (Configuration.instance.Amazon)
        {
            appstoreUrl = "https://www.amazon.com/s/ref=bl_sr_mobile-apps?_encoding=UTF8&field-brandtextbin=biber%20games&node=2350149011";
        }
        else if (Configuration.instance.Udp)
        {
            appstoreUrl = "https://appgallery.huawei.com/app/C104686269";
        }
        else
        {
            appstoreUrl = "https://play.google.com/store/apps/developer?id=Biber+Games+LLC";
        }
       
#endif


        Application.OpenURL(appstoreUrl);

    }

    public void rateus()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/app/toy-blast-party-time/id1208107706?mt=8";
#elif UNITY_ANDROID
        if (Configuration.instance.Amazon)
        {
            appstoreUrl = "https://www.amazon.com/Toy-Box-Party-Blast-Time/dp/B06WRVCHPV";
        }
        else
        {
            appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxblast";
        }
#endif

        Application.OpenURL(appstoreUrl);
        PlayerPrefs.SetInt(Configuration.instance.RateKey, 1);
        PlayerPrefs.Save();
    }
    public void rateustbmb()
    {
        string appstoreUrl = "";

        Application.OpenURL(appstoreUrl);

        PlayerPrefs.SetInt(Configuration.instance.RateKey, 1);
        PlayerPrefs.Save();
    }
    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }

    public void farmgame()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/developer/aydin-karabudak/id1052609585";
#elif UNITY_ANDROID
        appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.farmpartytime";
#endif

        Application.OpenURL(appstoreUrl);

    }
    public void cubegame()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/developer/aydin-karabudak/id1052609585";
#elif UNITY_ANDROID
        appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.cubeblock";
#endif

        Application.OpenURL(appstoreUrl);

    }

    public void crazyblast()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/developer/aydin-karabudak/id1052609585";
#elif UNITY_ANDROID
        if (Configuration.instance.Amazon)
        {
            appstoreUrl = "https://www.amazon.com/biber-games-Crazy-Blast/dp/B0745LCF55";
        }
        else
        {
            appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxcrazycubes";
        }
        
#endif

        Application.OpenURL(appstoreUrl);

        //if (PlayerPrefs.GetInt("CRAZY Odul Verildi", 0) == 0)
        //{
        //    PlayerPrefs.SetInt("CRAZY Odul Verildi", 1);
        //    PlayerPrefs.Save();
        //    CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 15);
        //    AudioManager.instance.CoinAddAudio();
        //    FirebaseAnalytics.SetCurrentScreen("Crazy Blast Odul verildi", "MOREGAMES");
        //    try { GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel(); } catch { }
        //}
    }
    public void rateuscrazyblast()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/developer/aydin-karabudak/id1052609585";
#elif UNITY_ANDROID
        if (Configuration.instance.Amazon)
        {
            appstoreUrl = "https://www.amazon.com/biber-games-Crazy-Blast/dp/B0745LCF55";
        }
        else
        {
            appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxcrazycubes";
        }
#endif

        Application.OpenURL(appstoreUrl);
        PlayerPrefs.SetInt(Configuration.instance.RateKey, 1);

    }
    public void TBB()
    {
        string appstoreUrl = "";

#if UNITY_IOS
        appstoreUrl = "https://itunes.apple.com/us/app/toy-blast-party-time/id1208107706?mt=8";
#elif UNITY_ANDROID
        if (Configuration.instance.Amazon)
        {
            appstoreUrl = "https://www.amazon.com/Toy-Box-Party-Blast-Time/dp/B06WRVCHPV";
        }
        else
        {
            appstoreUrl = "https://play.google.com/store/apps/details?id=com.bibergames.toyboxblast";
        }
        
#endif

        Application.OpenURL(appstoreUrl);

        //if (PlayerPrefs.GetInt("TBB Odul Verildi", 0) == 0)
        //{
        //    PlayerPrefs.SetInt("TBB Odul Verildi", 1);
        //    PlayerPrefs.Save();
        //    CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += 15);
        //    AudioManager.instance.CoinAddAudio();
        //    FirebaseAnalytics.SetCurrentScreen("TBB odul Verildi", "MOREGAMES");
        //    try { GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel(); } catch { }
        //}
    }
}
