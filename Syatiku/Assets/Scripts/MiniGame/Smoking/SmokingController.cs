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
    
    private ScenarioController scenario;

    [SerializeField]
    private GameObject scenarioCanvas;

    [SerializeField]
    private GameObject[] nonActive;

    private Mushikui mushikui; // Mushikuiコンストラクタ
    private int qNum; // 今が何番目の問題か
    private int succesCount; // 正解数
    [SerializeField]
    private int qLength; // 合計問題数

    private string filePath = "CSV/Smoking2"; // CSVパス名

    private Vector2 tabacoSize;

    public GameObject selectUI;

    // Use this for initialization
    void Start () {
        //scenarioCanvas.SetActive(false);
        selectUI.SetActive(false);
        foreach(var i in nonActive)
        {
            i.SetActive(false);
        }
        
        firstAnswerCount = answerCount;
        
        succesCount = 0;

        tabacoSize = tabaco.rectTransform.sizeDelta;
        //StartCoroutine(TimeDown());

        mushikui = new Mushikui(filePath);

        Question();
	}

    void Update()
    {
        //if (scenario.IsShowAllMessage())
        //{
        //    StartCoroutine(ChangeSelect());
        //}
    }

    bool timeFlag = false;
    void OnEnable()
    {
        if (timeFlag)
        {
            StartCoroutine(TimeDown());
            timeFlag = false;
        }
    }

    public IEnumerator ActiveChange()
    {
        yield return new WaitForSeconds(2f);
        selectUI.SetActive(true);
        scenario.gameObject.SetActive(false);
        
    }

    public IEnumerator ChangeSelect()
    {
        new WaitForSeconds(1.0f);
        scenarioCanvas.SetActive(false);
        yield return null;
        timeFlag = true;
    }

    public IEnumerator TimeDown()
    {
        yield return new WaitForSeconds(1f);
        while (tabaco.rectTransform.sizeDelta.x > 0)
        {
            tabaco.rectTransform.sizeDelta -= new Vector2(time,0);
            if(tabaco.rectTransform.sizeDelta.x <= tabacoSize.x / 2 &&
                tabaco.rectTransform.sizeDelta.x >= tabacoSize.x / 4) {
                tabaco.color = Color.yellow;
            } else if(tabaco.rectTransform.sizeDelta.x < tabacoSize.x / 4) {
                tabaco.color = Color.red;
            }
            yield return null;
        }
        Common.Instance.ChangeScene(Common.SceneName.Result);
    }

    public void OnClick(Text text) {
        if (tabaco.rectTransform.sizeDelta.x <= 0) return;

        Debug.Log(text.text);
        if (text.text == mushikui.data[qNum].Musikui) {
            Debug.Log("〇");
            face.color = Color.white;

            tabaco.rectTransform.sizeDelta = tabacoSize;
            answerCount = firstAnswerCount;

            succesCount++;
            qNum++;
            qLength--;
            if (qLength <= 0) {
                Result();
                return;
            }
                Question();

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
                    StopCoroutine(TimeDown());
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

        if(succesCount >= 8)
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
