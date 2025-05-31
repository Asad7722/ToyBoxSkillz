using UnityEngine;
using System.Collections;

public class NotifButton : ToggleButton
{
    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        btnToggleGraphicsPosition = btnToggleGraphics.rectTransform.anchoredPosition;
    }

    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
        Invoke("initNotifStatus", 0.1F);
    }

    /// <summary>
    /// Inits the music status.
    /// </summary>
    void initNotifStatus()
    {
        bool isNotifEnabled = AudioManager.instance.isNotifEnabled;
        Vector2 points = btnToggleGraphics.rectTransform.anchoredPosition;
        btnToggleGraphics.rectTransform.anchoredPosition = new Vector2(((isNotifEnabled) ? (Mathf.Abs(points.x)) : (Mathf.Abs(points.x))), points.y);
    }

    /// <summary>
    /// Raises the toggle status changed event.
    /// </summary>
    /// <param name="status">If set to <c>true</c> status.</param>
    public override void OnToggleStatusChanged(bool status)
    {
        AudioManager.instance.PlayButtonClickSound();
        AudioManager.instance.ToggleNotifStatus(status);
    }
}
