using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChallengeAmount : MonoBehaviour
{
    public Text LevelChallengeAmountText;
    // Use this for initialization
    void Start()
    {
       

            if (Configuration.instance.GiftBoxWinNum <= Configuration.instance.GiftBoxRequiredWinLevel)
            {
                LevelChallengeAmountText.text = Configuration.instance.GiftBoxWinNum + "/ "+ Configuration.instance.GiftBoxRequiredWinLevel;
            }
            else
            {
                LevelChallengeAmountText.text = Configuration.instance.GiftBoxRequiredWinLevel+" / "+ Configuration.instance.GiftBoxRequiredWinLevel;

            }      
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
