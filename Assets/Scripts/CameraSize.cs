using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using EZCameraShake;


public class CameraSize : MonoBehaviour
{
   
    public static CameraSize instance = null;
    public static float CameraCurrentSize = 6.0f;
    public bool Shake;
    public bool MedShake;
    public bool BigShake;
    public bool MinShake;  
    public bool MinElastic;
    public bool TinyElastic;
    public float MaxZoom = 0.2f;
    public float smooth = 4, MaxZoomRocket = 1.7f, SmoothRocket = 4, bombsmooth = 0.2f;
    public bool isZoomed = false;
    

    Vector3 posInf = new Vector3(0.25f, 0.25f, 0.25f);
    Vector3 rotInf = new Vector3(1, 1, 1);
    public float magn = 1, rough = 12, fadeIn = 0.1f, fadeOut = 1f;
    public float medmagn = 1.5f, medrough = 18, medfadeIn = 0.1f, medfadeOut = 1.5f;
    public float bigmagn = 2, bigrough = 25, bigfadeIn = 0.1f, bigfadeOut = 2f;




    // Use this for initialization
    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        Aspect();

    }

    // Update is called once per frame
    void FixedUpdate()    {

        #region Shake
        if (BigShake)
        {
            startBigShakeCamera();
            //ShakeBox(30);
        }
        if (MedShake)
        {
            startMedShakeCamera();
            //ShakeBox(25);
        }
        if (Shake)
        {
            startShakeCamera();
            //ShakeBox(20);
        }
        if (MinShake)
        {
            MinShake = false;
            startShakeCameraManual(0.2f, 3, 0.1f, 1);
            //float CameraSize = CameraCurrentSize;
            //GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, CameraSize + MaxZoomRocket, Time.deltaTime * SmoothRocket);

        }
        if (MinElastic)
        {
            MinElastic = false;
            startShakeCameraManual(0.2f, 3, 0.1f, 1);
            //float CameraSize = CameraCurrentSize;
            //GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, CameraSize + 2f * MaxZoomRocket, Time.deltaTime * 0.1f * SmoothRocket);

        }

        if (TinyElastic)
        {
            TinyElastic = false;
            startShakeCameraManual(0.2f, 3, 0.1f, 1);
            //float CameraSize = CameraCurrentSize;
            //GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, CameraSize + 12f * MaxZoomRocket, Time.deltaTime * 0.1f * SmoothRocket);

        }
        //else
        //{
        //    float CameraSize = CameraCurrentSize;
        //    GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, CameraSize, Time.deltaTime * SmoothRocket);
        //}

        #endregion

    }
    public void startShakeCameraManual(float magn, float rough, float fadeIn, float fadeOut)
    {
        Shake = false;
        CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(magn, rough, fadeIn, fadeOut);
        c.PositionInfluence = posInf;
        c.RotationInfluence = rotInf;
        //vibration();
    }
    public void ShakeBox(float rotate)
    {
        //StartCoroutine(StartShakeBox(rotate));
        var boxes = itemGrid.instance.GetListItems();
        foreach (var item in boxes)
        {
            if (item != null && item.IsCookie())
            {
                iTween.ShakeRotation(item.gameObject, iTween.Hash(
                  "name", "HintAnimation",
                  "amount", new Vector3(0f, 0f, rotate),
                  "easetype", iTween.EaseType.easeOutBounce,
                  //"looptype", iTween.LoopType.pingPong,
                  //"oncomplete", "OnCompleteShowHint",
                  //"oncompletetarget", gameObject,
                  // "oncompleteparams", new Hashtable() { { "item", item } },
                  "time", 1f
                   ));
                //yield return new WaitForSeconds(0.1f);
                //item.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
    }


    IEnumerator StartShakeBox(float rotate)
    {
        var boxes = itemGrid.instance.GetListItems();
        foreach (var item in boxes)
        {
            if (item != null && item.IsCookie())
            {
                iTween.ShakeRotation(item.gameObject, iTween.Hash(
                  "name", "HintAnimation",
                  "amount", new Vector3(0f, 0f, rotate),
                  "easetype", iTween.EaseType.easeOutBounce,
                  //"looptype", iTween.LoopType.pingPong,
                  //"oncomplete", "OnCompleteShowHint",
                  //"oncompletetarget", gameObject,
                  // "oncompleteparams", new Hashtable() { { "item", item } },
                  "time", 1f
                   ));
                yield return new WaitForSeconds(1f);
                item.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
    }

    public void Aspect()
    {
        #region Aspect
        float w = Screen.width;
        float h = Screen.height;

        int row = StageLoader.instance.row;
        int column = StageLoader.instance.column;
        var aspect = h / w;
        //Debug.Log(" var W = " + Screen.width);
        //Debug.Log(" var H = " + Screen.height);
        //Debug.Log(" var aspect = " + aspect);
        if (aspect >= 1.45f && aspect < 1.7f)
        {
            if (row > 8)
            {
                GetComponent<Camera>().orthographicSize = aspect * 4.55f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row == 8)
            {
                GetComponent<Camera>().orthographicSize = aspect * 4.7f - 0.5f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row == 7)
            {
                GetComponent<Camera>().orthographicSize = aspect * 4.6f - 1.0f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row == 6)
            {
                GetComponent<Camera>().orthographicSize = aspect * 5.2f - 2.0f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row <= 5)
            {
                GetComponent<Camera>().orthographicSize = aspect * 5.3f - 2.0f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }

        }
        else if (aspect >= 1.7f)
        {
            if (row > 8)
            {
                GetComponent<Camera>().orthographicSize = aspect * 4.40f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row == 8)
            {
                GetComponent<Camera>().orthographicSize = aspect * 4.35f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row == 7)
            {
                GetComponent<Camera>().orthographicSize = aspect * 4.10f - 0.50f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row == 6)
            {
                GetComponent<Camera>().orthographicSize = aspect * 4.0f - 1.25f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row <= 5)
            {
                GetComponent<Camera>().orthographicSize = aspect * 4.0f - 1.25f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }


        }
        else
        {
            if (row > 8)
            {
                GetComponent<Camera>().orthographicSize = aspect * 7f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row == 8)
            {
                GetComponent<Camera>().orthographicSize = aspect * 6.0f - 0.5f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row == 7)
            {
                GetComponent<Camera>().orthographicSize = aspect * 6.2f - 2.5f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row == 6)
            {
                GetComponent<Camera>().orthographicSize = aspect * 6.5f - 3.5f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }
            else if (row <= 5)
            {
                GetComponent<Camera>().orthographicSize = aspect * 6.5f - 3.5f;
                CameraCurrentSize = GetComponent<Camera>().orthographicSize;
            }



        }
        #endregion
    }
    public void ResetCamera()
    {
        float CameraSize = CameraCurrentSize;
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, CameraSize, Time.deltaTime * SmoothRocket);

    }
    public void startShakeCamera()
    {
        Shake = false;
        CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(magn, rough, fadeIn, fadeOut);
        c.PositionInfluence = posInf;
        c.RotationInfluence = rotInf;
    }
    public void startMedShakeCamera()
    {
        MedShake = false;
        CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(medmagn, medrough, medfadeIn, medfadeOut);
        c.PositionInfluence = posInf;
        c.RotationInfluence = rotInf;
    }
    public void startBigShakeCamera()
    {
        BigShake = false;
        CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(bigmagn, bigrough, bigfadeIn, bigfadeOut);
        c.PositionInfluence = posInf;
        c.RotationInfluence = rotInf;
    }
}
