using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoPopup : MonoBehaviour {

    public Text TitleText;
    public Text InfoText;    

    // Use this for initialization
    void Start ()
    {
        TitleText.text = ""+ Configuration.instance.MessageBoardTitle;
        InfoText.text = "" + Configuration.instance.MessageBoardInfo;       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
