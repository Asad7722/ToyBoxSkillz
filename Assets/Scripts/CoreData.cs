using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class CoreData : MonoBehaviour
{
    public static CoreData instance = null;
    public static string STORE_PURCHASED_REMOVE_ADS = "Purchased Remove Ads";


    [Header("Player & Levels Data")]
    public int playerCoin; //Coin initial value
    public int openedLevel;
    public int playerStars;
    public int playerPuan;
    public int giftAmount;
    public int toplamScore;
    public bool Loaded;


    [Header("Breaker Values")]
    public int singleBreaker;
    public int rowBreaker;
    public int columnBreaker;
    public int rainbowBreaker;
    public int ovenBreaker;

    [Header("Power Ups Value")]
    public int beginFiveMoves;
    public int beginRainbow;
    public int beginBombBreaker;   

    public List<Dictionary<string, object>> levelStatistics = new List<Dictionary<string, object>>();

    List<string> readPermission = new List<string>() { "public_profile", "user_friends", "user_games_activity" },
          publishPermission = new List<string>() { "publish_actions" };

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

        if (LoadGameData() == null)
        {
            SaveGameData(PrepareGameData());

            return;
        }

    }

    #region Load

    string LoadGameData()
    {

        if (File.Exists(Application.persistentDataPath + "/" + Configuration.game_data))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + Configuration.game_data, FileMode.Open);

            string jsonString = (string)bf.Deserialize(file);

            file.Close();

            Dictionary<string, object> dict = Json.Deserialize(jsonString) as Dictionary<string, object>;

            playerCoin = int.Parse(dict[Configuration.player_coin].ToString());
            openedLevel = int.Parse(dict[Configuration.opened_level].ToString());
            openedLevel = (openedLevel > 0) ? openedLevel : 1;
            singleBreaker = int.Parse(dict[Configuration.single_breaker].ToString());
            rowBreaker = int.Parse(dict[Configuration.row_breaker].ToString());
            columnBreaker = int.Parse(dict[Configuration.column_breaker].ToString());
            rainbowBreaker = int.Parse(dict[Configuration.rainbow_breaker].ToString());
            ovenBreaker = int.Parse(dict[Configuration.oven_breaker].ToString());
            beginFiveMoves = int.Parse(dict[Configuration.begin_five_moves].ToString());
            beginRainbow = int.Parse(dict[Configuration.begin_rainbow].ToString());
            beginBombBreaker = int.Parse(dict[Configuration.begin_bomb_breaker].ToString());
                       
            List<object> list = (List<object>)dict[Configuration.level_statistics];
            foreach (object t in list)
            {
                Dictionary<string, object> d = (Dictionary<string, object>)t;

                levelStatistics.Add(d);
            }

            playerStars = int.Parse(dict[Configuration.player_stars].ToString());
            playerPuan = int.Parse(dict[Configuration.player_puan].ToString());
            giftAmount = int.Parse(dict[Configuration.gift_amount].ToString());
            toplamScore = int.Parse(dict[Configuration.toplam_score].ToString());           
            return jsonString;
        }

        return null;
    }

    #endregion

    #region Save

    void SaveGameData(string jsonString)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/" + Configuration.game_data);

        bf.Serialize(file, jsonString);

        file.Close();       
    }

    string PrepareGameData()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();

      if (openedLevel == 0) openedLevel = 1;

        dict.Add(Configuration.player_coin, playerCoin);
        dict.Add(Configuration.opened_level, openedLevel);
        dict.Add(Configuration.single_breaker, singleBreaker);
        dict.Add(Configuration.row_breaker, rowBreaker);
        dict.Add(Configuration.column_breaker, columnBreaker);
        dict.Add(Configuration.rainbow_breaker, rainbowBreaker);
        dict.Add(Configuration.oven_breaker, ovenBreaker);
        dict.Add(Configuration.begin_five_moves, beginFiveMoves);
        dict.Add(Configuration.begin_rainbow, beginRainbow);
        dict.Add(Configuration.begin_bomb_breaker, beginBombBreaker);
        dict.Add(Configuration.player_stars, playerStars);
        dict.Add(Configuration.player_puan, playerPuan);
        dict.Add(Configuration.level_statistics, levelStatistics);
        dict.Add(Configuration.gift_amount, giftAmount);

        dict.Add(Configuration.toplam_score, toplamScore);

        return Json.Serialize(dict);
    }

    #endregion

    #region Level

    public int GetOpendedLevel()
    {
        return openedLevel;
    }

   

    public int GetLevelScore(int level)
    {
        foreach (Dictionary<string, object> statistics in levelStatistics)
        {
            if (int.Parse(statistics[Configuration.level_number].ToString()) == level)
            {
                return int.Parse(statistics[Configuration.level_score].ToString());
            }
        }

        return 0;
    }

    public int GetLevelStar(int level)
    {
        foreach (Dictionary<string, object> statistics in levelStatistics)
        {
            if (int.Parse(statistics[Configuration.level_number].ToString()) == level)
            {
                return int.Parse(statistics[Configuration.level_star].ToString());
            }
        }

        return 0;
    }

    public void SaveLevelStatistics(int level, int score, int star)
    {
        foreach (Dictionary<string, object> statistics in levelStatistics)
        {
            if (int.Parse(statistics[Configuration.level_number].ToString()) == level)
            {
                // only update if new score/star is greater then the old one
                if (int.Parse(statistics[Configuration.level_score].ToString()) < score)
                {
                    statistics[Configuration.level_score] = score;
                }

                if (int.Parse(statistics[Configuration.level_star].ToString()) < star)
                {
                    statistics[Configuration.level_star] = star;
                }

                SaveGameData(PrepareGameData());

                return;
            }
        }

        // if don't find a old record then create a new one
        Dictionary<string, object> stats = new Dictionary<string, object>();

        stats.Add(Configuration.level_number, level);
        stats.Add(Configuration.level_score, score);
        stats.Add(Configuration.level_star, star);

        levelStatistics.Add(stats);

        SaveGameData(PrepareGameData());
    }

    #endregion

    #region Data

    public int GetPlayerCoin()
    {
        return playerCoin;
    }

    public int GetPlayerStars()
    {
        return playerStars;
    }
    public int GetPlayerPuan()
    {
        return playerPuan;
    }
    public int GetGiftAmount()
    {
        return giftAmount;
    }
    public int GetToplamScore()
    {
        return toplamScore;
    }

    public void SaveGiftAmount(int gamount)
    {
        giftAmount = gamount;

        SaveGameData(PrepareGameData());
    }
    public void SaveOpendedLevel(int level)
    {
        openedLevel = level;

        SaveGameData(PrepareGameData());
      

    }
    public void SavePlayerCoin(int coin)
    {
        playerCoin = coin;

        SaveGameData(PrepareGameData());
       
    }  
    public void SaveToplamScore(int topscore)
    {
        toplamScore = topscore;

        SaveGameData(PrepareGameData());
      
    }

    public void SavePlayerStars(int stars)
    {
        playerStars = stars;

        SaveGameData(PrepareGameData());
    }
    public void SavePlayerPuan(int puan)
    {
        playerPuan = puan;

        SaveGameData(PrepareGameData());
    }
  

    public int GetBeginFiveMoves()
    {
        return beginFiveMoves;
    }

    public void SaveBeginFiveMoves(int number)
    {
        beginFiveMoves = number;

        SaveGameData(PrepareGameData());
    }

    public int GetBeginRainbow()
    {
        return beginRainbow;
    }

    public void SaveBeginRainbow(int number)
    {
        beginRainbow = number;

        SaveGameData(PrepareGameData());
    }

    public int GetBeginBombBreaker()
    {
        return beginBombBreaker;
    }

    public void SaveBeginBombBreaker(int number)
    {
        beginBombBreaker = number;

        SaveGameData(PrepareGameData());
    }

    public int GetSingleBreaker()
    {
        return singleBreaker;
    }

    public void SaveSingleBreaker(int number)
    {
        singleBreaker = number;

        SaveGameData(PrepareGameData());
    }

    public int GetRowBreaker()
    {
        return rowBreaker;
    }

    public void SaveRowBreaker(int number)
    {
        rowBreaker = number;

        SaveGameData(PrepareGameData());
    }

    public int GetColumnBreaker()
    {
        return columnBreaker;
    }

    public void SaveColumnBreaker(int number)
    {
        columnBreaker = number;

        SaveGameData(PrepareGameData());
    }

    public int GetRainbowBreaker()
    {
        return rainbowBreaker;
    }

    public void SaveRainbowBreaker(int number)
    {
        rainbowBreaker = number;

        SaveGameData(PrepareGameData());
    }

    public int GetOvenBreaker()
    {
        return ovenBreaker;
    }

    public void SaveOvenBreaker(int number)
    {
        ovenBreaker = number;

        SaveGameData(PrepareGameData());
    }
    public void PrintLog(string msg)
    {
        if (msg.Length > 3000)
        {
            print(msg);

        }
        else
        {
            print(msg);

        }
    }

    #endregion
}
