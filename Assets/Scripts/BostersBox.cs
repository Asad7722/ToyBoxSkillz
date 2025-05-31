using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
public class BostersBox : MonoBehaviour
{

    public Text Booster1;
    public Text Booster2;
    public Text Booster3;
    public Text Booster4;
    public Text Booster5;
    public Text Booster6;
    public Text Booster7;
    public Text Booster8;
    public Text Booster1cost;
    public Text Booster2cost;
    public Text Booster3cost;
    public Text Booster4cost;
    public Text Booster5cost;
    public Text Booster6cost;
    public Text Booster7cost;
    public Text Booster8cost;
    public GameObject b1buy;
    public GameObject b2buy;
    public GameObject b3buy;
    public GameObject b4buy;
    public GameObject b5buy;
    public GameObject b6buy;
    public GameObject b7buy;
    public GameObject b8buy;
    public static BostersBox instance = null;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        updateBoostersLabels();    
    }

    public void updateBoostersLabels()
    {
        Booster1.text = CoreData.instance.GetRowBreaker().ToString();
        Booster2.text = CoreData.instance.GetColumnBreaker().ToString();
        Booster3.text = CoreData.instance.GetRainbowBreaker().ToString();
        Booster4.text = CoreData.instance.GetOvenBreaker().ToString();
        Booster5.text = CoreData.instance.GetSingleBreaker().ToString();
        Booster6.text = CoreData.instance.GetBeginBombBreaker().ToString();
        Booster7.text = CoreData.instance.GetBeginRainbow().ToString();
        Booster8.text = CoreData.instance.GetBeginFiveMoves().ToString();
        try
        {
            Booster1cost.text = (Configuration.instance.rowBreakerCost1 / 3).ToString();
            Booster2cost.text = (Configuration.instance.columnBreakerCost1 / 3).ToString();
            Booster3cost.text = (Configuration.instance.rainbowBreakerCost1 / 3).ToString();
            Booster4cost.text = (Configuration.instance.ovenBreakerCost1 / 3).ToString();
            Booster5cost.text = (Configuration.instance.singleBreakerCost1 / 3).ToString();
            Booster6cost.text = (Configuration.instance.beginBombBreakerCost1 / 3).ToString();
            Booster7cost.text = (Configuration.instance.beginRainbowCost1 / 3).ToString();
            Booster8cost.text = (Configuration.instance.beginFiveMovesCost1 / 3).ToString();
        }
        catch { }
        if ((Configuration.instance.singleBreakerCost1 / 3) <= CoreData.instance.GetPlayerCoin())
        {
            b1buy.SetActive(true);
            b2buy.SetActive(true);
            b3buy.SetActive(true);
            b4buy.SetActive(true);
            b5buy.SetActive(true);
            b6buy.SetActive(true);
            b8buy.SetActive(true);

        }
        else
        {
            b1buy.SetActive(false);
            b2buy.SetActive(false);
            b3buy.SetActive(false);
            b4buy.SetActive(false);
            b5buy.SetActive(false);
            b6buy.SetActive(false);
            b8buy.SetActive(false);
        }
        if ((Configuration.instance.beginRainbowCost1 / 3) <= CoreData.instance.GetPlayerCoin())
        {
            b7buy.SetActive(true);
        }
        else
        {
            b7buy.SetActive(false);
        }

    }
    public void b1()
    {
        if ((Configuration.instance.rowBreakerCost1 / 3) <= CoreData.instance.GetPlayerCoin())
        {
            CoreData.instance.SaveRowBreaker(CoreData.instance.rowBreaker += 1);
            CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= (Configuration.instance.rowBreakerCost1 / 3));
            Booster1.text = CoreData.instance.GetRowBreaker().ToString();
            AudioManager.instance.CoinPayAudio();
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
            updateBoostersLabels();
            AnalyticsEvent.Custom("BuyRowBreaker 1");
        }
    }
    public void b2()
    {
        if ((Configuration.instance.columnBreakerCost1 / 3) <= CoreData.instance.GetPlayerCoin())
        {
            CoreData.instance.SaveColumnBreaker(CoreData.instance.columnBreaker += 1);
            CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= (Configuration.instance.columnBreakerCost1 / 3));
            Booster2.text = CoreData.instance.GetColumnBreaker().ToString();
            AudioManager.instance.CoinPayAudio();
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
            updateBoostersLabels();
            AnalyticsEvent.Custom("BuyColumnBreaker 1");
        }
    }
    public void b3()
    {
        if ((Configuration.instance.rainbowBreakerCost1 / 3) <= CoreData.instance.GetPlayerCoin())
        {
            CoreData.instance.SaveRainbowBreaker(CoreData.instance.rainbowBreaker += 1);
            CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= (Configuration.instance.rainbowBreakerCost1 / 3));
            Booster3.text = CoreData.instance.GetRainbowBreaker().ToString();
            AudioManager.instance.CoinPayAudio();
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
            updateBoostersLabels();
            AnalyticsEvent.Custom("BuyColorHunter 1");
        }
    }
    public void b4()
    {
        if ((Configuration.instance.ovenBreakerCost1 / 3) <= CoreData.instance.GetPlayerCoin())
        {
            CoreData.instance.SaveOvenBreaker(CoreData.instance.ovenBreaker += 1);
            CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= (Configuration.instance.ovenBreakerCost1 / 3));
            Booster4.text = CoreData.instance.GetOvenBreaker().ToString();
            AudioManager.instance.CoinPayAudio();
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
            updateBoostersLabels();
            AnalyticsEvent.Custom("BuyMixer 1");
        }
    }
    public void b5()
    {
        if ((Configuration.instance.singleBreakerCost1 / 3) <= CoreData.instance.GetPlayerCoin())
        {
            CoreData.instance.SaveSingleBreaker(CoreData.instance.singleBreaker += 1);
            CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= (Configuration.instance.singleBreakerCost1 / 3));
            Booster5.text = CoreData.instance.GetSingleBreaker().ToString();
            AudioManager.instance.CoinPayAudio();
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
            updateBoostersLabels();
            AnalyticsEvent.Custom("BuySingleBreaker 1");
        }
    }
    public void b6()

    {
        if ((Configuration.instance.beginBombBreakerCost1 / 3) <= CoreData.instance.GetPlayerCoin())
        {
            CoreData.instance.SaveBeginBombBreaker(CoreData.instance.beginBombBreaker += 1);
            CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= (Configuration.instance.beginBombBreakerCost1 / 3));
            Booster6.text = CoreData.instance.GetBeginBombBreaker().ToString();
            AudioManager.instance.CoinPayAudio();
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
            updateBoostersLabels();
            AnalyticsEvent.Custom("BuyBeginBomb 1");

        }
    }
    public void b7()
    {
        if ((Configuration.instance.beginRainbowCost1 / 3) <= CoreData.instance.GetPlayerCoin())
        {
            CoreData.instance.SaveBeginRainbow(CoreData.instance.beginRainbow += 1);
            CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= (Configuration.instance.beginRainbowCost1 / 3));
            Booster7.text = CoreData.instance.GetBeginRainbow().ToString();
            AudioManager.instance.CoinPayAudio();
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
            updateBoostersLabels();
            AnalyticsEvent.Custom("BuyBeginColorHunter 1");

        }
    }
    public void b8()
    {
        if ((Configuration.instance.beginFiveMovesCost1 / 3) <= CoreData.instance.GetPlayerCoin())
        {
            CoreData.instance.SaveBeginFiveMoves(CoreData.instance.beginFiveMoves += 1);
            CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin -= (Configuration.instance.beginFiveMovesCost1 / 3));
            Booster8.text = CoreData.instance.GetBeginFiveMoves().ToString();
            AudioManager.instance.CoinPayAudio();
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
            updateBoostersLabels();
            AnalyticsEvent.Custom("BuyBeginXBreaker 1");
        }
    }







}