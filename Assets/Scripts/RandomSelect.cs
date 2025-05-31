using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour {

    public GameObject[] node;
	// Use this for initialization
	void Start ()
    {
      node[Random.Range(0, node.Length)].SetActive(true);         
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
