using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePopupReward : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Configuration.instance.LifePopupReward)
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
