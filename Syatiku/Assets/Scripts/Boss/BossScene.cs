using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossScene : MonoBehaviour {
    public static BossScene Instance { get; private set; }

    [SerializeField]
    GameObject standingBoss;
    [SerializeField]
    GameObject slappedBoss;
    [SerializeField]
    GameObject flickTextPrefab;
    [SerializeField]
    RectTransform flickTextBox;
    [SerializeField]
    DamagePointerController damagePointer;
    [SerializeField]
    UnityEngine.UI.Text timerText;

    //ボスシーンのゲーム時間
    [SerializeField]
    float gameTime;
    [SerializeField, Header("ダメージ判定までの成功回数")]
    int attackValue = 3;

    string[,] textContents = new string[,]
    {
        //Wrong
        { "あいうえお", "かきくけこ" },
        //Correct
        { "さしすせそ", "たちつてと" },
    };

    List<FlickTextController> flickTextList = new List<FlickTextController>();

    float spawnTextTimer;
    float spawnTextTime = 3.0f;

    int missCount;
    int successCount;

    private Vector3 touchStartPos;

    [Header("フリック成功アニメーション")]
    [SerializeField, Header("振動する時間")]
    float successDuration = 0.5f;
    [SerializeField, Header("振動する強さ")]
    float successStrength = 5f;
    [SerializeField, Header("振動する回数")]
    int successVibrate = 5;

    [Header("ボスダメージアニメーション")]
    [SerializeField, Header("振動する時間")]
    float damageDuration = 1f;
    [SerializeField, Header("振動する強さ")]
    float damageStrength = 10f;
    [SerializeField, Header("振動する回数")]
    int damageVibrate = 10;

    void Awake () {
        Instance = this.GetComponent<BossScene>();
	}

	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            //フリックの開始
            touchStartPos = Input.mousePosition;
        }

        UpdateTimer();
	}

    /// <summary>
    /// タイマーの更新
    /// </summary>
    void UpdateTimer()
    {
        if (gameTime < 0)
        {
            Result();
        }
        else
        {
            gameTime -= Time.deltaTime;
            timerText.text = gameTime.ToString("F0");
        }

        if (spawnTextTimer > spawnTextTime)
        {
            //テキストの生成
            SpawnFlickText();
        }
        spawnTextTimer += Time.deltaTime;
    }

    void SpawnFlickText()
    {
        //タイマー初期化
        spawnTextTimer = 0;
        spawnTextTime = Random.Range(1, 5);

        //タイプ決定(0:Wrong, 1:Correct)
        int typeNum = Random.Range(0, 2);
        //テキストの内容決定
        int textNum = Random.Range(0, textContents.GetLength(typeNum));

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

    /// <summary>
    /// テキストの移動速度、方向を更新
    /// </summary>
    public void SetMoveForce(ref Vector3 moveForce)
    {
        //離した位置
        Vector3 touchEndPos = Input.mousePosition;
        //フリックの長さ
        Vector3 flickLength = touchEndPos - touchStartPos;
        Vector3 newMoveForce = flickLength.normalized * flickLength.sqrMagnitude / 10000;

        //フリックが小さすぎなければ
        if (newMoveForce.x > 0.1f || newMoveForce.x < -0.1f ||
            newMoveForce.y > 0.1f || newMoveForce.y < -0.1f)
        {
            //値を変更
            moveForce = newMoveForce;
        }

    }

    public void MissCountUP()
    {
        missCount++;
        damagePointer.DamagePointDown();
    }

    public void SuccessCountUP()
    {
        successCount++;

        if (successCount % attackValue == 0)
        {
            ChangeBossState();
            damagePointer.DamagePointUp();
            slappedBoss.transform.DOShakePosition(damageDuration, damageStrength, damageVibrate);
            Invoke(((System.Action)ChangeBossState).Method.Name, damageDuration + 0.5f);
        }
        else
        {
            standingBoss.transform.DOShakePosition(successDuration, successStrength, successVibrate);
        }
    }

    public void ChangeBossState()
    {
        standingBoss.SetActive(!standingBoss.activeSelf);
        slappedBoss.SetActive(!slappedBoss.activeSelf);
    }

    void Result()
    {
        Debug.Log("ゲーム終了：結果発表");
    }
    
}
