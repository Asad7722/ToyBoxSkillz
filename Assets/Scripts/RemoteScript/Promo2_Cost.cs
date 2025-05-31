using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Promo2_Cost : MonoBehaviour {

    public Text promo2_cost;
    public Text Gems_1200_cost;

    // Use this for initialization
    void Start () {

        promo2_cost.text = Configuration.instance.promo2_cost; 
        Gems_1200_cost.text = Configuration.instance.Gems_1200_cost; 
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
