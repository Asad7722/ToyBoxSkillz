using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Promo0_Cost : MonoBehaviour {
    public Text promo0_cost;
   // public Text Pack4_cost;

    // Use this for initialization
    void Start()
    {

        promo0_cost.text = Configuration.instance.promo0_cost; 
        //Pack4_cost.text = Configuration.instance.Pack4_cost; 

    }
}
