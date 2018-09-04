using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HackGetWord : MonoBehaviour {

    //-------------------引き出しで使う変数---------------------------------------
    //private struct FolderPlaceList
    //{
    //    public GameObject pos;
    //    public string word;
    //};

    //private FolderPlaceList[] folder_place_list = new FolderPlaceList[]
    //{
    //    new FolderPlaceList(){ pos=null, word = "" },
    //};
    public int AA = 10;

    [Tooltip("集めた単語(Folder内に出すObject)")]
    private GameObject CollectFolderPrefab;
    [Tooltip("集めた単語(リスト内に出すObject)")]
    private GameObject GetWordFolderPrefab;
    //-----------------------------------------------------------------------------

    //-------------------棚で使う変数----------------------------------------------
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

    [Tooltip("集めた単語(リスト内に出すObject)")]
    private GameObject GetWordPrefab;
    private GameObject GetWord;

    [Tooltip("集めた単語(PC内に出すObject)")]
    private GameObject CollectedPrefab;
    [Tooltip("集めた単語(PC内に出す場所)")]
    private GameObject CollectedWord;
    //-----------------------------------------------------------------------------

    private HackMain hack_main;
    private HackTap hack_tap;
    private GameObject check_img;

    private void Awake()
    {
        try
        {
            hack_main = GameObject.Find("controll").GetComponent<HackMain>();
            hack_tap = GameObject.Find("controll").GetComponent<HackTap>();
            GetWord = GameObject.Find("Canvas/Check/GetWord");
            check_img = GameObject.Find("Canvas/Check/Image");
            CollectedWord = GameObject.Find("Canvas/PC/PassWordFase/Collect");
            CollectedPrefab = Resources.Load("Prefabs/MiniGame/Hacking/str") as GameObject;
            GetWordPrefab = Resources.Load("Prefabs/MiniGame/Hacking/WordImage") as GameObject;
            GetWordFolderPrefab = Resources.Load("Prefabs/MiniGame/Hacking/folder_str") as GameObject;
            CollectFolderPrefab = Resources.Load("Prefabs/MiniGame/Hacking/FolderWordImage") as GameObject;
        }
        catch
        {
            Debug.Log("Not Find");
        }
    }

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 棚ver 単語のタップした時の処理
    /// </summary>
    /// <param name="placeNum"></param>
    public void SearchTap(int placeNum)
    {
        //押したらアニメーション
        Text appearChild_text = gameObject.transform.GetChild(0).GetComponent<Text>();
        GetWordAnim(gameObject);
        DOTween.ToAlpha(
            () => appearChild_text.color,
            color => appearChild_text.color = color,
            0f, 2.0f);

        //PC内に集めた単語を表示
        GameObject _collected_word = Instantiate(CollectedPrefab, CollectedWord.transform);
        _collected_word.transform.position = hack_tap.pos_list[placeNum].transform.position;
        _collected_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();

        //集めたものリストの中に単語を表示
        GameObject _get_word = Instantiate(GetWordPrefab, GetWord.transform);
        _get_word.transform.SetAsFirstSibling();
        _get_word.GetComponentInChildren<Text>().text = place_list[placeNum].word.ToString();
    }

    /// <summary>
    /// Drawer ver 単語のタップした時の処理
    /// </summary>
    /// <param name="place"></param>
    public void DrawerTap(int place)
    {
        GetWordAnim(gameObject);
        Text appearChild_text = gameObject.transform.GetChild(0).GetComponent<Text>();
        DOTween.ToAlpha(
            () => appearChild_text.color,
            color => appearChild_text.color = color,
            0f, 2.0f);

        //PC内に集めた単語を表示
        GameObject _collected_word = Instantiate(CollectFolderPrefab, hack_tap.CollectWordFolder.transform);
        _collected_word.transform.position = hack_tap.folder_pos_list[place].transform.position;
        _collected_word.GetComponentInChildren<Text>().text = hack_tap.folder_pos_list[place].ToString();

        //集めたものリストの中に単語を表示
        GameObject _get_word = Instantiate(GetWordFolderPrefab, GetWord.transform);
        _get_word.transform.SetAsFirstSibling();
        _get_word.GetComponentInChildren<Text>().text = hack_tap.folder_pos_list[place].ToString();
    }

    /// <summary>
    /// 文字取得時のDOToweenアニメーション処理
    /// </summary>
    /// <param name="obj">動かすオブジェクト</param>
    public void GetWordAnim(GameObject obj)
    {
        Sequence seq = DOTween.Sequence();
        Image obj_img = obj.GetComponent<Image>();
        RectTransform obj_rect = obj.GetComponent<RectTransform>();
        seq.Append(obj_rect.DOMove(check_img.transform.position, 1.3f).SetEase(Ease.Linear))
            .Join(obj_rect.DOScale(new Vector2(0.5f, 0.5f), 1.3f))
            .Join((
                DOTween.ToAlpha(
                () => obj_img.color,
                color => obj_img.color = color,
                0f, 1.6f)));
    }

    private void AddWord()
    {

    }
}
