using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioScene : MonoBehaviour {

<<<<<<< HEAD
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
    char n = '\n';
    void UpdateNextText()
    {
        messageViewElapsedTime = 0;
        //改行
        if(allMessage[nextMessageIndex] == n)
        {
            //少し待つ
            StartCoroutine(WaitTime(0.8f));
        }
        viewMessage.Append(allMessage[nextMessageIndex]);
        nextMessageIndex++;
        window.message.text = viewMessage.ToString();
    }

    IEnumerator WaitTime(float t)
    {
        yield return new WaitForSeconds(t);
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
=======
    [SerializeField]
    string filePath;
    [SerializeField]
    CriAtomSource voiceSource;

    void Start () {
        //現在のシーン名を取得
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.SetVoiceSource(voiceSource);

        //決戦前のシナリオの場合
        if (sceneName == "BeforeBattle")
        {
            //White
            if (Common.Instance.gameMode == 1)
            {
                BeginScenarioMainBeforeBattle();
            }
            //Another
            else
            {
                BeginScenarioAnotherBeforeBattle();
            }
        }
        else
        {
            ScenarioController.Instance.BeginScenario(filePath);
        }
    }

    /// <summary>
    /// ボス決戦前のメインシナリオ再生(シナリオを始める番号:最初のボイス番号)
    /// vs社長(bad 0:0 normal 6:1 good 11:2)
    /// vs課長
    /// </summary>
    private void BeginScenarioMainBeforeBattle()
    {
        int startInfoIndex = 0;
        int startVoiceIndex = 0;
        bool[] clearFlag = Common.Instance.clearFlag;
        int clearCount = 0;
        //クリアしたミニゲームの数
        for (int i = 0; i < clearFlag.Length; i++)
        {
            clearCount += clearFlag[i] ? 1 : 0;
        }
        switch (clearCount)
        {
            //Bad
            case 0:
            case 1:
                break;
            //Normal
            case 2:
                startInfoIndex = 6;
                startVoiceIndex = 1;
                break;
            //Good
            case 3:
                startInfoIndex = 11;
                startVoiceIndex = 2;
                break;
        }

        ScenarioController.Instance.BeginScenario(filePath, startInfoIndex, startVoiceIndex);
    }

    /// <summary>
    /// ボス決戦前のアナザーシナリオ再生(シナリオを始める番号:最初のボイス番号)
    /// vs社長(bad,normal 0:0 good 4:1)
    /// vs課長
    /// </summary>
    private void BeginScenarioAnotherBeforeBattle()
    {
        int startInfoIndex = 0;
        int startVoiceIndex = 0;
        bool[] clearFlag = Common.Instance.clearFlag;
        int clearCount = 0;
        //クリアしたミニゲームの数
        for (int i = 0; i < clearFlag.Length; i++)
        {
            clearCount += clearFlag[i] ? 1 : 0;
>>>>>>> master
        }
        switch (clearCount)
        {
<<<<<<< HEAD
            ShowAllMessage();
        }
=======
            //Bad or Normal
            case 0:
            case 1:
                startVoiceIndex = 1;
                break;
            //Good
            case 2:
                startInfoIndex = 4;
                break;
        }

        ScenarioController.Instance.BeginScenario(filePath, startInfoIndex, startVoiceIndex);
>>>>>>> master
    }
}
