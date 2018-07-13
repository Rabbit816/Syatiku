using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HumanWalk : MonoBehaviour {
    // 人間画像
    [SerializeField]
    private Sprite idle, walk;

    // 移動距離限界
    [SerializeField]
    private float minDis, maxDis;

    [SerializeField]
    private float moveTime;

    private float beginTime;
    private int randTime;
    private bool isWalk = false;
    
    void Start () {
        randTime = Random.Range(1, 6);// ランダムに時間を取得
    }
	
	void Update () {
        beginTime += Time.deltaTime; // walk開始の時間まで
        if(beginTime >= randTime)
        {
            isWalk = true;
            if (!isWalk) return;
            StartCoroutine(Walk());
        }
	}

    public IEnumerator Walk()
    {
        gameObject.GetComponent<Image>().sprite = walk;
        var randDis = Random.Range(minDis, maxDis);
        var distance = transform.localPosition.x + randDis;
        transform.localPosition = new Vector2(distance, transform.localPosition.y);
        transform.DOMoveX(distance, moveTime).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                gameObject.GetComponent<Image>().sprite = idle;
                beginTime = 0;
            });
        isWalk = false;
        yield return null;
    }
}
