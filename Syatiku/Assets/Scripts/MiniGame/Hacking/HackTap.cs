using UnityEngine;
using DG.Tweening;

public class HackTap : MonoBehaviour
{
    private string[] str;

    [SerializeField]
    private GameObject IntoPC;
    
    [Tooltip("集めた単語(Folder内に出す場所の親)")]
    public GameObject CollectWordFolder;

    [SerializeField, Tooltip("集めたリストに出す資料Object")]
    private GameObject DocPrefab;
    [HideInInspector]
    public GameObject GetWord;

    [Tooltip("PC内のposition")]
    public GameObject[] pos_list;
    [Tooltip("Folder内のposition")]
    public GameObject[] folder_pos_list;
    [SerializeField, Tooltip("資料Object")]
    private GameObject Document;

    [SerializeField, Tooltip("額縁Object")]
    private RectTransform Gakubuti;
    [SerializeField, Tooltip("名刺Object")]
    private GameObject Meishi;
    [SerializeField, Tooltip("名刺RectTransform")]
    private RectTransform Meishi_obj;
    
    [SerializeField, Tooltip("WindowObject")]
    private GameObject Window;
    [Tooltip("Image 5こ")]
    public Sprite[] img_list;
    [SerializeField, Tooltip("Zoom Object")]
    private GameObject Zoom;

    private GameObject DoorSide;
    private HackMain hack_main;
    private IntoPCAction intopc_action;
    private PatteringEvent patte;
    private HackGetWord hack_getword;
    private GameObject pat;
    private int GakuCount = 0;
    public int Gakubuti_max = 7;
    //比較する資料を取得したかどうか
    [HideInInspector]
    public bool _getDocument = false;

    //LowAnimが終わったかどうか
    private bool _lowAnim = false;
    private bool _animloop = false;
    [HideInInspector]
    public bool _windowFase = false;

    // Use this for initialization
    void Start ()
    {
        try
        {
            GetWord = GameObject.Find("Canvas/Check/GetWord");
            DoorSide = GameObject.Find("Canvas/DoorSide");
            pat = GameObject.Find("Canvas/PC/PatteringFase");
            GameObject getword = Resources.Load("Prefabs/MiniGame/Hacking/folder_word") as GameObject;
            hack_getword = getword.GetComponent<HackGetWord>();
        }
        catch
        {
            Debug.Log("Not Find");
        }
        
        Document.SetActive(false);
        Common.Instance.Shuffle(pos_list);
        GakuCount = 0;
        hack_main = GetComponent<HackMain>();
        patte = GetComponent<PatteringEvent>();
        intopc_action = GetComponent<IntoPCAction>();
        Meishi.SetActive(false);
        _getDocument = false;
        _lowAnim = false;
        
        _windowFase = false;
        _animloop = false;
	}

    /// <summary>
    /// タップしたところから単語が出てくる処理
    /// </summary>
    /// <param name="placeNum">どの場所かを指定</param>
    public void PlaceButton(int placeNum){
        //PC画面内を表示
        //戻るボタンで画面外に移動
        switch (placeNum)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                //SearchTap(placeNum);
                break;
            case 10:
                IntoPC.transform.localPosition = new Vector2(0, 0);
                break;
            case 11:
                IntoPC.transform.localPosition = new Vector2(0, -1200);
                pat.transform.SetSiblingIndex(0);
                break;
            case 12:
                DoorSide.transform.localPosition = new Vector2(-1960, 0);
                break;
            case 13:
                DoorSide.transform.localPosition = new Vector2(0, 0);
                break;
            case 14:
                if (_lowAnim)
                    return;
                IntoPC.transform.localPosition = new Vector2(0, 0);
                Window.SetActive(false);
                ZoomActive(3);
                pat.transform.SetSiblingIndex(2);
                StartCoroutine(patte.Start_AnimWaitTime(true));
                _lowAnim = true;
                break;
            case 15:
                if (_animloop)
                    return;
                IntoPC.transform.localPosition = new Vector2(0, 0);
                Window.SetActive(false);
                ZoomActive(4);
                pat.transform.SetSiblingIndex(2);
                StartCoroutine(patte.Start_AnimWaitTime(false));
                _animloop = true;
                break;
            case 26:
                intopc_action.DocumentsComparison();
                break;
        }
    }

    /// <summary>
    /// ズームにする時の処理
    /// </summary>
    /// <param name="childNum"></param>
    public void ZoomActive(int childNum)
    {
        if(!Zoom.transform.GetChild(childNum).gameObject.activeSelf)
            Zoom.transform.GetChild(childNum).gameObject.SetActive(true);
        else
            Zoom.transform.GetChild(childNum).gameObject.SetActive(false);
    }

    /// <summary>
    /// 額縁イベント処理
    /// </summary>
    public void GakuEvent()
    {
        GakuCount++;
        if (GakuCount > Gakubuti_max)
            return;
        Sequence seq = DOTween.Sequence();
        Gakubuti.DOPunchRotation(new Vector3(0, 0, 30), 0.7f);
        if (GakuCount == Gakubuti_max)
        {
            seq.Append(Gakubuti.DOLocalMoveY(-305, 0.6f))
                .OnComplete(() => { Meishi.SetActive(true); Meishi_obj.DOLocalMove(new Vector3(551, -551, 0), 0.5f); });
        }
    }


    /// <summary>
    /// 比較する資料を取得した時の処理
    /// </summary>
    public void DocumentAnim()
    {
        if (_getDocument)
            return;
        GameObject _get_doc = Instantiate(DocPrefab, GetWord.transform);
        _get_doc.transform.SetAsLastSibling();
        _getDocument = true;
        Document.SetActive(true);
        hack_getword.GetWordAnim(Document);
    }
}