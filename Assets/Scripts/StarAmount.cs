using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StarAmount : MonoBehaviour {

    public Text StarAmountText;
    public Text ArenaTitleText;
  //  public PopupOpener ArenaPopup;

    // Use this for initialization
    void Start ()
    {
        UpdateArenaLabels();
    }

    // Update is called once per frame
    public void UpdateArenaLabels()
    {     
            for (int i = 1; i < 12; i++)
            {
            int coin = CoreData.instance.GetPlayerCoin();
            int staramount = PlayerPrefs.GetInt("staramount");
                int GiftAmount = Configuration.instance.GiftAmountText[i];
                int GerekliStar = Configuration.instance.GerekliStarText[i];
                if (staramount >= GerekliStar)
                {
                    PlayerPrefs.SetInt("arenanumber", i);
                    PlayerPrefs.Save();                 
                }
            int arenanumber = PlayerPrefs.GetInt("arenanumber");
            if(staramount>0)
            {
                StarAmountText.text = "" + staramount;
            }
            else
            {
                StarAmountText.text = "0";
            }
           
            ArenaTitleText.text = "Arena " + arenanumber;

        }
      
       // int staramount = PlayerPrefs.GetInt("staramount");
     /*   int arenanumber = PlayerPrefs.GetInt("arenanumber");
        StarAmountText.text = "" + staramount;
        ArenaTitleText.text = "Arena " + arenanumber;*/
    } 
    void Update () {
		
	}
}
