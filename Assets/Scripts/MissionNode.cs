using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionNode : MonoBehaviour
{

    public int ID = 0;
    public Image ItemImage;
    public Text InfoText;
    public Image ProgressBar;
    public Text ProgressText;
    public GameObject CompleteImage;

    // Use this for initialization
    void Start()

    {

        int Mission_01 = PlayerPrefs.GetInt("Mission_01");
        int Mission_02 = PlayerPrefs.GetInt("Mission_02");
        int Mission_03 = PlayerPrefs.GetInt("Mission_03");

        if (ID == Mission_01 ||
             ID == Mission_02 ||
             ID == Mission_03
            )
        {
            gameObject.SetActive(true);
            //Old Current
            int OldCurrent = PlayerPrefs.GetInt("CurrentMission" + ID);

            //Current
            int CurrentMission = PlayerPrefs.GetInt("" + ID);

            //Request 
            int RequestMission = PlayerPrefs.GetInt("Request" + ID);

            PlayerPrefs.SetInt("CurrentMission" + ID, CurrentMission);
            PlayerPrefs.Save();
            ProgressText.text = OldCurrent.ToString() + "/" + RequestMission.ToString();
            ProgressBar.fillAmount = (float)OldCurrent / (float)RequestMission;

            if (CurrentMission >= RequestMission)
            {
                ProgressText.text = RequestMission.ToString() + "/" + RequestMission.ToString();
                ProgressBar.fillAmount = 1;
                CompleteImage.SetActive(true);
            }
            else
            {
                StartCoroutine(CountUpToTarget(ProgressText, ProgressBar, OldCurrent, CurrentMission, RequestMission, 1));
                //StartCoroutine(CountUpToTargetEfect(CurrentMission- OldCurrent));
            }

        }
        else
        {
            gameObject.SetActive(false);
        }



        //ProgressText.text = CurrentMission.ToString() + "/" + RequestMission.ToString();
        //ProgressBar.fillAmount = CurrentMissionFloat / RequestMissionFloat;    

    }

    IEnumerator CountUpToTarget(Text Label, Image ProgressBar, int CurrentValue, int TargetValue, int RequestValue, int smooth)
    {



        float TargetValueFloat = (float)TargetValue;
        float CurrentValueFloat = (float)CurrentValue;
        float RequestValueFloat = (float)RequestValue;
        yield return new WaitForSeconds(1.0f);
        if (TargetValue == 0)
        {
            ProgressText.text = TargetValue.ToString() + "/" + RequestValue.ToString();
            ProgressBar.fillAmount = 0;
            CompleteImage.SetActive(false);
        }
        if (TargetValue == CurrentValue)
        {
            ProgressText.text = CurrentValue.ToString() + "/" + RequestValue.ToString();
            ProgressBar.fillAmount = CurrentValueFloat / RequestValueFloat;
            CompleteImage.SetActive(false);
        }
        while (CurrentValue < TargetValue)
        {
            CurrentValue += smooth;//= TargetValueFloat / (smooth / Time.deltaTime); // or whatever to get the speed you like
            CurrentValue = Mathf.Clamp(CurrentValue, 0, TargetValue);
            Label.text = CurrentValue + "/" + RequestValue.ToString();
            ProgressBar.fillAmount = (float)CurrentValue / (float)RequestValue;
            AudioManager.instance.SwapAudio();
            yield return new WaitForSeconds(0.015f);

            // AudioManager.instance.DropAudio();
        }              

        int CurrentMission = PlayerPrefs.GetInt("" + ID);
        PlayerPrefs.SetInt("CurrentMission" + ID, CurrentMission);
        PlayerPrefs.Save();
        Debug.Log("CurrentMission" + ID + "/" + PlayerPrefs.GetInt("CurrentMission" + ID));
        if (!Configuration.instance.MenuToMap)
        {
            yield return new WaitForSeconds(1.2f);
            var MissionMiniPopup = GameObject.Find("MiniMissionChallengePopup(Clone)");
            //  MissionMiniPopup.< GetComponent > ().< Popup > ().Close();
            if (CurrentValue < RequestValue)
            {
                if (MissionMiniPopup)
                {
                    MissionMiniPopup.GetComponent<Popup>().Close();
                }
                else
                {
                    Configuration.instance.CloseClick = true;
                }
            }
        }

    }
    IEnumerator CountUpToTargetEfect(int soundCount)
    {
        float count = 0;
        float soundCountFloat = (float)soundCount;

        while (count < soundCountFloat)
        {
            count++;
            AudioManager.instance.SwapAudio();
            // AudioManager.instance.DropAudio();
            yield return new WaitForSeconds(0.02f);
        }
    }

}
