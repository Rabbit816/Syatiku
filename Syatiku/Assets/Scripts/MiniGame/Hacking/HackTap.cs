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

    [SerializeField,Tooltip("出す単語画像")]
    private GameObject Prefab;

    [SerializeField, Tooltip("集めた単語")]
    private GameObject CollectedPrefab;
    private GameObject CollectedWord;
    private Collider2D collected_position;

    // Use this for initialization
    void Start () {
        CollectedWord = GameObject.Find("Canvas/IntoPC/CollectedWord");
        collected_position = CollectedWord.GetComponent<Collider2D>();
        place_list = new PlaceList[place.Length];
        _placeAnim = false;
        AddPlaceWord();
	}
	
	// Update is called once per frame
	void Update () {
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

    /// <summary>
    /// タップしたところから単語が出てくる処理
    /// </summary>
    /// <param name="placeNum">どの場所かを指定</param>
    public void PlaceButton(int placeNum){
        //PC画面内を表示
        //戻るボタンで画面外に移動
        if (placeNum == 6) {
            IntoPC.transform.position = new Vector2(0, 1);
        }else if (placeNum == 7)
        {
            IntoPC.transform.position = new Vector2(0, 15);
        }
        // 一回もタップされてなかったら
        else if(place[placeNum].transform.childCount == 0)
        {
            GameObject _prefab = Instantiate(Prefab);
            _prefab.transform.SetParent(place[placeNum].transform, false);  // SetParent(親の場所,大きさを変えるか)
            place[placeNum].transform.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
            _placeAnim = true;

            GameObject _collected_word = Instantiate(CollectedPrefab);
            _collected_word.transform.SetParent(CollectedWord.transform, false);
        }
        place_current = placeNum;
    }

    /// <summary>
    /// 各場所に単語を入れる
    /// </summary>
    private void AddPlaceWord()
    {
        Shuffle(str);
        for (int j = 0; j < place.Length - 1; j++)
        {
            place_list[j].pos = place[j];
            place_list[j].word = str[j];
            Debug.Log(j + "週目 place_list.pos: " + place_list[j].pos);
            Debug.Log(j + "週目 place_list.word: " + place_list[j].word);
        }
    }

    /// <summary>
    /// 仮シャッフル（あとでわっしーに共通スクリプトに書いてもらう）
    /// </summary>
    /// <param name="s">シャッフルしたい配列</param>
    /// <returns></returns>
    private string[] Shuffle(string[] s)
    {
        int length = s.Length;
        string[] s_result = new string[length];
        var rand = new System.Random();
        int n = length;
        while (1 < n)
        {
            n--;
            int rand_Num = rand.Next(n + 1);
            var tmp = s_result[rand_Num];
            s_result[rand_Num] = s_result[n];
            s_result[n] = tmp;
        }
        return s_result;
    }
}