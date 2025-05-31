using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    public int level;
    public InputField InputLevelText;
    void Start()
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        if (Configuration.instance.CurrentEpisode >= 80)
        {
            if (openedLevel == level + 1448)
            {
                if (GameObject.Find("TargetPointer"))
                {
                    var person = GameObject.Find("TargetPointer") as GameObject;

                    person.transform.position = this.gameObject.transform.position + new Vector3(0, 0.2f, 0);

                    person.transform.SetParent(this.gameObject.transform.parent.transform);

                    iTween.MoveBy(person, iTween.Hash(
                        "y", 0.2f,
                        "looptype", iTween.LoopType.pingPong,
                        "easetype", iTween.EaseType.easeInElastic,
                        "time", 1f
                        ));
                }
            }
            else if (openedLevel < level + 1448)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (openedLevel == level)
            {
                if (GameObject.Find("TargetPointer"))
                {
                    var person = GameObject.Find("TargetPointer") as GameObject;

                    person.transform.position = this.gameObject.transform.position + new Vector3(0, 1f, 0);

                    person.transform.SetParent(this.gameObject.transform.parent.transform);

                    iTween.MoveBy(person, iTween.Hash(
                        "y", 0.2f,
                        "looptype", iTween.LoopType.pingPong,
                        "easetype", iTween.EaseType.easeInElastic,
                        "time", 1f
                        ));
                }
            }
            else if (openedLevel < level)
            {
                gameObject.SetActive(false);
            }
        }

        if (Configuration.instance.CurrentEpisode < 80 && Configuration.instance.CurrentEpisode > 77 && level < 1000)
        {
            gameObject.SetActive(false);
        }
    }

    public void LevelClick()
    {
        AudioManager.instance.ButtonClickAudio();
    }

    int openedLevel;
    public void PlayClick()
    {
        //sameerlevelplay
        //skillz
        Configuration.instance.EpisodePlay = true;
        openedLevel = Configuration.instance.LeveltoPlay;
        //if (PlayerPrefs.GetInt("FirstTime") == 0)
        //{
        //    openedLevel = CoreData.instance.GetOpendedLevel();
        //    print("no tutorial 111");
        //}
        //else
        //{
        //    openedLevel = Configuration.instance.LeveltoPlay;
        //    print("no tutorial");
        //}

        //PlayerPrefs.SetInt("SkillzLevel", 0);
        PlayerPrefs.SetFloat("SkillzTimer", 180);

        //skillzlevelplay
        //if (openedLevel == Configuration.instance.EpisodemaxLevel)
        //{
        //    RandomPlay();
        //}
        //else if (openedLevel > Configuration.instance.maxLevel)
        //{
        //    StageLoader.instance.Stage = openedLevel-1448;
        //    StageLoader.instance.LoadLevel(StageLoader.instance.Stage);
        //    if (!GameObject.Find("LevelPopup(Clone)"))
        //    {
        //        //levelPopup.OpenPopup();
        //        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //        var eventfab = Instantiate(Resources.Load("Prefabs/Map/LevelPlay")) as GameObject;
        //        eventfab.transform.SetParent(m_canvas.transform, false);
        //    }
        //}       
        //else
        {
            StageLoader.instance.Stage = openedLevel;
            StageLoader.instance.LoadLevel(StageLoader.instance.Stage);
            if (!GameObject.Find("LevelPopup(Clone)"))
            {
                //levelPopup.OpenPopup();
                Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                var eventfab = Instantiate(Resources.Load("Prefabs/Map/LevelPlay")) as GameObject;
                eventfab.transform.SetParent(m_canvas.transform, false);
            }
        }
    }

    public void PlayDebugClick()
    {
        StageLoader.instance.Stage = int.Parse(InputLevelText.text);
        StageLoader.instance.LoadLevel(StageLoader.instance.Stage);

        Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        var eventfab = Instantiate(Resources.Load("Prefabs/Map/LevelPlay")) as GameObject;
        eventfab.transform.SetParent(m_canvas.transform, false);

    }

    public void RandomPlay()
    {
        Configuration.instance.RandomPlay = true;
        int MaxLevel = Configuration.instance.maxLevel;
        var level = UnityEngine.Random.Range(100, MaxLevel);
        StageLoader.instance.Stage = level;
        StageLoader.instance.LoadLevel(StageLoader.instance.Stage);
        if (!GameObject.Find("LevelPopup(Clone)"))
        {
            // levelPopup.OpenPopup();
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var eventfab = Instantiate(Resources.Load("Prefabs/Map/LevelPlay")) as GameObject;
            eventfab.transform.SetParent(m_canvas.transform, false);

        }

    }
    public void ArenaPlay()
    {
        Configuration.instance.ArenaMode = true;
        // Configuration.instance.StarChallenge = false;
        Configuration.instance.GiftBox = false;
        int MaxLevel = Configuration.instance.maxLevel;
        int arenanumber = PlayerPrefs.GetInt("arenanumber");
        int level = UnityEngine.Random.Range(Configuration.instance.LevelArenaRangeA[arenanumber], Configuration.instance.LevelArenaRangeB[arenanumber]);


        StageLoader.instance.Stage = level;
        StageLoader.instance.LoadLevel(StageLoader.instance.Stage);
        if (!GameObject.Find("LevelPopup(Clone)"))
        {
            //levelPopup.OpenPopup();
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var eventfab = Instantiate(Resources.Load("Prefabs/Map/LevelPlay")) as GameObject;
            eventfab.transform.SetParent(m_canvas.transform, false);
        }
    }

    public void GiftBoxPlay()
    {
        Configuration.instance.GiftBox = true;
        Configuration.instance.StarChallenge = false;
        int MaxLevel = Configuration.instance.maxLevel;
        int GiftBoxWinNum = Configuration.instance.GiftBoxWinNum;


        var level = UnityEngine.Random.Range(Configuration.instance.LevelGiftBoxRangeA[GiftBoxWinNum], Configuration.instance.LevelGiftBoxRangeB[GiftBoxWinNum]);
        StageLoader.instance.Stage = level;
        StageLoader.instance.LoadLevel(StageLoader.instance.Stage);
        if (!GameObject.Find("LevelPopup(Clone)"))
        {
            //levelPopup.OpenPopup();
            Canvas m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            var eventfab = Instantiate(Resources.Load("Prefabs/Map/LevelPlay")) as GameObject;
            eventfab.transform.SetParent(m_canvas.transform, false);
        }

    }
    public void StarChallengePlay()
    {
        if (PlayerPrefs.GetInt("StarChallengeHediyeAlindi") == 0)
        {
            Configuration.instance.StarChallenge = true;
            Configuration.instance.EpisodePlay = true;

            int openedLevel = CoreData.instance.GetOpendedLevel();

            if (PlayerPrefs.GetInt("StarChallengeEnable") == 0)
            {
                PlayerPrefs.SetString("E" + "StarChallenge", DateTime.Now.Ticks.ToString());
                PlayerPrefs.SetInt("StarChallengeEnable", 1);
                PlayerPrefs.SetInt("StarChallengeNum", 0);
                PlayerPrefs.Save();
                NotificationsSetup.NotificationStarChallengeCall();
            }
        }
        //else
        //{
        //    Notifications.CancelPendingLocalNotification(EM_NotificationsConstants.UserCategory_notification_category_StarCall);
        //}
    }

    public void ShowHelp(int level)
    {
        StartCoroutine(StartShowHelp(level));
    }

    IEnumerator StartShowHelp(int level)
    {
        yield return new WaitForSeconds(0.5f);
    }
}
