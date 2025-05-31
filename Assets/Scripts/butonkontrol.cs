using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butonkontrol : MonoBehaviour {
    public string KeyName;

    // Use this for initialization
    void Start () {
    
        int Alinmis = PlayerPrefs.GetInt(KeyName);
        int openedLevel = CoreData.instance.GetOpendedLevel();

        if (Alinmis == 1 || openedLevel <= 12)
        {
            gameObject.SetActive(false);
        }
        else 
        {
            gameObject.SetActive(true);
        }
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
