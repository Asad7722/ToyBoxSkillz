using UnityEngine;
using System.Collections;

public class UISettings : MonoBehaviour 
{
    public PopupOpener settingsPopup;
	itemGrid itmInstance;
	public static bool isclick;
	void Start(){
		isclick = true;
		itmInstance = gameObject.GetComponent<itemGrid> ();
	}

    public void SettingsClick()
    {
		isclick = false;
		Debug.Log ("setting button clicked");
        AudioManager.instance.ButtonClickAudio();
            settingsPopup.OpenPopup();	            
    }
}
