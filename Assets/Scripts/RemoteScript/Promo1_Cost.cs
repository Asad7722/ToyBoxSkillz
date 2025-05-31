using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Promo1_Cost : MonoBehaviour {
    public Text promo1_cost;
    public Text Pack2_cost;

    // Use this for initialization
    void Start()
    {
       promo1_cost.text = Configuration.instance.promo1_cost;
       Pack2_cost.text = Configuration.instance.Pack2_cost; 

    }
}
