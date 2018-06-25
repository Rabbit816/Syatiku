using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private HackMain hack_main;
    private int count = 0;
    private GameObject DoorSide;
    private int GakuCount = 0;
    private GameObject Gakubuti;
    private GameObject Zoom;

    // Use this for initialization
    void Start () {
        collectObject = GameObject.Find("Canvas/Check/GetWord");
        CollectedWord = GameObject.Find("Canvas/PC/PassWordFase/Collect");
        GetWord = GameObject.Find("Canvas/Check/GetWord");
        DoorSide = GameObject.Find("Canvas/DoorSide");
        Gakubuti = GameObject.Find("Canvas/Desk/Gakubuti");
        Zoom = GameObject.Find("Canvas/Zoom");
        
        Common.Instance.Shuffle(pos_list);
        count = 0;
        GakuCount = 0;
        hack_main = GetComponent<HackMain>();
        place_list = new PlaceList[place.Length];
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
                // 一回もタップされてなかったら
                // PC内とリスト内とその場所に表示
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
            case 7:
                IntoPC.transform.localPosition = new Vector2(0, 0);
                break;
            case 8:
                IntoPC.transform.localPosition = new Vector2(0, -500);
                break;
            case 9:
                DoorSide.transform.localPosition = new Vector2(-800, 0);
                break;
            case 10:
                DoorSide.transform.localPosition = new Vector2(0, 0);
                break;
            case 11:
                GakuCount++;
                if (GakuCount == 7)
                    Gakubuti.GetComponent<Animator>().Play(0);
                Debug.Log("GakuCount: " + GakuCount);
                break;
            case 12:
                Zoom.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 13:
                Zoom.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 14:
                Zoom.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 15:
                Zoom.transform.GetChild(1).gameObject.SetActive(false);
                break;
            case 16:
                Zoom.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 17:
                Zoom.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 18:
                Zoom.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 19:
                Zoom.transform.GetChild(3).gameObject.SetActive(false);
                break;
            case 20:
                Zoom.transform.GetChild(4).gameObject.SetActive(true);
                break;
            case 21:
                Zoom.transform.GetChild(4).gameObject.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// 各場所に単語を入れる
    /// </summary>
    private void AddPlaceWord()
    {
        string[] stren = hack_main.Quest_list.ToArray();

        for (int j = 0; j < place.Length; j++)
        {
            place_list[j].pos = place[j];
            if (j >= place.Length - (place.Length - pos_list.Length))
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