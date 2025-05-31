using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpisodeBoxInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OpenEpisodeBoxInfo()
    {
        int episode = Configuration.instance.episode;
        int[] EpisodesRange = Configuration.instance.EpisodesRange;
        int openedLevel = CoreData.instance.GetOpendedLevel();

        int MaxLevel = 0;
        // int levelsayisi = Configuration.instance.EpisodesRange[Configuration.instance.CurrentEpisode] CoreData.instance.openedLevel

        if (openedLevel < Configuration.instance.EpisodemaxLevel)
        {
            for (int i = 0; i < Configuration.instance.CurrentEpisode + 1; i++)
            {
                MaxLevel += EpisodesRange[i];
            }
            Configuration.OpenInfoPopup("Episode Gift Box", Language.TranslateWord("Reach level") + " " + MaxLevel.ToString());
        }
    }
}
