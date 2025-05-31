using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoxEventControl : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		if(Configuration.instance.LevelBoxEvent)
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
