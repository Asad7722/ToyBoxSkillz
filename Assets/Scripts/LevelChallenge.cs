using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChallenge : MonoBehaviour {
        
    public Text LevelChallengeGiftAmountText;

    // Use this for initialization
    void Start()
    {     
        LevelChallengeGiftAmountText.text = "x" + Configuration.instance.GiftBoxGiftAmount;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
