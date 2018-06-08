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

    [SerializeField, Tooltip("全部の発見できる単語")]
    private string[] str;

    private GameObject collectObject;
    private List<int> rand_list = new List<int>();
    private List<string> rand_strList = new List<string>();

    //現在の場所
    private int place_current;
    [SerializeField]
    private GameObject IntoPC;

    //単語が表示されているかどうか
    private bool _placeAnim = false;

    [SerializeField,Tooltip("単語が表示される時間")]
    private float t = 5.0f;

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

    [HideInInspector]
    public List<string[]> Quest_list = new List<string[]>();
    [HideInInspector]
    public List<string[]> Answer_list = new List<string[]>();

    private float getWord_Num = 0.0f;
    private HackPC hack_pc;
    private int old_counter = 0;
    private int count = 0;
    private int Maxline = 0;
    private int currentline = 0;

    private string str_quest;
    private string str_answer;

    // Use this for initialization
    void Start () {
        currentline = 0;
        Maxline = 0;
        collectObject = GameObject.Find("Canvas/Check");
        CollectedWord = GameObject.Find("Canvas/IntoPC/CollectedWord");
        GetWord = GameObject.Find("Canvas/Check/GetWord");
        ReadText();
        Common.Instance.Shuffle(pos_list);
        getWord_Num = 0;
        count = 0;
        hack_pc = GetComponent<HackPC>();
        place_list = new PlaceList[place.Length];
        _placeAnim = false;
        AddPlaceWord();
	}
	
	// Update is called once per frame
	void Update () {
        //if (hack_pc.counter != old_counter)
        //    AddPlaceWord();
        //if (_placeAnim)
        //{
        //    t -= Time.deltaTime;
        //    if (t <= 0.0f)
        //    {
        //        place[place_current].transform.GetChild(0).gameObject.SetActive(false);
        //        _placeAnim = false;
        //    }
        //}
	}

    private void ReadText()
    {
        TextAsset csvfile_quest = Resources.Load("Minigame/Hacking/Quest") as TextAsset;
        TextAsset csvfile_answer = Resources.Load("Minigame/Hacking/Answer") as TextAsset;
        System.IO.StringReader stren_quest = new System.IO.StringReader(csvfile_quest.text);
        System.IO.StringReader stren_answer = new System.IO.StringReader(csvfile_answer.text);
        Debug.Log("text: " + csvfile_quest.ToString());
        // 表示
        while (stren_quest.Peek() > -1)
        {
            str_quest = stren_quest.ReadLine();
            str_answer = stren_answer.ReadLine();
            Answer_list.Add(str_answer.Split(','));
            Quest_list.Add(str_quest.Split(','));
            Debug.Log("str_answer:" + Quest_list);

            Maxline++; // 行数加算
        }
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
            GameObject _prefab = Instantiate(AppearPrefab, place[placeNum].transform);
            place[placeNum].transform.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
            _placeAnim = true;

            GameObject _collected_word = Instantiate(CollectedPrefab, CollectedWord.transform);
            _collected_word.transform.position = pos_list[placeNum].transform.position;
            _collected_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();

            GameObject _get_word = Instantiate(GetWordPrefab,GetWord.transform);
            _get_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
        }
        place_current = placeNum;
    }

    /// <summary>
    /// 各場所に単語を入れる
    /// </summary>
    private void AddPlaceWord()
    {
        string[][] stren = Quest_list.ToArray();
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