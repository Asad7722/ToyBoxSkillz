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
           StartCoroutine(StartGetPrefabs());
    }
    // Update is called once per frame

   
    IEnumerator StartGetPrefabs()
    {
        yield return new WaitForSeconds(0.01f);      
        //for (int i = 0; i < Events.Length; i++)
        //{
            if(Configuration.instance.Promo0)               
            {
                GetPrefab(0);
            }
            if (Configuration.instance.Promo1)
            {
                GetPrefab(1);
            }
            if (Configuration.instance.Promo2)
            {
                GetPrefab(2);
            }


       // }

    }
  

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
