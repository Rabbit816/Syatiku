using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : BaseController {

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void ChangeMinigame(int num)
    {
        switch (num)
        {
            case 0:
                ChangeScene(SceneName.Smoking);
                break;
            case 1:
                ChangeScene(SceneName.Hacking);
                break;
            case 2:
                ChangeScene(SceneName.Drinking);
                break;
            default:
                break;
        }
    }
}
