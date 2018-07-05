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
    [SerializeField, Tooltip("PaperPrefab")]
    private GameObject paper_prefab;

    private IntoPCAction intopc_action;
    private HackTap hack_tap;
    private HackMain hack_main;
    private HackBoss hack_boss;
    private int successCount = 0;

    private GameObject GetWord;

    //いいタイミングかどうか
    private bool _success = false;

    [HideInInspector]
    public bool _lowAnimClear = false;

	// Use this for initialization
	void Start ()
    {
        GetWord = GameObject.Find("Canvas/Check/GetWord");
        _success = false;
        tx.text = "黄色のページをタップしよう！";
        intopc_action = GetComponent<IntoPCAction>();
        hack_tap = GetComponent<HackTap>();
        hack_main = GetComponent<HackMain>();
        hack_boss = GetComponent<HackBoss>();
        getDocument_obj.SetActive(false);
        _lowAnimClear = false;
        successCount = 0;
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

    private IEnumerator Wait_Time(float time)
    {
        if(_success)
            tx.text = "資料を取得しました。";
        else
            tx.text = "資料を取得できませんでした。";
        yield return new WaitForSeconds(time);
        getDocument_obj.SetActive(false);
        tx.text = "黄色のページをタップしよう！";
    }

    /// <summary>
    /// アニメーション中のタップの時処理
    /// </summary>
    public void TapResult()
    {
        if (!_success)
        {
            hack_boss.MoveBoss();
        }
        else
        {
            successCount++;
            getDocument_obj.SetActive(true);
        }
        StartCoroutine(Wait_Time(2f));
    }

    private void PatteResult()
    {
        if(successCount >= 1)
            _lowAnimClear = true;
        else
            _lowAnimClear = false;
        hack_tap.PlaceButton(11);
    }

    /// <summary>
    /// 遅めのアニメーション処理
    /// </summary>
    public void LowAnim()
    {
        Sequence se = DOTween.Sequence();

        se.Append(Paper_1.DOLocalRotate(new Vector2(0, Paper_1.localRotation.y + 180), 1.0f).SetDelay(0.5f).SetLoops(32, LoopType.Restart))
           .InsertCallback(3.7f, () => ChangeColor(0))
           .InsertCallback(3.7f, () => _success = true)
           .InsertCallback(4.5f, () => _success = false)
           .InsertCallback(4.5f, () => ChangeColor(1))
           .InsertCallback(14.7f, () => ChangeColor(0))
           .InsertCallback(14.7f, () => _success = true)
           .InsertCallback(15.5f, () => _success = false)
           .InsertCallback(15.5f, () => ChangeColor(1))
           .InsertCallback(27.7f, () => ChangeColor(0))
           .InsertCallback(27.7f, () => _success = true)
           .InsertCallback(28.5f, () => _success = false)
           .InsertCallback(28.5f, () => ChangeColor(1))
           .OnComplete(() => PatteResult());
    }

    /// <summary>
    /// Animationのイベント処理（Loopバージョン）
    /// </summary>
    public void AnimLoop()
    {
        Sequence se = DOTween.Sequence();
        se.Append(Paper_1.DOLocalRotate(new Vector2(0, Paper_1.localRotation.y + 180), 0.6f).SetDelay(0.3f).SetLoops(38, LoopType.Restart))
           .InsertCallback(3.0f, () => ChangeColor(0))
           .InsertCallback(3.0f, () => _success = true)
           .InsertCallback(3.3f, () => _success = false)
           .InsertCallback(3.3f, () => ChangeColor(1))
           .InsertCallback(10.8f, () => ChangeColor(0))
           .InsertCallback(10.8f, () => _success = true)
           .InsertCallback(11.1f, () => _success = false)
           .InsertCallback(11.1f, () => ChangeColor(1))
           .InsertCallback(18.0f, () => ChangeColor(0))
           .InsertCallback(18.0f, () => _success = true)
           .InsertCallback(18.3f, () => _success = false)
           .InsertCallback(18.3f, () => ChangeColor(1))
           .OnComplete(() => PatteResult());
    }
}
