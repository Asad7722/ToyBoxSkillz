using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(CoreData.instance.GetOpendedLevel() == Configuration.instance.EpisodemaxLevel)
        {
            gameObject.SetActive(false);
        }else
        if(Configuration.instance.RandomPlay)
        {
            gameObject.SetActive(true);
            
        }
        else
        {
            gameObject.SetActive(false);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
