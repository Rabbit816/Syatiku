using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DenmokuMeter : MonoBehaviour {

    public Slider TimeMeter;
    [HideInInspector] public bool TimeMeterFlg;
    [Range(1, 60), Tooltip("デンモクの制限時間(秒)")] public float Timer;

	void Start () {
        this.TimeMeter.maxValue = Timer;
        this.TimeMeter.value = this.TimeMeter.maxValue;
	}
	
	
	void Update () {
        if (TimeMeterFlg)
        {
            TimeMeter.value -= Time.deltaTime;
            
            //時間切れの処理
            if(TimeMeter.value == 0)
            {
                TimeMeterFlg = false;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
        }
	}
}
