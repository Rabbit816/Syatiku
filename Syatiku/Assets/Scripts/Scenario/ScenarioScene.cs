using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScenarioScene : MonoBehaviour {

    [SerializeField]
    Text name;
    [SerializeField]
    Text message;

    public List<ScenarioInfo> scenarioInfoList = new List<ScenarioInfo>();
    //参照しているデータ番号
    int dateIndex;
    int allInfoNum;
    System.Text.StringBuilder viewMessage = new System.Text.StringBuilder();
    string allMessage;
    int nextMessageIndex;

    //文字の表示速度
    [SerializeField]
    float messageViewSpeed = 0.05f;
    float messageViewElapsedTime;

	void Start () {
        //ImportScenarioDate scenarioData = GetComponent<ImportScenarioDate>();
        scenarioInfoList = GetComponent<ImportScenarioDate>().CreateScenarioInfo();
        dateIndex = 0;
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
            UpdateNextCharacter();
        }
    }

    /// <summary>
    /// 次の文字を表示
    /// </summary>
    void UpdateNextCharacter()
    {
        messageViewElapsedTime = 0;
        viewMessage.Append(allMessage[nextMessageIndex]);
        nextMessageIndex++;
        message.text = viewMessage.ToString();
    }

    /// <summary>
    /// 次のセリフデータをセット
    /// </summary>
    /// <param name="num"></param>
    void SetNextInfo()
    {
        viewMessage.Length = 0;
        nextMessageIndex = 0;

        allMessage = scenarioInfoList[dateIndex].message;
        name.text = scenarioInfoList[dateIndex].name;

        dateIndex++;
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
        message.text = viewMessage.ToString();
    }

    /// <summary>
    /// 画面を押したとき
    /// </summary>
    public void OnPointerClick()
    {
        if (IsShowAllMessage())
        {
            if (allInfoNum == dateIndex)
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
