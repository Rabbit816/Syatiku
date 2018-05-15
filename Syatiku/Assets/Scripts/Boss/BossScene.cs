using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScene : MonoBehaviour {

    [SerializeField]
    GameObject sarcasmTextPrefab;
    [SerializeField]
    RectTransform sarcasmTextBox;
    List<GameObject> sarcasmTextList = new List<GameObject>();

    static Image attackGageMask;
    [SerializeField]
    Image bossDamageGageMask;
    [SerializeField]
    GameObject tapEffect;

    float spawnTextTimer;
    float spawnTextTime = 3.0f;

    private static Vector3 touchStartPos;

    void Start () {
        attackGageMask = GameObject.Find("AttackGageMask").GetComponent<Image>();
	}
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            //フリックの開始
            touchStartPos = Input.mousePosition;

            //確認用にマウスダウンで攻撃
            HarisenAttack();

            if (!tapEffect.activeSelf)
            {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                tapEffect.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
                tapEffect.SetActive(true);
            }
        }

        if(spawnTextTimer > spawnTextTime)
        {
            SpawnSarcasmText();
        }

        spawnTextTimer += Time.deltaTime;
	}

    void SpawnSarcasmText()
    {
        //タイマー初期化
        spawnTextTimer = 0;
        spawnTextTime = Random.Range(3, 10);

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
    /// テキストに移動速度、方向をセット
    /// </summary>
    /// <param name="moveDir"></param>
    /// <param name="moveSpeed"></param>
    public static void SetMoveForce(out Vector3 moveDir, out float moveSpeed)
    {
        //離した位置
        Vector3 touchEndPos = Input.mousePosition;
        //フリックの長さ
        Vector3 flickLength = touchEndPos - touchStartPos;

        moveDir = flickLength.normalized;
        moveSpeed = flickLength.magnitude / 80;
    }

    /// <summary>
    /// 攻撃ゲージの上昇
    /// </summary>
    public static void AttackGageAccumulate()
    {
        attackGageMask.fillAmount += 0.5f;
    }

    public void HarisenAttack()
    {
        //攻撃ゲージが満タン時
        if (attackGageMask.fillAmount >= 1.0f)
        {
            attackGageMask.fillAmount = 0;
            bossDamageGageMask.fillAmount += 0.1f;
        }
    }
    
}
