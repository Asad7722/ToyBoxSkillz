using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBox : MonoBehaviour {
    public static LevelBox instance = null;

    public int levelsayisi;
    public GameObject levelinfopen;
    public GameObject box;
    public GameObject disablekutu;
    public GameObject kapak;
    public Text levelinfotext;
    public int openedLevel;
   // public Text levellabel;
    


	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
    
    public void showlevelinfopopup()
    {
        //levelsayisihesapla();
       // Language.StartGlobalTranslateWord(levellabel, "Reach level ", levelsayisi, "");
        // StarLanguage(3, levelsayisi, "");
        // levelinfotext.text = "Reach level " + levelsayisi + " to get the Level Toy Box!";
        StartCoroutine(levelinfopopup());
        AudioManager.instance.ButtonClickAudio();

    }
    public void levelsayisihesapla()
    {
        openedLevel = CoreData.instance.openedLevel;
        if (PlayerPrefs.GetInt("levelgift alindi" + openedLevel, 1) == 1)
        {
            if (openedLevel <= levelsayisi)
            {
                disablekutu.gameObject.SetActive(false);
                box.gameObject.SetActive(true);
                kapak.gameObject.SetActive(true);

            }
            else if (openedLevel > levelsayisi)
            {
                disablekutu.gameObject.SetActive(true);
                box.gameObject.SetActive(false);
                kapak.gameObject.SetActive(false);
            }


        }
        if (openedLevel < 10)
        {
            levelsayisi = 10;
           // Language.StartGlobalTranslateWord(levellabel, "Reach level ", levelsayisi, "");
            //StarLanguage(3, levelsayisi, "");
            //levellabel.text = "Reach level " + levelsayisi;
        }
        else
        {
            int ShowLevelBoxEvery = Configuration.instance.ShowLevelBoxEvery;
            int levelboxinfogoster = openedLevel % ShowLevelBoxEvery;           
            Debug.Log("levelboxgoster :" + levelboxinfogoster);

            if (levelboxinfogoster > 0)
            {
                levelsayisi = openedLevel + (ShowLevelBoxEvery - levelboxinfogoster);
               // Language.StartGlobalTranslateWord(levellabel, "Reach level ", levelsayisi, "");

                // StarLanguage(3, levelsayisi, "");
                //levellabel.text = "Reach level " + levelsayisi;
            }
            else if (levelboxinfogoster == 0)
            {
                levelsayisi = openedLevel;
                //Language.StartGlobalTranslateWord(levellabel, "Reach level ", levelsayisi, "");

                // StarLanguage(3, levelsayisi, "");
                //levellabel.text = "Reach level " + levelsayisi;
            }
        }   
    }   
    IEnumerator levelinfopopup()
    {
        levelsayisihesapla();
        //yield return new WaitForSeconds(0.5f);
        //Language.StartGlobalTranslateWord(levellabel, "Reach level ", levelsayisi, "");

        //open info popup        
        Configuration.OpenInfoPopup("Level Gift Box", Language.TranslateWord("Reach level") +" "+ levelsayisi.ToString());
        

        //levelinfopen.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);       
        //levelinfopen.gameObject.SetActive(false);

    }
  
   
}
