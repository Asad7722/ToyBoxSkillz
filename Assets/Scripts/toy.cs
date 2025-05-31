using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class toy : MonoBehaviour {
    public static toy instance = null;
    public int ToyNumber;
    public GameObject Label;
    public Text LabelText;
    public GameObject GetGiftButton;
    public GameObject Addone;
    public GameObject GiftAmountPopup;
    public Text GiftAmountPopupText;
    public GameObject ToyBoxTarget;
    public Text ToyBoxTargetText;

    public int TargetToyAmount;
    public int GiftGemsAmount;
    public int Odul;

    public GameObject NewToyPopup;
    public Text NewToyPopupText;
    
    
    public int CurrentToy; 
    //public Image[] ToySprite;    
    public bool GiftButton;
    public bool Gift;
    public GameObject toyimagelocked;
    public GameObject toyimage;

    private float[] noiseValues;
    public GAME_STATE state;


    void changeToySprite()
    {

       // toyimagelocked.GetComponent<Image>().sprite = Resources.Load<Sprite>("Collectible/collectible_1_" + transform.GetSiblingIndex());
       // toyimage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Collectible/collectible_1_" + transform.GetSiblingIndex());


    }

    void Start()
    {
        instance = this;
        Gift = false;
        if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            Invoke("RandomGitHesapla", 2.0f);

        }
        else
        {
            updatetoys();
            // Invoke("RandomGitHesapla", 2.0f);
        }

        





    }

    public void RandomGitHesapla()
    {
        if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {               
            updatetoys();
            StartCoroutine(randomGift());
           
        }
        else if(state == GAME_STATE.PREPARING_LEVEL)
        {
         updatetoys();
         StartCoroutine(randomGift());      
        }
    }
    public void updatetoys()
    {
           
        //changeToySprite();

       
        LabelText.text = "" + (PlayerPrefs.GetInt("toy" + ToyNumber)) + " /" + TargetToyAmount;
        CurrentToy = PlayerPrefs.GetInt("" + "toy" + ToyNumber);
        if (CurrentToy >= TargetToyAmount)
        {
            toyimage.SetActive(true);
            toyimagelocked.SetActive(false);
            GiftButton = true;
        }
       if (CurrentToy > 0)
        {
            toyimagelocked.SetActive(false);
            toyimage.SetActive(true);
        }
        if (GiftButton == true)
        {
            GetGiftButton.SetActive(true);
            Label.SetActive(false);
        }
        else
        {
            GetGiftButton.SetActive(false);
            Label.SetActive(true);
        }
       
    }
    public void buy()
    {
        PlayerPrefs.SetInt("" + "toy" + ToyNumber, CurrentToy + 1);
        PlayerPrefs.Save();
      
        LabelText.text = "" + (PlayerPrefs.GetInt("toy" + ToyNumber)) + " /" + TargetToyAmount;
        AudioManager.instance.CollectibleExplodeAudio();
        Gift = false;
        Addone.SetActive(false);
        updatetoys();
        closeNewToy();
        Configuration.instance.giftnum = 0;
    }
   
    public void getgifts()
    {
        PlayerPrefs.SetInt("" + "toy" + ToyNumber, 0);
        PlayerPrefs.Save();
        LabelText.text = "" + (PlayerPrefs.GetInt("toy" + ToyNumber)) + " /" + TargetToyAmount;
        //CoreData.instance.SavePlayerCoin(CoreData.instance.playerCoin += GiftGemsAmount);
        Configuration.instance.OpenGiftBoxPopup("Gift Box", REWARD_TYPE.ToyCollectionReward);
        AnalyticsEvent.Custom("ToyCollectionsWinPopup");
        try
        {
            GameObject.Find("MapScene").GetComponent<MapScene>().UpdateCoinAmountLabel();
        }
        catch  {   }
       
        AudioManager.instance.CoinPayAudio();
        GiftButton = false;
        //showgiftamount();
        updatetoys();
        GetGiftButton.SetActive(false);
    }
    public void Giftlerisec()
    {

        StartCoroutine(randomGift());

    }
   

  IEnumerator randomGift()
    {
        int seed = Random.Range(1, 31);
        if (seed == ToyNumber)
        {
            Gift = true;
                yield return new WaitForSeconds(0.2f);
                Addone.SetActive(true);               
                yield return new WaitForSeconds(0.2f);
             //   Debug.Log("Gift " + seed);
                yield return new WaitForSeconds(0.2f);
                Configuration.instance.gift1 = ToyNumber;
                yield return new WaitForSeconds(0.2f);
                Configuration.instance.giftnum += 1;
                yield return new WaitForSeconds(0.2f);
                AudioManager.instance.CollectibleExplodeAudio();
               // NewToyBildir();              
                   // UIWinPopup.instance.Showtoybox();      

            }
            else if( Gift == false)
            {
                if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }
            yield return new WaitForEndOfFrame();

        buy();

    }

    public void showgiftamount()
    {
      
        StartCoroutine(giftamount());

    }
    IEnumerator giftamount()
    {
        GiftAmountPopup.gameObject.SetActive(true);
        GiftAmountPopupText.text = "+" + GiftGemsAmount;
        yield return new WaitForSeconds(3.0f);
        GiftAmountPopup.gameObject.SetActive(false);

    }
    public void ToyButton()
    {
        if (Gift == true)
        {
            buy();           
        }                  
        else
        {
          AudioManager.instance.ButtonClickAudio();
          StartCoroutine(ToyBoxTargettext());
        }
    }

    IEnumerator ToyBoxTargettext()
    {
        ToyBoxTarget.gameObject.SetActive(true);
        Language.StartGlobalTranslateWord(ToyBoxTargetText, "Collect toys open Gift Box!");
        //ToyBoxTargetText.text = "Collect " + TargetToyAmount + " toys and earn " + GiftGemsAmount + " gems";
        yield return new WaitForSeconds(3.0f);
        ToyBoxTarget.gameObject.SetActive(false);

    }
    public void NewToyBildir()
    {
    //    GoogleAnalyticsV4.instance.LogScreen("New toy");
       
        StartCoroutine(NewToygoster());

    }
    IEnumerator NewToygoster()
    {
        
        try
        {
            int giftsayisi = Configuration.instance.giftnum;
            NewToyPopup.gameObject.SetActive(true);
            Language.StartGlobalTranslateWord(NewToyPopupText, "New Toys: ", giftsayisi, "");

            //NewToyPopupText.text = "New Toy" + giftsayisi + " Toys! ";
        }
        catch { }
       
       // GetComponent<Animator>().Play("toyzipla");


       
        yield return new WaitForSeconds(3.0f);
        
        //NewToyPopup.gameObject.SetActive(false);
    }
    public void closeNewToy()
    {
        try { NewToyPopup.gameObject.SetActive(false); }
        catch { }
       
    }


}
