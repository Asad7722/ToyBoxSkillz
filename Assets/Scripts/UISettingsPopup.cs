using UnityEngine;
using System.Collections;


public class UISettingsPopup : MonoBehaviour 
{
   
    public SceneTransition toMap;   
    public void GoToMap()
    {
        itemGrid.instance.GameEnd();
        //AudioManager.instance.ButtonClickAudio();
        //Configuration.instance.WinLevel = 0; 
        //toMap.PerformTransition();
        //if (Configuration.instance.GiftBox)
        //{
        //    Configuration.instance.GiftBoxWinNum = 0;
        //}
    }

    public void Replay()
    {
        AudioManager.instance.ButtonClickAudio();
        Configuration.instance.autoPopup = StageLoader.instance.Stage;
        Configuration.instance.WinLevel = 0;
        Transition.LoadLevel("Map", 0.1f, Color.black);

        if (Configuration.instance.GiftBox)
        {
            Configuration.instance.GiftBoxWinNum = 0;
        }
    }
	
	public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }

    public void CloseButtonClick()
    {

		UISettings.isclick = true;
        AudioManager.instance.ButtonClickAudio();
		PlayerPrefs.SetInt ("canvas", 1);
        if (GameObject.Find("Board"))
        {
			GameObject.Find("Board").GetComponent<itemGrid>().state = GAME_STATE.WAITING_USER_SWAP;
        }
    }
}
