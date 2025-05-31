using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChallengeObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Configuration.instance.LevelChallengeEvent)
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
