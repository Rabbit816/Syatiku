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

    //オート中か
    bool isAuto;
    [SerializeField, Header("オート時セリフ更新待ち時間")]
    float nextWaitTime = 1f;
    //シナリオ中か
    bool isPlayScenario;
    #endregion

    void Start () {
        window.scenarioCanvas.alpha = 0;

        DontDestroyOnLoad(gameObject);
        //BeginScenario(filePath);
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
            Init();
            SetNextInfo();
            isPlayScenario = true;
        });
    }

    private void Init()
    {
        infoIndex = 0;
        allInfoNum = scenarioInfoList.Count;
        originMessageViewSpeed = messageViewSpeed;
        messageViewElapsedTime = 0;
        isSkip = false;
        isAuto = false;
        isLogView = false;
        isPlayScenario = false;

        window.name.text = "";
        window.message.text = "";
    }

    /// <summary>v
    /// 次のシナリオ情報をセット
    /// </summary>
    /// <param name="num"></param>
    void SetNextInfo()
    {
        window.recommendIcon.SetActive(false);
        //感情アイコンの非表示
        window.iconLeft.gameObject.SetActive(false);
        window.iconCenter.gameObject.SetActive(false);
        window.iconRight.gameObject.SetActive(false);
        //セリフ部分の初期化
        viewMessage.Length = 0;
        nextMessageIndex = 0;
        allMessage = scenarioInfoList[infoIndex].message;

        //各コマンド
        foreach (var action in scenarioInfoList[infoIndex].commandActionList)
        {
            action();
        }

        infoIndex++;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BeginScenario(filePath);
        }

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
            if (isAuto)
            {
                StartCoroutine(SetNextInfo(nextWaitTime));
            }
        }
    }

    /// <summary>
    /// オート時のセリフ更新メソッド
    /// </summary>
    IEnumerator SetNextInfo(float time)
    {
        yield return new WaitForSeconds(time);
        UpdateInfoOrMessage();
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

    void UpdateInfoOrMessage(System.Action action = null)
    {
        if (IsShowAllMessage())
        {
            if (IsReachLastInfo())
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
            if(action != null) action();
        }
    }

    /// <summary>
    /// 最後のシナリオ情報まで到達しているか
    /// </summary>
    bool IsReachLastInfo()
    {
        return infoIndex == allInfoNum;
    }

    /// <summary>
    /// シナリオ終了
    /// </summary>
    void EndScenario()
    {
        isPlayScenario = false;
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
        if (isPlayScenario) UpdateInfoOrMessage(ShowAllMessage);
    }

    public void OnClickSkipButton()
    {
        if (isPlayScenario)
        {
            isSkip = !isSkip;
            window.skipText.text = isSkip ? "スキップ中" : "スキップ";
        }
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

    public void OnClickAutoButton()
    {
        isAuto = !isAuto;
        window.autoText.text = isAuto ? "オート中" : "オート";
        if (IsShowAllMessage()) StartCoroutine(SetNextInfo(nextWaitTime));
    }

    #endregion
}
