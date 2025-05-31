using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

   public GameObject []info;


	// Use this for initialization
	void Start ()
    {
        int activeinfo = Random.Range(0, info.Length);

        info[activeinfo].SetActive(true);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
