using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmokingController : MonoBehaviour {
    [SerializeField]
    private Image tabaco,face;
    [SerializeField]
    private float time;
    [SerializeField]
    private Text[] wordText = new Text[4];
    [SerializeField]
    private Text answer;
    [SerializeField]
    private int answerCount;
    private int firstAnswerCount;
    [SerializeField]
    private GameObject scenarioWin;
    
    private ScenarioController scenario;

    private Mushikui mushikui; // Mushikuiコンストラクタ
    private int qNum; // 今が何番目の問題か
    private int succesCount; // 正解数
    [SerializeField]
    private int qLength; // 合計問題数

    private string musiFilePath = "CSV/Smoking3"; // CSVパス名

    private string talkFilePath = "Text/Smoking/"; // 会話パートテキストパス名

    private string smokePath = "SmokingTalk"; // 喫煙シナリオのPath

    private string badSmokePath = "Bad/SmokingTalkBad"; // 喫煙BadシナリオPath

    bool isTime = false; // タイマースタートフラグ

    bool timeOver = false; // タイムオーバーフラグ

    private bool badFlag = false;

    private Vector2 tabacoSize;

    public GameObject selectUI;

    private Coroutine timeDown;

    // Use this for initialization
    void Start () {
<<<<<<< HEAD
        ScenarioController.Instance.BeginScenario(talkFilePath); // シナリオ再生
        ScenarioController.Instance.hideButtons();
=======
        IsScenario(talkFilePath + smokePath);
>>>>>>> master

        selectUI.SetActive(false); // 回答選択UIを非表示
        
        firstAnswerCount = answerCount; // 回答権を設定
        
        succesCount = 0; // 正解数

        tabacoSize = tabaco.rectTransform.sizeDelta; // 制限時間のUIの長さを設定
        //StartCoroutine(TimeDown());

        mushikui = new Mushikui(musiFilePath);

        Question();
	}

    
    void Update(){
        if (ScenarioController.Instance.IsReachLastInfo()) {
            if(!badFlag) StartCoroutine(SelectStart());
            else
            {
                IsScenario(talkFilePath + qNum.ToString());
                badFlag = false;
            }
            
            Debug.Log("Callaaaa");
        }

        if (tabaco.rectTransform.sizeDelta.x < 0)
            if (!timeOver){
                timeOver = true;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
    }

    public void InitCorutine() {
        timeDown = null;
        timeDown = StartCoroutine(TimeDown());
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
<<<<<<< HEAD
        yield return new WaitForSeconds(1f);
        selectUI.SetActive(true);
=======
        //yield return new WaitForSeconds(1f);
        
>>>>>>> master
        if (!isTime)
        {
            Debug.Log("Call");
            isTime = true;
            selectUI.SetActive(true);
            if (selectUI.activeSelf)
                StartCoroutine(TimeDown());
            //Question();
        }
        yield return null;
    }

    /// <summary>
    /// 選択肢を選んだ時の処理
    /// </summary>
    /// <param name="text"></param>
    public void OnClick(Text text) {
        if (tabaco.rectTransform.sizeDelta.x <= 0) return;

        qNum++;
        qLength--;
        Invoke("AA", 0.1f);

        Debug.Log(text.text);
        if (text.text == mushikui.data[qNum].Musikui) {
            Debug.Log("〇");

            succesCount++;
            
            if (qLength <= 0) {
                Result();
                return;
            }

            // 初期化と会話表示非表示---------
            face.color = Color.white;
            tabaco.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            tabaco.rectTransform.sizeDelta = tabacoSize;

            answerCount = firstAnswerCount;
<<<<<<< HEAD
            
            
            isTime = false;
            ScenarioController.Instance.BeginScenario(talkFilePath + qNum.ToString());
            ScenarioController.Instance.hideButtons();
=======

            IsScenario(talkFilePath + qNum.ToString());

            isTime = false;
            //selectUI.SetActive(false);
            Debug.Log("Active=" + selectUI.activeSelf);
>>>>>>> master
            //Question();
            // ------------------------------

        } else {
            Debug.Log("×");

            badFlag = true;

            answerCount--;
            //tabaco.rectTransform.sizeDelta -= new Vector2(50f, 0);

            IsScenario(talkFilePath + badSmokePath + answerCount.ToString());

            //ScenarioController.Instance.BeginScenario("");

            //switch (answerCount)
            //{
            //    case 3:
            //        face.color = Color.white;
            //        break;
            //    case 2:
            //        face.color = Color.yellow;
            //        break;
            //    case 1:
            //        face.color = Color.red;
            //        break;
            //    case 0:
            //        Common.Instance.clearFlag[Common.Instance.isClear] = false;
            //        Common.Instance.ChangeScene(Common.SceneName.Result);
            //        break;
            //    default:
            //        break;
            //}
        }
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
        }
        else
        {
            Common.Instance.clearFlag[Common.Instance.isClear] = false;
        }
        Common.Instance.ChangeScene(Common.SceneName.Result);
    }
}
