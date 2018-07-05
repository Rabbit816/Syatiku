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

    [SerializeField]
    private GameObject[] nonActive;

    private Mushikui mushikui; // Mushikuiコンストラクタ
    private int qNum; // 今が何番目の問題か
    private int succesCount; // 正解数
    [SerializeField]
    private int qLength; // 合計問題数

    private string musiFilePath = "CSV/Smoking2"; // CSVパス名

    private string talkFilePath = "Text/Smoking/SmokingTalk";

    private Vector2 tabacoSize;

    public GameObject selectUI;

    private Coroutine timeDown;

    // Use this for initialization
    void Start () {
        ScenarioController.Instance.BeginScenario(talkFilePath);
        
        selectUI.SetActive(false);
        foreach(var i in nonActive)
        {
            i.SetActive(false);
        }
        
        firstAnswerCount = answerCount;
        
        succesCount = 0;

        tabacoSize = tabaco.rectTransform.sizeDelta;
        //StartCoroutine(TimeDown());

        mushikui = new Mushikui(musiFilePath);

        Question();
	}

    bool isTime = false;
    void Update(){
        if (ScenarioController.Instance.IsReachLastInfo()) {
            selectUI.SetActive(true);
            if (!isTime) {
                isTime = true;
                if (selectUI.activeSelf)
                    StartCoroutine(TimeDown());
                Question();
            }
        }

        if(tabaco.rectTransform.sizeDelta.x < 0)
            Common.Instance.ChangeScene(Common.SceneName.Result);
    }

    public void InitCorutine() {
        timeDown = null;
        timeDown = StartCoroutine(TimeDown());
    }

    public IEnumerator ActiveChange(){
        yield return new WaitForSeconds(2f);
        selectUI.SetActive(true);
        scenario.gameObject.SetActive(false);
    }

    public IEnumerator TimeDown(){
        while (tabaco.rectTransform.sizeDelta.x > 0 && isTime)
        {
            tabaco.rectTransform.sizeDelta -= new Vector2(time * Time.deltaTime,0);
            if(tabaco.rectTransform.sizeDelta.x <= tabacoSize.x / 2 &&
                tabaco.rectTransform.sizeDelta.x >= tabacoSize.x / 4) {
                tabaco.color = Color.yellow;
            } else if(tabaco.rectTransform.sizeDelta.x < tabacoSize.x / 4) {
                tabaco.color = Color.red;
            }
            yield return null;
        }
    }

    public void OnClick(Text text) {
        if (tabaco.rectTransform.sizeDelta.x <= 0) return;

        Debug.Log(text.text);
        if (text.text == mushikui.data[qNum].Musikui) {
            Debug.Log("〇");

            succesCount++;
            qNum++;
            qLength--;
            if (qLength <= 0) {
                Result();
                return;
            }

            // 初期化と会話表示非表示---------
            face.color = Color.white;
            tabaco.color = Color.white;
            tabaco.rectTransform.sizeDelta = tabacoSize;

            answerCount = firstAnswerCount;
            
            scenarioWin.SetActive(true);
            selectUI.SetActive(false);
            isTime = false;
            ScenarioController.Instance.BeginScenario(talkFilePath + qNum.ToString());
            Question();
            // ------------------------------

        } else {
            Debug.Log("×");
            answerCount--;
            tabaco.rectTransform.sizeDelta -= new Vector2(50f, 0);
            switch (answerCount)
            {
                case 3:
                    face.color = Color.white;
                    break;
                case 2:
                    face.color = Color.yellow;
                    break;
                case 1:
                    face.color = Color.red;
                    break;
                case 0:
                    StopCoroutine(timeDown);
                    InitCorutine();
                    Common.Instance.clearFlag[Common.Instance.isClear] = false;
                    Common.Instance.ChangeScene(Common.SceneName.Result);
                    break;
                default:
                    break;
            }
        }
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
