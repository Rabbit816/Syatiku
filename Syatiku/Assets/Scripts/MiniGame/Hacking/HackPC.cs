﻿using UnityEngine;
using UnityEngine.UI;

public class HackPC : MonoBehaviour {
    
    private Text prefabText;
    [HideInInspector]
    public int counter = 0;

    // Use this for initialization
    void Start () {
        ChippedString();
    }

    // Update is called once per frame
    void Update () {
        
	}

    /// <summary>
    /// 欠けてる文章の処理
    /// </summary>
    private void ChippedString()
    {
        
    }

    /// <summary>
    /// バラバラの文字処理
    /// </summary>
    private void PiecesString()
    {
        // 欠けてる文章によって出す文字変更
        // 場所を設定　ランダムでやるかも

    }

    public void CheckString()
    {
        for (int i = 0; i <= 5; i++)
        {
            GameObject ans = GameObject.Find("Canvas/IntoPC/Answer/AnswerText_" + i);
            GameObject quest = GameObject.Find("Canvas/IntoPC/Quest/QuestText_" + i);
            Text que_text = quest.GetComponent<Text>();
            Transform ans_child = ans.transform.GetChild(0).GetChild(0);
            Text ansChild_text = ans_child.GetComponent<Text>();
            if (ansChild_text.text.Substring(0, ansChild_text.text.Length) == que_text.text)
            {
                if (i == 5)
                {
                    //Common.Instance.gameScore("Hacking",100);
                    Common.Instance.ChangeScene(Common.SceneName.Result);
                }
            }
            else
            {
                break;
            }
        }
    }
}
