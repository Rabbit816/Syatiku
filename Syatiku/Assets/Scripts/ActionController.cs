using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour {
    [SerializeField]
    private Text TMission,TActionCount;
	void Start () {
		
	}
	
	void Update () {
		
	}
    public void ChangeMinigame(int num)
    {
        switch (num)
        {
            case 0:
                Common.Instance.ChangeScene(Common.SceneName.Smoking, 1.0f);
                break;
            case 1:
                Common.Instance.ChangeScene(Common.SceneName.Hacking, 1.0f);
                break;
            case 2:
                Common.Instance.ChangeScene(Common.SceneName.Drinking, 1.0f);
                break;
            default:
                break;
        }
    }
}
