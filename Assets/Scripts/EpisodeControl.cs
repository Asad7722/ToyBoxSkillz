using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeControl : MonoBehaviour {

    public GameObject Lock;
    public GameObject Back;
    public Image Map;
    public Text EpisodeText;
    public Text TitleText;
    public LevelMap LevelMapList;
    public int ObjeEpisodeIndex;
    
    // Use this for initialization
    void Start ()
    {

        int currentepisode = Configuration.instance.CurrentEpisode;
        ObjeEpisodeIndex = transform.GetSiblingIndex()+1;
        EpisodeText.text = "Episode "+ ObjeEpisodeIndex.ToString();
        TitleText.text = LevelMapList.EpisodesLabel[ObjeEpisodeIndex];
        Map.sprite = Resources.Load<Sprite>("MAP/bigmap_" + ObjeEpisodeIndex);
        if (ObjeEpisodeIndex > currentepisode)
        {
            Lock.SetActive(true);
            Back.GetComponent<Image>().color = new Color(0, 0, 0);
            Map.color = new Color(0.3f, 0.3f, 0.3f);
        }
        else if (ObjeEpisodeIndex == currentepisode)
        {

            Back.GetComponent<Image>().color = new Color(0.5f, 0.8f, 1);
            if (GameObject.Find("CurrentEpisodePoint"))
            {
                Configuration.instance.CurruentEpisodeObje = gameObject;

                var person = GameObject.Find("CurrentEpisodePoint") as GameObject;
                var target = GameObject.Find("TargetEpisode") as GameObject;

                LevelMapList.pointCurrentEpisode = (float)ObjeEpisodeIndex / (float)Configuration.instance.episode;

                person.transform.SetParent(this.gameObject.transform);
                person.transform.localPosition = new Vector3(0, 0, 0);
                person.transform.localScale = new Vector3(1, 1, 1);




                target.transform.position = this.gameObject.transform.position;
                target.transform.SetParent(this.gameObject.transform);               

            }
        }
       
      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
   
    
}
