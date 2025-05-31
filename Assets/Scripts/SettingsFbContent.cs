using UnityEngine;
using System.Collections;

public class SettingsFbContent : MonoBehaviour
{

    public Transform settings_main;
    public Transform settings_gameplay;

    public static SettingsFbContent instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }
}
