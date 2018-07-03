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

    private Mushikui mushikui; // Mushikuiコンストラクタ
    private int qNum; // 今が何番目の問題か
    private int succesCount; // 正解数
    [SerializeField]
    private int qLength; // 合計問題数

    private string filePath = "CSV/Smoking"; // CSVパス名

    private Vector2 tabacoSize;

    // Use this for initialization
    void Start () {
        firstAnswerCount = answerCount;
        
        tabacoSize = tabaco.rectTransform.sizeDelta;
        StartCoroutine(TimeDown());

        mushikui = new Mushikui(filePath);

        Question();
	}

    public IEnumerator TimeDown()
    {
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

            qNum++;
            qLength--;
            if (qLength <= 0) {
                Result();
                return;
            }
                Question();

        } else {
            Debug.Log("×");
            int oldCount = answerCount;
            answerCount--;
            switch (answerCount)
            {
                case 3:
                case 2:
                    face.color = Color.yellow;
                    break;
                case 1:
                    face.color = Color.red;
                    break;
                case 0:
                    Common.Instance.clearFlag["Smoking"] = false;
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

        if(succesCount >= 8) {
            Common.Instance.clearFlag["Smoking"] = true;
            Common.Instance.isClear = "Smoking";
        } 
        Common.Instance.ChangeScene(Common.SceneName.Result);
    }
}
