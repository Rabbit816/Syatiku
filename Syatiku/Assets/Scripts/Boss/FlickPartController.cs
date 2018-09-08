using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlickPartController : MonoBehaviour {

    string[,] textContents;

    [SerializeField]
    Sprite[] slappedBossSprites;
    [SerializeField]
    GameObject slappedBoss;
    [SerializeField]
    RectTransform bomRect;
    [SerializeField]
    GameObject bom;
    [SerializeField]
    GameObject flickTextPrefab;
    [SerializeField]
    RectTransform flickTextBox;

    List<FlickTextController> flickTextList = new List<FlickTextController>();

    float spawnTextTimer;
    float spawnTextTime = 1.0f;

    //ボスシーンのゲーム時間
    [SerializeField]
    float gameTime;
    [SerializeField]
    UnityEngine.UI.Text timerText;

    float blinkInterval = 0.2f;
    float blinkTimer = 0;

    [Header("ボスダメージアニメーション")]
    [SerializeField, Header("振動する時間")]
    float duration = 1f;
    [SerializeField, Header("振動する強さ")]
    float strength = 10f;
    [SerializeField, Header("振動する回数")]
    int vibrate = 10;

    public void Initialize(int gameMode)
    {
        slappedBoss.GetComponent<UnityEngine.UI.Image>().sprite = slappedBossSprites[gameMode - 1];

        textContents = new string[,]
        {
            //Wrong
            { "知らん！" },
            //Correct
            { "制裁！" },
        };
        timerText.text = gameTime.ToString("F0");
    }

    public void UpdateFlickPart () {
        if (spawnTextTimer > spawnTextTime)
        {
            //テキストの生成
            SpawnFlickText();
        }

        UpdateTimer();
    }

    /// <summary>
    /// タイマーの更新
    /// </summary>
    void UpdateTimer()
    {
        //ゲームタイマー
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            timerText.text = Mathf.Ceil(gameTime).ToString();
        }
        else
        {
            BossScene.Instance.Result();
        }
        //タイマーの点滅
        if(gameTime < 10)
        {
            if (blinkTimer > blinkInterval)
            {
                blinkTimer = 0;
                //少しずつインターバルを短く
                blinkInterval -= 0.002f;
                bom.SetActive(!bom.activeSelf);
            }
            blinkTimer += Time.deltaTime;
        }
        //テキストタイマー
        spawnTextTimer += Time.deltaTime;
    }

    void SpawnFlickText()
    {
        //タイマー初期化
        spawnTextTimer = 0;
        spawnTextTime = Random.Range(0.1f, 2f);

        //タイプ決定(0:Wrong, 1:Correct)
        int typeNum = Random.Range(0, 2);
        //テキストの内容決定
        int textNum = Random.Range(0, textContents.GetLength(1));
        for (int i = 0; i < flickTextList.Count; i++)
        {

            //使われていない（非表示中の）ものがあれば再利用
            if (!flickTextList[i].gameObject.activeSelf)
            {
                flickTextList[i].Initialize(typeNum, textContents[typeNum, textNum]);
                flickTextList[i].gameObject.SetActive(true);
                return;
            }
        }

        //全て使用中なら新しく生成
        FlickTextController text = Instantiate(flickTextPrefab).GetComponent<FlickTextController>();
        //sarcasmMessageBoxの子要素に
        text.transform.SetParent(flickTextBox, false);
        text.Initialize(typeNum, textContents[typeNum, textNum]);
        //リストに追加
        flickTextList.Add(text);
    }

    public void FlickSuccess()
    {
        BossScene.Instance.ChangeBossState(slappedBoss, duration, true);
        slappedBoss.transform.DOShakePosition(duration, strength, vibrate);
    }
}
