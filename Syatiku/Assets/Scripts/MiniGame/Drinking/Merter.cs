using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merter : MonoBehaviour {

    Slider _slider;
    private void Start()
    {
        _slider = GameObject.Find("InfoMeter").GetComponent<Slider>();
    }
    float _score = 0;
    private void Update()
    {
        _score +=0.01f;
        if (_score > _slider.maxValue)
        {
            _score = _slider.minValue;
        }
        _slider.value = _score;
    }
    /*public GameObject InfoMeter;
    private int MaxValue;
    private int NowValue;
    private int t;
    private RectTransform rt;

    public void Start()
    {
    }
    public void Update()
    {
    }*//*
     public void Awake()
     {
         rt = transform.FindChild("Fill").gameObject.GetComponent<RectTransform>();
         maxValue = rt.sizeDelta.x;
         t = 1f;
     }
     public void UpdateValue (float t)
     {
         float x = Mathf.Lerp(0f, maxValue, t);
         rt.sizeDelta = new Vector2(x,rt.sizeDelta.y);
     }

     // Update is called once per frame
     void Update ()
     {
         t -= 0.02f;

         UpdateValue(t);

         if (t <= 0f)
         {
             t = 1f;
         }
     }
     private float t;
     private float maxValue;
     private RectTransform rt;*/
}
