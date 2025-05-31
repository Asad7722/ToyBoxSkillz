using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPopup : MonoBehaviour {

    public Text HelpText;
    public GameObject ContinueButton;
    // Use this for initialization
	void Start ()
    {
        HelpText.text = Configuration.instance.MessageBoardInfo;
	}	
	public void Continue()
    {
        try
        {
            itemGrid.instance.CloseHelp();
        }
        catch (System.Exception)
        {
            Destroy(gameObject);
            Configuration.instance.pause = false;
        }
        
    }
    public void CloseHelpMap()
    {
        if (GameObject.Find("HelpPopupMap(Clone)"))
        {
            Destroy(GameObject.Find("HelpPopupMap(Clone)"));
        }

        if (GameObject.Find("DiffuseMap(Clone)"))
        {
            Destroy(GameObject.Find("DiffuseMap(Clone)"));
        }
        if (GameObject.Find("Arrow(Clone)"))
        {
            Destroy(GameObject.Find("Arrow(Clone)"));

        }
        if (GameObject.Find("Hand(Clone)"))
        {
            Destroy(GameObject.Find("Hand(Clone)"));
        }
        if (GameObject.Find("HandBoard(Clone)"))
        {
            Destroy(GameObject.Find("HandBoard(Clone)"));
        }
       
       Configuration.instance.pause = false;
        

    }
}
