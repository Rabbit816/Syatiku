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
    private string t_answer = "あけましておめでとう";
    private string[] word = {
        "こんにちは",
        "こんばんわ",
        "おはよう",
        "おめでとう",
    };

    private Vector2 tabacoSize;

    // Use this for initialization
    void Start () {
        firstAnswerCount = answerCount;
        answer.text = t_answer.Substring(0,5);
        tabacoSize = tabaco.rectTransform.sizeDelta;
        StartCoroutine(TimeDown());
        Common.Instance.Shuffle(word);
        for(int i = 0; i < wordText.Length; i++) {
            wordText[i].text = word[i];
        }
        Debug.Log(tabacoSize);
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
    }

    public void OnClick(Text text) {
        if(text.text == t_answer.Substring(5, 5)) {
            Debug.Log("〇");
            tabaco.rectTransform.sizeDelta = tabacoSize;
            answerCount = firstAnswerCount;
        } else {
            Debug.Log("×");
            int oldCount = answerCount;
            answerCount--;
            switch (answerCount)
            {
                case 3:
                    face.color = Color.yellow;
                    break;
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
}
