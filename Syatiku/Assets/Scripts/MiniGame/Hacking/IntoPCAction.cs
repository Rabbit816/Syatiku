using System;
using System.Collections;
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
    [SerializeField, Tooltip("資料比較するボタン")]
    private GameObject comp_btn;
    [SerializeField, Tooltip("間違っている箇所_0")]
    private GameObject wrongbtn_0;
    [SerializeField, Tooltip("間違っている箇所_1")]
    private GameObject wrongbtn_1;

    [Tooltip("資料比較の時に何回ミスしてもいいかの回数")]
    public int tappingCount = 6;

    private Transform password_child;

    private HackBoss hack_boss;
    private HackMain hack_main;
    private HackTap hack_tap;
    private GameObject PC_login;
    private GameObject WindowFase;
    private GameObject Window;
    private GameObject PassWordFase;

    //配置した結果の判断
    private bool _isResult = false;

    //資料比較中かどうか
    private bool _comparisoning = false;
    [HideInInspector]
    public bool _compariClear = false;

    //資料比較の時の間違っている部分をタップできたかどうか
    private bool doc_0 = false;
    private bool doc_1 = false;

    // Use this for initialization
    void Start () {
        try
        {
            PC_login = GameObject.Find("Canvas/PC/PassWordFase/Title");
            Window = GameObject.Find("Canvas/PC/WindowFase/Window");
            WindowFase = GameObject.Find("Canvas/PC/WindowFase");
            PassWordFase = GameObject.Find("Canvas/PC/PassWordFase");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        _compariClear = false;
        PassWordFase.transform.SetSiblingIndex(2);
        tappingCount = 6;
        CountText.text = tappingCount.ToString();
        Window.SetActive(false);
        Document_1.SetActive(false);
        NotComp.SetActive(true);
        Comparisoning.SetActive(false);
        comp_btn.SetActive(true);
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            hack_tap.PlaceButton(0); hack_tap.PlaceButton(1); hack_tap.PlaceButton(2); hack_tap.PlaceButton(3);
            hack_tap.PlaceButton(4); hack_tap.PlaceButton(5); hack_tap.PlaceButton(6); hack_tap.PlaceButton(7); hack_tap.PlaceButton(8);
        }else if (Input.GetKeyDown(KeyCode.Z))
        {
            hack_tap.PlaceButton(25);
        }
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
            hack_tap._windowFase = true;
            WindowFase.transform.SetSiblingIndex(2);
            Window.SetActive(true);
        }
        else
        {
            PC_login.GetComponent<Text>().text = "パスワードが違います。";
            PC_login.SetActive(true);
            yield return new WaitForSeconds(wait);
            PC_login.SetActive(false);
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
    }

    /// <summary>
    /// 資料比較中の周りの何もない部分タップ処理
    /// </summary>
    public void OutTap()
    {
        _comparisoning = false;
        Comparisoning.SetActive(false);
        Window.SetActive(true);
    }

    /// <summary>
    /// 資料比較の時のタップ処理
    /// </summary>
    /// <param name="docNum">0.当たり 1.当たり 2.はずれ 3.何もないとこ</param>
    public void CheckDocuments(int docNum)
    {
        if (!hack_tap._getDocument)
            return;
        switch (docNum)
        {
            case 0:
                wrongbtn_0.GetComponent<Image>().color = new Color(255, 255, 0);
                doc_0 = true;
                break;
            case 1:
                wrongbtn_1.GetComponent<Image>().color = new Color(255, 255, 0);
                doc_1 = true;
                break;
            case 2:
                tappingCount--;
                CountText.text = tappingCount.ToString();
                hack_boss.MoveBoss();
                break;
        }

        if(doc_0 && doc_1)
        {
            comp_btn.SetActive(false);
            _compariClear = true;
            OutTap();
        }

        if(tappingCount == 0)
        {
            hack_boss.ComeOnBoss();
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
            else
            {
                _isResult = false;
                hack_boss.MoveBoss();
                StartCoroutine(WaitTime(1.5f));
                break;
            }
            Text child_text = password_child.GetComponent<Text>();
            if(child_text.text.Substring(0,1) == hack_main.Quest_list[i].ToString())
            {
                if(i == PassWordObject.Length - 1)
                {
                    _isResult = true;
                    StartCoroutine(WaitTime(1.5f));
                }
            }
            else
            {
                _isResult = false;
                hack_boss.MoveBoss();
                StartCoroutine(WaitTime(1.5f));
                break;
            }
        }
    }
}
