using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctionPart : MonoBehaviour {

    //制裁画面の表示時間
    [SerializeField]
    float sanctionTime = 1.5f;
    float timer;

	void Update () {
        timer += Time.deltaTime;

        if(timer > sanctionTime)
        {
            timer = 0;
            BossScene.Instance.ChangePart();
        }
	}
}
