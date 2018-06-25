using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ActionController : MonoBehaviour {
    [SerializeField]
    private Text TActionCount;
    [SerializeField]
    private Image[] getData;
    [SerializeField]
    private Image missionSeat,isData,dataDetail;

    private bool missionOpen = true;
    private bool dataOpen = true;
    private bool datailOpen = true;

    void Start () {
        IsDataSelect();
        missionSeat.gameObject.SetActive(false);
        isData.gameObject.SetActive(false);
        dataDetail.gameObject.SetActive(false);
    }

    public void IsDataSelect() {
        int num = 0;
        foreach(var i in Common.Instance.dataDic) {
            if (!i) {
                getData[num].GetComponent<Image>().color = new Color(255, 255, 255, 125);
                num++;
            }
        }
    }

    /// <summary>
    /// 任務確認シート
    /// </summary>
    public void OpenMission()
    {
        missionSeat.gameObject.SetActive(missionOpen);
        if (missionOpen) missionOpen = false;
        else missionOpen = true;
    }

    /// <summary>
    /// 獲得資料シート
    /// </summary>
    public void IsDataList()
    {
        isData.gameObject.SetActive(dataOpen);
        if (dataOpen) dataOpen = false;
        else dataOpen = true;
    }

    /// <summary>
    /// 資料詳細
    /// </summary>
    public void IsDataDetail()
    {
        dataDetail.gameObject.SetActive(datailOpen);
        if (datailOpen) datailOpen = false;
        else datailOpen = true;
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
