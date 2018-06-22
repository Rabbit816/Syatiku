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

    private int qNum;
    private string t_answer = "あけましておめでとう";

    private Vector2 tabacoSize;

    // Use this for initialization
    void Start () {
        firstAnswerCount = answerCount;
        Question();
        tabacoSize = tabaco.rectTransform.sizeDelta;
        StartCoroutine(TimeDown());

        Debug.Log(CSVLoad.csvData[qNum][0]);
	}
	
	// Update is called once per frame
	void Update () {
        
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

            qNum++;
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
        answer.text = CSVLoad.csvData[qNum][0];
        int[] randNum = { 1,2,3,4 };
        Common.Instance.Shuffle(randNum);
        for(int i = 0; i < wordText.Length; i++)
        {
            wordText[i].text = CSVLoad.csvData[qNum][randNum[i]];
        }
    }
}
