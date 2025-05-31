using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldEpisodesShow : MonoBehaviour {

    protected Canvas m_canvas;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GetPrefabEpisodeConf(int episodenumber)
    {
        if (episodenumber > 0)
        {
            var episodeprefab = Instantiate(Resources.Load("Prefabs/EPISODES/Raw Image (" + episodenumber + ")")) as GameObject;
            episodeprefab.transform.SetParent(m_canvas.transform, false);
        }
    }
    public void ShowOldEpisodesPrefab()
    {
        int currentepisode = Configuration.instance.CurrentEpisode;
        if (currentepisode > 3)
        {
            int openedLevel = CoreData.instance.GetOpendedLevel();
            //int beginepisodes = Configuration.instance.BeginEpisodes;
            if (!m_canvas) m_canvas = GameObject.Find("Canvaslevel").GetComponent<Canvas>();

            m_canvas.GetComponent<RectTransform>().sizeDelta = new Vector3(720f, ((currentepisode - 1) * 950), 0);


            if (GameObject.Find("bulut alt(Clone)"))
            {

                Destroy(GameObject.Find("bulut alt(Clone)"));
            }
            if (GameObject.Find("OldEpisodes(Clone)"))
            {

                Destroy(GameObject.Find("OldEpisodes(Clone)"));
            }


            for (int i = 1; i < currentepisode; i++)
            {
                if (currentepisode > 3)
                {
                    GetPrefabEpisodeConf((currentepisode - 1) - i);
                }
            }
            var bulutaltprefab = Instantiate(Resources.Load("Prefabs/EPISODES/bulut alt")) as GameObject;
            var oldepisodes = Instantiate(Resources.Load("Prefabs/EPISODES/OldEpisodes")) as GameObject;
            bulutaltprefab.transform.SetParent(m_canvas.transform, false);
            oldepisodes.transform.SetParent(m_canvas.transform, false);
        }
        // StartCoroutine(updateOldEpisodesPrefabConf());



    }
}
