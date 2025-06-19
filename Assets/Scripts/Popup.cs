using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Color backgroundColor = new Color(10.0f / 255.0f, 10.0f / 255.0f, 10.0f / 255.0f, 0.6f);
    public bool AutoClose = false;
    public float AutoCloseSeconds = 1.0f;

    private GameObject m_background;

    void Start()
    {
        if (AutoClose)
        {
            Invoke("Close", AutoCloseSeconds);
        }
    }

    public void Open()
    {
        //	AddBackground();
    }

    public void Close()
    {
       // var animator = GetComponent<Animator>();
       // if (animator.GetCurrentAnimatorStateInfo(0).IsName("PopupOpen"))
          //  animator.Play("PopupClose");

        //RemoveBackground();
        StartCoroutine(RunPopupDestroy());
        AudioManager.instance.ButtonClickAudio();
    }

    public void Closeilk()
    { 
        RemoveBackgroundilk();
        StartCoroutine(RunPopupDestroy());
    }
    public void CloseRandomPlay()
    {
        Configuration.instance.RandomPlay = false;
        Configuration.instance.GiftBox = false;
        Configuration.instance.ArenaMode = false;
        Configuration.instance.EpisodePlay = false;
        Configuration.instance.playing = false;
        Configuration.instance.RewardPlay = false;
        AudioManager.instance.ButtonClickAudio();
    }

    private IEnumerator RunPopupDestroy()
    {
        var animator = GetComponent<Animator>();
         if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            animator.Play("Close");

        yield return new WaitForSeconds(0.13f);
        Destroy(m_background);
        Destroy(gameObject);
        Configuration.instance.CloseClick = true;
        yield return new WaitForSeconds(0.5f);
        Configuration.instance.CloseClick = false;
        Time.timeScale = 1;
    }

    private void AddBackground()
    {
        var bgTex = new Texture2D(1, 1);
        bgTex.SetPixel(0, 0, backgroundColor);
        bgTex.Apply();

        m_background = new GameObject("PopupBackground");
        var image = m_background.AddComponent<Image>();
        var rect = new Rect(0, 0, bgTex.width, bgTex.height);
        var sprite = Sprite.Create(bgTex, rect, new Vector2(0.5f, 0.5f), 1);
        image.material.mainTexture = bgTex;
        image.sprite = sprite;
        var newColor = image.color;
        image.color = newColor;
        image.canvasRenderer.SetAlpha(0.0f);
        image.CrossFadeAlpha(1.0f, 0.4f, false);

        var canvas = GameObject.Find("Page 4");
        m_background.transform.localScale = new Vector3(1, 1, 1);
        m_background.GetComponent<RectTransform>().sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
        m_background.transform.SetParent(canvas.transform, false);
        m_background.transform.SetSiblingIndex(transform.GetSiblingIndex());
    }

    private void RemoveBackground()
    {
        /*var image = m_background.GetComponent<Image>();
		if (image != null)
			image.CrossFadeAlpha(0.0f, 0.2f, false);*/
    }

    private void RemoveBackgroundilk()
    {
        //  var image = m_background.GetComponent<Image>();
        //  if (image != null)
        //      image.CrossFadeAlpha(0.0f, 0.2f, false);
    }
}

