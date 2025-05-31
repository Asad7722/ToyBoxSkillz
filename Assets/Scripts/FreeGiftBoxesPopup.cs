using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeGiftBoxesPopup : MonoBehaviour {
    public Text WhichText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {

        
            if (Configuration.instance.ReadyFreeBoxesCount > 0)
            {               
                //WhichText.text = "Your gift box is ready to open.";
                Language.StartGlobalTranslateWord(WhichText, "Your gift box is ready to open.");

            }
            else if (Configuration.instance.ActiveFreeBoxesCount > 0)
            {               
                //WhichText.text = "Preparing a Gift Box ...!";
                Language.StartGlobalTranslateWord(WhichText, "Preparing a Gift Box ...!");

            }
            else if (Configuration.instance.WinLevel >= 3)
            {
                //WhichText.text = "Which gift box would you like?";
                Language.StartGlobalTranslateWord(WhichText, "Which gift box would you like?");

            }
            else
            {
               //WhichText.text = "Pass 3 levels without losing to choose gift boxes.";
                Language.StartGlobalTranslateWord(WhichText, "Pass 3 levels without losing to choose gift boxes.");

            }
       

    }
}
