using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class achievementtable : MonoBehaviour
{

    public GameObject[] achievement;

    void Start()
    {
        Invoke("updateachiment", 0.2f);
    }


    public void updateachiment()
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        // Achievement and leaderboard
        int toplamscore = CoreData.instance.GetToplamScore();
        int toplamstar = 0;

        for (int i = 1; i <= CoreData.instance.GetOpendedLevel(); i++)
        {
            toplamstar += CoreData.instance.GetLevelStar(i);
        }

        // Achiments
        if (openedLevel >= 10)
        {
            achievement[1].SetActive(true);
        }

        if (openedLevel >= 30)
        {
            achievement[2].SetActive(true);
        }
        if (openedLevel >= 55)
        {
            achievement[3].SetActive(true);
        }
        if (openedLevel >= 70)
        {
            achievement[4].SetActive(true);
        }
        if (openedLevel >= 85)
        {
            achievement[5].SetActive(true);
        }
        if (openedLevel >= 100)
        {
            achievement[6].SetActive(true);
        }
        if (openedLevel >= 130)
        {
            achievement[7].SetActive(true);
        }
        if (openedLevel >= 155)
        {
            achievement[8].SetActive(true);
        }
        if (openedLevel >= 185)
        {
            achievement[9].SetActive(true);
        }
        if (openedLevel >= 200)
        {
            achievement[10].SetActive(true);
        }
        if (openedLevel >= 222)
        {
            achievement[11].SetActive(true);
        }
        if (openedLevel >= 250)
        {
            achievement[12].SetActive(true);
        }
        if (openedLevel >= 270)
        {
            achievement[13].SetActive(true);
        }
        if (toplamstar > 74)
        {
            achievement[14].SetActive(true);
        }
        if (toplamstar > 299)
        {
            achievement[15].SetActive(true);
        }
        if (toplamstar > 599)
        {
            achievement[16].SetActive(true);
        }
        if (toplamstar > 899)
        {
            achievement[17].SetActive(true);
        }
        if (toplamscore > 199999)
        {
            achievement[18].SetActive(true);
        }
        if (toplamscore > 399999)
        {
            achievement[19].SetActive(true);
        }

        if (toplamscore > 599999)
        {
            achievement[20].SetActive(true);
        }
    }

    public void Showleaderboard()
    {
//        if (GameServices.IsInitialized())
//        {
//            GameServices.ShowLeaderboardUI();
//        }
//        else
//        {
//#if UNITY_ANDROID
//            GameServices.Init();    // start a new initialization process
//#elif UNITY_IOS
//    Debug.Log("Cannot show leaderboard UI: The user is not logged in to Game Center.");
//#endif
//        }
    }
}
