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

    [SerializeField, Tooltip("PC内のposition")]
    private GameObject[] pos_list;

    private HackMain hack_main;
    private int count = 0;
    

    // Use this for initialization
    void Start () {
        collectObject = GameObject.Find("Canvas/Check/GetWord");
        CollectedWord = GameObject.Find("Canvas/PC/Collect");
        GetWord = GameObject.Find("Canvas/Check/GetWord");
        Common.Instance.Shuffle(pos_list);
        count = 0;
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
        if (placeNum == 6) {
            IntoPC.transform.localPosition = new Vector2(0, -23);
        }else if (placeNum == 7)
        {
            IntoPC.transform.localPosition = new Vector2(0, 535);
        }
        // 一回もタップされてなかったら
        // PC内とリスト内とその場所に表示
        else if(place[placeNum].transform.childCount == 0)
        {
            Instantiate(AppearPrefab, place[placeNum].transform);
            place[placeNum].transform.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();

            GameObject _collected_word = Instantiate(CollectedPrefab, CollectedWord.transform);
            _collected_word.transform.position = pos_list[placeNum].transform.position;
            _collected_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();

            GameObject _get_word = Instantiate(GetWordPrefab,GetWord.transform);
            _get_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
        }
    }

    /// <summary>
    /// 各場所に単語を入れる
    /// </summary>
    private void AddPlaceWord()
    {
        string[][] stren = hack_main.Quest_list.ToArray();
        //Common.Instance.Shuffle(stren);
        for (int j = 0; j < place.Length; j++)
        {
            place_list[j].pos = place[j];
            place_list[j].word = stren[1][j];
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