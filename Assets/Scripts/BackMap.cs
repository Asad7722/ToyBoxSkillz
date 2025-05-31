using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMap : MonoBehaviour
{

    SpriteRenderer BackSprite;

    // Use this for initialization
    void Start()
    {

        if (CoreData.instance.openedLevel > 1)
        {

            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MAP/blur/bigmap_" + Configuration.instance.CurrentEpisode + " copy");

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
