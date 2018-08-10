using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DenmokuMeter : MonoBehaviour {

    [HideInInspector] public Slider TimeMeter;
    [HideInInspector] public bool TimeMeterFlg;
    [SerializeField, Tooltip("制限時間が減る間隔")] private float Timer;

	void Start () {
        this.TimeMeter = GameObject.Find("Denmoku/TimeMeter").GetComponent<Slider>();
        this.TimeMeter.value = 1.0f;
	}
	
	
	void Update () {
        if (TimeMeterFlg)
        {
            TimeMeter.value -= Timer;
            if(TimeMeter.value == 0)
            {
                TimeMeterFlg = false;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
        }
	}
}
