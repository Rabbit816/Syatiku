using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool isFade;
    //シナリオ中か
    bool isScenario;
    #endregion

    void Start () {
        window.scenarioCanvas.alpha = 0;
        isFade = false;
        isScenario = false;

        BeginScenario(filePath);
	}

    /// <summary>
    /// シナリオパート開始
    /// </summary>
    /// <param name="path">読み込みたいtxtのパス</param>
    public void BeginScenario(string path)
    {
        StartCoroutine(FadeCanvas(1f));

        //必要なデータを取得
        new ImportScenarioInfo(path, ref scenarioInfoList, window);
        isFade = true;

        Init();
        SetNextInfo();
    }

    /// <summary>
    /// シナリオ画面のフェード
    /// </summary>
    public IEnumerator FadeCanvas(float targetAlpha)
    {
        if(targetAlpha != 0 && targetAlpha != 1f)
        {
            Debug.logger.LogWarning("notTargtetAlphaSet", "指定した透明度が0か1ではありません");
            yield return null;
        }

        window.scenarioCanvas.gameObject.SetActive(true);
        float waitTime = 1f / 60f;
        float fadeSpeed = (targetAlpha == 1f ? waitTime : -waitTime);
        while (window.scenarioCanvas.alpha != targetAlpha)
        {
            window.scenarioCanvas.alpha += fadeSpeed;

            if (window.scenarioCanvas.alpha <= 0)
            {
                window.scenarioCanvas.alpha = 0;
                window.scenarioCanvas.gameObject.SetActive(false);
            }
            else if (window.scenarioCanvas.alpha >= 1f)
            {
                window.scenarioCanvas.alpha = 1f;
                isScenario = true;
            }
            yield return new WaitForSeconds(waitTime);
        }

        yield return new WaitForSeconds(1f);
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
            action();
        }

        infoIndex++;
    }

    void Update()
    {
        if (!isLogView && isScenario)
        {
            if (!isSkip)
            {
                UpdateMessage();
            }
            else
            {
                UpdateInfoOrMessage(UpdateMessage);
            }
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
        if (!isScenario)
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
        isScenario = false;
        StartCoroutine(FadeCanvas(0f));
    }

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
