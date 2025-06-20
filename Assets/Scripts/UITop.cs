﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITop : MonoBehaviour 
{
    public Text levelText;
    public Text scoreText;
    public Text movesText;
    public Image progess;
    public Image progessmoves;
    public Image doll;
    public Image progressStar1;
    public Image progressStar2;
    public Image progressStar3;
    public static UITop instance;
    float progress = 0f;
    float progress1 = 0.33f;
    float progress2 = 0.66f;
    float progress3 = 1.00f;
    int star1;
    int star2;
    int star3;

    bool greeting1;
    bool greeting2;
    bool greeting3;

    float duration = 0.5f;
    int start;
    public int moves;

	void Start () 
    {
        if(!instance)
        {
            instance = this;
        }
		levelText.text = "Level " + StageLoader.instance.Stage.ToString();
        scoreText.text =    PlayerPrefs.GetInt("BaseScore").ToString();
      

        moves = StageLoader.instance.moves;

        if (Configuration.instance.beginFiveMoves == true)
        {
            itemGrid.instance.moveLeft += Configuration.instance.plusMoves;
        }

        movesText.text = moves.ToString();

		star1 = StageLoader.instance.score_Star_1;
		star2 = StageLoader.instance.score_Star_2;
		star3 = StageLoader.instance.score_Star_3;

        progess.fillAmount = 0;
        progessmoves.fillAmount = 1;

        //var name = "doll_" + StageLoader.instance.doll + "_1";
    }

    public void UpdateScoreAmount(int score)
    {
        StartCoroutine("StartUpdateScore", score);
    }

    IEnumerator StartUpdateScore(int target)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            scoreText.text = ((int)Mathf.Lerp((float)start, (float)target, (timer / duration))).ToString();

            yield return null;
        }

        start = target;

        scoreText.text = target.ToString();
    }

    public void DecreaseMoves(bool effect = false)
    {
        if (effect == true)
        {
            var explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RingExplosion()) as GameObject);
            explosion.transform.position = movesText.gameObject.transform.position;
        }

        if (itemGrid.instance.moveLeft >= 0)
        {
            //itemGrid.instance.moveLeft--;

            movesText.text = itemGrid.instance.moveLeft.ToString();            
            UpdateProgressBarMoves(itemGrid.instance.moveLeft);
        }        
    }

    // when user runs out of moves and click keep playing
    public void Set5Moves()
    {
        var explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RingExplosion()) as GameObject);
        explosion.transform.position = movesText.gameObject.transform.position;

        itemGrid.instance.moveLeft = 5;

        movesText.text = itemGrid.instance.moveLeft.ToString();
    }
    public void Set2Moves()
    {
        var explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RingExplosion()) as GameObject);
        explosion.transform.position = movesText.gameObject.transform.position;

        itemGrid.instance.moveLeft = 4;

        movesText.text = itemGrid.instance.moveLeft.ToString();
    }

    public void UpdateProgressBar(int score)
    {       
          
       
        if (score < star1)
        {
            progress = ((float)score / (float)star1) * progress1;
        }
        else if (star1 <= score && score < star2)
        {
            progress = progress1 + (((float)score - (float)star1) / ((float)star2 - (float)star1)) * (progress2 - progress1);

            if (greeting1 == false)
            {
                greeting1 = true;
                

			//	var name = "doll_" + StageLoader.instance.doll + "_2";
                StartCoroutine(Star2Gold(progressStar1));
            }
        }
        else if (star2 <= score && score < star3)
        {
            progress = progress2 + (((float)score - (float)star2) / ((float)star3 - (float)star2)) * (progress3 - progress2);

            if (greeting2 == false)
            {
                greeting2 = true;


			//	var name = "doll_" + StageLoader.instance.doll + "_3";
                StartCoroutine(Star2Gold(progressStar2));
            }
        }
        else if (score >= star3)
        {
            progress = progress3;

            if (greeting3 == false)
            {
                greeting3 = true;
                

			//	var name = "doll_" + StageLoader.instance.doll + "_4";
                StartCoroutine(Star2Gold(progressStar3));
            }
        }


        StartCoroutine("StartUpdateProgress", progress);
    }
    public void UpdateProgressBarMoves(int moves)
    {
         progessmoves.fillAmount =(float)moves/ (float)StageLoader.instance.moves;             
    }

    IEnumerator StartUpdateProgress(float progress)
    {
        float start = progess.fillAmount;

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            progess.fillAmount = Mathf.Lerp(start, progress, (timer / duration));

            yield return null;
        }
    }

    IEnumerator Star2Gold(Image progressStar)
    {
        yield return new WaitForSeconds(duration);

        progressStar.sprite = Resources.Load<GameObject>(Configuration.ProgressGoldStar()).GetComponent<SpriteRenderer>().sprite;
    }
}
