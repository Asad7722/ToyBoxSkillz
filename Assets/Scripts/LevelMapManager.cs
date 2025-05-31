using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapManager : MonoBehaviour {
    public static LevelMapManager instance = null;
    
    protected Canvas m_canvas;
    public GameObject scrollContent;
    float canvasHeight;
    
    // Use this for initialization
    void Start() {
        if (instance == null)
        {
            instance = this;
        }

        
        StartCoroutine(updateEpisodesPrefabConf());
        
       
    }
    IEnumerator updateEpisodesPrefabConf()
    {
        int currentepisode = Configuration.instance.CurrentEpisode;

        //if (currentepisode > 1)
        //{
        //    gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(720f, 4200, 0);
        //}
        //else
        //{
        //    gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(720f, 3000, 0);
        //}
        // canvasHeight = ((float)Screen.height / (float)Screen.width) * 720f;
        // int openedLevel = CoreData.instance.GetOpendedLevel();
        //int beginepisodes = Configuration.instance.BeginEpisodes;
        if (!m_canvas) m_canvas = GameObject.Find("Canvaslevel").GetComponent<Canvas>();
        var bulut = Instantiate(Resources.Load("Prefabs/EPISODES/bulut")) as GameObject;
        bulut.transform.SetParent(m_canvas.transform, false);
        if (currentepisode > 0)
        {
            // GetPrefabEpisodeConf(currentepisode + 1);
            // yield return new WaitForSeconds(0.01f);
            GetPrefabEpisodeConf(currentepisode);
            // yield return new WaitForSeconds(0.01f);
            //if (currentepisode >= 2)
            //{
            //    GetPrefabEpisodeConf(currentepisode - 1);
            //}
        }
        var bulutaltprefab = Instantiate(Resources.Load("Prefabs/EPISODES/bulut alt")) as GameObject;
        // var oldepisodes = Instantiate(Resources.Load("Prefabs/EPISODES/OldEpisodes")) as GameObject;       
        bulutaltprefab.transform.SetParent(m_canvas.transform, false);
        //  yield return new WaitForSeconds(0.011f);
        // oldepisodes.transform.SetParent(m_canvas.transform, false);
        yield return new WaitForSeconds(0.5f);
        // FoundTargetButtonClick2();
    }
    IEnumerator updateEpisodesCBPrefabConf()
    {
        int currentepisode = Configuration.instance.CurrentEpisode;

        if (currentepisode > 1)
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(720f, 4200, 0);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(720f, 3000, 0);
        }
        canvasHeight = ((float)Screen.height / (float)Screen.width) * 720f;
        int openedLevel = CoreData.instance.GetOpendedLevel();
        //int beginepisodes = Configuration.instance.BeginEpisodes;
        if (!m_canvas) m_canvas = GameObject.Find("Canvaslevel").GetComponent<Canvas>();
        var bulut = Instantiate(Resources.Load("Prefabs/EPISODES/bulut")) as GameObject;
        bulut.transform.SetParent(m_canvas.transform, false);
        if (currentepisode > 0)
        {
            GetPrefabEpisodeConf(currentepisode + 1);
            // yield return new WaitForSeconds(0.01f);
            GetPrefabEpisodeConf(currentepisode);
            // yield return new WaitForSeconds(0.01f);
            if (currentepisode >= 2)
            {
                GetPrefabEpisodeConf(currentepisode - 1);
            }
        }
        var bulutaltprefab = Instantiate(Resources.Load("Prefabs/EPISODES/bulut alt")) as GameObject;
        //var oldepisodes = Instantiate(Resources.Load("Prefabs/EPISODES/OldEpisodes")) as GameObject;       
        bulutaltprefab.transform.SetParent(m_canvas.transform, false);
        //  yield return new WaitForSeconds(0.011f);
        // oldepisodes.transform.SetParent(m_canvas.transform, false);
        yield return new WaitForSeconds(0.5f);
        FoundTargetButtonClick2();
    }
    public void GetPrefabEpisodeConf(int episodenumber)
    {
        if (episodenumber > 0)
        {
            if (!m_canvas) m_canvas = GameObject.Find("Canvaslevel").GetComponent<Canvas>();
            var episodeprefab = Instantiate(Resources.Load("Prefabs/EPISODES/Raw Image (" + episodenumber + ")")) as GameObject;
            episodeprefab.transform.SetParent(m_canvas.transform, false);
        }
    }
    public void ShowOldEpisodesPrefab()
    {


        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(720f, 4400, 0);

        int openedLevel = CoreData.instance.GetOpendedLevel();
        //int beginepisodes = Configuration.instance.BeginEpisodes;
        if (!m_canvas) m_canvas = GameObject.Find("Canvaslevel").GetComponent<Canvas>();

        if (GameObject.Find("Prefabs/EPISODES/bulut alt(Clone)"))
        {
            Destroy(GameObject.Find("Prefabs/EPISODES/bulut alt(Clone)"));
        }
        if (GameObject.Find("Prefabs/EPISODES/OldEpisodes(Clone)"))
        {
            Destroy(GameObject.Find("Prefabs/EPISODES/OldEpisodes(Clone)"));
        }

        int currentepisode = Configuration.instance.CurrentEpisode;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(720f, ((currentepisode - 1) * 940), 0);
        for (int i = 1; i < currentepisode; i++)
        {
            if (currentepisode > 3)
            {
                GetPrefabEpisodeConf((currentepisode - 3) - i);
            }
        }
        var bulutaltprefab = Instantiate(Resources.Load("Prefabs/EPISODES/bulut alt")) as GameObject;
        var oldepisodes = Instantiate(Resources.Load("Prefabs/EPISODES/OldEpisodes")) as GameObject;
        bulutaltprefab.transform.SetParent(m_canvas.transform, false);
        oldepisodes.transform.SetParent(m_canvas.transform, false);
        // StartCoroutine(updateOldEpisodesPrefabConf());



    }
    public void FoundTargetButtonClick2()
    {
        var person = GameObject.Find("TargetPointer") as GameObject;
        int openedLevel = CoreData.instance.GetOpendedLevel();
        int currentepisode = Configuration.instance.CurrentEpisode;

        if (currentepisode > 1)
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, canvasHeight / 2 - TargetPosition().y, 0);
        }
        else
        {
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(0, canvasHeight / 2 - TargetPosition().y+1000, 0);

        }
    }

    //MagicBlast
    public void ShowEpisodes()
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        if (!m_canvas) m_canvas = GameObject.Find("Canvaslevel").GetComponent<Canvas>();
        //   int level = transform.GetSiblingIndex();
        int EpisodeMaxLevel = Configuration.instance.EpisodemaxLevel;


        if (openedLevel <= 20)
        {
            //Episodes[1].SetActive(true);
            var episodeprefab = Instantiate(Resources.Load("Prefabs/EPISODES/Raw Image (1)")) as GameObject;
            episodeprefab.transform.SetParent(m_canvas.transform, false);
        }

        if (openedLevel > 20 && openedLevel <= 40)
        {
            // Episodes[2].SetActive(true);
            var episodeprefab = Instantiate(Resources.Load("Prefabs/EPISODES/Raw Image (2)")) as GameObject;
            episodeprefab.transform.SetParent(m_canvas.transform, false);
        }
        if (openedLevel > 40 && openedLevel <= 70)
        {
            //Episodes[3].SetActive(true);
            var episodeprefab = Instantiate(Resources.Load("Prefabs/EPISODES/Raw Image (3)")) as GameObject;
            episodeprefab.transform.SetParent(m_canvas.transform, false);
        }
        if (openedLevel > 70 && openedLevel <= 110)
        {
            //Episodes[4].SetActive(true);
            var episodeprefab = Instantiate(Resources.Load("Prefabs/EPISODES/Raw Image (4)")) as GameObject;
            episodeprefab.transform.SetParent(m_canvas.transform, false);
        }
        if (openedLevel > 110 && openedLevel <= 150)
        {
            //Episodes[5].SetActive(true);
            var episodeprefab = Instantiate(Resources.Load("Prefabs/EPISODES/Raw Image (5)")) as GameObject;
            episodeprefab.transform.SetParent(m_canvas.transform, false);
        }
        if (openedLevel > 150 && openedLevel <= 200)
        {
            // Episodes[6].SetActive(true);
            var episodeprefab = Instantiate(Resources.Load("Prefabs/EPISODES/Raw Image (6)")) as GameObject;
            episodeprefab.transform.SetParent(m_canvas.transform, false);
        }
        if (openedLevel > 200)
        {
            int episode = (openedLevel / 50) + 3;
           // Episodes[episode].SetActive(true);
            var episodeprefab = Instantiate(Resources.Load("Prefabs/EPISODES/Raw Image (" + episode + ")")) as GameObject;
            episodeprefab.transform.SetParent(m_canvas.transform, false);

        }

    }
    IEnumerator ScrollContent(Vector3 target)
    {
        if (target.y > 0) target.y = 0;

        var from = scrollContent.GetComponent<RectTransform>().localPosition;
        float step = Time.fixedDeltaTime;
        float t = 0;

        while (t <= 1.0f)
        {
            t += step;
            scrollContent.GetComponent<RectTransform>().localPosition = Vector3.Lerp(from, target, t);
            yield return new WaitForFixedUpdate();
        }

        scrollContent.GetComponent<RectTransform>().localPosition = target;
    }
    Vector3 TargetPosition()
    {
        var currentPosition = Vector3.zero;
        var person = GameObject.Find("TargetPointer") as GameObject;

        currentPosition = person.GetComponent<RectTransform>().localPosition;

        return currentPosition;
    }
}



