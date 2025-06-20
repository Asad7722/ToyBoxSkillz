﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITarget : MonoBehaviour 
{
    public Image target1;
    public Text target1Amount;
    public Image target1Tick;

    public Image target2;
    public Text target2Amount;
    public Image target2Tick;

    public Image target3;
    public Text target3Amount;
    public Image target3Tick;

    public Image target4;
    public Text target4Amount;
    public Image target4Tick;
    public GameObject goalnull;

   

    void Start () 
    {
        for (int i = 1; i <= 4; i++)
        {
            Image target = null;
            Text targetAmount = null;
            Image targetTick = null;

            GameObject prefab = null;

            TARGET_TYPE type = TARGET_TYPE.NONE;
            int amount = 0;
            int color = 0;

            switch (i)
            {
                case 1:
                    target = target1;
                    targetAmount = target1Amount;
                    targetTick = target1Tick;

                    type = StageLoader.instance.target1Type;
                    amount = StageLoader.instance.target1Amount;
                    color = StageLoader.instance.target1Color;
                    break;
                case 2:
                    target = target2;
                    targetAmount = target2Amount;
                    targetTick = target2Tick;

                    type = StageLoader.instance.target2Type;
                    amount = StageLoader.instance.target2Amount;
                    color = StageLoader.instance.target2Color;
                    break;
                case 3:
                    target = target3;
                    targetAmount = target3Amount;
                    targetTick = target3Tick;

                    type = StageLoader.instance.target3Type;
                    amount = StageLoader.instance.target3Amount;
                    color = StageLoader.instance.target3Color;
                    break;
                case 4:
                    target = target4;
                    targetAmount = target4Amount;
                    targetTick = target4Tick;

                    type = StageLoader.instance.target4Type;
                    amount = StageLoader.instance.target4Amount;
                    color = StageLoader.instance.target4Color;
                    break;
            }
            // 1 - score : currently not support
            // 2 - cookie
			if (type == TARGET_TYPE.ITEM)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                switch (color)
                {
                    case 1:
					prefab = Resources.Load(Configuration.Item1()) as GameObject;
                        break;
                    case 2:
					prefab = Resources.Load(Configuration.Item2()) as GameObject;
                        break;
                    case 3:
					prefab = Resources.Load(Configuration.Item3()) as GameObject;
                        break;
                    case 4:
					prefab = Resources.Load(Configuration.Item4()) as GameObject;
                        break;
                    case 5:
					prefab = Resources.Load(Configuration.Item5()) as GameObject;
                        break;
                    case 6:
					prefab = Resources.Load(Configuration.Item6()) as GameObject;
                        break;
                }

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();

                target.rectTransform.localScale = new Vector3(1, 1, 0);
            }
            // 3 - Breakable item
			else if (type == TARGET_TYPE.BREAKABLE)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);
                //Breakable
                int stage = StageLoader.instance.Stage;
                int changeLevels = StageLoader.instance.BreakableChangeLevels;
                int changeLevelsLego = StageLoader.instance.LegoChangeLevels;

                if (stage % (9 * changeLevels) >= 8 * changeLevels)
                {
                    prefab = Resources.Load(Configuration.Breakable8()) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 7 * changeLevels)
                {
                    prefab = Resources.Load(Configuration.Breakable7()) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 6 * changeLevels)
                {
                    prefab = Resources.Load(Configuration.Breakable6()) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 5 * changeLevels)
                {
                    prefab = Resources.Load(Configuration.Breakable5()) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 4 * changeLevels)
                {
                    prefab = Resources.Load(Configuration.Breakable4()) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 3 * changeLevels)
                {
                    prefab = Resources.Load(Configuration.Breakable3()) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= 2 * changeLevels)
                {
                    prefab = Resources.Load(Configuration.Breakable2()) as GameObject;
                }
                else if (stage % (9 * changeLevels) >= changeLevels)
                {
                    prefab = Resources.Load(Configuration.Breakable1()) as GameObject;
                }
                else 
                {
                    prefab = Resources.Load(Configuration.Breakable()) as GameObject;
                }





                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();

                target.rectTransform.localScale = new Vector3(1, 1, 0);
            }
            // 4 -waffle
            else if (type == TARGET_TYPE.WAFFLE)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);
                int stage = StageLoader.instance.Stage;
                int changeLevelsChoco = StageLoader.instance.ChocolateChangeLevels;
                int changeLevelsBubbles = StageLoader.instance.BubblesChangeLevels;

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

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();

                target.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0);
            }
            // 5 - collectible
            else if (type == TARGET_TYPE.COLLECTIBLE)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                switch (color)
                {
                    case 1:
                        prefab = Resources.Load(Configuration.Collectible1()) as GameObject;
                        break;
                    case 2:
                        prefab = Resources.Load(Configuration.Collectible2()) as GameObject;
                        break;
                    case 3:
                        prefab = Resources.Load(Configuration.Collectible3()) as GameObject;
                        break;
                    case 4:
                        prefab = Resources.Load(Configuration.Collectible4()) as GameObject;
                        break;
                    case 5:
                        prefab = Resources.Load(Configuration.Collectible5()) as GameObject;
                        break;
                    case 6:
                        prefab = Resources.Load(Configuration.Collectible6()) as GameObject;
                        break;
                    case 7:
                        prefab = Resources.Load(Configuration.Collectible7()) as GameObject;
                        break;
                    case 8:
                        prefab = Resources.Load(Configuration.Collectible8()) as GameObject;
                        break;
                    case 9:
                        prefab = Resources.Load(Configuration.Collectible9()) as GameObject;
                        break;
                    case 10:
                        prefab = Resources.Load(Configuration.Collectible10()) as GameObject;
                        break;
                    case 11:
                        prefab = Resources.Load(Configuration.Collectible11()) as GameObject;
                        break;
                    case 12:
                        prefab = Resources.Load(Configuration.Collectible12()) as GameObject;
                        break;
                    case 13:
                        prefab = Resources.Load(Configuration.Collectible13()) as GameObject;
                        break;
                    case 14:
                        prefab = Resources.Load(Configuration.Collectible14()) as GameObject;
                        break;
                    case 15:
                        prefab = Resources.Load(Configuration.Collectible15()) as GameObject;
                        break;
                    case 16:
                        prefab = Resources.Load(Configuration.Collectible16()) as GameObject;
                        break;
                    case 17:
                        prefab = Resources.Load(Configuration.Collectible17()) as GameObject;
                        break;
                    case 18:
                        prefab = Resources.Load(Configuration.Collectible18()) as GameObject;
                        break;
                    case 19:
                        prefab = Resources.Load(Configuration.Collectible19()) as GameObject;
                        break;
                    case 20:
                        prefab = Resources.Load(Configuration.Collectible20()) as GameObject;
                        break;
                }

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();

                target.rectTransform.localScale = new Vector3(1, 1, 0);
            }
            // 6 - col_row_breaker
            else if (type == TARGET_TYPE.COLUMN_ROW_BREAKER)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                prefab = Resources.Load(Configuration.ColumnRowBreaker()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();
            }
            // 7 - bomb_breaker
			else if (type == TARGET_TYPE.BOMB_BREAKER) 
			{
				target.gameObject.SetActive(true);
				targetAmount.gameObject.SetActive(true);
				targetTick.gameObject.SetActive(false);

				prefab = Resources.Load(Configuration.GenericBombBreaker()) as GameObject;

				if (prefab != null)
				{
					target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
				}

				targetAmount.text = amount.ToString();
			}
            // 8 - x_breaker
			else if (type == TARGET_TYPE.X_BREAKER)
			{
				target.gameObject.SetActive(true);
				targetAmount.gameObject.SetActive(true);
				targetTick.gameObject.SetActive(false);

				prefab = Resources.Load(Configuration.GenericXBreaker()) as GameObject;

				if (prefab != null)
				{
					target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
				}

				targetAmount.text = amount.ToString();
			}
            // 9 - cage
			else if (type == TARGET_TYPE.LOCK)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

				prefab = Resources.Load(Configuration.Lock1()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();

                target.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0);
            }
            // 10 - rainbow
			else if (type == TARGET_TYPE.COLORCONE)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

				prefab = Resources.Load(Configuration.ItemColorCone()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();
            }
            // 11 - gingerbread
			else if (type == TARGET_TYPE.ROCKET)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

				prefab = Resources.Load(Configuration.RocketGeneric()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();
            }
            // 12 - chocolate
			else if (type == TARGET_TYPE.TOYMINE)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);
                int stage = StageLoader.instance.Stage;
                int changeLevelsChoco = StageLoader.instance.ChocolateChangeLevels;
                int changeLevelsBubbles = StageLoader.instance.BubblesChangeLevels;

                if (stage % (6 * changeLevelsChoco) >= 5 * changeLevelsChoco)
                {
                    prefab = Resources.Load(Configuration.ToyMine1e()) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 4 * changeLevelsChoco)
                {
                    prefab = Resources.Load(Configuration.ToyMine1d()) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 3 * changeLevelsChoco)
                {
                    prefab = Resources.Load(Configuration.ToyMine1c()) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= 2 * changeLevelsChoco)
                {
                    prefab = Resources.Load(Configuration.ToyMine1b()) as GameObject;
                }
                else if (stage % (6 * changeLevelsChoco) >= changeLevelsChoco)
                {
                    prefab = Resources.Load(Configuration.ToyMine1a()) as GameObject;
                }
                else
                {
                    prefab = Resources.Load(Configuration.ToyMine1()) as GameObject;
                }
                //prefab = Resources.Load(Configuration.ToyMine1()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();
            }
            // 13 - rock candy
            else if (type == TARGET_TYPE.ROCK_CANDY)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                int stage = StageLoader.instance.Stage;
                int changeLevels = StageLoader.instance.BreakableChangeLevels;
                int changeLevelsLego = StageLoader.instance.LegoChangeLevels;

                if (stage % (5 * changeLevelsLego) >= 4 * changeLevelsLego)
                {
                    prefab = Resources.Load(Configuration.LegoBoxGenericd()) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 3 * changeLevelsLego)
                {
                    prefab = Resources.Load(Configuration.LegoBoxGenericc()) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= 2 * changeLevelsLego)
                {
                    prefab = Resources.Load(Configuration.LegoBoxGenericb()) as GameObject;
                }
                else if (stage % (5 * changeLevelsLego) >= changeLevelsLego)
                {
                    prefab = Resources.Load(Configuration.LegoBoxGenerica()) as GameObject;
                }
                else 
                {
                    prefab = Resources.Load(Configuration.LegoBoxGeneric()) as GameObject;
                }



                //prefab = Resources.Load(Configuration.LegoBoxGeneric()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();
            }
            else
            {
                target.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
	}

    public void UpdateTargetAmount(int target)
    {
        var explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RingExplosion()) as GameObject);
        explosion.transform.position = goalnull.gameObject.transform.position;
        if (target == 1)
        {
            target1Amount.text = (int.Parse(target1Amount.text) - 1).ToString();

            if (int.Parse(target1Amount.text) <= 0)
            {
                target1Amount.gameObject.SetActive(false);
                target1Tick.gameObject.SetActive(true);
               // AudioManager.instance.amazingAudio();
            }
        }
        else if (target == 2)
        {
            target2Amount.text = (int.Parse(target2Amount.text) - 1).ToString();

            if (int.Parse(target2Amount.text) <= 0)
            {
                target2Amount.gameObject.SetActive(false);
                target2Tick.gameObject.SetActive(true);
               //AudioManager.instance.exellentAudio();
            }
        }
        else if (target == 3)
        {
            target3Amount.text = (int.Parse(target3Amount.text) - 1).ToString();

            if (int.Parse(target3Amount.text) <= 0)
            {
                target3Amount.gameObject.SetActive(false);
                target3Tick.gameObject.SetActive(true);
               // AudioManager.instance.greatAudio();
            }
        }
        else if (target == 4)
        {
            target4Amount.text = (int.Parse(target4Amount.text) - 1).ToString();

            if (int.Parse(target4Amount.text) <= 0)
            {
                target4Amount.gameObject.SetActive(false);
                target4Tick.gameObject.SetActive(true);
                //AudioManager.instance.amazingAudio();
            }
        }
    }
}
