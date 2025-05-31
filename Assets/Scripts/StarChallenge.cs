using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarChallenge : MonoBehaviour {
    public Text StarAciklamaText;
    //public Text StarGiftAmountText;

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("StarChallengeHediyeAlindi") == 0)
        {
            Language.StartGlobalTranslateWord(StarAciklamaText, "Number of stars required: ", Configuration.instance.StarChallengeStarAmount, "");


            // StarAciklamaText.text = "Collect " + Configuration.instance.StarChallengeStarAmount + " stars in";
        }
        else
        {
            Language.StartGlobalTranslateWord(StarAciklamaText, "for activating ", 0, "");

            //StarAciklamaText.text = "For Activiting ";
        }
        //StarGiftAmountText.text = "x" + Configuration.instance.StarChallengeGiftAmount;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
