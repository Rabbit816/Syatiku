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

    //[SerializeField, Tooltip("全部の発見できる単語")]
    private string[] str;

    private GameObject collectObject;
    
    [SerializeField]
    private GameObject IntoPC;

    [SerializeField,Tooltip("出現する単語")]
    private GameObject AppearPrefab;

    [SerializeField, Tooltip("集めた単語(PC内に出すObject)")]
    private GameObject CollectedPrefab;
    private GameObject CollectedWord;
    private Collider2D collected_position;
    [SerializeField, Tooltip("集めた単語(リスト内に出すObject)")]
    private GameObject GetWordPrefab;
    private GameObject GetWord;

    [SerializeField, Tooltip("単語を取得できるボタンの場所")]
    private GameObject[] Getting_position;

    [SerializeField, Tooltip("PC内のposition")]
    private GameObject[] pos_list;

    [SerializeField, Tooltip("資料Object")]
    private GameObject Document;

    private HackMain hack_main;
    private int count = 0;
    private GameObject DoorSide;
    private int GakuCount = 0;
    [SerializeField, Tooltip("額縁Object")]
    private RectTransform Gakubuti;
    private GameObject Zoom;
    private IntoPCAction intopc_action;
    //比較する資料を取得したかどうか
    [HideInInspector]
    public bool _getDocument = false;

    // Use this for initialization
    void Start () {
        collectObject = GameObject.Find("Canvas/Check/GetWord");
        CollectedWord = GameObject.Find("Canvas/PC/PassWordFase/Collect");
        GetWord = GameObject.Find("Canvas/Check/GetWord");
        DoorSide = GameObject.Find("Canvas/DoorSide");
        Zoom = GameObject.Find("Canvas/Zoom");
        intopc_action = GetComponent<IntoPCAction>();

        Document.SetActive(false);
        Common.Instance.Shuffle(pos_list);
        count = 0;
        GakuCount = 0;
        hack_main = GetComponent<HackMain>();
        place_list = new PlaceList[Getting_position.Length];
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

                    GameObject _collected_word = Instantiate(CollectedPrefab, CollectedWord.transform);
                    _collected_word.transform.position = pos_list[placeNum].transform.position;
                    _collected_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();

                    GameObject _get_word = Instantiate(GetWordPrefab, GetWord.transform);
                    _get_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
                }
                break;
            case 10:
                IntoPC.transform.localPosition = new Vector2(0, 0);
                break;
            case 11:
                IntoPC.transform.localPosition = new Vector2(0, -500);
                break;
            case 12:
                DoorSide.transform.localPosition = new Vector2(-800, 0);
                break;
            case 13:
                DoorSide.transform.localPosition = new Vector2(0, 0);
                break;
            case 14:
                GakuCount++;
                Sequence seq = DOTween.Sequence();
                Gakubuti.DOShakeRotation(1f);
                if (GakuCount == 7)
                {
                    seq.Append(Gakubuti.DOLocalMoveY(-122, 1.0f));
                }
                break;
            //case 15:
            //case 16:
            //case 17:
            //case 18:
            //case 19:
            //case 20:
            //case 21:
            //case 22:
            //case 23:
            //case 24:
            //    if (placeNum % 2 == 0)
            //        selectNum = placeNum % 16;
            //    else
            //        selectNum = placeNum % 15;
            //    Debug.Log("selectNum: " + selectNum);
            //    if (Zoom.transform.GetChild(placeNum - selectNum).gameObject.activeSelf)
            //        Zoom.transform.GetChild(placeNum - selectNum).gameObject.SetActive(false);
            //    else
            //        Zoom.transform.GetChild(placeNum - selectNum).gameObject.SetActive(true);
            //    break;
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
                _getDocument = true;
                Document.SetActive(true);
                break;
            case 26:
                intopc_action.DocumentsComparison();
                break;
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
                return;
            else
                place_list[j].word = stren[j];
        }
    }

    public void CollectWordsOpen()
    {
        switch (count)
        {
            case 0:
                count++;
                Debug.Log("1回目");
                collectObject.transform.localPosition = new Vector2(collectObject.transform.localPosition.x - 160, collectObject.transform.localPosition.y);
                break;
            case 1:
                count--;
                collectObject.transform.localPosition = new Vector2(collectObject.transform.localPosition.x + 160, collectObject.transform.localPosition.y);
                break;
        }
    }
}