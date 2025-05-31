using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCake : MonoBehaviour {

    public GameObject male;
    public GameObject female;
    // Use this for initialization
    void Start ()
    {
        if(Random.Range(0, 2)==0)
        {
            male.gameObject.SetActive(true);
            female.gameObject.SetActive(false);
        }
        else
        {
            male.gameObject.SetActive(false);
            female.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
