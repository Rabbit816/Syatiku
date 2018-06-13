using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {
    [SerializeField]
    private Text scoreText;
	// ミニゲームで獲得した資料を表示
	void Start () {
        if (Common.gameClear)
            scoreText.text = Common.Instance.data[0] + "を手に入れた！\n" + Common.Instance.data[1] + "を手に入れた！\n";
        else
            scoreText.text = "何も手に入らなかった...";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TitleBack()
    {
        Common.Instance.ChangeScene(Common.SceneName.Title);
    }
}
