﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EpisodeImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("MAP/bigmap_" + Configuration.instance.CurrentEpisode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
