using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonText : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
       
       gameObject.GetComponent<Text>().text = CoreData.instance.openedLevel.ToString();

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
