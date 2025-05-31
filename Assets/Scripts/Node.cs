using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour 
{
    [Header("Variables")]
	public itemGrid grid;
    public Tile tile;
    public Item item;
    public Waffle waffle;
    public Cage cage;
    public GameObject ovenActive;    
    [Header("Row & Columns")]
    public int i; // row of node
    public int j; // column of node   
   
   
   

    #region Neighbor

    public Node LeftNeighbor()
    {
		return grid.GetNode(i, j - 1);
    }

    public Node RightNeighbor()
    {
		return grid.GetNode(i, j + 1);
    }

    public Node TopNeighbor()
    {
		return grid.GetNode(i - 1, j);
    }

    public Node BottomNeighbor()
    {
		return grid.GetNode(i + 1, j);
    }

    public Node TopLeftNeighbor()
    {
		return grid.GetNode(i - 1, j - 1);
    }

    public Node TopRightNeighbor()
    {
		return grid.GetNode(i - 1, j + 1);
    }

    public Node BottomLeftNeighbor()
    {
		return grid.GetNode(i + 1, j - 1);
    }

    public Node BottomRightNeighbor()
    {
		return grid.GetNode(i + 1, j + 1);
    }

    //Komsu TYPE

    public Node LeftKomsu()
    {
        return grid.GetNode(i, j - 1);
    }

    public Node RightKomsu()
    {
        return grid.GetNode(i, j + 1);
    }

    public Node TopKomsu()
    {
        return grid.GetNode(i - 1, j);
    }

    public Node BottomKomsu()
    {
        return grid.GetNode(i + 1, j);
    }
    #endregion

    #region Item

    // Some how the function does not return a object. 
    // It always return a null pointer.
    public Item GenerateItem(ITEM_TYPE type)
    {
        Item item = null;

        switch (type)
        {
            case ITEM_TYPE.ITEM_RAMDOM:
                GenerateRandomCookie();
                break;


            case ITEM_TYPE.BlueBox:
            case ITEM_TYPE.GreenBox:
            case ITEM_TYPE.ORANGEBOX:
            case ITEM_TYPE.PURPLEBOX:
            case ITEM_TYPE.REDBOX:
            case ITEM_TYPE.YELLOWBOX:
            case ITEM_TYPE.BREAKABLE:

            case ITEM_TYPE.MINE_1_LAYER:
            case ITEM_TYPE.MINE_2_LAYER:
            case ITEM_TYPE.MINE_3_LAYER:
            case ITEM_TYPE.MINE_4_LAYER:
            case ITEM_TYPE.MINE_5_LAYER:
            case ITEM_TYPE.MINE_6_LAYER:
            case ITEM_TYPE.ROCK_CANDY_1:
            case ITEM_TYPE.ROCK_CANDY_2:
            case ITEM_TYPE.ROCK_CANDY_3:
            case ITEM_TYPE.ROCK_CANDY_4:
            case ITEM_TYPE.ROCK_CANDY_5:
            case ITEM_TYPE.ROCK_CANDY_6:
            case ITEM_TYPE.COLLECTIBLE_1:
            case ITEM_TYPE.COLLECTIBLE_2:
            case ITEM_TYPE.COLLECTIBLE_3:
            case ITEM_TYPE.COLLECTIBLE_4:
            case ITEM_TYPE.COLLECTIBLE_5:
            case ITEM_TYPE.COLLECTIBLE_6:
            case ITEM_TYPE.COLLECTIBLE_7:
            case ITEM_TYPE.COLLECTIBLE_8:
            case ITEM_TYPE.COLLECTIBLE_9:
            case ITEM_TYPE.COLLECTIBLE_10:
            case ITEM_TYPE.COLLECTIBLE_11:
            case ITEM_TYPE.COLLECTIBLE_12:
            case ITEM_TYPE.COLLECTIBLE_13:
            case ITEM_TYPE.COLLECTIBLE_14:
            case ITEM_TYPE.COLLECTIBLE_15:
            case ITEM_TYPE.COLLECTIBLE_16:
            case ITEM_TYPE.COLLECTIBLE_17:
            case ITEM_TYPE.COLLECTIBLE_18:
            case ITEM_TYPE.COLLECTIBLE_19:
            case ITEM_TYPE.COLLECTIBLE_20:
            case ITEM_TYPE.ROCKET_1:
            case ITEM_TYPE.ROCKET_2:
            case ITEM_TYPE.ROCKET_3:
            case ITEM_TYPE.ROCKET_4:
            case ITEM_TYPE.ROCKET_5:
            case ITEM_TYPE.ROCKET_6:
                InstantiateItem(type);
                break;

            case ITEM_TYPE.ROCKET_RANDOM:
                GenerateRandomGingerbread();
                break;

            case ITEM_TYPE.ROCK_CANDY_RANDOM:
                GenerateRandomRockCandy();
                break;

            case ITEM_TYPE.ITEM_COLORCONE:
            case ITEM_TYPE.ITEM_BIGBOMB:
            case ITEM_TYPE.ITEM_MEDBOMB:
            case ITEM_TYPE.ITEM_ROW:
            case ITEM_TYPE.ITEM_COLUMN:
            case ITEM_TYPE.ITEM_BOMB:
            case ITEM_TYPE.ITEM_CROSS:
            case ITEM_TYPE.ITEM_COLORHUNTER1:
            case ITEM_TYPE.ITEM_COLORHUNTER2:
            case ITEM_TYPE.ITEM_COLORHUNTER3:
            case ITEM_TYPE.ITEM_COLORHUNTER4:
            case ITEM_TYPE.ITEM_COLORHUNTER5:
            case ITEM_TYPE.ITEM_COLORHUNTER6:
                InstantiateBoosterItem(type);
                StartCoroutine(StartGenerateItem());
                break;

        }

        return item;
    }
    //skillzgeneraterandomcookie
    Item GenerateRandomCookie()
    {
        var type = StageLoader.instance.RandomItems();
        Debug.Log(((int)type) + "===========");
        return InstantiateItem(type);
    }

    Item GenerateRandomGingerbread()
    {
        var type = StageLoader.instance.RandomItems();

        switch (type)
        {
            case ITEM_TYPE.BlueBox:
                InstantiateItem(ITEM_TYPE.ROCKET_1);
                break;

            case ITEM_TYPE.GreenBox:
                InstantiateItem(ITEM_TYPE.ROCKET_2);
                break;

            case ITEM_TYPE.ORANGEBOX:
                InstantiateItem(ITEM_TYPE.ROCKET_3);
                break;

            case ITEM_TYPE.PURPLEBOX:
                InstantiateItem(ITEM_TYPE.ROCKET_4);
                break;

            case ITEM_TYPE.REDBOX:
                InstantiateItem(ITEM_TYPE.ROCKET_5);
                break;

            case ITEM_TYPE.YELLOWBOX:
                InstantiateItem(ITEM_TYPE.ROCKET_6);
                break;
        }

        return null;
    }

    Item GenerateRandomRockCandy()
    {
        var type = StageLoader.instance.RandomItems();

        switch (type)
        {
            case ITEM_TYPE.BlueBox:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_1);
                break;

            case ITEM_TYPE.GreenBox:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_2);
                break;

            case ITEM_TYPE.ORANGEBOX:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_3);
                break;

            case ITEM_TYPE.PURPLEBOX:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_4);
                break;

            case ITEM_TYPE.REDBOX:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_5);
                break;

            case ITEM_TYPE.YELLOWBOX:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_6);
                break;
        }

        return null;
    }

    Item InstantiateItem(ITEM_TYPE type)
    {
        GameObject piece = null;
        var color = 0;
        var destroycolor = 0;
        int stage = StageLoader.instance.Stage;
        int changeLevels = StageLoader.instance.BreakableChangeLevels;
        int changeLevelsLego = StageLoader.instance.LegoChangeLevels;
        int changeLevelsChoco = StageLoader.instance.ChocolateChangeLevels;
        int changeLevelsBubbles = StageLoader.instance.BubblesChangeLevels;


        switch (type)
        {
            case ITEM_TYPE.BlueBox:
                color = 1;
                piece = Instantiate(Resources.Load(Configuration.Item1())) as GameObject;
                break;

            case ITEM_TYPE.GreenBox:
                color = 2;
                piece = Instantiate(Resources.Load(Configuration.Item2())) as GameObject;
                break;

            case ITEM_TYPE.ORANGEBOX:
                color = 3;
                piece = Instantiate(Resources.Load(Configuration.Item3())) as GameObject;
                break;

            case ITEM_TYPE.PURPLEBOX:
                color = 4;
                piece = Instantiate(Resources.Load(Configuration.Item4())) as GameObject;
                break;

            case ITEM_TYPE.REDBOX:
                color = 5;
                piece = Instantiate(Resources.Load(Configuration.Item5())) as GameObject;
                break;

            case ITEM_TYPE.YELLOWBOX:
                color = 6;
                piece = Instantiate(Resources.Load(Configuration.Item6())) as GameObject;
                break;

            case ITEM_TYPE.BREAKABLE:
                if (stage % (9 * changeLevels) >= 8 * changeLevels)
                {
                    piece = Instantiate(Resources.Load(Configuration.Breakable8())) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 7 * changeLevels)
                {
                    piece = Instantiate(Resources.Load(Configuration.Breakable7())) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 6 * changeLevels)
                {
                    piece = Instantiate(Resources.Load(Configuration.Breakable6())) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 5 * changeLevels)
                {
                    piece = Instantiate(Resources.Load(Configuration.Breakable5())) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 4 * changeLevels)
                {
                    piece = Instantiate(Resources.Load(Configuration.Breakable4())) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 3 * changeLevels)
                {
                    piece = Instantiate(Resources.Load(Configuration.Breakable3())) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 2 * changeLevels)
                {
                    piece = Instantiate(Resources.Load(Configuration.Breakable2())) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= changeLevels)
                {
                    piece = Instantiate(Resources.Load(Configuration.Breakable1())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.Breakable())) as GameObject;
                }


                break;


            case ITEM_TYPE.MINE_1_LAYER:
                if (stage % (6 * changeLevelsChoco) >= 5 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine1e())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 4 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine1d())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 3 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine1c())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 2 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine1b())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine1a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine1())) as GameObject;
                }

                //piece = Instantiate(Resources.Load(Configuration.ToyMine1())) as GameObject;
                break;
            case ITEM_TYPE.MINE_2_LAYER:
                if (stage % (6 * changeLevelsChoco) >= 5 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine2e())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 4 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine2d())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 3 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine2c())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 2 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine2b())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine2a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine2())) as GameObject;
                }
                //piece = Instantiate(Resources.Load(Configuration.ToyMine2())) as GameObject;
                break;
            case ITEM_TYPE.MINE_3_LAYER:
                if (stage % (6 * changeLevelsChoco) >= 5 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine3e())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 4 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine3d())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 3 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine3c())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 2 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine3b())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine3a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine3())) as GameObject;
                }
                //piece = Instantiate(Resources.Load(Configuration.ToyMine3())) as GameObject;
                break;
            case ITEM_TYPE.MINE_4_LAYER:
                if (stage % (6 * changeLevelsChoco) >= 5 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine4e())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 4 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine4d())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 3 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine4c())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 2 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine4b())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine4a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine4())) as GameObject;
                }
                //piece = Instantiate(Resources.Load(Configuration.ToyMine4())) as GameObject;
                break;
            case ITEM_TYPE.MINE_5_LAYER:
                if (stage % (6 * changeLevelsChoco) >= 5 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine5e())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 4 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine5d())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 3 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine5c())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 2 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine5b())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine5a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine5())) as GameObject;
                }
                //piece = Instantiate(Resources.Load(Configuration.ToyMine5())) as GameObject;
                break;
            case ITEM_TYPE.MINE_6_LAYER:
                if (stage % (6 * changeLevelsChoco) >= 5 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine6e())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 4 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine6d())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 3 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine6c())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 2 * changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine6b())) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= changeLevelsChoco)
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine6a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.ToyMine6())) as GameObject;
                }
                //piece = Instantiate(Resources.Load(Configuration.ToyMine6())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_1:
                color = 1;

                if (stage % (5 * changeLevelsLego) >= 4 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox1d())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 3 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox1c())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 2 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox1b())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox1a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox1())) as GameObject;
                }

                //piece = Instantiate(Resources.Load(Configuration.LegoBox1())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_2:
                color = 2;
                if (stage % (5 * changeLevelsLego) >= 4 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox2d())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 3 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox2c())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 2 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox2b())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox2a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox2())) as GameObject;
                }



                //piece = Instantiate(Resources.Load(Configuration.LegoBox2())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_3:
                color = 3;

                if (stage % (5 * changeLevelsLego) >= 4 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox3d())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 3 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox3c())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 2 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox3b())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox3a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox3())) as GameObject;
                }


                //piece = Instantiate(Resources.Load(Configuration.LegoBox3())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_4:
                color = 4;

                if (stage % (5 * changeLevelsLego) >= 4 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox4d())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 3 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox4c())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 2 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox4b())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox4a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox4())) as GameObject;
                }


                //piece = Instantiate(Resources.Load(Configuration.LegoBox4())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_5:
                color = 5;

                if (stage % (5 * changeLevelsLego) >= 4 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox5d())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 3 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox5c())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 2 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox5b())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox5a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox5())) as GameObject;
                }


                //piece = Instantiate(Resources.Load(Configuration.LegoBox5())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_6:
                color = 6;

                if (stage % (5 * changeLevelsLego) >= 4 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox6d())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 3 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox6c())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 2 * changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox6b())) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= changeLevelsLego)
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox6a())) as GameObject;
                }
                else
                {
                    piece = Instantiate(Resources.Load(Configuration.LegoBox6())) as GameObject;
                }


                //piece = Instantiate(Resources.Load(Configuration.LegoBox6())) as GameObject;
                break;



            case ITEM_TYPE.COLLECTIBLE_1:
                piece = Instantiate(Resources.Load(Configuration.Collectible1())) as GameObject;
                color = 1;
                break;

            case ITEM_TYPE.COLLECTIBLE_2:
                piece = Instantiate(Resources.Load(Configuration.Collectible2())) as GameObject;
                color = 2;
                break;

            case ITEM_TYPE.COLLECTIBLE_3:
                piece = Instantiate(Resources.Load(Configuration.Collectible3())) as GameObject;
                color = 3;
                break;

            case ITEM_TYPE.COLLECTIBLE_4:
                piece = Instantiate(Resources.Load(Configuration.Collectible4())) as GameObject;
                color = 4;
                break;

            case ITEM_TYPE.COLLECTIBLE_5:
                piece = Instantiate(Resources.Load(Configuration.Collectible5())) as GameObject;
                color = 5;
                break;

            case ITEM_TYPE.COLLECTIBLE_6:
                piece = Instantiate(Resources.Load(Configuration.Collectible6())) as GameObject;
                color = 6;
                break;

            case ITEM_TYPE.COLLECTIBLE_7:
                piece = Instantiate(Resources.Load(Configuration.Collectible7())) as GameObject;
                color = 7;
                break;

            case ITEM_TYPE.COLLECTIBLE_8:
                piece = Instantiate(Resources.Load(Configuration.Collectible8())) as GameObject;
                color = 8;
                break;

            case ITEM_TYPE.COLLECTIBLE_9:
                piece = Instantiate(Resources.Load(Configuration.Collectible9())) as GameObject;
                color = 9;
                break;

            case ITEM_TYPE.COLLECTIBLE_10:
                piece = Instantiate(Resources.Load(Configuration.Collectible10())) as GameObject;
                color = 10;
                break;

            case ITEM_TYPE.COLLECTIBLE_11:
                piece = Instantiate(Resources.Load(Configuration.Collectible11())) as GameObject;
                color = 11;
                break;

            case ITEM_TYPE.COLLECTIBLE_12:
                piece = Instantiate(Resources.Load(Configuration.Collectible12())) as GameObject;
                color = 12;
                break;

            case ITEM_TYPE.COLLECTIBLE_13:
                piece = Instantiate(Resources.Load(Configuration.Collectible13())) as GameObject;
                color = 13;
                break;

            case ITEM_TYPE.COLLECTIBLE_14:
                piece = Instantiate(Resources.Load(Configuration.Collectible14())) as GameObject;
                color = 14;
                break;

            case ITEM_TYPE.COLLECTIBLE_15:
                piece = Instantiate(Resources.Load(Configuration.Collectible15())) as GameObject;
                color = 15;
                break;

            case ITEM_TYPE.COLLECTIBLE_16:
                piece = Instantiate(Resources.Load(Configuration.Collectible16())) as GameObject;
                color = 16;
                break;

            case ITEM_TYPE.COLLECTIBLE_17:
                piece = Instantiate(Resources.Load(Configuration.Collectible17())) as GameObject;
                color = 17;
                break;

            case ITEM_TYPE.COLLECTIBLE_18:
                piece = Instantiate(Resources.Load(Configuration.Collectible18())) as GameObject;
                color = 18;
                break;

            case ITEM_TYPE.COLLECTIBLE_19:
                piece = Instantiate(Resources.Load(Configuration.Collectible19())) as GameObject;
                color = 19;
                break;

            case ITEM_TYPE.COLLECTIBLE_20:
                piece = Instantiate(Resources.Load(Configuration.Collectible20())) as GameObject;
                color = 20;
                break;

            case ITEM_TYPE.ROCKET_1:
                color = 1;
                piece = Instantiate(Resources.Load(Configuration.Rocket1())) as GameObject;
                break;
            case ITEM_TYPE.ROCKET_2:
                color = 2;
                piece = Instantiate(Resources.Load(Configuration.Rocket2())) as GameObject;
                break;
            case ITEM_TYPE.ROCKET_3:
                color = 3;
                piece = Instantiate(Resources.Load(Configuration.Rocket3())) as GameObject;
                break;
            case ITEM_TYPE.ROCKET_4:
                color = 4;
                piece = Instantiate(Resources.Load(Configuration.Rocket4())) as GameObject;
                break;
            case ITEM_TYPE.ROCKET_5:
                color = 5;
                piece = Instantiate(Resources.Load(Configuration.Rocket5())) as GameObject;
                break;
            case ITEM_TYPE.ROCKET_6:
                color = 6;
                piece = Instantiate(Resources.Load(Configuration.Rocket6())) as GameObject;
                break;

        }


        if (piece != null)
        {
            piece.transform.SetParent(gameObject.transform);
            piece.name = "Item";
            piece.transform.localPosition = grid.NodeLocalPosition(i, j);
            piece.GetComponent<Item>().node = this;
            piece.GetComponent<Item>().board = this.grid;
            piece.GetComponent<Item>().type = type;
            piece.GetComponent<Item>().color = color;
            piece.GetComponent<Item>().destroycolor = destroycolor;

            this.item = piece.GetComponent<Item>();

            return piece.GetComponent<Item>();
        }
        else
        {
            return null;
        }
    }

    Item InstantiateBoosterItem(ITEM_TYPE type)
    {
        GameObject piece = null;
        var color = 0;
        var destroycolor = 0;
        int stage = StageLoader.instance.Stage;
        int changeLevels = StageLoader.instance.BreakableChangeLevels;
        int changeLevelsLego = StageLoader.instance.LegoChangeLevels;
        int changeLevelsChoco = StageLoader.instance.ChocolateChangeLevels;
        int changeLevelsBubbles = StageLoader.instance.BubblesChangeLevels;


        switch (type)
        {
            case ITEM_TYPE.ITEM_COLUMN:
                color = 51;
                piece = Instantiate(Resources.Load(Configuration.Item1Column())) as GameObject;
                break;
            case ITEM_TYPE.ITEM_ROW:
                color = 61;
                piece = Instantiate(Resources.Load(Configuration.Item1Row())) as GameObject;
                break;
            case ITEM_TYPE.ITEM_BOMB:
                color = 71;
                piece = Instantiate(Resources.Load(Configuration.Item1Bomb())) as GameObject;
                break;

            case ITEM_TYPE.ITEM_CROSS:
                piece = Instantiate(Resources.Load(Configuration.Item1Cross())) as GameObject;
                color = 81;
                break;

            case ITEM_TYPE.ITEM_COLORCONE:
                piece = Instantiate(Resources.Load(Configuration.ItemColorCone())) as GameObject;
                break;

            case ITEM_TYPE.ITEM_BIGBOMB:
                piece = Instantiate(Resources.Load(Configuration.BigBomb())) as GameObject;
                break;

            case ITEM_TYPE.ITEM_MEDBOMB:
                piece = Instantiate(Resources.Load(Configuration.MedBomb())) as GameObject;
                break;

            case ITEM_TYPE.ITEM_COLORHUNTER1:
                piece = Instantiate(Resources.Load(Configuration.ColorHunter1())) as GameObject;
                destroycolor = 1;
                color = 201;
                break;

            case ITEM_TYPE.ITEM_COLORHUNTER2:
                piece = Instantiate(Resources.Load(Configuration.ColorHunter2())) as GameObject;
                destroycolor = 2;
                color = 202;
                break;

            case ITEM_TYPE.ITEM_COLORHUNTER3:
                piece = Instantiate(Resources.Load(Configuration.ColorHunter3())) as GameObject;
                destroycolor = 3;
                color = 203;
                break;

            case ITEM_TYPE.ITEM_COLORHUNTER4:
                piece = Instantiate(Resources.Load(Configuration.ColorHunter4())) as GameObject;
                destroycolor = 4;
                color = 204;
                break;

            case ITEM_TYPE.ITEM_COLORHUNTER5:
                piece = Instantiate(Resources.Load(Configuration.ColorHunter5())) as GameObject;
                destroycolor = 5;
                color = 205;
                break;

            case ITEM_TYPE.ITEM_COLORHUNTER6:
                piece = Instantiate(Resources.Load(Configuration.ColorHunter6())) as GameObject;
                destroycolor = 6;
                color = 206;
                break;





        }

        if (piece != null)
        {

            piece.transform.SetParent(gameObject.transform);
            piece.name = "Item";
            piece.transform.localPosition = grid.NodeLocalPosition(i, j);
            piece.GetComponent<Item>().node = this;
            piece.GetComponent<Item>().board = this.grid;
            piece.GetComponent<Item>().type = type;
            piece.GetComponent<Item>().color = color;
            piece.GetComponent<Item>().destroycolor = destroycolor;


            this.item = piece.GetComponent<Item>();
            return piece.GetComponent<Item>();

        }
        else
        {
            return null;
        }

    }

    IEnumerator StartGenerateItem()
    {
        //grid.lockSwap = true;

        //while (grid.dropTime>0)
        //{
        //    yield return null;
        //}
        if (item != null)
        {
            item.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Effect";
            item.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 102 ;
         
            item.gameObject.transform.localScale = new Vector3(3f, 3f, 3f);

            iTween.ShakeRotation(item.gameObject, iTween.Hash(
                  "name", "HintAnimation",
                  "amount", new Vector3(0f, 0f, 20f),
                  "easetype", iTween.EaseType.easeInBounce,
                  //"looptype", iTween.LoopType.pingPong,
                  //"oncomplete", "OnCompleteShowHint",
                  //"oncompletetarget", gameObject,
                  // "oncompleteparams", new Hashtable() { { "item", item } },
                  "time", 1.0f
              ));

            iTween.ScaleTo(item.gameObject, iTween.Hash(
                          "scale", new Vector3(1f, 1f, 0),
                          //"onstart", "OnStartDestroy",
                          //"oncomplete", "OnCompleteDestroy",
                          "easetype", iTween.EaseType.easeInOutBack,
                          "time", 0.7f
                      ));
            //item.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Item";
            //item.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
            //item.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10 - item.node.i;
            yield return new WaitForSeconds(0.1f);

        }


        // grid.lockSwap = false;

        //item.gameObject.GetComponent<Item>().drag = false;
    }
    #endregion

    #region Match

    // find matches at a node
    public List<Item> FindMatches(FIND_DIRECTION direction = FIND_DIRECTION.NONE, int matches = 2)
    {
        var list = new List<Item>();
        var countedNodes = new Dictionary<int, Item>();

        if (item == null || !item.Matchable())
        {
            return null;
        }

        if (direction != FIND_DIRECTION.COLUMN)
        {
            countedNodes = FindMoreMatches(item.color, countedNodes, FIND_DIRECTION.ROW);
        }

        if (countedNodes.Count < matches)
        {
            countedNodes.Clear();
        }

        if (direction != FIND_DIRECTION.ROW)
        {
            countedNodes = FindMoreMatches(item.color, countedNodes, FIND_DIRECTION.COLUMN);
        }

        if (countedNodes.Count < matches)
        {
            countedNodes.Clear();
        }

        foreach (KeyValuePair<int, Item> entry in countedNodes)
        {
            list.Add(entry.Value);
        }

        return list;
    }

    // helper function to find matches
    Dictionary<int, Item> FindMoreMatches(int color, Dictionary<int, Item> countedNodes, FIND_DIRECTION direction)
    {
        if (item == null || item.destroying)
        {
            return countedNodes;
        }

        if (item.color == color && !countedNodes.ContainsValue(item) && item.Matchable() && item.node != null)
        {
            countedNodes.Add(item.node.OrderOnBoard(), item);

			if (direction == FIND_DIRECTION.ROW) {
				if (LeftNeighbor () != null) {
					countedNodes = LeftNeighbor ().FindMoreMatches (color, countedNodes, FIND_DIRECTION.ROW);
				}

				if (RightNeighbor () != null) {
					countedNodes = RightNeighbor ().FindMoreMatches (color, countedNodes, FIND_DIRECTION.ROW);
				}

				if (TopNeighbor () != null) {
					countedNodes = TopNeighbor ().FindMoreMatches (color, countedNodes, FIND_DIRECTION.COLUMN);
				}

				if (BottomNeighbor () != null) {
					countedNodes = BottomNeighbor ().FindMoreMatches (color, countedNodes, FIND_DIRECTION.COLUMN);
				}

			} else if (direction == FIND_DIRECTION.COLUMN) {
				//Top, Top Left and Top Right neighbor
				if (TopNeighbor () != null) {
					countedNodes = TopNeighbor ().FindMoreMatches (color, countedNodes, FIND_DIRECTION.COLUMN);
				}

				if (BottomNeighbor () != null) {
					countedNodes = BottomNeighbor ().FindMoreMatches (color, countedNodes, FIND_DIRECTION.COLUMN);
				}

				if (LeftNeighbor () != null) {
					countedNodes = LeftNeighbor ().FindMoreMatches (color, countedNodes, FIND_DIRECTION.ROW);
				}

				if (RightNeighbor () != null) {
					countedNodes = RightNeighbor ().FindMoreMatches (color, countedNodes, FIND_DIRECTION.ROW);
				}
			} 
        }

        return countedNodes;
    }

    //BOOSTER MERGE CONTROL
    public List<Item> FindMatchesBooster(FIND_DIRECTION direction = FIND_DIRECTION.NONE, int matches = 2)
    {
        var list = new List<Item>();
        var countedBoosterNodes = new Dictionary<int, Item>();

        if (item == null || !item.Matchable())
        {
            return null;
        }

        if (direction != FIND_DIRECTION.COLUMN)
        {
            countedBoosterNodes = FindMoreBoosterMatches(item.IsBooster(), countedBoosterNodes, FIND_DIRECTION.ROW);
        }

        if (countedBoosterNodes.Count < matches)
        {
            countedBoosterNodes.Clear();
        }

        if (direction != FIND_DIRECTION.ROW)
        {
            countedBoosterNodes = FindMoreBoosterMatches(item.IsBooster(), countedBoosterNodes, FIND_DIRECTION.COLUMN);
        }

        if (countedBoosterNodes.Count < matches)
        {
            countedBoosterNodes.Clear();
        }

        foreach (KeyValuePair<int, Item> entry in countedBoosterNodes)
        {
            list.Add(entry.Value);
        }

        return list;
    }
    Dictionary<int, Item> FindMoreBoosterMatches(bool type, Dictionary<int, Item> countedBoosterNodes, FIND_DIRECTION direction)
    {
        if (item == null || item.destroying)
        {
            return countedBoosterNodes;
        }

        if (item.IsBooster() && !countedBoosterNodes.ContainsValue(item) && item.Matchable() && item.node != null)
        {
            countedBoosterNodes.Add(item.node.OrderOnBoard(), item);

            if (direction == FIND_DIRECTION.ROW)
            {
                if (LeftNeighbor() != null)
                {
                    countedBoosterNodes = LeftNeighbor().FindMoreBoosterMatches(type, countedBoosterNodes, FIND_DIRECTION.ROW);
                }

                if (RightNeighbor() != null)
                {
                    countedBoosterNodes = RightNeighbor().FindMoreBoosterMatches(type, countedBoosterNodes, FIND_DIRECTION.ROW);
                }

                if (TopNeighbor() != null)
                {
                    countedBoosterNodes = TopNeighbor().FindMoreBoosterMatches(type, countedBoosterNodes, FIND_DIRECTION.COLUMN);
                }

                if (BottomNeighbor() != null)
                {
                    countedBoosterNodes = BottomNeighbor().FindMoreBoosterMatches(type, countedBoosterNodes, FIND_DIRECTION.COLUMN);
                }

            }
            else if (direction == FIND_DIRECTION.COLUMN)
            {
                //Top, Top Left and Top Right neighbor
                if (TopNeighbor() != null)
                {
                    countedBoosterNodes = TopNeighbor().FindMoreBoosterMatches(type, countedBoosterNodes, FIND_DIRECTION.COLUMN);
                }

                if (BottomNeighbor() != null)
                {
                    countedBoosterNodes = BottomNeighbor().FindMoreBoosterMatches(type, countedBoosterNodes, FIND_DIRECTION.COLUMN);
                }

                if (LeftNeighbor() != null)
                {
                    countedBoosterNodes = LeftNeighbor().FindMoreBoosterMatches(type, countedBoosterNodes, FIND_DIRECTION.ROW);
                }

                if (RightNeighbor() != null)
                {
                    countedBoosterNodes = RightNeighbor().FindMoreBoosterMatches(type, countedBoosterNodes, FIND_DIRECTION.ROW);
                }
            }
        }

        return countedBoosterNodes;
    }

   
    #endregion

    #region Utility

    // return the order base on i and j
    public int OrderOnBoard()
    {
        return (i * StageLoader.instance.column + j );
    }


    #endregion

    #region Type

    public bool CanStoreItem()
    {
        if (tile != null)
        {
            if (tile.type == TILE_TYPE.DARD_TILE || tile.type == TILE_TYPE.LIGHT_TILE)
            {
                return true;
            }
        }

        return false;
    }

    public bool CanGoThrough()
    {
        if (tile == null || tile.type == TILE_TYPE.NONE)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CanGenerateNewItem()
    {
        if (CanStoreItem() == true)
        {
            for (int row = i - 1; row >= 0; row--)
            {
				Node upNode = grid.GetNode(row, j);

                if (upNode != null)
                {
                    if (upNode.CanGoThrough() == false)
                    {
                        return false;
                    }
                    else
                    {
                        if (upNode.item != null)
                        {
                            if (upNode.item.Movable() == false)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region Node

    // get source node of an empty node
    public Node GetSourceNode()
    {
        Node source = null;

        // top
		Node top = grid.GetNode(i - 1, j);
        if (top != null)
        {
            if (top.item == null && top.CanGoThrough())
            {
                if (top.GetSourceNode() != null)
                {
                    source = top.GetSourceNode();
                }
            }
        }

        if (source != null)
        {
            return source;
        }

        // left
		Node left = grid.GetNode(i - 1, j - 1);
        if (left != null)
        {
            if (left.item == null && left.CanGoThrough())
            {
                if (left.GetSourceNode() != null)
                {
                    source = left.GetSourceNode();
                }
            }
            else
            {
                if (left.item != null && left.item.Movable())
                {
                    source = left;
                }
            }
        }

        if (source != null)
        {
            return source;
        }

        // right
		Node right = grid.GetNode(i - 1, j + 1);
        if (right != null)
        {
            if (right.item == null && right.CanGoThrough())
            {
                if (right.GetSourceNode() != null)
                {
                    source = right.GetSourceNode();
                }
            }
            else
            {
                if (right.item != null && right.item.Movable())
                {
                    source = right;
                }
            }
        }

        return source;
    }

    // get move path from an empty node to source node
    public List<Vector3> GetMovePath()
    {
        List<Vector3> path = new List<Vector3>();

		path.Add(grid.NodeLocalPosition(i, j));

        // top
		Node top = grid.GetNode(i - 1, j);
        if (top != null)
        {
            if (top.item == null && top.CanGoThrough())
            {
                if (top.GetSourceNode() != null)
                {
                    path.AddRange(top.GetMovePath());
                    return path;
                }
            }
        }

        // left
		Node left = grid.GetNode(i - 1, j - 1);
        if (left != null)
        {
            if (left.item == null && left.CanGoThrough())
            {
                if (left.GetSourceNode() != null)
                {
                    path.AddRange(left.GetMovePath());
                    return path;
                }
            }
            else
            {
                if (left.item != null && left.item.Movable())
                {
                    return path;
                }
            }
        }

        // right
		Node right = grid.GetNode(i - 1, j + 1);
        if (right != null)
        {
            if (right.item == null && right.CanGoThrough())
            {
                if (right.GetSourceNode() != null)
                {
                    path.AddRange(right.GetMovePath());
                    return path;
                }
            }
            else
            {
                if (right.item != null && right.item.Movable())
                {
                    return path;
                }
            }
        }

        return path;
    }

    #endregion

    #region Waffle

    public void WaffleExplode()
    {
		if (waffle != null && item != null & (item.IsCookie() == true || item.IsBreaker(item.type) || item.type == ITEM_TYPE.ITEM_COLORCONE || item.type == ITEM_TYPE.ITEM_BIGBOMB || item.type == ITEM_TYPE.ITEM_MEDBOMB || item.IsColorHunterBreaker(item.type)))
        {
            int stage = StageLoader.instance.Stage;
            int changeLevelsBubbles = StageLoader.instance.BubblesChangeLevels;

            AudioManager.instance.WaffleExplodeAudio();

			grid.CollectWaffle(waffle);

            GameObject prefab = null;

            if (waffle.type == WAFFLE_TYPE.WAFFLE_3)
            {
                if (stage % (3 * changeLevelsBubbles) >= 2 * changeLevelsBubbles)
                {
                    prefab = Resources.Load(Configuration.Waffle2b()) as GameObject;
                }
                else if (stage % (3 * changeLevelsBubbles) >= changeLevelsBubbles)
                {
                    prefab = Resources.Load(Configuration.Waffle2a()) as GameObject;
                }               
                else
                {
                    prefab = Resources.Load(Configuration.Waffle2()) as GameObject;
                }
                // prefab = Resources.Load(Configuration.Waffle2()) as GameObject;

                waffle.gameObject.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

                waffle.type = WAFFLE_TYPE.WAFFLE_2;
            }
            else if (waffle.type == WAFFLE_TYPE.WAFFLE_2)
            {

                if (stage % (3 * changeLevelsBubbles) >= 2 * changeLevelsBubbles)
                {
                    prefab = Resources.Load(Configuration.Waffle1b()) as GameObject;
                }
                else if (stage % (3 * changeLevelsBubbles) >= changeLevelsBubbles)
                {
                    prefab = Resources.Load(Configuration.Waffle1a()) as GameObject;
                }
                else
                {
                    prefab = Resources.Load(Configuration.Waffle1()) as GameObject;
                }
                // prefab = Resources.Load(Configuration.Waffle1()) as GameObject;

                waffle.gameObject.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

                waffle.type = WAFFLE_TYPE.WAFFLE_1;
            }
            else if (waffle.type == WAFFLE_TYPE.WAFFLE_1)
            {
                Destroy(waffle.gameObject);

                waffle = null;
            }
        }
    }

    #endregion

    #region Cage

    public void CageExplode()
    {
        if (cage == null)
        {
            return;
        }

        GameObject explosion = null;

        if (item != null)
        {
            switch (item.GetCookie(item.type))
            {
			case ITEM_TYPE.BlueBox:
				explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BlueBoxExplosion()) as GameObject);
                    break;
			case ITEM_TYPE.GreenBox:
				explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.GreenBoxExplosion()) as GameObject);
                    break;
			case ITEM_TYPE.ORANGEBOX:
                    explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.OrangeCookieExplosion()) as GameObject);
                    break;
			case ITEM_TYPE.PURPLEBOX:
                    explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.PurpleCookieExplosion()) as GameObject);
                    break;
			case ITEM_TYPE.REDBOX:
                    explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RedCookieExplosion()) as GameObject);
                    break;
			case ITEM_TYPE.YELLOWBOX:
                    explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.YellowCookieExplosion()) as GameObject);
                    break;
            }
        }

		grid.CollectCage(cage);

        if (explosion != null) explosion.transform.position = item.transform.position;

        AudioManager.instance.CageExplodeAudio();

        Destroy(cage.gameObject);

        cage = null;

        StartCoroutine(item.ResetDestroying());
    }

    #endregion

    #region Booster

    public void AddOvenBoosterActive()
    {
        ovenActive = Instantiate(Resources.Load(Configuration.BoosterActive())) as GameObject;

		ovenActive.transform.localPosition = grid.NodeLocalPosition(i, j);
    }

    public void RemoveOvenBoosterActive()
    {
        Destroy(ovenActive);

        ovenActive = null;
    }

    #endregion
}
