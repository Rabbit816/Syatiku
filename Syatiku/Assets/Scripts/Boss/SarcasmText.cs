using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SarcasmText : MonoBehaviour
{
    Text sarcasmText;
    Vector3 moveForce;
    float alpha;

    void Awake()
    {
        sarcasmText = GetComponent<Text>();
        Initialize();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Initialize()
    {
        //座標
        float posX = Random.Range(0, 2);
        posX = (posX > 0 ? 200 : -200);
        float posY = Random.Range(-80, 80);
        Vector3 pos = new Vector3(posX, posY, 0);
        sarcasmText.rectTransform.localPosition = pos;

        //移動
        float moveX = posX / Random.Range(100, 800);
        float moveY = Random.Range(-0.1f, 0.1f);
        moveForce = new Vector3(moveX, moveY, 0);

        //文字
        sarcasmText.fontSize = Random.Range(15, 41);
        alpha = 0;
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

        //移動
        sarcasmText.rectTransform.localPosition += moveForce;

        //画面外に外れた時
        if (sarcasmText.rectTransform.localPosition.x > 500 || sarcasmText.rectTransform.localPosition.x < -500
            || sarcasmText.rectTransform.localPosition.y > 200 || sarcasmText.rectTransform.localPosition.y < -200)
        {
            Initialize();
            gameObject.SetActive(false);
        }
    }

    public void FlickEnd()
    {
        BossScene.Instance.SetMoveForce(ref moveForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("textにhit");
    }
}
