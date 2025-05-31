using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayButonKontrol : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if(CoreData.instance.openedLevel>Configuration.instance.BeginLevelLevelChallenge)
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
