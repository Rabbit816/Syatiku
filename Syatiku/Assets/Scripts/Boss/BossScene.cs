using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScene : MonoBehaviour {
    public static BossScene Instance { get; private set; }
    enum GameState
    {
        Flick,
        Sanction,
        GameEnd,
    }
    GameState gameState = 0;

    [SerializeField]
    GameObject sanctionPartCanvas;
    [SerializeField]
    GameObject flickPartCanvas;

    [SerializeField]
    GameObject sarcasmTextPrefab;
    [SerializeField]
    RectTransform sarcasmTextBox;
    List<GameObject> sarcasmTextList = new List<GameObject>();

    float spawnTextTimer;
    float spawnTextTime = 3.0f;

    [SerializeField]
    int attackGageMax = 3;
    int attackGage;
    [SerializeField]
    int damageGageMax;
    int damageGage;

    //フリックの成功、失敗回数
    int missCount;
    int successCount;

    //ボスシーンのゲーム時間
    [SerializeField]
    float gameTime;

    private static Vector3 touchStartPos;

    void Awake () {
        Instance = this.GetComponent<BossScene>();
	}
	
	void Update () {

        switch (gameState)
        {
            case GameState.Flick:
                UpdateFlickPart();
                break;
            case GameState.Sanction:
                UpdateSanctionPart();
                break;
            case GameState.GameEnd:
                Result();
                break;
        }

        if(gameTime < 0)
        {
            gameState = GameState.GameEnd;
        }
	}

    /// <summary>
    /// フリックパートの動作
    /// </summary>
    void UpdateFlickPart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //フリックの開始
            touchStartPos = Input.mousePosition;

            //-- 仮
            //攻撃ゲージが満タン時
            if (attackGage == attackGageMax)
            {
                ChangePart();
                attackGage = 0;
            }
            //--
        }

        if (spawnTextTimer > spawnTextTime)
        {
            //テキストの生成
            SpawnSarcasmText();
        }
        spawnTextTimer += Time.deltaTime;
        gameTime -= Time.deltaTime;
    }

    /// <summary>
    /// 制裁パートの動作
    /// </summary>
    void UpdateSanctionPart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangePart();
        }
    }

    void ChangePart()
    {
        flickPartCanvas.SetActive(!flickPartCanvas.activeSelf);
        sanctionPartCanvas.SetActive(!sanctionPartCanvas.activeSelf);
        if (gameState == GameState.Flick)
        {
            gameState = GameState.Sanction;
        }
        else
        {
            gameState = GameState.Flick;
        }
    }

    void SpawnSarcasmText()
    {
        //タイマー初期化
        spawnTextTimer = 0;
        spawnTextTime = Random.Range(1, 5);

        for (int i = 0; i < sarcasmTextList.Count; i++) {

            //使われていない（非表示中の）ものがあれば再利用
            if (!sarcasmTextList[i].activeSelf)
            {
                sarcasmTextList[i].SetActive(true);
                return;
            }
        }

        //全て使用中なら新しく生成
        GameObject sarcasmText = Instantiate(sarcasmTextPrefab);
        //sarcasmMessageBoxの子要素に
        sarcasmText.transform.SetParent(sarcasmTextBox, false);
        //リストに追加
        sarcasmTextList.Add(sarcasmText);
    }

    /// <summary>
    /// テキストの移動速度、方向を更新
    /// </summary>
    /// <param name="moveDir"></param>
    /// <param name="moveSpeed"></param>
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
    }

    /// <summary>
    /// 攻撃ゲージの上昇
    /// </summary>
    public void AttackGageAccumulate()
    {
        if (attackGage < attackGageMax)
        {
            attackGage += 1;
        }
        successCount++;
    }

    void Result()
    {
        Debug.Log("ゲーム終了：結果発表");
    }
    
}
