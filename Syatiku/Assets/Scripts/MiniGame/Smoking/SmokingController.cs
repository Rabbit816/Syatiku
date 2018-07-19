using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmokingController : MonoBehaviour {
    [SerializeField]
    private Image tabaco,face; // タバコUI、機嫌UI
    [SerializeField]
    private Text[] wordText = new Text[4]; // 選択肢テキスト
    [SerializeField]
    private Text answer; // 問題テキスト

    [SerializeField]
    private float time; // 制限時間減少値
    [SerializeField]
    private int answerCount; // 回答権
    [SerializeField]
    private int qLength; // 合計問題数

    private Mushikui mushikui; // Mushikuiコンストラクタ

    private int succesCount,qNum; // 正解数、今が何番目の問題か

    bool isTime = false; // タイマースタートフラグ

    bool timeOver = false; // タイムオーバーフラグ

    bool onceFlag = false; // 失敗時のシーン遷移フラグ

    bool successFlag = false;

    // パス-----------------------------------------------------------------------
    private string musiFilePath = "CSV/Smoking3"; // CSVパス名

    private string talkFilePath = "Text/Smoking/"; // 会話パートテキストパス名

    private string smokePath = "SmokingTalk"; // 喫煙シナリオのPath

    private string badSmokePath = "Bad/SmokingTalkBad"; // 喫煙BadシナリオPath
    // ---------------------------------------------------------------------------

    private bool[] badFlags = { false, false, false }; // 不正解フラグ

    private Vector2 tabacoSize; // タバコUIの大きさ

    public GameObject selectUI; // 選択肢UI

    // Use this for initialization
    void Start () {
        IsScenario(talkFilePath + smokePath);

        selectUI.SetActive(false); // 回答選択UIを非表示
        
        succesCount = 0; // 正解数

        tabacoSize = tabaco.rectTransform.sizeDelta; // 制限時間のUIの長さを設定
        //StartCoroutine(TimeDown());

        mushikui = new Mushikui(musiFilePath); // 虫食いデータ作成

        Question(); // 問題読み込み
    }

    
    void Update(){
        if (ScenarioController.Instance.IsReachLastInfo())
            if (successFlag)
            {
                successFlag = false;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
            if(!badFlags[answerCount])          // 成功時のシナリオ
                StartCoroutine(SelectStart());
            else                                // 失敗時のシナリオ
            {
                if(answerCount == 0 && !onceFlag)
                {
                    onceFlag = true;
                    Common.Instance.ChangeScene(Common.SceneName.Result);
                    return;
                }

                if (onceFlag) return;
                answerCount--;
                IsScenario(talkFilePath + smokePath + qNum.ToString());
            }

        if (tabaco.rectTransform.sizeDelta.x < 0)
            if (!timeOver){
                timeOver = true;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
    }

    public IEnumerator TimeDown(){
        while (tabaco.rectTransform.sizeDelta.x > 0 && isTime)
        {
            tabaco.rectTransform.sizeDelta -= new Vector2(time * Time.deltaTime,0);
            if(tabaco.rectTransform.sizeDelta.x <= tabacoSize.x / 2 &&
                tabaco.rectTransform.sizeDelta.x >= tabacoSize.x / 4) {
                tabaco.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
            } else if(tabaco.rectTransform.sizeDelta.x < tabacoSize.x / 4) {
                tabaco.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }
            yield return null;
        }
    }

    /// <summary>
    /// 回答選択UIを表示
    /// </summary>
    /// <returns></returns>
    public IEnumerator SelectStart()
    {
        //yield return new WaitForSeconds(1f);
        
        if (!isTime)
        {
            isTime = true;
            selectUI.SetActive(true);
            if (selectUI.activeSelf)
                StartCoroutine(TimeDown());
            Question();
        }
        yield return null;
    }

    /// <summary>
    /// 選択肢を選んだ時の処理
    /// </summary>
    /// <param name="text"></param>
    public void OnClick(Text text) {

        // 回答後の共通の値変化と初期化--------------------
        isTime = false;
        qLength--;
        tabaco.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        tabaco.rectTransform.sizeDelta = tabacoSize;
        //-------------------------------------------------
        
        if (text.text == mushikui.data[qNum].Musikui) {
            Debug.Log("〇");
            qNum++; // 問題Noを加算
            succesCount++; // 正解数を加算

            if (qLength <= 0) {
                isTime = true;
                Invoke("AA", 0.01f);
                Result();
                return;
            }

            IsScenario(talkFilePath + smokePath + qNum.ToString());

        } else {
            Debug.Log("×");
            qNum++;

            badFlags[answerCount] = true;

            IsScenario(talkFilePath + badSmokePath + answerCount.ToString());
        }
        Invoke("AA", 0.01f);
    }

    /// <summary>
    /// シナリオ呼び出し関数
    /// </summary>
    /// <param name="path"></param>
    public void IsScenario(string path)
    {
        ScenarioController.Instance.BeginScenario(path);
        ScenarioController.Instance.hideButtons();
    }

    //ベータ用
    public void AA()
    {
        selectUI.SetActive(false);
    }

    public void Question()
    {
        answer.text = mushikui.data[qNum].Question;
        for(int i = 0; i < mushikui.data[qNum].Select.Length; i++)
        {
            wordText[i].text = mushikui.data[qNum].Select[i];
        }
    }

    public void Result() {

        if(succesCount >= 3)
        {
            Common.Instance.clearFlag[Common.Instance.isClear] = true;
            IsScenario("GoodSmokingTalk");
            successFlag = true;
        }
        else
        {
            Common.Instance.clearFlag[Common.Instance.isClear] = false;
        }
        Common.Instance.ChangeScene(Common.SceneName.Result);
    }
}
