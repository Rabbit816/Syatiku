using System;
using System.Collections;
using System.Collections.Generic;
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
    //これにランダムで選ばれた場所に単語を格納していく
    private PlaceList[] place_list = new PlaceList[]
    {
        new PlaceList(){ pos=null, word = "" },
    };

    [SerializeField, Tooltip("全部のタップできる場所格納")]
    private GameObject[] place;

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
    private GameObject GetWord;

    [SerializeField, Tooltip("集めたリストに出す資料Object")]
    private GameObject DocPrefab;

    [SerializeField, Tooltip("単語を取得できるボタンの場所")]
    private GameObject[] Getting_position;

    [SerializeField, Tooltip("PC内のposition")]
    private GameObject[] pos_list;
    [SerializeField, Tooltip("資料Object")]
    private GameObject Document;
    [SerializeField, Tooltip("資料Object")]
    private RectTransform Doc_rect;

    [SerializeField, Tooltip("額縁Object")]
    private RectTransform Gakubuti;
    [SerializeField, Tooltip("名刺Object")]
    private GameObject Meishi;
    [SerializeField, Tooltip("名刺RectTransform")]
    private RectTransform Meishi_obj;
    [SerializeField, Tooltip("名刺裏のテキストObject")]
    private RectTransform place_button_10;
    [SerializeField, Tooltip("PaperPrefab")]
    private GameObject paper_prefab;
    [SerializeField, Tooltip("WindowObject")]
    private GameObject Window;
    [SerializeField, Tooltip("Image 5こ")]
    private Sprite[] img_list;

    private GameObject DoorSide;
    private GameObject Zoom;
    private HackMain hack_main;
    private IntoPCAction intopc_action;
    private PatteringEvent patte;
    private GameObject pat;
    private int count = 0;
    private int GakuCount = 0;
    //比較する資料を取得したかどうか
    [HideInInspector]
    public bool _getDocument = false;

    //LowAnimが終わったかどうか
    private bool _lowAnim = false;
    private bool _animloop = false;
    [HideInInspector]
    public bool _document = false;
    [HideInInspector]
    public bool _windowFase = false;

    // Use this for initialization
    void Start () {
        collectObject = GameObject.Find("Canvas/Check/GetWord");
        CollectedWord = GameObject.Find("Canvas/PC/PassWordFase/Collect");
        GetWord = GameObject.Find("Canvas/Check/GetWord");
        DoorSide = GameObject.Find("Canvas/DoorSide");
        Zoom = GameObject.Find("Canvas/Zoom");
        pat = GameObject.Find("Canvas/PC/PatteringFase");

        intopc_action = GetComponent<IntoPCAction>();

        Document.SetActive(false);
        Common.Instance.Shuffle(pos_list);
        count = 0;
        GakuCount = 0;
        hack_main = GetComponent<HackMain>();
        patte = GetComponent<PatteringEvent>();
        Meishi.SetActive(false);
        place_list = new PlaceList[Getting_position.Length];
        _getDocument = false;
        _lowAnim = false;
        _document = false;
        _windowFase = false;
        _animloop = false;
        AddPlaceWord();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    /// <summary>
    /// タップしたところから単語が出てくる処理
    /// </summary>
    /// <param name="placeNum">どの場所かを指定</param>
    public void PlaceButton(int placeNum){
        int selectNum = 0;
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
                // 一回もタップされてなかったらPC内とリスト内とその場所に表示
                if (Getting_position[placeNum].transform.childCount == 0)
                {
                    if (place_list[placeNum].word == null)
                        return;

                    Instantiate(AppearPrefab, Getting_position[placeNum].transform);
                    Getting_position[placeNum].transform.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
                    AppearPrefab.GetComponent<Image>().sprite = img_list[placeNum];

                    GameObject _collected_word = Instantiate(CollectedPrefab, CollectedWord.transform);
                    _collected_word.transform.position = pos_list[placeNum].transform.position;
                    _collected_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();

                    GameObject _get_word = Instantiate(GetWordPrefab, GetWord.transform);
                    _get_word.transform.SetAsFirstSibling();
                    _get_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
                }
                break;
            case 10:
                if (_windowFase)
                    Window.SetActive(true);

                IntoPC.transform.localPosition = new Vector2(0, 0);
                break;
            case 11:
                IntoPC.transform.localPosition = new Vector2(0, -500);
                pat.transform.SetSiblingIndex(0);
                break;
            case 12:
                DoorSide.transform.localPosition = new Vector2(-800, 0);
                break;
            case 13:
                DoorSide.transform.localPosition = new Vector2(0, 0);
                break;
            case 14:
                if (_lowAnim)
                    return;
                IntoPC.transform.localPosition = new Vector2(0, 0);
                Window.SetActive(false);
                pat.transform.SetSiblingIndex(2);
                patte.LowAnim();
                _lowAnim = true;
                break;
            case 15:
                Zoom.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 16:
                Zoom.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 17:
                Zoom.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 18:
                Zoom.transform.GetChild(1).gameObject.SetActive(false);
                break;
            case 19:
                Zoom.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 20:
                Zoom.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 21:
                Zoom.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 22:
                Zoom.transform.GetChild(3).gameObject.SetActive(false);
                break;
            case 23:
                Zoom.transform.GetChild(4).gameObject.SetActive(true);
                break;
            case 24:
                Zoom.transform.GetChild(4).gameObject.SetActive(false);
                break;
            case 25:
                if (_animloop)
                    return;
                IntoPC.transform.localPosition = new Vector2(0, 0);
                Window.SetActive(false);
                pat.transform.SetSiblingIndex(2);
                patte.AnimLoop();
                _animloop = true;
                break;
            case 26:
                intopc_action.DocumentsComparison();
                break;
            case 27:
                patte.AnimLoop();
                break;
            case 28:
                Zoom.transform.GetChild(5).gameObject.SetActive(false);
                break;
            case 29:
                Zoom.transform.GetChild(5).gameObject.SetActive(true);
                break;
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
        if (GakuCount > 7)
            return;
        Sequence seq = DOTween.Sequence();
        Gakubuti.DOPunchRotation(new Vector3(0, 0, 30), 0.7f);
        if (GakuCount == 7)
        {
            seq.Append(Gakubuti.DOLocalMoveY(-122, 0.6f))
                .OnComplete(() => { Meishi.SetActive(true); Meishi_obj.DOLocalMove(new Vector3(253, -226, 0), 0.5f); });
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
        seq.Append(Doc_rect.DOLocalMove(new Vector3(474, 377, 0), 1.3f).SetDelay(0.3f))
            .OnComplete(() =>
            {
                DOTween.ToAlpha(
                () => img_alpha.color,
                color => img_alpha.color = color,
                0f, 0.3f);
            });
        StartCoroutine(Wait_time(3f));
    }

    /// <summary>
    /// 名刺タップの処理
    /// </summary>
    public void MeishiTap()
    {
        if (place_button_10.transform.childCount == 2)
            return;
        GameObject _get_doc = Instantiate(paper_prefab, GetWord.transform);
        _get_doc.transform.SetAsLastSibling();
        GameObject obj = new GameObject();
        obj.transform.SetParent(place_button_10.transform, false);
        _document = true;

        Sequence s = DOTween.Sequence();
        s.Append(place_button_10.DOLocalMove(new Vector3(374, 221, 0), 0.7f))
            .Join(place_button_10.DOScale(0.5f, 0.7f))
            .OnComplete(() => place_button_10.gameObject.SetActive(false));
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
                return;
            else
                place_list[j].word = stren[j];
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
                Debug.Log("1回目");
                collectObject.transform.localPosition = new Vector2(collectObject.transform.localPosition.x - 155, collectObject.transform.localPosition.y);
                break;
            case 1:
                count--;
                collectObject.transform.localPosition = new Vector2(collectObject.transform.localPosition.x + 155, collectObject.transform.localPosition.y);
                break;
        }
    }
}