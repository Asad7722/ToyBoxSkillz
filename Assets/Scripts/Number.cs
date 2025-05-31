using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour {
    public int number;
    public Text NumberText;

    // Use this for initialization
    void Start () {

        number = transform.GetSiblingIndex();
        NumberText.text = "Episode " + (number + 1).ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
