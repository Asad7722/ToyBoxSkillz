using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreMovesReward : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Configuration.instance.MoreMovesReward)
        {
            if(Configuration.instance.LimitedMoreMovesReward)
            {
                if(!itemGrid.RewardMoreMoveUsed)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
               

            }else
            {
                gameObject.SetActive(true);
            }
           
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
