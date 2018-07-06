using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour {
    // Canvas
    [SerializeField]
    private GameObject humanClone;
    // 行動回数テキスト
    [SerializeField]
    private Text TActionCount;

    [SerializeField]
    private Text action;

    // 獲得資料配列
    [SerializeField]
    private Image[] getData;

    [SerializeField]
    private Image[] detailIcon;

    // 任務内容、獲得資料背景、獲得資料詳細
    [SerializeField]
    private Image missionSeat,isData,dataDetail;

    // 人間生成座標
    [SerializeField]
    private GameObject[] createPos = new GameObject[4];
    private GameObject pos2;

    // ミニゲーム遷移のための数字
    private int[] sceneNum = { 0, 1, 0 };

    // フキダシ付き人間のPrefab配列
    [SerializeField]
    private Image[] humanPrefab = new Image[3];

    // ボスボタン
    [SerializeField]
    private Image bossButton;

    [SerializeField]
    private Sprite[] miniGameImage = new Sprite[3];
    // 各UI表示フラグ---------------------------------
    private bool missionOpen = true;  // 任務内容
    private bool dataOpen = true;   // 獲得資料リスト
    private bool datailOpen = true; // 獲得資料詳細
    // -----------------------------------------------

    void Start () {
        var common = Common.Instance.GetComponent<AudioSource>();
        common.Stop();

        IsDataSelect();
        if (Common.Instance.actionCount <= 0)
            bossButton.gameObject.SetActive(true);
        else
            bossButton.gameObject.SetActive(false);
        action.text = Common.Instance.actionCount.ToString();

        missionSeat.gameObject.SetActive(false);
        isData.gameObject.SetActive(false);
        dataDetail.gameObject.SetActive(false);

        pos2 = createPos[2];
        Common.Instance.Shuffle(createPos);
        Common.Instance.Shuffle(sceneNum);

        CreateHuman();
    }

    public void GetDataList() {
        
    }

    /// <summary>
    /// 獲得資料
    /// </summary>
    public void IsDataSelect() {
        int num = 0;
        foreach(var i in Common.Instance.dataFlag) {
            if (!i) {
                getData[num].GetComponent<Button>().interactable = false;
            }else {
                getData[num].GetComponent<Button>().interactable = true;
                detailIcon[num].color = Color.white;
            }
            num++;
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

    public void ChangeBoss()
    {
        Common.Instance.ChangeScene(Common.SceneName.Boss);
    }

    /// <summary>
    /// ミニゲーム遷移
    /// </summary>
    public void CreateHuman()
    {
        int num = 0; // 仮
        foreach (var i in sceneNum)
        {
            Image mini = Instantiate(humanPrefab[i], humanClone.transform) as Image;
            mini.transform.localPosition = createPos[num].transform.localPosition;
            if (createPos[num] == pos2)
                mini.transform.localScale = new Vector2(-1, 1);

            Image s_mini = mini.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
            s_mini.sprite = miniGameImage[i];

            HukidashiController hukiCon = mini.GetComponent<HukidashiController>();
            hukiCon.MiniGameNum(i);
            num++;
        }
    }
}
