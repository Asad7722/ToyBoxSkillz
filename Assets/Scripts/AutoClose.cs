using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClose : MonoBehaviour {

public float second;
// Use this for initialization
    void Start () 
    {
        Invoke("DestroyObje",second);
    }
    
    // Update is called once per frame
    void Update () 
    {

    }

void DestroyObje()
{

Destroy(gameObject);
}




}
