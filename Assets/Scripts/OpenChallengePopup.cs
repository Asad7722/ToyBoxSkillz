using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChallengePopup : MonoBehaviour {
    public PopupOpener ChallengePopup;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PopupOpen()
    {
        ChallengePopup.OpenPopup();
    }
}
