using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour {
    [SerializeField]
    private Text TMission,TActionCount;
    [SerializeField]
    private Image[] getData;

	void Start () {
        
    }
	
	void Update () {
		
	}

    /// <summary>
    /// ミニゲーム遷移
    /// </summary>
    /// <param name="num"></param>
    public void ChangeMinigame(int num)
    {
        switch (num)
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
