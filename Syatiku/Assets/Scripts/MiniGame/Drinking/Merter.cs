using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
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
            if (!timeover && value < 0)
            {
                timeover = true;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
            else
            {
                value -= 0.001f;
            }
            UpdateValue(value);
        }
    }
    public void Moving()
    {

    }
    /// <summary>
    /// 関数宣言
    /// </summary>
    public float value = 1f;
    private float maxValue;
    private RectTransform rt;
    public int AnswerCounter;
    public bool MeterON;
    bool timeover = false;
}
