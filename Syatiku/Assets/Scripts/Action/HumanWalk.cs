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
    private float posX;
    private int moveDir = 1;
<<<<<<< HEAD
=======
    private bool dirFlag = false;
>>>>>>> master
    
    void Start () {
        randTime = Random.Range(1, 6);// ランダムに時間を取得
        posX = transform.localPosition.x;
    }
	
	void Update () {
        beginTime += Time.deltaTime; // walk開始の時間まで
        if (beginTime >= randTime && !isWalk)
        {
            var humanTransform = gameObject.transform.GetChild(0).transform.GetChild(0); // humanUIのTransform
<<<<<<< HEAD
            if (transform.localPosition.x > posX + 100 || transform.localPosition.x < posX) // positionで移動方向を決める
            {
                humanTransform.localScale = new Vector2(humanTransform.localScale.x * -1, 1);
                moveDir *= -1;
=======
            if (transform.localPosition.x > posX + 100) // positionで移動方向を決める
            {
                if (moveDir == -1) dirFlag = false;

                if(dirFlag)
                    humanTransform.localScale = new Vector2(humanTransform.localScale.x * -1, 1);
                moveDir = -1;
            }else if(transform.localPosition.x < posX)
            {
                if (moveDir == 1) dirFlag = true;
                if (dirFlag)
                    humanTransform.localScale = new Vector2(humanTransform.localScale.x * moveDir, 1);
                moveDir = 1;
>>>>>>> master
            }
                Walk(humanTransform);
        }
	}

    /// <summary>
    /// 歩行する関数
    /// </summary>
    /// <returns></returns>
    public void Walk(Transform human)
    {
        isWalk = true;
        human.GetComponent<Image>().sprite = walk;  // 画像変更

        var randDis = Random.Range(minDis, maxDis); // ランダムな値取得
        randDis *= moveDir;

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
