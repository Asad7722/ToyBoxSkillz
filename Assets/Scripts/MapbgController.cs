using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapbgController : MonoBehaviour {

    

    Image current_Bgsprite;

	public Sprite[] levels_Bgsprite;   

    // Use this for initialization
    void Start () {

          current_Bgsprite = GetComponent<Image> ();
        // background.sprite = currentBackgroundSprite;

        changeBGSprite ();
	}

	void changeBGSprite(){
        int openedLevel = CoreData.instance.GetOpendedLevel();
	
		if (openedLevel <= 20) {                       
            current_Bgsprite.sprite = levels_Bgsprite [0];
		}
		if (openedLevel >= 21 && openedLevel <= 40) {

			current_Bgsprite.sprite = levels_Bgsprite [1];
		}
		if (openedLevel >= 40 && openedLevel <= 60) {

			current_Bgsprite.sprite = levels_Bgsprite [2];
		}
		if (openedLevel >= 60 && openedLevel <= 80) {

			current_Bgsprite.sprite = levels_Bgsprite [3];
		}
		if (openedLevel >= 80 && openedLevel <= 100) {

			current_Bgsprite.sprite = levels_Bgsprite [4];
		}
		if (openedLevel >= 100 && openedLevel <= 120) {

			current_Bgsprite.sprite = levels_Bgsprite [5];
		}
		if (openedLevel >= 120 && openedLevel <= 140) {

			current_Bgsprite.sprite = levels_Bgsprite [6];
		}
		if (openedLevel >= 140 && openedLevel <= 160) {

			current_Bgsprite.sprite = levels_Bgsprite [7];
		}
        if (openedLevel >= 160 && openedLevel <= 180)
        {

            current_Bgsprite.sprite = levels_Bgsprite[8];
        }
        if (openedLevel >= 180 && openedLevel <= 200)
        {

            current_Bgsprite.sprite = levels_Bgsprite[9];
        }
        if (openedLevel >= 200 && openedLevel <= 220)
        {

            current_Bgsprite.sprite = levels_Bgsprite[10];
        }
        if (openedLevel >= 220 && openedLevel <= 240)
        {

            current_Bgsprite.sprite = levels_Bgsprite[11];
        }
        if (openedLevel >= 240 && openedLevel <= 260)
        {

            current_Bgsprite.sprite = levels_Bgsprite[12];
        }
        if (openedLevel >= 260 && openedLevel <= 280)
        {

            current_Bgsprite.sprite = levels_Bgsprite[13];
        }
        if (openedLevel >= 280 && openedLevel <= 300)
        {

            current_Bgsprite.sprite = levels_Bgsprite[14];
        }
        if (openedLevel >= 300 && openedLevel <= 320)
        {

            current_Bgsprite.sprite = levels_Bgsprite[15];
        }
      /*  if (StageLoader.instance.Stage >= 152 && StageLoader.instance.Stage <= 165)
        {

            current_Bgsprite.sprite = levels_Bgsprite[16];
        }
        if (StageLoader.instance.Stage >= 165 && StageLoader.instance.Stage <= 179)
        {

            current_Bgsprite.sprite = levels_Bgsprite[17];
        }
        if (StageLoader.instance.Stage >= 179 && StageLoader.instance.Stage <= 192)
        {

            current_Bgsprite.sprite = levels_Bgsprite[18];
        }
        if (StageLoader.instance.Stage >= 192 && StageLoader.instance.Stage <= 202)
        {

            current_Bgsprite.sprite = levels_Bgsprite[19];
        }
        if (StageLoader.instance.Stage >= 202 && StageLoader.instance.Stage <= 213)
        {

            current_Bgsprite.sprite = levels_Bgsprite[20];
        }
        if (StageLoader.instance.Stage >= 213 && StageLoader.instance.Stage <= 225)
        {

            current_Bgsprite.sprite = levels_Bgsprite[21];
        }
        if (StageLoader.instance.Stage >= 225 && StageLoader.instance.Stage <= 238)
        {

            current_Bgsprite.sprite = levels_Bgsprite[22];
        }
        if (StageLoader.instance.Stage >= 238 && StageLoader.instance.Stage <= 252)
        {

            current_Bgsprite.sprite = levels_Bgsprite[23];
        }
        if (StageLoader.instance.Stage >= 252 && StageLoader.instance.Stage <= 263)
        {

            current_Bgsprite.sprite = levels_Bgsprite[24];
        }
        if (StageLoader.instance.Stage >= 263 && StageLoader.instance.Stage <= 273)
        {

            current_Bgsprite.sprite = levels_Bgsprite[25];
        }
        if (StageLoader.instance.Stage >= 273 && StageLoader.instance.Stage <= 284)
        {

            current_Bgsprite.sprite = levels_Bgsprite[26];
        }
        if (StageLoader.instance.Stage >= 284 && StageLoader.instance.Stage <= 295)
        {

            current_Bgsprite.sprite = levels_Bgsprite[27];
        }
        if (StageLoader.instance.Stage >= 295 && StageLoader.instance.Stage <= 301)
        {

            current_Bgsprite.sprite = levels_Bgsprite[28];
        }*/
        
    }

	// Update is called once per frame
	void Update () {
	
	}
}
