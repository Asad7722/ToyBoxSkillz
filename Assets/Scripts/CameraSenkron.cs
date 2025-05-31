using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSenkron : MonoBehaviour {   
	// Use this for initialization
	void Start () {

        Invoke("startCameraSenkron", 1.0f);
		
	}
	
	// Update is called once per frame
	void Update () {
      
    }
    public void startCameraSenkron()
    {
        try { GetComponent<Camera>().orthographicSize = CameraSize.CameraCurrentSize; }// - 0.5f; }
        catch {}
    }
}
