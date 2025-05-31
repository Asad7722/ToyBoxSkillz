using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class AchievementNode : MonoBehaviour
{
    public string id;
    public bool repeating;
    public bool ReachLevel;
   
   
    public string AchievementTextContent;
    public Text AchievementText;
    public int requiredNum;
    public GameObject OpenBox;
    public Image ProgresBar;
    public Text ProgresText;
    private int curruentNum;
    // Start is called before the first frame update
    void Start()
    {

        if(!PlayerPrefs.HasKey(id))
        {
            PlayerPrefs.SetInt(id,0);
            PlayerPrefs.Save();
        }

        AchievementText.text = AchievementTextContent.ToString();


        if(ReachLevel)
        {
            curruentNum = CoreData.instance.GetOpendedLevel();
        }
        else
        {
            curruentNum = PlayerPrefs.GetInt(id);
        }
       


        if(curruentNum >= requiredNum)
        {
            ProgresBar.fillAmount = 1;
            ProgresText.text = "COMPLETED";
            if (PlayerPrefs.GetInt("OpenenedGiftBox" + id)==0)
            {
                OpenBox.SetActive(true);
            }
            else
            {
                OpenBox.SetActive(false);
            }
           
        }
        else
        {
            OpenBox.SetActive(false);
            ProgresBar.fillAmount = (float)curruentNum / (float)requiredNum;
            ProgresText.text = curruentNum.ToString() +"/"+ requiredNum.ToString();
        }

    }

    public void StartOpenBox()
    {
        Configuration.instance.OpenGiftBoxPopup("Gift Box", REWARD_TYPE.RewardGems);
        AnalyticsEvent.Custom("Achievement "+ id);

        OpenBox.SetActive(false);
        

        if (repeating)
        {
            PlayerPrefs.SetInt(id, 0);
            PlayerPrefs.Save();
            ProgresBar.fillAmount = 0;
            ProgresText.text = 0 + "/" + requiredNum.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("OpenenedGiftBox" + id, 1);
            PlayerPrefs.Save();
        }

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
