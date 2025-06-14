using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingTips : MonoBehaviour
{

    public string[] tips;

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        text.text =  tips[Random.Range(0, tips.Length)];
    }
}
