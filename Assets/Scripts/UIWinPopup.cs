using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.UIElements;


public class UIWinPopup : MonoBehaviour
{
    public float countDuration = 1.5f;

    public Text Label = null;
    public Text scoreText = null;
    public Text TimeBonusText = null;
    public Text TotalScoreText = null;
    public Text timetextsc, blockssc, movesrem, baseTotalsc, timeTotalsc, movesTotalsc;
    [Header("ScoresTexts")]

    public Text baseScoretxt, singleBreakertxt,
         columnBreakertxt,
          rowBreakertxt,
          rainbowBreakertxt,
          ovenBreakertxt;
    [Header("ScoresMultiplierTexts")]
    public Text baseScoreMultipliertxt, singleBreakerMultipliertxt,
         columnBreakerMultipliertxt,
          rowBreakerMultipliertxt,
          rainbowBreakerMultipliertxt,
          ovenBreakerMultipliertxt;
    public Text StarAmountText = null;
    public Text TotalStarAmountText = null;

    public GameObject loadingScreen;



    public GameObject ButtonPlay = null;

    public GameObject ArenaStar = null;
    public GameObject ArenaRutbe = null;



    public Text ArenaRutbeText = null;
    public bool x2Star = false;
    public bool x2Score = false;

    [Header("Scores")]
    public int baseScore, singleBreakerScore, columnBreakerScore, rowBreakerScore, rainbowBreakerScore, ovenBreakerScore, totalScore;

    [Header("Scores Multiplier")]
    public int baseScoreMultiplier, singleBreakerScoreMultiplier, columnBreakerScoreMultiplier, rowBreakerScoreMultiplier, rainbowBreakerScoreMultiplier, ovenBreakerScoreMultiplier;

    void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
    }


    void Start()
    {
        var board = GameObject.Find("Board").GetComponent<itemGrid>();

        BackgroundMusic.instance.WinMusic();
        int openedLevel = CoreData.instance.GetOpendedLevel();

        var star = board.star;
        int StarChallengeNum = PlayerPrefs.GetInt("StarChallengeNum");
        int arenanumber = PlayerPrefs.GetInt("arenanumber");


        baseScoreMultipliertxt.text = "x"+ baseScoreMultiplier.ToString();
        singleBreakerMultipliertxt.text = "x" + singleBreakerScoreMultiplier.ToString();
        columnBreakerMultipliertxt.text = "x" + columnBreakerScoreMultiplier.ToString();
        rowBreakerMultipliertxt.text = "x" + rowBreakerScoreMultiplier.ToString();
        rainbowBreakerMultipliertxt.text = "x" + rainbowBreakerScoreMultiplier.ToString();
        ovenBreakerMultipliertxt.text = "x" + ovenBreakerScoreMultiplier.ToString();

        baseScore = PlayerPrefs.GetInt("BaseScore",0)*baseScoreMultiplier;
        singleBreakerScore = CoreData.instance.singleBreaker*singleBreakerScoreMultiplier;
        columnBreakerScore = CoreData.instance.columnBreaker*columnBreakerScoreMultiplier;
        rowBreakerScore = CoreData.instance.rowBreaker*rowBreakerScoreMultiplier;
        rainbowBreakerScore = CoreData.instance.rainbowBreaker*rainbowBreakerScoreMultiplier;
        ovenBreakerScore = CoreData.instance.ovenBreaker*ovenBreakerScoreMultiplier;

        baseScoretxt.text =baseScore.ToString();
        singleBreakertxt.text = singleBreakerScore.ToString();
        columnBreakertxt.text = columnBreakerScore.ToString();
        rowBreakertxt.text = rowBreakerScore.ToString();
        rainbowBreakertxt.text = rainbowBreakerScore.ToString();
        ovenBreakertxt.text = ovenBreakerScore.ToString();

        int remaingTime = (int)Timer.instance.timeRemaining * 25;
        int secondsrem = (int)Timer.instance.timeRemaining;




        blockssc.text = baseScore.ToString();
        baseTotalsc.text = baseScore.ToString();


        int baseScoreFromPrefs = PlayerPrefs.GetInt("BaseScore");
        Debug.Log("Base Score from PlayerPrefs: " + baseScoreFromPrefs);
        Debug.Log("Base Score Multiplier: " + baseScoreMultiplier);
        //   timetextsc.text = secondsrem + "";
    //    timeTotalsc.text = remaingTime.ToString();
    //    movesrem.text = itemGrid.instance.moveLeft.ToString();
        int movesmult = itemGrid.instance.moveLeft * 10;
    //    movesTotalsc.text = movesmult.ToString();
        totalScore = baseScore + singleBreakerScore + columnBreakerScore + rowBreakerScore + rainbowBreakerScore + ovenBreakerScore;


        Debug.LogError("Before Animate score "+baseScore);
    //    AnimateScore(TotalScoreText, totalScore);

        TotalScoreText.text="Total Score: "+ totalScore.ToString();


        SkillzCrossPlatform.SubmitScore(totalScore, OnSuccess, OnFailure);

        ButtonPlay.SetActive(true);


    }

    private IEnumerator AnimateScore(Text scoreText, int targetScore)
    {
        int currentValue = 0;
        float duration = 0.5f; // Duration of the animation
        float elapsedTime = 0f;
        Debug.LogError("Before While Animate score ");
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            currentValue = Mathf.RoundToInt(Mathf.Lerp(0, targetScore, t));
            scoreText.text = currentValue.ToString();

            Debug.LogError("Set Animate score " + currentValue);
            yield return null;
        }
        Debug.LogError("After Animate score ");

        scoreText.text = targetScore.ToString();
    }


    private void OnSuccess()
    {
        ButtonPlay.SetActive(true);

    }

    private void OnFailure(string temp)
    {
        //thiscomment
        SkillzCrossPlatform.DisplayTournamentResultsWithScore(totalScore);

    }




    public void Showtoybox(float time)
    {
        StartCoroutine(StartShowToyBox(time));
    }

    IEnumerator StartShowToyBox(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<Animator>().Play("toyboxwinyandangel");
        toy.instance.closeNewToy();
        AudioManager.instance.CollectTargetAudio();
    }

    public void likethislevel()
    {
        AudioManager.instance.ButtonClickAudio();
    }
    public void dislikethislevel()
    {
        AudioManager.instance.ButtonClickAudio();
    }





    public void zipla()
    {
        AudioManager.instance.DropAudio();
    }
    public void bostikirti()
    {
        AudioManager.instance.PopupBosTikirtiAudio();
    }
    public void dolutikirti()
    {
        AudioManager.instance.PopupDolutikirtiAudio();
    }
    public void swosh()
    {
        AudioManager.instance.PopupSwoshAudio();
    }
    public void bonuscoins()
    {
        AudioManager.instance.PopupBonusCoinsAudio();
    }

    public void MapAutoPopup()
    {
        Configuration.instance.LeveltoPlay = UnityEngine.Random.Range(0, 5);
        PlayerPrefs.SetInt("SkilzzEnd", 0);
        SkillzCrossPlatform.ReturnToSkillz();
    }


    public void doubleScore()
    {
        AudioManager.instance.ButtonClickAudio();
        //sameer
        x2Score = true;
        x2Star = false;


    }
    public void doubleStar()
    {
        AudioManager.instance.ButtonClickAudio();
        //sameer
        x2Score = false;
        x2Star = true;

    }



}
