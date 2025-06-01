using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UI.Pagination;
using UnityEngine.Analytics;


public class LifePopup : MonoBehaviour
{
    
    public Text lifeRemain;
    public Text recoveryCost;
    public GameObject recoveryButton;

    public static LifePopup instance;

    int cost;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;

        if(Configuration.instance.recoveryCostPerLife == 0)
        {
            Configuration.instance.recoveryCostPerLife = 20;
        }

        if (NewLife.instance.lives < NewLife.instance.maxLives)
        {
            recoveryButton.SetActive(true);
            lifeRemain.text = "" + NewLife.instance.lives.ToString() + "/" + NewLife.instance.maxLives.ToString();
            cost = Configuration.instance.recoveryCostPerLife * (NewLife.instance.maxLives - NewLife.instance.lives);
            recoveryCost.text = cost.ToString(); ;
        }
        else
        {
            lifeRemain.text = "" + NewLife.instance.maxLives.ToString() + "/" + NewLife.instance.maxLives.ToString();
            recoveryButton.SetActive(false);
            recoveryCost.gameObject.transform.parent.gameObject.SetActive(false);
        }
        updatelife();
        


    }


    public void updatelife()
    {
        if (Configuration.instance.recoveryCostPerLife == 0)
        {
            Configuration.instance.recoveryCostPerLife = 20;
        }

        if (NewLife.instance.lives < NewLife.instance.maxLives)
        {
            recoveryButton.SetActive(true);
            lifeRemain.text = "" + NewLife.instance.lives.ToString() + "/" + NewLife.instance.maxLives.ToString();
            cost = Configuration.instance.recoveryCostPerLife * (NewLife.instance.maxLives - NewLife.instance.lives);
            recoveryCost.text = cost.ToString(); ;
        }
        else
        {
            lifeRemain.text = "" + NewLife.instance.maxLives.ToString() + "/" + NewLife.instance.maxLives.ToString();
            recoveryButton.SetActive(false);
            recoveryCost.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
    public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }
    public void DisableAll()
    {
        Configuration.instance.RandomPlay = false;
        Configuration.instance.GiftBox = false;
        Configuration.instance.ArenaMode = false;
        Configuration.instance.EpisodePlay = false;
        Configuration.instance.playing = false;
        Configuration.instance.RewardPlay = false;
        Configuration.instance.OneTimePlay = false;
    }
    public void RecoveryButtonClick()
    {
        //
        if (CoreData.instance.playerCoin < cost)
        {
            // open shop popup
            //GameObject.Find("MapScene").GetComponent<MapScene>().CoinButtonClick();           
            StartCoroutine(Goshop());
            
            



        }
        else
        {
            // reduce coin and refill life
            CoreData.instance.SavePlayerCoin(CoreData.instance.GetPlayerCoin() - cost);

            // play add coin sound
            AudioManager.instance.CoinPayAudio();

            // update text label
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();

            // update life text
            NewLife.instance.lives = NewLife.instance.maxLives;

            // update life pupup text
            lifeRemain.text = "" + NewLife.instance.maxLives.ToString() + "/" + NewLife.instance.maxLives.ToString();
            recoveryButton.SetActive(false);
            recoveryCost.gameObject.transform.parent.gameObject.SetActive(false);
         
            

        }
    }

    public void odullureklamgoster()
    {
        //sameer
        //admanager.instance.ShowRewardedVideAdGeneric(8);
        //AdsManager.instance.ShowReward();
    }

    IEnumerator Goshop()
    {
        yield return new WaitForSeconds(0.5f);
        //try
        //{
        //    GameObject.Find("MAPMAIN").GetComponent<PagedRect>().SetCurrentPage(2);
        //}
        //catch { }

        //yield return new WaitForSeconds(0.2f);
        //try
        //{
        //    GameObject.Find("LifePopup(Clone)").GetComponent<Popup>().Close();
        //}
        //catch { }
        //yield return new WaitForSeconds(0.2f);
        //try
        //{
        //    GameObject.Find("LevelPlay(Clone)").GetComponent<Popup>().Close();
        //}
        //catch { }
        MapScene.instance.page2();


    }

    public void RewardedAdCompletedHandler()
    {
        //sameerrewardcomplete
        //REWARD
        AudioManager.instance.ButtonClickAudio();
        Configuration.instance.OneTimePlay = true;
        Transition.LoadLevel("Play", 0.1f, Color.black);
      
        
        //REWARD 
        //AdsManager.LoadReward();
        //achievement
        Configuration.SaveAchievement("ach_watchAds", 1);
    }
   

}