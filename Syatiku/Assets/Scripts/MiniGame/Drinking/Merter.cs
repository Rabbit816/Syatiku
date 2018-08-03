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
        if (MeterON)
        {

            UpdateValue(value);
            value -= 0.001f;

            if (value <= 0)
            {
                Common.Instance.ChangeScene(Common.SceneName.Result);
                value = 1f;
            }
            if (value > 1)
            {
                value = 1f;
            }
        }
        
    }



    public void Moving()
    {
       /* if (AnswerCounter == 1)
        {
            value += 0.05f;
        }else if (AnswerCounter == 2)
        {
            value += 0.1f;
        }else if (AnswerCounter == 3)
        {
            value += 0.15f;
        }else if (AnswerCounter == 4)
        {
            value += 0.2f;
        }*/
    }
    public float value = 1f;
    private float maxValue;
    private RectTransform rt;
    public int AnswerCounter;
    public bool MeterON;
}
