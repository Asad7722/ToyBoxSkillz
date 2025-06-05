using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{


    public Animator animator;

    public GameObject MainPopupDestroy;
    public void GamePause()
    {
        Time.timeScale = 0f;
    }
    public void GameResume()
    {
        Debug.LogError("Game resumed... ");
        Time.timeScale = 1f;
    }
    public void PauseAnim()
    {
        animator.speed = 0f;
    }
    public void DestroyPopup()
    {
        GameResume();
        Destroy(MainPopupDestroy);
    }
    public void ResumeAnim()
    {
        animator.speed = 1f;
    }

    public void RestartAnim()
    {
        animator.Play("target", 0, 0f); // Replace "Run" with your animation state name
        animator.speed = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}