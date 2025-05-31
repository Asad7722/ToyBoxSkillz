using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpisodeShow : MonoBehaviour {
    public static EpisodeShow instance = null;
    public PopupOpener EpisodesPopup;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame

    public void EpisodeGoster()
    {
        EpisodesPopup.OpenPopup();
        AudioManager.instance.ButtonClickAudio();
    }
    void Update () {
		
	}
}
