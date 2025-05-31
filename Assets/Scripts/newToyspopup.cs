using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class newToyspopup : MonoBehaviour {

    SpriteRenderer current_Bgsprite;

    public Sprite[] levels_Bgsprite;

    // Use this for initialization
   
    void Start () {
        current_Bgsprite = GetComponent<SpriteRenderer>();
        changeBGSprite();
       

    }
    void changeBGSprite()
    {
       int gift = Configuration.instance.gift1;      
       current_Bgsprite.sprite = levels_Bgsprite[gift];
        
      

    }

    // Update is called once per frame
    public void zipla()
    {
        AudioManager.instance.DropAudio();
    }
   
}
