using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCanvas : MonoBehaviour {
    public GameObject[] Events;
    [Header("ENABLE TIME")]
    [RangeAttribute(0, 50000)]
    public int[] EnableLevel;    
    protected Canvas e_canvas;
    
   
    // Use this for initialization
    void Start ()
    {
           if (!e_canvas) e_canvas = GameObject.Find("Canvaspromo").GetComponent<Canvas>();
      
    }
    // Update is called once per frame

   
   

    public void GetPrefab(int prefabnumber)
    {
        int openedLevel = CoreData.instance.GetOpendedLevel();
        if (openedLevel >= EnableLevel[prefabnumber])
        {
            var episodeprefab = Instantiate(Events[prefabnumber]) as GameObject;
            episodeprefab.transform.SetParent(e_canvas.transform, false);
        }
    }
}
