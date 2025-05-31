using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDataWWW : MonoBehaviour 
{
    public static GameDataWWW instance = null;

    [Header("Data")]
    public int playerCoin;
    public int openedLevel;

    [Header("")]
    public int singleBreaker;
    public int rowBreaker;
    public int columnBreaker;
    public int rainbowBreaker;
    public int ovenBreaker;

    [Header("")]
    public int beginFiveMoves;
    public int beginRainbow;
    public int beginBombBreaker;

    public List<Dictionary<string, object>> levelStatistics = new List<Dictionary<string, object>>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (LoadGameDataWWW() == null)
        {
            SaveGameDataWWW(PrepareGameDataWWW());

            return;
        }
    }

    #region Load

    string LoadGameDataWWW()
    {
        return "";
    }

    #endregion

    #region Save

    void SaveGameDataWWW(string jsonString)
    {
    }

    string PrepareGameDataWWW()
    {
        return "";
    }

    #endregion
}
