using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {
    
    private string[] scoreText = new string[2];

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private RectTransform[] proPos = new RectTransform[2];

    private GameObject[] property = new GameObject[2];
	// ミニゲームで獲得した情報を表示
	void Start () {
        // ミニゲーム分岐
        if (Common.Instance.clearFlag[Common.Instance.miniNum])
            switch (Common.Instance.miniNum)
            {
                case 0:
                    scoreText[0] = "情報A";
                    scoreText[1] = "情報B";
                    break;
                case 1:
                    scoreText[0] = "情報C";
                    scoreText[1] = "情報D";
                    break;
                case 2:
                    scoreText[0] = "情報E";
                    break;
            }
        else
        {
            scoreText[0] = "スカ";
            scoreText[1] = "スカ";
        }

        StartCoroutine(CreateProperty());
    }

    public void TitleBack()
    {
        Common.Instance.actionCount--;
        Common.Instance.ChangeScene(Common.SceneName.Action);
    }

    public IEnumerator CreateProperty()
    {
        // prefabから情報フキダシを生成
        for (int i = 0; i < property.Length; i++)
        {
            property[i] = Resources.Load("Prefabs/Result/Property") as GameObject;
            Instantiate(property[i], canvas.transform);
            property[i].transform.localPosition = proPos[i].transform.localPosition;
            property[i].transform.GetChild(0).GetComponent<Text>().text = scoreText[i];
            yield return new WaitForSeconds(0.5f);
        }
    }
    
}
