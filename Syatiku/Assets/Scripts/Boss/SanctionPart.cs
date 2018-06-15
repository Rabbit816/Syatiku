using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctionPart : MonoBehaviour {

    [SerializeField]
    float sanctionDisplayTime = 1.5f;
    float timer;

	void Update () {
        timer += Time.deltaTime;

        if(timer > sanctionDisplayTime)
        {
            timer = 0;
            BossScene.Instance.ChangePart();
        }
	}
}
