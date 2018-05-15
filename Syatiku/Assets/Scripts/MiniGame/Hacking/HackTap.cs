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
    private System.Random rand;

    //現在の場所
    private int place_current;

    //単語が表示されているかどうか
    private bool _placeAnim = false;

    [SerializeField,Tooltip("単語が表示される時間")]
    private float t = 5.0f;

    [SerializeField,Tooltip("出す単語画像")]
    private GameObject Prefab;
    
	// Use this for initialization
	void Start () {
        place_list = new PlaceList[5];
        _placeAnim = false;
        rand = new System.Random();
        AddPlaceWord();
	}
	
	// Update is called once per frame
	void Update () {
        if (_placeAnim)
        {
            t -= Time.deltaTime;
            if (t <= 0.0f)
            {
                place[place_current].transform.GetChild(0).gameObject.SetActive(false);
                _placeAnim = false;
            }
        }
	}

    /// <summary>
    /// タップしたところから単語が出てくる処理
    /// </summary>
    /// <param name="placeNum">どの場所かを指定</param>
    public void PlaceButton(int placeNum){
        // 一回もタップされてなかったら
        if(place[placeNum].transform.childCount == 0)
        {
            GameObject _prefab = Instantiate(Prefab);
            _prefab.transform.SetParent(place[placeNum].transform, false);  // SetParent(親の場所,大きさを変えるか)
            place[placeNum].transform.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
            _placeAnim = true;
        }
        place_current = placeNum;
    }

    /// <summary>
    /// ランダムで単語を選ぶ処理
    /// </summary>
    /// <returns></returns>
    private string HackRandom()
    {
        if (rand_list.Count > str.Length)
            rand_list.Clear();

        int int_rand = 0;
        HashSet<int> hashset = new HashSet<int>();
        //foreach(var i in hashset[1])
        //{
        //    while (!i(int_rand))
        //    {
        //        int_rand = rand.Next(0, str.Length - 1);
        //        hashset.Add(int_rand);
        //        Debug.Log("きてるよrand:" + int_rand);
        //    }
        //    rand_list.Add(int_rand);
        //}
        return /*rand_strList.Add(str[int_rand])*/"";
    }

    /// <summary>
    /// 各場所に単語を入れる
    /// </summary>
    private void AddPlaceWord()
    {
        int count = 0;
        //randが重複したら回す

        //var a = new System.Random();
        foreach (var i in HackRandom())
        {
            place_list[count].pos = place[count];
            place_list[count].word = i.ToString();
            count++;
        }
        //for (int i = 0; i < place.Length - 1; i++)
        //{
        //    place_list[i].pos = place[i];
        //    place_list[i].word = HackRandom();
        //    Debug.Log(i + "週目 place_list.pos: " + place_list[i].pos);
        //    Debug.Log(i + "週目 place_list.word: " + place_list[i].word);
        //}
    }
}