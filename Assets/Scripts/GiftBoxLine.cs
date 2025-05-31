using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftBoxLine : MonoBehaviour {

    public Image Line;
    public GameObject Toy;
    public GameObject[] Dots;
    


	// Use this for initialization
	void Start ()
    {
        GiftBoxLineUpdate();
    }
	
	public void GiftBoxLineUpdate()
    {
        if(Configuration.instance.ActiveFreeBoxesCount>0 || CoreData.instance.openedLevel<7)
        {
            gameObject.SetActive(false);
            return;
        }

       if(Configuration.instance.WinLevel <3)
        {
            for (int i = 0; i < Dots.Length; i++)
            {
                if (Configuration.instance.WinLevel == i)
                {
                    Line.fillAmount = (float)i / Dots.Length;
                    Toy.transform.SetParent(Dots[i].transform, false);

                }

            }

        }
        else
        {
            Line.fillAmount = 1f;
            Toy.transform.SetParent(Dots[2].transform, false);
        }

        //if (Configuration.instance.WinLevel == 0)
        //{
        //    Line.fillAmount = 0f;
        //    Toy.transform.localPosition = new Vector3(-180f, 2f, 0);
        //}
        //else if (Configuration.instance.WinLevel == 1)
        //{
        //    Line.fillAmount = 0f;
        //    Toy.transform.localPosition = new Vector3(-147f, 2f, 0);
        //}
        //else if (Configuration.instance.WinLevel == 2)
        //{
        //    Line.fillAmount = 0.5f;
        //    Toy.transform.localPosition = new Vector3(0f, 2f, 0);
        //}
        //else if (Configuration.instance.WinLevel >= 3)
        //{
        //    Line.fillAmount = 1f;
        //    Toy.transform.localPosition = new Vector3(146.2f, 2f, 0);
        //}

    }
    // Update is called once per frame
	void Update () {
		
	}
}
