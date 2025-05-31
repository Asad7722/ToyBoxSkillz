using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarChallengeAmount : MonoBehaviour
{
    public Text StarChallengeAmountText;
    // public Text Ready;


    // Use this for initialization
    void Start()
    {
        UpdateAmountText();
    }

    public void UpdateAmountText()
    {
        int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
        if (StarChallengeNum >= Configuration.instance.StarChallengeStarAmount)
        {
            StarChallengeAmountText.text = Configuration.instance.StarChallengeStarAmount + "/" + Configuration.instance.StarChallengeStarAmount;
        }
        else
        {
            StarChallengeAmountText.text = StarChallengeNum + "/" + Configuration.instance.StarChallengeStarAmount;
        }

    }

}
