using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChallengeLine : MonoBehaviour {

    public Image Line;
    public GameObject Toy;
    public GameObject[] Dots;

    // Use this for initialization
    void Start()
    {
        GiftBoxLineUpdate();
    }

    public void GiftBoxLineUpdate()
    {
        if (!Configuration.instance.GiftBox)
        {
            gameObject.SetActive(false);
            return;
        }


        for (int i = 0; i < Dots.Length; i++)
        {
            if(Configuration.instance.GiftBoxWinNum == i)
            {
                Line.fillAmount = (float)i / Dots.Length;
                Toy.transform.SetParent(Dots[i].transform, false);
               
            }
           
        }

        //else
        //{
        //    gameObject.SetActive(false);

        //}

    }
    // Update is called once per frame
    void Update()
    {

    }
}
