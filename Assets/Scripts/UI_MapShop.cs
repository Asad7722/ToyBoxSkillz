using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;



public class UI_MapShop : MonoBehaviour 
{
    public GameObject removeadsbuton;
    public GameObject unlimitedbuton;

    public Text Pack1_cost;
    public Text Pack2_cost;
    public Text Pack3_cost;
    public Text Pack4_cost;

    public Text Gems_60_cost;
    public Text Gems_600_cost;
    public Text Gems_1200_cost;
    public Text Gems_2500_cost;
    public Text Gems_5200_cost;
    public Text Gems_12000_cost;

    public Text RemoveAds_cost;
    public Text UnlimitedLife_cost;
   



    void Start () 
    {
        
        updateUnlimitedbuton();
        updateRemovebuton();
        updateButonLabel();



    }
   
    public void updateUnlimitedbuton()
    {
      
        int unlimitedlife = PlayerPrefs.GetInt("unlimitedlife");

        if (unlimitedlife == 1)
        {
           unlimitedbuton.SetActive(false);
        }

    }
    public void updateRemovebuton()
    {
        int removeads = PlayerPrefs.GetInt("removeads");
        if (removeads == 1)
        {
            removeadsbuton.SetActive(false);          
        }


    }
    public void updateButonLabel()
    {
        Pack1_cost.text = Configuration.instance.Pack1_cost;

        Pack2_cost.text = Configuration.instance.Pack2_cost; 

        Pack3_cost.text = Configuration.instance.Pack3_cost; 

        Pack4_cost.text = Configuration.instance.Pack4_cost; 

        Gems_60_cost.text = Configuration.instance.Gems_60_cost; 

        Gems_600_cost.text = Configuration.instance.Gems_600_cost;

        Gems_1200_cost.text = Configuration.instance.Gems_1200_cost;

        Gems_2500_cost.text = Configuration.instance.Gems_2500_cost;

        Gems_5200_cost.text = Configuration.instance.Gems_5200_cost;

        Gems_12000_cost.text = Configuration.instance.Gems_12000_cost;

        RemoveAds_cost.text = Configuration.instance.RemoveAds_cost; 

        UnlimitedLife_cost.text = Configuration.instance.UnlimitedLife_cost; 


    }

    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }

}
