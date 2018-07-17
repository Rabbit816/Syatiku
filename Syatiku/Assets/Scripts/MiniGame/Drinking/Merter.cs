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
         //UpdateValue(value);
    }
    public void Update()
    {
        value -= 0.0002f;
        UpdateValue(value);
        if (value <= 0)
        {
            value = 1f;
        }
    }

    public void Moving()
    {
        Debug.Log("Moving");
    }
    /// <summary>
    /// シーン遷移
    /// </summary>
    /*public void Results()
    {
        Common.Instance.ChangeScene(Common.SceneName.Result);
    }*/
    private float value;
    private float maxValue;
    private RectTransform rt;
}
