using UnityEngine;
using System.Collections;

public class ScaleToScreenSize : MonoBehaviour 
{

    void Start () 
    {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2.5f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        //Vector3 xWidth = transform.localScale;
        //xWidth.x = worldScreenWidth / width;
        //transform.localScale = xWidth;

        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;

        Vector3 xWidth = transform.localScale;
        xWidth.x = yHeight.y * 0.8256f;
        transform.localScale = xWidth;

    }
}
