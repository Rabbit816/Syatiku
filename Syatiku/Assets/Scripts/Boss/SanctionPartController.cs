using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctionPartController : MonoBehaviour {

    [SerializeField]
    RectTransform slappedBoss;

    [SerializeField]
    float endTime;
    float timer;

    // Use this for initialization
    void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if(!slappedBoss.gameObject.activeSelf) BossScene.Instance.ChangeBossState(slappedBoss.gameObject);

            Vector3 scale = slappedBoss.localScale;
            scale.x *= -1;
            //向きの変更
            slappedBoss.localScale = scale;
        }
        
        UpdateTimer();
	}

    void UpdateTimer()
    {
        timer += Time.deltaTime;
        if (timer > endTime)
        {
            timer = 0;
            slappedBoss.gameObject.SetActive(false);
            BossScene.Instance.ChangePart();
        }
    }
}
