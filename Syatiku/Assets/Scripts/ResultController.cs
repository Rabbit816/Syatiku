using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {
    [SerializeField]
    private Text scoreText;
	// ミニゲームで獲得した資料を表示
	void Start () {
        if (Common.Instance.clearFlag[Common.Instance.isClear])
            switch (Common.Instance.isClear) {
                case 0: // hack
                    Common.Instance.dataFlag[0] = true;
                    Common.Instance.dataFlag[1] = true;
                    scoreText.text = "資料Aを手に入れた！\n" + "資料Aを手に入れた！";
                    break;
                case 1: // drink
                    Common.Instance.dataFlag[2] = true;
                    Common.Instance.dataFlag[3] = true;
                    scoreText.text = "資料Cを手に入れた！\n" + "資料Dを手に入れた！";
                    break;
                case 2: // smoke
                    Common.Instance.dataFlag[4] = true;
                    scoreText.text = "資料Eを手に入れた！";
                    break;
            }
        else
        {
            scoreText.text = "失敗...\n" + "何も手に入らなかった...";
        }
    }

    public void TitleBack()
    {
        Common.Instance.actionCount--;
        Common.Instance.ChangeScene(Common.SceneName.Action);
    }
}
