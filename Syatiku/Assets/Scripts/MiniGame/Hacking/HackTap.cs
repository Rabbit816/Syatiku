using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private HashSet<int> rand_list = new HashSet<int>();
    private int rand;

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
        _placeAnim = false;
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
            place_list[placeNum].word.ToString();
            GameObject _prefab = Instantiate(Prefab);
            _prefab.transform.SetParent(place[placeNum].transform, false);  // SetParent(親の場所,大きさを変えるか)
            _placeAnim = true;
        }
        place_current = placeNum;
    }

    /// <summary>
    /// ランダムで単語を選ぶ処理
    /// </summary>
    /// <returns></returns>
    private string HackRandom() {
        if (rand_list.Count > str.Length)
            rand_list.Clear();
        //randが重複したら回す
        while (!rand_list.Add(rand))
        {
            rand = Random.Range(0, str.Length);
            rand_list.Add(rand);
            Debug.Log("rand:" + rand);
        }
        return str[rand];
    }

    /// <summary>
    /// 各場所に単語を入れる
    /// </summary>
    private void AddPlaceWord()
    {
        for (int i=0; i<place.Length; i++)
        {
            place_list[i].word = HackRandom();
            place_list[i].pos = place[i];
        }
    }
}
