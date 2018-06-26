using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour {
    // Canvas
    [SerializeField]
    private Canvas canvas;
    // 行動回数テキスト
    [SerializeField]
    private Text TActionCount;

    // 獲得資料配列
    [SerializeField]
    private Image[] getData;

    // 任務内容、獲得資料背景、獲得資料詳細
    [SerializeField]
    private Image missionSeat,isData,dataDetail;

    // 人間生成座標
    [SerializeField]
    private Vector2[] createPos = new Vector2[4];

    // ミニゲーム遷移のための数字
    private int[] sceneNum = { 0, 1, 2 };

    // フキダシ付き人間のPrefab配列
    [SerializeField]
    private Image[] miniGamePrefab = new Image[3];

    // 各UI表示フラグ---------------------------------
    private bool missionOpen = true;  // 任務内容
    private bool dataOpen = true;   // 獲得資料リスト
    private bool datailOpen = true; // 獲得資料詳細
    // -----------------------------------------------

    void Start () {
        IsDataSelect();
        missionSeat.gameObject.SetActive(false);
        isData.gameObject.SetActive(false);
        dataDetail.gameObject.SetActive(false);
        Common.Instance.Shuffle(createPos);
        Common.Instance.Shuffle(sceneNum);
    }

    /// <summary>
    /// 獲得資料
    /// </summary>
    public void IsDataSelect() {
        int num = 0;
        foreach(var i in Common.Instance.dataFlag) {
            if (!i) {
                getData[num].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
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
    /// 獲得資料シート表示
    /// </summary>
    public void IsDataList()
    {
        isData.gameObject.SetActive(dataOpen);
        if (dataOpen) dataOpen = false;
        else dataOpen = true;
    }

    /// <summary>
    /// 資料詳細表示
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
        
        foreach (var i in sceneNum)
        switch (i)
        {
            case 0:
            case 1:
            case 2:
                Image mini = Instantiate(miniGamePrefab[i], canvas.transform) as Image;
                mini.transform.localPosition = createPos[i];
                HukidashiController hukiCon = mini.GetComponent<HukidashiController>();
                hukiCon.MiniGameNum(i);
                break;
            default:
            break;
        }
    }
}
