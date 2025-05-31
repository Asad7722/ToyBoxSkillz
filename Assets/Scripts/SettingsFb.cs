using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Settings.
/// </summary>
public class SettingsFb : MonoBehaviour
{
    public Image MenuButtonImage;
    public Sprite MenuOpenedSprite;
    public Sprite MenuClosedSprite;
    public GameObject SettingContent;
    public PopupOpener EpisodePrefab;
    bool isMenuOpened = false;
    bool isGameMenuOpened = false;
    bool isEpisodesGameMenuOpened = false;




    public static SettingsFb instance;

    void Start()
    {
        instance = this;
    }

    /// <summary>
    /// Raises the menu button pressed event.
    /// </summary>
    public void OnMenuButtonPressed()
    {
        if (InputManager.instance.canInput(1F))
        {
            AudioManager.instance.PlayButtonClickSound();
            if (!isMenuOpened)
            {
                OpenMenu();
                return;
            }
            else
            {
                CloseMenu();
                return;
            }
            //GameController.instance.OnBackButtonPressed ();
        }
    }

    public void OnGameMenuButtonPressed()
    {
        if (InputManager.instance.canInput(1F))
        {
            AudioManager.instance.PlayButtonClickSound();
            if (!isGameMenuOpened)
            {
                OpenGameMenu();
                return;
            }
            else
            {
                CloseGameMenu();
                return;
            }
            //GameController.instance.OnBackButtonPressed ();
        }
    }
    public void EpisodesMenuButtonPressed()
    {
        AudioManager.instance.PlayButtonClickSound();
        EpisodePrefab.OpenPopup();
        //if (InputManager.instance.canInput(1F))
        //{
        //    AudioManager.instance.PlayButtonClickSound();
        //    if (!isEpisodesGameMenuOpened)
        //    {
        //        OpenEpisodesMenu();

        //        return;
        //    }
        //    else
        //    {
        //        CloseEpisodesMenu();
        //        return;
        //    }
        //    //GameController.instance.OnBackButtonPressed ();
        //}
    }
    /// <summary>
    /// Opens the menu.
    /// </summary>
    void OpenMenu()
    {
        isMenuOpened = true;
        MenuButtonImage.sprite = MenuOpenedSprite;
        GetComponent<Animator>().Play("Open-Settings");
    }
    public void CloseMenu()
    {
        //AudioManager.instance.PlayButtonClickSound ();
        isMenuOpened = false;
        MenuButtonImage.sprite = MenuClosedSprite;
        GetComponent<Animator>().Play("Close-Settings");

    }
    void OpenGameMenu()
    {
        isGameMenuOpened = true;
        MenuButtonImage.sprite = MenuOpenedSprite;
        GetComponent<Animator>().Play("gameopen");
    }
    public void CloseGameMenu()
    {
        //AudioManager.instance.PlayButtonClickSound ();
        isGameMenuOpened = false;
        MenuButtonImage.sprite = MenuClosedSprite;
        GetComponent<Animator>().Play("gameclose");
    }
    void OpenEpisodesMenu()
    {
        isEpisodesGameMenuOpened = true;
        MenuButtonImage.sprite = MenuOpenedSprite;
        GetComponent<Animator>().Play("episodesmenuopen");
    }
    public void CloseEpisodesMenu()
    {
        // AudioManager.instance.PlayButtonClickSound ();
        isEpisodesGameMenuOpened = false;
        MenuButtonImage.sprite = MenuClosedSprite;
        GetComponent<Animator>().Play("episodesmenuclose");

    }

   
         
    
}