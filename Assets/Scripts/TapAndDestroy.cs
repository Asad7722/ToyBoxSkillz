using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAndDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonUp(0) && Configuration.instance.CurrentScene == CURRENT_SCENE.PLAY)
        //{            
        //    Configuration.instance.MessageBoardInfo = "";
        //    Skip();
        //    Destroy(gameObject);
        //}
    }
    public void Skip()
    {
        Configuration.instance.pause = false;
        Destroy(gameObject);


        itemGrid.instance.helpBoard = false;
        itemGrid.instance.lockSwap = false;
        itemGrid.instance.lockSwapHelp = false;
        itemGrid.instance.TutorialCubesList.Clear();
        //Configuration.instance.SkipTutuorial = true;
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
