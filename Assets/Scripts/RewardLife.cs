using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardLife : MonoBehaviour {
    public GameObject EmptyBoxTextObject;
	// Use this for initialization
	void Start ()
    {
        UpdateRewardList();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateRewardList()
    {
        int RewardLifeCount = NewLife.instance._Rewardlives;
        //  GameObject RewardLifeList = null;
        //  RewardLifeList = GameObject.Find("RewardLifeList");
        if(RewardLifeCount>0)
        {
            EmptyBoxTextObject.SetActive(false);
            string RewardLifeNode = "Prefabs/Map/RewardLifeNode";
            for (int i = 0; i < RewardLifeCount; i++)
            {

                var RewardNode = Instantiate(Resources.Load(RewardLifeNode)) as GameObject;
                RewardNode.transform.SetParent(gameObject.transform, false);
            }

        }
        else
        {
            EmptyBoxTextObject.SetActive(true);
        }
        
    }
}
