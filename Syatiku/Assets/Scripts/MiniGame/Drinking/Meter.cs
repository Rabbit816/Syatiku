using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Meter : MonoBehaviour {
    public void Awake()
    {
         rt = GameObject.Find("TimeMeter").gameObject.GetComponent<RectTransform>();
         maxValue = rt.sizeDelta.x;
    }
    public void UpdateValue (float t)
    {
         float x = Mathf.Lerp(0.00f, maxValue, t);
         rt.sizeDelta = new Vector2(x,rt.sizeDelta.y);
    }
    /// <summary>
    /// メーターの減少及びシーン移動
    /// </summary>
    public void Update()
    {
        if (MeterON == true)
        {
            TimeMeter.SetActive(true);
            UpdateValue(value);
            value -= 0.001f;
            if (value < 0.00f || flg == true)
            {
                Common.Instance.ChangeScene(Common.SceneName.Result);
                if (value < 0.00f)
                {
                    TimeMeter.SetActive(false);
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
    /// <summary>
    /// 関数宣言
    /// </summary>
    public GameObject TimeMeter;
    private bool flg;
    public float value = 1f;
    private float maxValue;
    private RectTransform rt;
    public bool MeterON;
}
