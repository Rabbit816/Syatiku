using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PatteringEvent : MonoBehaviour {

    [SerializeField, Tooltip("Paper_1")]
    private RectTransform Paper_1;
    [SerializeField, Tooltip("Paper_2")]
    private RectTransform Paper_2;
    [SerializeField, Tooltip("Patteringfaseのtext")]
    private Text tx;
    [SerializeField, Tooltip("取得するDocument")]
    private RectTransform getDocument;
    [SerializeField, Tooltip("取得するDocument")]
    private GameObject getDocument_obj;
    [SerializeField, Tooltip("place_button_6")]
    private GameObject place_button_6;

    private IntoPCAction intopc_action;
    private HackTap hack_tap;
    private HackMain hack_main;

    private Sequence seq;
    Sequence se;
    //いいタイミングかどうか
    private bool _success = false;

	// Use this for initialization
	void Start () {
        tx.text = "黄色のページをタップしよう！";
        intopc_action = GetComponent<IntoPCAction>();
        hack_tap = GetComponent<HackTap>();
        hack_main = GetComponent<HackMain>();
        getDocument_obj.SetActive(false);
        seq = DOTween.Sequence();
         se = DOTween.Sequence();
        _success = false;
    }
	
	// Update is called once per frame
	void Update () {

    }

    /// <summary>
    /// アニメーション中の色変更
    /// </summary>
    /// <param name="num">0.黄色,1.白</param>
    private void ChangeColor(int num)
    {
        switch (num)
        {
            case 0:
                Paper_1.GetComponent<Image>().color = new Color(255, 255, 0);
                Paper_2.GetComponent<Image>().color = new Color(255, 255, 0);
                break;
            case 1:
                Paper_1.GetComponent<Image>().color = new Color(255, 255, 255);
                Paper_2.GetComponent<Image>().color = new Color(255, 255, 255);
                break;
            default:
                Debug.Log("ColorNum :" + num);
                break;
        }
    }

    private IEnumerator StopTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    /// <summary>
    /// アニメーション中のタップの時処理
    /// </summary>
    public void TapResult()
    {
        _success = true;
        if (!_success)
        {
            tx.text = "資料を取得できませんでした。";
        }
        else
        {
            Sequence sequ = DOTween.Sequence();
            getDocument_obj.SetActive(true);
            tx.text = "資料を取得しました。";
            Image img_alpha = getDocument.GetComponent<Image>();
            sequ.Append(getDocument.DOLocalMove(new Vector3(332, 147, 0), 1f))
                .Join(getDocument.DOLocalRotate(new Vector3(0, 0, 720), 1f, RotateMode.FastBeyond360))
                .OnComplete(() => DOTween.ToAlpha(
                    () => img_alpha.color,
                    color => img_alpha.color = color,
                    0f,
                    0.3f));
        }
        StartCoroutine(StopTime(5.0f));
        hack_tap.PlaceButton(11);
        place_button_6.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 遅めのアニメーション処理
    /// </summary>
    public void LowAnim()
    {
        se.Append(Paper_1.DOLocalRotate(new Vector2(0, Paper_1.localRotation.y + 180), 1.0f).SetDelay(0.5f).SetLoops(70, LoopType.Restart))
           .InsertCallback(3.9f, () => ChangeColor(0))
           .InsertCallback(3.9f, () => _success = true)
           .InsertCallback(4.5f, () => _success = false)
           .InsertCallback(4.5f, () => ChangeColor(1))
           .InsertCallback(14.9f, () => ChangeColor(0))
           .InsertCallback(14.9f, () => _success = true)
           .InsertCallback(15.5f, () => _success = false)
           .InsertCallback(15.5f, () => ChangeColor(1))
           .InsertCallback(29.9f, () => ChangeColor(0))
           .InsertCallback(29.9f, () => _success = true)
           .InsertCallback(30.5f, () => _success = false)
           .InsertCallback(30.5f, () => ChangeColor(1))
           .OnComplete(() => TapResult());
    }

    /// <summary>
    /// Animationのイベント処理（Loopバージョン）
    /// </summary>
    public void AnimLoop()
    {
        se.Append(Paper_1.DOLocalRotate(new Vector2(0, Paper_1.localRotation.y + 180), 0.5f).SetDelay(0.3f).SetLoops(70, LoopType.Restart))
           .InsertCallback(3.0f, () => ChangeColor(0))
           .InsertCallback(3.0f, () => _success = true)
           .InsertCallback(3.3f, () => _success = false)
           .InsertCallback(3.3f, () => ChangeColor(1))
           .InsertCallback(12.0f, () => ChangeColor(0))
           .InsertCallback(12.0f, () => _success = true)
           .InsertCallback(12.3f, () => _success = false)
           .InsertCallback(12.3f, () => ChangeColor(1))
           .InsertCallback(18.0f, () => ChangeColor(0))
           .InsertCallback(18.0f, () => _success = true)
           .InsertCallback(18.3f, () => _success = false)
           .InsertCallback(18.3f, () => ChangeColor(1))
           .OnComplete(() => TapResult());
    }
}
