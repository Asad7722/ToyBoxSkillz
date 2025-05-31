using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreYouSureText : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (Configuration.instance.GiftBox)
        {
            
            //gameObject.GetComponent<Text>().text = "you will lose your challenges!";
            Language.StartGlobalTranslateWord(gameObject.GetComponent<Text>(), "you will lose your challenges!");


        }
        else if (Configuration.instance.ActiveFreeBoxesCount == 0)
        {
            //gameObject.GetComponent<Text>().text = "you will lose your Free Box Challenge!";
            Language.StartGlobalTranslateWord(gameObject.GetComponent<Text>(), "you will lose your Free Box Challenge!");



        }
        else if (NewLife.instance.rewardLifeLive)
        {
            //gameObject.GetComponent<Text>().text = "are you sure!";
            Language.StartGlobalTranslateWord(gameObject.GetComponent<Text>(), "are you sure!");



        }
        else

        {
          //gameObject.GetComponent<Text>().text = "you will lose a life!";
            Language.StartGlobalTranslateWord(gameObject.GetComponent<Text>(), "you will lose a life!");

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
