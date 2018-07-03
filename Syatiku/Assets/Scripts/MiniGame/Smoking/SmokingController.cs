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
    private Mushikui mushikui;

    private string t_answer = "あけましておめでとう";
    private string filePath = "CSV/MushikuiTest_2";

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
            yield return null;
        }
        Common.gameClear = false;
        Common.Instance.ChangeScene(Common.SceneName.Result);
    }

    public void OnClick(Text text) {
        if (tabaco.rectTransform.sizeDelta.x <= 0) return;

        Debug.Log(text.text);
        if (text.text == "まる") {
            Debug.Log("〇");
            face.color = Color.white;

            tabaco.rectTransform.sizeDelta = tabacoSize;
            answerCount = firstAnswerCount;

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
                    Common.gameClear = false;
                    Common.Instance.ChangeScene(Common.SceneName.Result);
                    break;
                default:
                    break;
            }
        }
    }

    public void Question()
    {
        for(int i = 0; i < mushikui.data[0].Select.Length; i++)
        {
            wordText[i].text = mushikui.data[0].Select[i];
        }
    }
}
