using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntoPCAction : MonoBehaviour {

    [SerializeField,Tooltip("PC内のでる場所")]
    private GameObject[] PassWordObject;

    private Transform password_child;

    private HackMain hack_main;
    private GameObject PC_login;
    private GameObject PC_notlogin;
    private GameObject PC;
    private GameObject Window;

    //配置した結果の判断
    private bool _isResult = false;

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
        PC_notlogin.SetActive(false);
        Window.SetActive(false);
        hack_main = GetComponent<HackMain>();
        _isResult = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    /// <summary>
    /// チェックした時のテキスト表示
    /// </summary>
    /// <param name="wait"></param>
    /// <returns></returns>
    private IEnumerator WaitTime(float wait)
    {
        if (_isResult)
        {
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
                StartCoroutine(WaitTime(2f));
                break;
            }
            Text child_text = password_child.GetComponent<Text>();
            if(child_text.text.Substring(0,1) == hack_main.Quest_list[i].ToString())
            {
                if(i == PassWordObject.Length - 1)
                {
                    _isResult = true;
                    StartCoroutine(WaitTime(2f));
                }
            }
            else
            {
                _isResult = false;
                StartCoroutine(WaitTime(2f));
            }
        }
    }
}
