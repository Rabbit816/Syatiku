using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctionPartController : MonoBehaviour {

    [SerializeField]
    RectTransform slappedBoss;
    [SerializeField]
    RectTransform harisen;
    [SerializeField]
    Vector3 harisenLeftPos;
    [SerializeField]
    Vector3 harisenRightPos;

    [SerializeField]
    float endTime;
    float timer = 0;
    //叩いた回数
    int slappedCount = 0;

    public void UpdateSanctionPart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (slappedCount == 0)
            {
                BossScene.Instance.ChangeBossState(slappedBoss.gameObject);
                harisen.gameObject.SetActive(true);
            }
            slappedCount++;
            Vector3 bossScale = slappedBoss.localScale;
            Vector3 harisenScale = harisen.localScale;
            bossScale.x *= -1;
            harisenScale.x *= -1;
            //向きの変更
            slappedBoss.localScale = bossScale;
            //位置の変更
            harisen.localScale = harisenScale;
            harisen.localPosition = harisenScale.x > 0 ? harisenLeftPos : harisenRightPos;
        }

        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        if (timer > endTime)
        {
            timer = 0;
            slappedCount = 0;
            slappedBoss.gameObject.SetActive(false);
            harisen.gameObject.SetActive(false);
            BossScene.Instance.ChangePart();
        }
    }
}
