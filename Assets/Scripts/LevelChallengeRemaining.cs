using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChallengeRemaining : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<Text>().text = (Configuration.instance.GiftBoxRequiredWinLevel - Configuration.instance.GiftBoxWinNum).ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
