using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour {
    [SerializeField]
    private Text TMission,TActionCount;
    [SerializeField]
    private Image[] getData;
    [SerializeField]
    private Image missionSeat,isData;

    private bool mOpen = true,dOpen = false;
	void Start () {
        missionSeat.gameObject.SetActive(false);
        isData.gameObject.SetActive(false);
    }
	
	void Update () {
		
	}

    /// <summary>
    /// 任務確認シート
    /// </summary>
    public void OpenMission()
    {
        missionSeat.gameObject.SetActive(mOpen);
        if (mOpen) mOpen = false;
        else mOpen = true;
    }

    public void OpenDataList()
    {
        isData.gameObject.SetActive(mOpen);
        if (dOpen) dOpen = false;
        else dOpen = true;
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
