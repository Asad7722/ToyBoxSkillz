﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChallengeGiftAmountText : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<Text>().text = "x"+ Configuration.instance.GiftBoxGiftAmount;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
