using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyCollect : MonoBehaviour {
    public PopupOpener ToyCollectionPopup;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenToyCollection()
    {
        ToyCollectionPopup.OpenPopup();
    }
}
