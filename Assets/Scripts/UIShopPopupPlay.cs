using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIShopPopupPlay : MonoBehaviour 
{
  //  public Text coinAmount;

   // public Text coin1;
    public Text cost1;

   // public Text coin2;
    public Text cost2;

   // public Text coin3;
    public Text cost3;

   // public Text coin4;
    public Text cost4;

   // public Text coin5;
    public Text cost5;

    public Text cost6;

    void Start () 
    {
      //  UpdateCoinAmountLabel();
      //  GoogleAnalyticsV4.instance.LogScreen("shop popup ");

        //coin1.text = Configuration.instance.product1Coin.ToString();
        //coin2.text = Configuration.instance.product2Coin.ToString();
        //coin3.text = Configuration.instance.product3Coin.ToString();
        //coin4.text = Configuration.instance.product4Coin.ToString();
        //coin5.text = Configuration.instance.product5Coin.ToString();

        cost1.text =  Configuration.instance.Gems_60_cost.ToString();
        cost2.text =  Configuration.instance.Gems_600_cost.ToString();
        cost3.text =  Configuration.instance.Gems_1200_cost.ToString();
        cost4.text =  Configuration.instance.Gems_2500_cost.ToString();
        cost5.text =  Configuration.instance.Gems_5200_cost.ToString();
        cost6.text = Configuration.instance.Gems_12000_cost.ToString();
    }

    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }

   /* public void UpdateCoinAmountLabel()
    {
        coinAmount.text = CoreData.instance.GetPlayerCoin().ToString();
    }*/
}
