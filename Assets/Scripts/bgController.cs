using UnityEngine;
using System.Collections;


public class bgController : MonoBehaviour {


    SpriteRenderer current_Bgsprite;

    public Sprite[] levels_Bgsprite;
   
    // Use this for initialization
    void Start () {
        if (CoreData.instance.openedLevel == 1)
        {
            Debug.Log("firstlevel");
            
        }
        else
        {                  
            current_Bgsprite = GetComponent<SpriteRenderer>();          
            changeBGSprite();
        }
    }

    void changeBGSprite(){

        int episode = Configuration.instance.episode;
        int[] EpisodesRange = Configuration.instance.EpisodesRange;      
        int CurrentLevel = 0;
        for (int i = 1; i < episode; i++)
        {                        
            CurrentLevel += EpisodesRange[i];      

            if (CurrentLevel < StageLoader.instance.Stage)
            {
                current_Bgsprite.sprite = levels_Bgsprite[i];              
            }           
        }    
    }
}
