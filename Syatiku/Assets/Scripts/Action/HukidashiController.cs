using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HukidashiController : MonoBehaviour {
    private int miniGameNum;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MiniGameNum(int i)
    {
        miniGameNum = i;
    }

    public void ChangeMiniGame()
    {
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
