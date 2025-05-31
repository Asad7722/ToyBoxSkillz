using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemBoxPopup : MonoBehaviour {
    public Text GemBoxInfoText, GemboxGemsMapAmountText;
    public Button PaidTurnButton;

    // Use this for initialization
    void Start()
    {
        ShowTurnButtons();
        updateGemsAmount();
    }
    void Update()
    {
        ShowTurnButtons();
    }
  
    private void ShowTurnButtons()
    {
        int PlayerCoins = CoreData.instance.GetPlayerCoin();
        int CurrentGems = PlayerPrefs.GetInt("GemBoxAmount", 0);
        int gemboxminamount = Configuration.instance.gemboxMinAmount;
        // If haven't free turn


        ShowPaidTurnButton();
        if (CurrentGems < gemboxminamount)
        {
            DisablePaidTurnButton(); // Make button non interactable if user has not enough money for the turn of if wheel is turning right now
            Language.StartGlobalTranslateWord(GemBoxInfoText, "The number of stones you need to collect to get the box: ", gemboxminamount, "");

        }
        else
        {
            EnablePaidTurnButton();           // Can make paid turn right now
            Language.StartGlobalTranslateWord(GemBoxInfoText, "Buy the GEM BOX for your collected gems!", 0, "");

        }

    }
    public void updateGemsAmount()
    {
        int CurrentGems = PlayerPrefs.GetInt("GemBoxAmount", 0);
        GemboxGemsMapAmountText.text = "" + CurrentGems;
    }
    private void EnableButton(Button button)
    {
        button.interactable = true;
        //button.GetComponent<Image> ().color = new Color(255, 255, 255, 1f);
    }

    private void DisableButton(Button button)
    {
        button.interactable = false;
        //button.GetComponent<Image> ().color = new Color(255, 255, 255, 0.5f);
    }

    // Function for more readable calls   
    private void EnablePaidTurnButton() { EnableButton(PaidTurnButton); }
    private void DisablePaidTurnButton() { DisableButton(PaidTurnButton); }

    private void ShowFreeTurnButton()
    {
       PaidTurnButton.gameObject.SetActive(true);
    }

    private void ShowPaidTurnButton()
    {
        PaidTurnButton.gameObject.SetActive(true);       
    }
}
	
	
	
