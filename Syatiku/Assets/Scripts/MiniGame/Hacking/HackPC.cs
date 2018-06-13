using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HackPC : MonoBehaviour {
    
    private Text prefabText;
    [HideInInspector]
    public int counter = 0;
    private GameObject collect;

    private bool _isResult = false;

    [SerializeField, Tooltip("OKを押した結果を表示するGameObject")]
    private GameObject result_temp;

    private Transform ans_child;

    // Use this for initialization
    void Start () {
<<<<<<< HEAD
        collect = GameObject.Find("Canvas/IntoPC/CollectedWord");
=======
        result_temp.SetActive(false);
        _isResult = false;
>>>>>>> 28e16f5bd3286929b5a04a3c81772ffbe166d95a
        ChippedString();
    }

    // Update is called once per frame
    void Update()
    {
        
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

<<<<<<< HEAD
    public void CheckString()
    {
        bool _isCheck = false;
        
            for (int i = 0; i <= 5; i++)
            {
                GameObject ans = GameObject.Find("Canvas/IntoPC/Answer/AnswerText_" + i);
                GameObject quest = GameObject.Find("Canvas/IntoPC/Quest/QuestText_" + i);
                Text que_text = quest.GetComponent<Text>();
                Transform ans_child = ans.transform.GetChild(0).GetChild(0);
                Text ansChild_text = ans_child.GetComponent<Text>();
                if (ansChild_text.text.Substring(0, ansChild_text.text.Length) == que_text.text)
                {
                    _isCheck = true;
                    if (i == 5)
                    {
                        //Common.Instance.gameScore("Hacking",100);
                        Common.Instance.ChangeScene(Common.SceneName.Result);
                    }
                }
                else
                {
                    _isCheck = false;
                    break;
                }
        }
        
        // 文字列があっているかどうか処理
        counter++;
=======
    private IEnumerator WaitTime(float time)
    {
        if (_isResult)
            result_temp.transform.GetChild(0).GetComponent<Text>().text = "〇";
        else
            result_temp.transform.GetChild(0).GetComponent<Text>().text = "×";

        result_temp.SetActive(true);
        yield return new WaitForSeconds(time);
        result_temp.SetActive(false);
    }

    public void CheckString()
    {
        for (int i = 0; i <= 5; i++)
        {
            GameObject ans = GameObject.Find("Canvas/IntoPC/Answer/AnswerText_" + i);
            GameObject quest = GameObject.Find("Canvas/IntoPC/Quest/QuestText_" + i);
            Text que_text = quest.GetComponent<Text>();
            if (ans.transform.childCount != 0)
                ans_child = ans.transform.GetChild(0).GetChild(0);
            else
            {
                _isResult = false;
                StartCoroutine(WaitTime(4f));
                break;
            }
            Text ansChild_text = ans_child.GetComponent<Text>();
            if (ansChild_text.text.Substring(0, ansChild_text.text.Length) == que_text.text)
            {
                if (i == 5)
                {
                    _isResult = true;
                    StartCoroutine(WaitTime(2.5f));
                    //Common.Instance.gameScore("Hacking",100);
                    Common.Instance.ChangeScene(Common.SceneName.Result);
                }
            }
            else
            {
                _isResult = false;
                StartCoroutine(WaitTime(2.5f));
                break;
            }
        }
>>>>>>> 28e16f5bd3286929b5a04a3c81772ffbe166d95a
    }
}
