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

    private float tabacoWidth;

    // Use this for initialization
    void Start () {
        firstAnswerCount = answerCount;
        answer.text = t_answer.Substring(0,5);
        tabacoWidth = tabaco.rectTransform.sizeDelta.x;
        StartCoroutine(TimeDown());
        Common.Instance.Shuffle(word);
        for(int i = 0; i < wordText.Length; i++) {
            wordText[i].text = word[i];
        }
        Debug.Log(tabacoWidth);
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
            tabaco.rectTransform.sizeDelta = new Vector2(550, 0);
            answerCount = firstAnswerCount;
        } else {
            Debug.Log("×");
            answerCount--;
            face.color = Color.yellow;
        }
    }
}
