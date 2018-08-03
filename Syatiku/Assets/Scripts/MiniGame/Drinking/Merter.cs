using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Merter : MonoBehaviour {
    public void Awake()
     {
         rt = GameObject.Find("InfoMeter").gameObject.GetComponent<RectTransform>();
         maxValue = rt.sizeDelta.x;
    }

    public void UpdateValue (float t)
     {
         float x = Mathf.Lerp(0f, maxValue, t);
         rt.sizeDelta = new Vector2(x,rt.sizeDelta.y);
    }
    public void Update()
    {
        if (MeterON == true)
        {
            UpdateValue(value);
            value -= 0.002f;
            if (value < 0 || flg == true)
            {
                Common.Instance.ChangeScene(Common.SceneName.Result);
                if (value < 0)
                {
                    flg = true;
                    MeterON = false;
                }
            }
        }
        
    }



    public void Moving()
    {
        flg = false;
    }
    private bool flg;
    public float value = 1f;
    private float maxValue;
    private RectTransform rt;
    public int AnswerCounter;
    public bool MeterON;
}
