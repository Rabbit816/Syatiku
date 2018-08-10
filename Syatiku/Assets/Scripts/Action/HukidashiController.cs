using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HukidashiController : MonoBehaviour {
    private int miniGameNum;

    public void MiniGameNum(int i)
    {
        miniGameNum = i;
    }

    public void ChangeMiniGame()
    {
        Common.Instance.miniNum = miniGameNum;
        switch (miniGameNum)
        {
            case 0:
                Common.Instance.ChangeScene(Common.SceneName.Smoking);
                break;
            case 1:
                Common.Instance.ChangeScene(Common.SceneName.Hacking);
                break;
            case 2:
                Common.Instance.ChangeScene(Common.SceneName.Drinking);
                break;
            default:
                break;
        }
    }
}
