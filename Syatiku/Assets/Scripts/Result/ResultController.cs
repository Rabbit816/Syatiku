using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {
    
    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private Image enemy, spot;

    [SerializeField]
    private Button backAction;

    [SerializeField]
    private Sprite _enemySprite;

    [SerializeField]
    private RectTransform[] proPos = new RectTransform[2];

    private string[] scoreText = new string[2];

    private GameObject[] property = new GameObject[2];

    private bool successFlag = false;
	// ミニゲームで獲得した情報を表示
	void Start () {
        // ミニゲーム分岐
        if (Common.Instance.clearFlag[Common.Instance.miniNum]) {
            successFlag = true;
            switch (Common.Instance.miniNum) {
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
        } else {
            successFlag = false;
            scoreText[0] = "スカ";
            scoreText[1] = "スカ";
        }

        StartCoroutine(CreateProperty());
    }

    /// <summary>
    /// 行動選択に戻る
    /// </summary>
    public void ActionBack()
    {
        Common.Instance.actionCount--;
        Common.Instance.ChangeScene(Common.SceneName.Action);
    }

    /// <summary>
    /// 獲得した情報を表示する
    /// </summary>
    /// <returns></returns>
    public IEnumerator CreateProperty()
    {
        // EnemyとSpotを表示
        yield return new WaitForSeconds(0.5f);
        enemy.gameObject.SetActive(true);
        spot.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        // prefabから情報フキダシを生成
        for (int i = 0; i < property.Length; i++)
        {
            property[i] = Resources.Load("Prefabs/Result/Property") as GameObject;
            Instantiate(property[i], canvas.transform);
            property[i].transform.localPosition = proPos[i].transform.localPosition;
            property[i].transform.GetChild(0).GetComponent<Text>().text = scoreText[i];
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        if(successFlag) enemy.sprite = _enemySprite;
        else {

        }
        
        backAction.gameObject.SetActive(true);
    }
    
}
