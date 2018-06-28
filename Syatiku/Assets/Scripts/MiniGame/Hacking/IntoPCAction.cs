using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntoPCAction : MonoBehaviour {

    [SerializeField,Tooltip("PC内のでる場所")]
    private GameObject[] PassWordObject;
    [SerializeField, Tooltip("資料比較グループObject")]
    private GameObject Comparisoning;
    [SerializeField, Tooltip("残り回数更新Text")]
    private Text CountText;
    [SerializeField, Tooltip("見つけた資料")]
    private GameObject Document_1;
    [SerializeField, Tooltip("資料見つけてない場合のテキストObject")]
    private GameObject NotComp;

    private int tappingCount = 6;

    private Transform password_child;

    private HackBoss hack_boss;
    private HackMain hack_main;
    private HackTap hack_tap;
    private GameObject PC_login;
    private GameObject PC_notlogin;
    private GameObject PC;
    private GameObject Window;

    //配置した結果の判断
    private bool _isResult = false;

    private bool _comparisoning = false;

    private bool doc_0 = false;
    private bool doc_1 = false;

    // Use this for initialization
    void Start () {
        try
        {
            PC_login = GameObject.Find("Canvas/PC/PassWordFase/Title");
            PC_notlogin = GameObject.Find("Canvas/PC/PassWordFase/SubText");
            PC = GameObject.Find("Canvas/PC");
            Window = GameObject.Find("Canvas/PC/WindowFase/Window");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        tappingCount = 6;
        CountText.text = tappingCount.ToString();
        PC_notlogin.SetActive(false);
        Window.SetActive(false);
        Document_1.SetActive(false);
        NotComp.SetActive(true);
        Comparisoning.SetActive(false);
        hack_boss = GetComponent<HackBoss>();
        hack_main = GetComponent<HackMain>();
        hack_tap = GetComponent<HackTap>();
        _isResult = false;
        _comparisoning = false;
        doc_0 = false;
        doc_1 = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    /// <summary>
    /// チェックした時のテキスト表示
    /// </summary>
    /// <param name="wait">待つ時間をfloat型で記入しなはれ</param>
    /// <returns></returns>
    private IEnumerator WaitTime(float wait)
    {
        if (_isResult)
        {
            PC_login.GetComponent<Text>().text = "ログインできました。";
            PC_login.SetActive(true);
            yield return new WaitForSeconds(wait);
            PC.transform.GetChild(0).SetAsLastSibling();
            Window.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(wait);
        }
    }

    /// <summary>
    /// 資料比較する時の処理
    /// </summary>
    public void DocumentsComparison()
    {
        if (hack_tap._getDocument)
        {
            Document_1.SetActive(true);
            NotComp.SetActive(false);
        }
        _comparisoning = true;
        Window.SetActive(false);
        Comparisoning.SetActive(true);
        if (!_comparisoning)
            Window.SetActive(true);
    }

    /// <summary>
    /// 資料比較の時のタップ処理
    /// </summary>
    /// <param name="docNum">0.当たり 1.当たり 2.はずれ 3.何もないとこ</param>
    public void CheckDocuments(int docNum)
    {
        switch (docNum)
        {
            case 0:
                doc_0 = true;
                break;
            case 1:
                doc_1 = true;
                break;
            case 2:
                tappingCount--;
                CountText.text = tappingCount.ToString();
                break;
            case 3:
                _comparisoning = false;
                Comparisoning.SetActive(false);
                break;
        }

        if(doc_0 && doc_1)
        {
            Window.SetActive(false);
            PC.transform.GetChild(0).SetAsLastSibling();
        }

        if(tappingCount == 0)
        {
            Common.gameClear = false;
            Common.Instance.ChangeScene(Common.SceneName.Result);
        }
    }

    /// <summary>
    /// パスワードチェック
    /// </summary>
    public void CheckPassWord()
    {
        for(int i=0; i < PassWordObject.Length; i++)
        {
            GameObject password_parent = GameObject.Find("Canvas/PC/PassWordFase/PassWord/Password_" + i);
            if (PassWordObject[i].gameObject.transform.childCount != 0)
            {
                password_child = password_parent.transform.GetChild(0).GetChild(0);
            }
            else {
                _isResult = false;
                hack_boss.MoveBoss();
                StartCoroutine(WaitTime(2f));
                break;
            }
            Text child_text = password_child.GetComponent<Text>();
            if(child_text.text.Substring(0,1) == hack_main.Quest_list[i].ToString())
            {
                if(i == PassWordObject.Length - 1)
                {
                    _isResult = true;
                    hack_boss.MoveBoss();
                    StartCoroutine(WaitTime(2f));
                }
            }
            else
            {
                _isResult = false;
                hack_boss.MoveBoss();
                StartCoroutine(WaitTime(2f));
            }
        }
    }
}
