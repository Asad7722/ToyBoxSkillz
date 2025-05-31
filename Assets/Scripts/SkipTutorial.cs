using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTutorial : MonoBehaviour {

    public bool destroyobje;
	// Use this for initialization
	void Start () {
		
	}
	
	public void Skip()
    {
        Configuration.instance.pause = false;
        Destroy(gameObject);
        itemGrid.instance.DeFocusObject();


        itemGrid.instance.helpBoard = false;
        itemGrid.instance.lockSwap = false;
        itemGrid.instance.lockSwapHelp = false;
        itemGrid.instance.TutorialCubesList.Clear();
        Configuration.instance.SkipTutuorial = true;
        itemGrid.instance.huntering = false;
        itemGrid.instance.merging = false;


        if (GameObject.Find("Arrow(Clone)"))
        {
            Destroy(GameObject.Find("Arrow(Clone)"));
        }
        if (GameObject.Find("Diffuse(Clone)"))
        {
            Destroy(GameObject.Find("Diffuse(Clone)"));
        }
        if (GameObject.Find("Hand(Clone)"))
        {
            Destroy(GameObject.Find("Hand(Clone)"));
        }
        if (GameObject.Find("HandBoard(Clone)"))
        {
            Destroy(GameObject.Find("HandBoard(Clone)"));
        }
        if (GameObject.Find("HelpPopup(Clone)"))
        {
            Destroy(GameObject.Find("HelpPopup(Clone)"));
        }
        if (GameObject.Find("HelpPopupMap(Clone)"))
        {
            Destroy(GameObject.Find("HelpPopupMap(Clone)"));
        }
        if (GameObject.Find("SkipHelpButon(Clone)"))
        {
            Destroy(GameObject.Find("SkipHelpButon(Clone)"));
        }




    }
    // Update is called once per frame
	void Update ()
    {
      
        
	}
}
