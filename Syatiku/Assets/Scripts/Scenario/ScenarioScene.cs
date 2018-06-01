using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioScene : MonoBehaviour {

    [SerializeField]
    string filePath = "Text/chapter_0_0";

    ScenarioWindow window;
    public List<ScenarioInfo> scenarioInfoList = new List<ScenarioInfo>();
    //参照している情報番号
    int infoIndex;
    //全ての情報量
    int allInfoNum;
    //表示するセリフ
    System.Text.StringBuilder viewMessage = new System.Text.StringBuilder();
    string allMessage;
    int nextMessageIndex;

    //文字の表示速度
    [SerializeField]
    float messageViewSpeed = 0.05f;
    float messageViewElapsedTime;

	void Start () {
        window = GetComponent<ScenarioWindow>();
        //必要なデータを取得
        new ImportScenarioInfo(filePath, ref scenarioInfoList, window);
        infoIndex = 0;
        allInfoNum = scenarioInfoList.Count;
        messageViewElapsedTime = 0;

        SetNextInfo();
	}

    void Update()
    {
        if (!IsShowAllMessage())
        {
            messageViewElapsedTime += Time.deltaTime;
        }

        if (messageViewElapsedTime > messageViewSpeed)
        {
            UpdateNextText();
        }
    }

    /// <summary>
    /// 次の文字を表示
    /// </summary>
    void UpdateNextText()
    {
        messageViewElapsedTime = 0;
        viewMessage.Append(allMessage[nextMessageIndex]);
        nextMessageIndex++;
        window.message.text = viewMessage.ToString();
    }

    /// <summary>
    /// 次のセリフデータをセット
    /// </summary>
    /// <param name="num"></param>
    void SetNextInfo()
    {
        viewMessage.Length = 0;
        nextMessageIndex = 0;
        allMessage = scenarioInfoList[infoIndex].message;

        foreach (var action in scenarioInfoList[infoIndex].commandActionList)
        {
            action();
        }

        infoIndex++;
    }

    /// <summary>
    ///　セリフが全て表示されているか
    /// </summary>
    /// <returns></returns>
    bool IsShowAllMessage()
    {
        return viewMessage.Length == allMessage.Length;
    }

    /// <summary>
    /// セリフをすべて表示
    /// </summary>
    void ShowAllMessage()
    {
        viewMessage.Length = 0;
        viewMessage.Append(allMessage);
        window.message.text = viewMessage.ToString();
    }

    /// <summary>
    /// 画面を押したとき
    /// </summary>
    public void OnPointerClick()
    {
        if (IsShowAllMessage())
        {
            if (allInfoNum == infoIndex)
            {
                //すべてのセリフを表示し終えた
                Debug.Log("end");
            }
            else
            {
                SetNextInfo();
            }
        }
        else
        {
            ShowAllMessage();
        }
    }
}
