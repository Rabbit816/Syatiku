using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScenarioController : MonoBehaviour {

    #region variable

    [SerializeField]
    string filePath = "Text/chapter_0_0";
    [SerializeField]
    ScenarioWindow window;
    public List<ScenarioInfo> scenarioInfoList = new List<ScenarioInfo>();
    //参照している情報番号
    int infoIndex;
    //全ての情報数
    int allInfoNum;
    //表示するセリフ
    System.Text.StringBuilder viewMessage = new System.Text.StringBuilder();
    string allMessage;
    int nextMessageIndex;
    //ログ
    System.Text.StringBuilder logMessage = new System.Text.StringBuilder();
    bool isLogView;

    //文字の表示速度
    [SerializeField]
    float messageViewSpeed = 0.05f;
    float originMessageViewSpeed;
    float messageViewElapsedTime;

    //スキップ中か
    bool isSkip;
    [SerializeField]
    int skipSpeed = 3;

    //フェード中か
    //bool isFade;
    //シナリオ中か
    bool isPlayScenario;
    #endregion

    void Start () {
        window.scenarioCanvas.alpha = 0;
        isPlayScenario = false;

        DontDestroyOnLoad(gameObject);
        BeginScenario(filePath);
	}

    /// <summary>
    /// シナリオパート開始
    /// </summary>
    /// <param name="path">読み込みたいtxtのパス</param>
    public void BeginScenario(string path)
    {
        //必要なデータを取得
        new ImportScenarioInfo(path, ref scenarioInfoList, window);

        FadeManager.Instance.Fade(window.scenarioCanvas, 2f, 1f, () =>
        {
            //isFade = false;
            isPlayScenario = true;
        });

        Init();
        SetNextInfo();
    }

    private void Init()
    {
        infoIndex = 0;
        allInfoNum = scenarioInfoList.Count;
        originMessageViewSpeed = messageViewSpeed;
        messageViewElapsedTime = 0;
        isSkip = false;
        isLogView = false;
    }

    /// <summary>
    /// 次のセリフデータをセット
    /// </summary>
    /// <param name="num"></param>
    void SetNextInfo()
    {
        window.recommendIcon.SetActive(false);
        viewMessage.Length = 0;
        nextMessageIndex = 0;
        allMessage = scenarioInfoList[infoIndex].message;

        foreach (var action in scenarioInfoList[infoIndex].commandActionList)
        {
            StartCoroutine(ActiveCommand(action));
            //action();
        }

        infoIndex++;
    }

    IEnumerator ActiveCommand(System.Action action)
    {
        yield return StartCoroutine(ActiveCommand(action));
        action();
    }

    void Update()
    {
        //シナリオ中ではない、ログを表示中
        if (!isPlayScenario　|| isLogView)
        {
            return;
        }

        if (!isSkip)
        {
            UpdateMessage();
        }
        else
        {
            UpdateInfoOrMessage(UpdateMessage);
        }
    }

    void UpdateMessage()
    {
        if (!IsShowAllMessage())
        {
            messageViewElapsedTime += Time.deltaTime;

            if (messageViewElapsedTime > messageViewSpeed)
            {
                ShowNextChar();
            }
        }
        else
        {
            ShowRecommendIcon();
        }
    }

    /// <summary>
    /// タップうながしアイコン表示
    /// </summary>
    void ShowRecommendIcon()
    {
        if (!window.recommendIcon.activeSelf)
        {
            window.recommendIcon.SetActive(true);
        }
    }

    //改行
    char n = '\n';
    /// <summary>
    /// 次の文字を表示
    /// </summary>
    void ShowNextChar()
    {
        if (!isSkip)
        {
            messageViewElapsedTime = 0;
            //改行
            if (allMessage[nextMessageIndex] == n)
            {
                //少し待つ
                StartCoroutine(WaitTime(0.8f));
            }
            viewMessage.Append(allMessage[nextMessageIndex]);
            nextMessageIndex++;
        }
        else
        {

            for (int i = 0; i < skipSpeed; i++)
            {
                if (IsShowAllMessage()) break;

                viewMessage.Append(allMessage[nextMessageIndex]);
                nextMessageIndex++;
            }

        }

        window.message.text = viewMessage.ToString();
    }

    IEnumerator WaitTime(float t)
    {
        yield return new WaitForSeconds(t);
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

    void UpdateInfoOrMessage(System.Action action)
    {
        //シナリオ中でない時何もしない
        if (!isPlayScenario)
        {
            return;
        }

        if (IsShowAllMessage())
        {
            if (allInfoNum == infoIndex)
            {
                EndScenario();
            }
            else
            {
                AddLogMessage();
                SetNextInfo();
            }
        }
        else
        {
            action();
        }
    }

    /// <summary>
    /// シナリオ終了
    /// </summary>
    void EndScenario()
    {
        isPlayScenario = false;
        //FadeManager.Instance.Fade(window.scenarioCanvas, 2f, 0, () => window.scenarioCanvas.gameObject.SetActive(false));
    }

    /// <summary>
    /// ログにテキスト追加
    /// </summary>
    void AddLogMessage()
    {
        logMessage.Insert(0, "【 " + window.name.text + " 】" + n + allMessage + n);
        window.logText.text = logMessage.ToString();
    }

    #region  OnClickAction

    /// <summary>
    /// 画面を押したとき
    /// </summary>
    public void OnPointerClick()
    {
        UpdateInfoOrMessage(ShowAllMessage);
    }

    public void OnClickSkipButton()
    {
        isSkip = !isSkip;
        window.skipText.text = isSkip ? "スキップ中" : "スキップ";
    }

    public void OnClickLogButton()
    {
        isLogView = true;
        window.log.SetActive(true);
    }

    public void OnClickBackButton()
    {
        isLogView = false;
        window.log.SetActive(false);
    }

    #endregion
}
