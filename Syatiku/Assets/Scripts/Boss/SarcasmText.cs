using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SarcasmText : MonoBehaviour
{
    Text sarcasmText;
    Vector3 moveDir;
    float moveSpeed;
    float alpha;
    bool isFlick;

    void Awake()
    {
        sarcasmText = GetComponent<Text>();
    }

    void OnEnable()
    {
        Initialize();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Initialize()
    {
        //座標系
        Vector3 pos = new Vector3(Random.Range(-200, 200), Random.Range(-100, 100), 0);
        sarcasmText.rectTransform.localPosition = pos;

        //大きさ系
        Vector2 size = new Vector2(Random.Range(200, 300), Random.Range(30, 60));
        sarcasmText.rectTransform.sizeDelta = size;
        sarcasmText.fontSize = (int)(size.y * 0.8f);

        //移動系
        moveDir = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        moveSpeed = 1.0f;
        alpha = 0;

        isFlick = false;
    }

    void Update()
    {
        //透明度の更新
        if (alpha >= 1)
        {
            alpha = 1.0f;
        }
        else
        {
            alpha += 0.005f;
            sarcasmText.color = new Color(1, 1, 1, alpha);
        }

        //座標移動
        sarcasmText.rectTransform.localPosition += moveDir * moveSpeed;

        //画面外に外れた時
        if (sarcasmText.rectTransform.localPosition.x > 500 || sarcasmText.rectTransform.localPosition.x < -500
            || sarcasmText.rectTransform.localPosition.y > 200 || sarcasmText.rectTransform.localPosition.y < -200)
        {
            gameObject.SetActive(false);
            BossScene.AttackGageAccumulate();
        }
    }

    public void FlickEnd()
    {
        if (!isFlick)
        {
            BossScene.SetMoveForce(out moveDir, out moveSpeed);
            isFlick = (moveDir.sqrMagnitude * moveSpeed > 0);
        }
        
    }

}
