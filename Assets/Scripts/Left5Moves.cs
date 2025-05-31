using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left5Moves : MonoBehaviour {

    private bool animated;


    // Use this for initialization
    void Start()
    {
       //gameObject.GetComponent<Animator>().

    }

    // Update is called once per frame
    void Update () 
    {
		if(itemGrid.instance.moveLeft <=5 && !animated && !itemGrid.instance.GameOver && !itemGrid.instance.merging && !itemGrid.instance.huntering)
        {
            animated = true;
          
            
            Configuration.OpenInfoPopupAutoClose("", "Last 5 Moves!",2);

           
            gameObject.GetComponent<Animator>().Play("leftmoves");

            CameraSize.instance.MinShake = true;

        }
    }
}
