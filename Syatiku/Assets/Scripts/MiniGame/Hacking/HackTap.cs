using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HackTap : MonoBehaviour
{
    private struct PlaceList
    {
        public GameObject pos;
        public string word;
    };
    private struct FolderPlaceList
    {
        public GameObject pos;
        public string word;
    };
    //これにランダムで選ばれた場所に単語を格納していく
    private PlaceList[] place_list = new PlaceList[]
    {
        new PlaceList(){ pos=null, word = "" },
    };
    private FolderPlaceList[] folder_place_list = new FolderPlaceList[]
    {
        new FolderPlaceList(){ pos=null, word = "" },
    };
    private string[] str;

    [SerializeField]
    private GameObject IntoPC;

    [SerializeField,Tooltip("出現する単語")]
    private GameObject AppearPrefab;

    [SerializeField, Tooltip("集めた単語(PC内に出すObject)")]
    private GameObject CollectedPrefab;
    private GameObject CollectedWord;
    private GameObject collectObject;

    [SerializeField, Tooltip("集めた単語(リスト内に出すObject)")]
    private GameObject GetWordPrefab;
    [HideInInspector]
    public GameObject GetWord;

    [SerializeField, Tooltip("Folderで使う出現単語")]
    private GameObject AppearFolderPrefab;
    [SerializeField, Tooltip("集めた単語(Folder内に出すObject)")]
    private GameObject CollectFolderPrefab;
    [SerializeField, Tooltip("集めた単語(リスト内に出すObject)")]
    private GameObject GetWordFolderPrefab;
    [SerializeField, Tooltip("集めた単語(Folder内に出す場所の親)")]
    private GameObject CollectWordFolder;

    [SerializeField, Tooltip("集めたリストに出す資料Object")]
    private GameObject DocPrefab;

    [SerializeField, Tooltip("単語を取得できるボタンの場所")]
    private GameObject[] Getting_position;
    [SerializeField, Tooltip("Drawer内で単語を取得できる場所")]
    private GameObject[] drawer_getting_position;

    [SerializeField, Tooltip("PC内のposition")]
    private GameObject[] pos_list;
    [SerializeField, Tooltip("Folder内のposition")]
    private GameObject[] folder_pos_list;
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
    [SerializeField, Tooltip("Image 5こ")]
    private Sprite[] img_list;
    [SerializeField, Tooltip("Zoom Object")]
    private GameObject Zoom;
    [SerializeField, Tooltip("チェックボックスとくっついてるやつ")]
    private GameObject Black_back;

    private GameObject DoorSide;
    private HackMain hack_main;
    private IntoPCAction intopc_action;
    private PatteringEvent patte;
    private GameObject pat;
    private int count = 0;
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
    void Start () {
        collectObject = GameObject.Find("Canvas/Check/GetWord");
        CollectedWord = GameObject.Find("Canvas/PC/PassWordFase/Collect");
        GetWord = GameObject.Find("Canvas/Check/GetWord");
        DoorSide = GameObject.Find("Canvas/DoorSide");
        pat = GameObject.Find("Canvas/PC/PatteringFase");

        Document.SetActive(false);
        Common.Instance.Shuffle(pos_list);
        count = 0;
        GakuCount = 0;
        hack_main = GetComponent<HackMain>();
        patte = GetComponent<PatteringEvent>();
        intopc_action = GetComponent<IntoPCAction>();
        Meishi.SetActive(false);
        place_list = new PlaceList[Getting_position.Length];
        folder_place_list = new FolderPlaceList[drawer_getting_position.Length];
        _getDocument = false;
        _lowAnim = false;
        
        _windowFase = false;
        _animloop = false;
        AddPlaceWord();
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
                SearchTap(placeNum);
                break;
            case 10:
                if (_windowFase)
                    Window.SetActive(true);
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
            case 16:
                ZoomActive(7);
                ZoomActive(2);
                break;
            case 17:
            case 18:
            case 19:
                DrawerTap(placeNum-17);
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
    /// 単語が出るところをタップした時の処理
    /// </summary>
    /// <param name="placeNum"></param>
    private void SearchTap(int placeNum)
    {
        // 一回もタップされてなかったらPC内とリスト内とその場所に表示
        if (Getting_position[placeNum].transform.childCount == 0)
        {
            if (place_list[placeNum].word == null)
                return;

            //押したところに単語を表示
            GameObject appearobj = Instantiate(AppearPrefab, Getting_position[placeNum].transform);
            Getting_position[placeNum].transform.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
            Image appear_img = appearobj.GetComponent<Image>();
            Text appearChild_text = appearobj.transform.GetChild(0).GetComponent<Text>();
            AppearPrefab.GetComponent<Image>().sprite = img_list[placeNum];
            DOTween.ToAlpha(
                () => appear_img.color,
                color => appear_img.color = color,
                0f, 2.0f);
            DOTween.ToAlpha(
                () => appearChild_text.color,
                color => appearChild_text.color = color,
                0f, 2.0f);

            //PC内に集めた単語を表示
            GameObject _collected_word = Instantiate(CollectedPrefab, CollectedWord.transform);
            _collected_word.transform.position = pos_list[placeNum].transform.position;
            _collected_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();

            //集めたものリストの中に単語を表示
            GameObject _get_word = Instantiate(GetWordPrefab, GetWord.transform);
            _get_word.transform.SetAsFirstSibling();
            _get_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
        }
    }

    /// <summary>
    /// 待つ時間処理
    /// </summary>
    /// <param name="time">待つ時間</param>
    /// <returns></returns>
    private IEnumerator Wait_time(float time)
    {
        yield return new WaitForSeconds(time);
        Document.SetActive(true);
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
    /// 比較する資料を取得した時のアニメーションと処理
    /// </summary>
    public void DocumentAnim()
    {
        if (_getDocument)
            return;
        GameObject _get_doc = Instantiate(DocPrefab, GetWord.transform);
        _get_doc.transform.SetAsLastSibling();
        _getDocument = true;
        Document.SetActive(true);
        Sequence seq = DOTween.Sequence();
        Image img_alpha = Document.GetComponent<Image>();
        RectTransform Doc_rect = Document.GetComponent<RectTransform>();
        seq.Append(Doc_rect.DOLocalMove(new Vector3(900, 377, 0), 1.3f).SetDelay(0.3f))
            .OnComplete(() =>
            {
                DOTween.ToAlpha(
                () => img_alpha.color,
                color => img_alpha.color = color,
                0f, 0.3f);
            });
        StartCoroutine(Wait_time(3f));
    }

    public void DrawerTap(int place)
    {
        if (drawer_getting_position[place].transform.childCount == 0)
        {
            //押したところに単語を表示
            GameObject appearobj = Instantiate(AppearFolderPrefab, drawer_getting_position[place].transform);
            drawer_getting_position[place].transform.GetComponentInChildren<Text>().text = folder_place_list[place].word.ToString();
            Image appear_img = appearobj.GetComponent<Image>();
            Text appearChild_text = appearobj.transform.GetChild(0).GetComponent<Text>();
            DOTween.ToAlpha(
                () => appear_img.color,
                color => appear_img.color = color,
                0f, 2.0f);
            DOTween.ToAlpha(
                () => appearChild_text.color,
                color => appearChild_text.color = color,
                0f, 2.0f);

            //PC内に集めた単語を表示
            GameObject _collected_word = Instantiate(CollectFolderPrefab, CollectWordFolder.transform);
            _collected_word.transform.position = folder_place_list[place].pos.transform.position;
            _collected_word.GetComponentInChildren<Text>().text = folder_place_list[place].word.ToString();

            //集めたものリストの中に単語を表示
            GameObject _get_word = Instantiate(GetWordFolderPrefab, GetWord.transform);
            _get_word.transform.SetAsFirstSibling();
            _get_word.GetComponentInChildren<Text>().text = folder_place_list[place].word.ToString();
        }
    }

    /// <summary>
    /// 各場所に単語を入れる
    /// </summary>
    private void AddPlaceWord()
    {
        string[] stren = hack_main.Quest_list.ToArray();
        //Common.Instance.Shuffle(Getting_position);
        for (int j = 0; j < Getting_position.Length; j++)
        {
            place_list[j].pos = Getting_position[j];
            if (j >= Getting_position.Length - (Getting_position.Length - pos_list.Length))
                continue;
            else
                place_list[j].word = stren[j];
        }
        string[] wd = hack_main.Folder_ans_list.ToArray();
        for(int i=0; i<drawer_getting_position.Length; i++)
        {
            folder_place_list[i].pos = drawer_getting_position[i];
            folder_place_list[i].word = wd[i];
            Debug.Log("folder_place_list: " + folder_place_list[i].word.ToString());
        }
    }

    /// <summary>
    /// 集めた単語を確認するUIの処理
    /// </summary>
    public void CollectWordsOpen()
    {
        switch (count)
        {
            case 0:
                count++;
                collectObject.transform.localPosition = new Vector2(collectObject.transform.localPosition.x - 415, collectObject.transform.localPosition.y);
                Black_back.transform.localPosition = new Vector2(Black_back.transform.localPosition.x - 1900, Black_back.transform.localPosition.y);
                break;
            case 1:
                count--;
                collectObject.transform.localPosition = new Vector2(collectObject.transform.localPosition.x + 415, collectObject.transform.localPosition.y);
                Black_back.transform.localPosition = new Vector2(Black_back.transform.localPosition.x + 1900, Black_back.transform.localPosition.y);
                break;
        }
    }
}