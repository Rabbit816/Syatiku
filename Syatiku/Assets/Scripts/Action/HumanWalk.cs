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
        if(beginTime >= randTime && !isWalk)
            Walk();
	}

    /// <summary>
    /// 歩行関数
    /// </summary>
    /// <returns></returns>
    public void Walk()
    {
        isWalk = true;
        gameObject.transform.GetChild(0).transform.GetChild(0)
            .GetComponent<Image>().sprite = walk;  // 画像変更

        var randDis = Random.Range(minDis, maxDis); // ランダムな値取得
        var distance = transform.localPosition.x + randDis; // 移動距離
            
        transform.DOLocalMoveX(distance, moveTime).SetEase(Ease.Linear) // DOTweenで移動
            .OnComplete(() => // 移動処理終了後
            {
                gameObject.transform.GetChild(0).transform.GetChild(0)
                .GetComponent<Image>().sprite = idle; // 待機画像に変更

                randTime = Random.Range(1, 6);
                beginTime = 0; // 開始時間を初期化
                isWalk = false;
            });
    }
}
