﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickTextController : MonoBehaviour
{
    Text text;
    Vector3 moveForce;
    float alpha;

    enum Type
    {
        Wrong,
        Correct,
    }
    Type type;

    void Start()
    {
        text = GetComponent<Text>();
        Debug.Log(text.rectTransform.localPosition);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(int num, string t)
    {
        if(text == null) text = GetComponent<Text>();

        //座標
        float posX = Random.Range(0, 2);
        posX = (posX > 0 ? 200 : -200);
        float posY = Random.Range(-100, 100);
        Debug.Log(text.rectTransform.localPosition);
        Vector3 pos = new Vector3(posX, posY, 0);
        text.rectTransform.localPosition = pos;

        //移動
        float moveX = posX / Random.Range(100, 800);
        float moveY = Random.Range(-0.5f, 0.5f);
        moveForce = new Vector3(moveX, moveY, 0);

        //テキスト
        text.fontSize = Random.Range(15, 41);
        alpha = 0;
        this.type = (Type)num;
        text.text = t;

        gameObject.SetActive(true);
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
            text.color = new Color(1, 1, 1, alpha);
        }

        //移動
        text.rectTransform.localPosition += moveForce;

        //画面外に外れた時
        if (text.rectTransform.localPosition.x > 500 || text.rectTransform.localPosition.x < -500
            || text.rectTransform.localPosition.y > 200 || text.rectTransform.localPosition.y < -200)
        {
            BossScene.Instance.MissCountUP();
            gameObject.SetActive(false);
        }
    }

    public void FlickEnd()
    {
        BossScene.Instance.SetMoveForce(ref moveForce);
    }

    /// <summary>
    /// ボスとの衝突判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == Type.Correct)
        {
            BossScene.Instance.SuccessCountUP();
        }
        else
        {
            BossScene.Instance.MissCountUP();
        }
        gameObject.SetActive(false);
    }
}
