using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaMap : MonoBehaviour
{
    public static ArenaMap instance = null;
    public Button[] ARENA;
    public Text[] GiftAmountText;
    public Text[] GerekliStarText;



    // Use this for initialization

    void Start()
    {
        for (int i = 1; i < 12; i++)
        {
            ARENA[i].interactable = false;
        }
        Invoke("ArenaMapGoster", 1.2f);
    }

    public void ArenaMapGoster()
    {
        StartCoroutine(updateArenaMap());
    }

    public void UpdateLabel()
    {

        for (int i = 1; i < 12; i++)
        {
            int GiftAmount = Configuration.instance.GiftAmountText[i];
            int GerekliStar = Configuration.instance.GerekliStarText[i];
            GiftAmountText[i].text = "" + GiftAmount;
            GerekliStarText[i].text = "" + GerekliStar + "+";
        }



    }

    IEnumerator updateArenaMap()
    {
        yield return new WaitForSeconds(0.2f);
        int arenanumber = PlayerPrefs.GetInt("arenanumber");

        for (int i = 1; i <= arenanumber; i++)
        {
            ARENA[i].interactable = true;
        }

        yield return new WaitForSeconds(0.2f);

        UpdateLabel();

    }

    public void close()
    {
        gameObject.SetActive(false);
        AudioManager.instance.ButtonClickAudio();
    }
}