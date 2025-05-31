using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FreeGiftBoxes : MonoBehaviour {    
    public float updateFrekans = 1.0f;
    float timeToGo;
    public static FreeGiftBoxes instance = null;

    public Text ActiveFreeBoxesCountText;
    public Text WhichText;
    public GameObject ActiveFreeBoxesCountObject;
   

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
       

    }


    void Start () 
    {
        timeToGo = Time.fixedTime + updateFrekans;
    }
	
	// Update is called once per frame
	//void Update () {
       

 //   }

    private void FixedUpdate()
    {
        if (Time.fixedTime >= timeToGo)
        {
            
                if (Configuration.instance.ReadyFreeBoxesCount > 0)
                {

                    ActiveFreeBoxesCountObject.SetActive(true);
                    ActiveFreeBoxesCountObject.GetComponent<Image>().color = new Color(1, 0, 0);
                    ActiveFreeBoxesCountText.GetComponent<Text>().color = new Color(1, 1, 1);
                    //ActiveFreeBoxesCountText.text = "OPEN GIFT BOX " + Configuration.instance.ReadyFreeBoxesCount.ToString();
                    //WhichText.text = "Your gift box is ready to open.";

                    Language.StartGlobalTranslateWord(WhichText, "Your gift box is ready to open.");
                    Language.StartGlobalTranslateWord(ActiveFreeBoxesCountText, "OPEN GIFT BOX ", Configuration.instance.ReadyFreeBoxesCount);


                }
                else if (Configuration.instance.ActiveFreeBoxesCount > 0)
                {

                    ActiveFreeBoxesCountObject.SetActive(false);
                    //WhichText.text = "Preparing a Gift Box ...!";
                    Language.StartGlobalTranslateWord(WhichText, "Preparing a Gift Box ...!");

                }
                else if (Configuration.instance.WinLevel >= 4)
                {

                    ActiveFreeBoxesCountObject.SetActive(true);
                    ActiveFreeBoxesCountObject.GetComponent<Image>().color = new Color(0, 1, 0);
                    ActiveFreeBoxesCountText.GetComponent<Text>().color = new Color(0, 0, 0);
                    // ActiveFreeBoxesCountText.text = "GIFT BOXES READY!";
                    // WhichText.text = "Which gift box would you like?";
                    Language.StartGlobalTranslateWord(WhichText, "Which gift box would you like?");
                    Language.StartGlobalTranslateWord(ActiveFreeBoxesCountText, "GIFT BOXES READY!");

                }
                else
                {



                    if (CoreData.instance.openedLevel < 7)
                    {
                        ActiveFreeBoxesCountObject.SetActive(false);
                        // WhichText.text = "Unlock at Level 7!";
                        Language.StartGlobalTranslateWord(WhichText, "Unlock at Level 7!");
                    }
                    else
                    {
                        ActiveFreeBoxesCountObject.SetActive(true);
                        ActiveFreeBoxesCountObject.GetComponent<Image>().color = new Color(0, 1, 0);
                        ActiveFreeBoxesCountText.GetComponent<Text>().color = new Color(0, 0, 0);
                        //ActiveFreeBoxesCountText.text = "New EVENT!";
                        // WhichText.text = "Pass 3 levels without losing to choose gift boxes.";
                        Language.StartGlobalTranslateWord(WhichText, "Pass 3 levels without losing to choose gift boxes.");
                        Language.StartGlobalTranslateWord(ActiveFreeBoxesCountText, "New EVENT!");

                    }

                }
                if (Configuration.instance.ReadyFreeBoxesCount > 3)
                {
                    Configuration.instance.ReadyFreeBoxesCount = 3;
                }
                if (Configuration.instance.ActiveFreeBoxesCount > 3)
                {
                    Configuration.instance.ActiveFreeBoxesCount = 3;
                }
                if (Configuration.instance.ActiveFreeBoxesCount < 0)
                {
                    Configuration.instance.ActiveFreeBoxesCount = 0;
                }
                if (Configuration.instance.ReadyFreeBoxesCount < 0)
                {
                    Configuration.instance.ReadyFreeBoxesCount = 0;
                }

                if (Configuration.instance.activeFreeBox != 0)
                {
                    Configuration.instance.ActiveFreeBoxesCount = 1;
                }
           
            // Do your thang
            timeToGo = Time.fixedTime + updateFrekans;
        }
    }
}
