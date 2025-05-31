using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomAppreciation : MonoBehaviour
{
    //public Appreciation _type;
    //[SerializeField] List<Sprite> sprites;
    //int i;
    
    private void Start()
    {
        //var v = this.gameObject
        Destroy(gameObject, 1f);//0.75f ali
        //i = Random.Range(0, sprites.Count);
        //switch (_type)
        //{
        //    case Appreciation.Divine:
        //        AudioManager.instance.FantasticSound(i);
        //        break;
        //    case Appreciation.Good:
        //        AudioManager.instance.GoodSound(i);
        //        break;
        //}
        //transform.GetChild(0).GetComponent<Image>().sprite = sprites[i];
    }
}
//public enum Appreciation
//{
//    Divine,
//    Good
//}