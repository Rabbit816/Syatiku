using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HackMain : MonoBehaviour {
    
    [SerializeField,Tooltip("時間制限初期値")]
    public float timer = 30.0f;
    [SerializeField,Tooltip("時間Text")]
    private Text time;
    [SerializeField,Tooltip("お題の名前")]
    private string[] _chipped;
    [SerializeField,Tooltip("お題のオブジェクト")]
    private GameObject theme_obj;
<<<<<<< HEAD

    private char spliter = ',';
    private string[] res;

    private string current;
    private int line = 0;
    [HideInInspector]
    public List<string[]> str_list = new List<string[]>();

    // Use this for initialization
    void Start () {
        //ReadText();
        _themeActive = false;
=======
    [SerializeField, Tooltip("チェックボックスとくっついてるやつ")]
    private GameObject Black_back;
    //[SerializeField, Tooltip("EventSystem")]
    public EventSystem event_system;
    private GameObject collectObject;

    private string str_quest;
    private string str_answer;
    private string str_folder_ans;

    [HideInInspector]
    public List<string> Quest_list = new List<string>();
    [HideInInspector]
    public List<string> Answer_list = new List<string>();
    [HideInInspector]
    public List<string> Folder_ans_list = new List<string>();

    private IntoPCAction into_pc;
    private PatteringEvent patte;
    private HackMeishi hack_meishi;
    private HackBoss hack_boss;
    private HackTap hack_tap;
    [HideInInspector]
    public EventSystem es;

    private bool _allClear = false;
    [HideInInspector]
    public bool _timerActive = false;
    private bool _overTime = false;
    private bool _confirmActive = false;
    private bool _start_ = false;

    private float _time = 0f;

    // Use this for initialization
    void Start () {
        into_pc = GetComponent<IntoPCAction>();
        patte = GetComponent<PatteringEvent>();
        hack_boss = GetComponent<HackBoss>();
        hack_tap = GetComponent<HackTap>();

        collectObject = GameObject.Find("Canvas/Check/GetWord");
        es = EventSystem.current;
        es.enabled = false;
        ReadText();
>>>>>>> master
        Theme();
        StartCoroutine(StartedTimer());

        _confirmActive = false;
        _allClear = false;
        _timerActive = false;
        _overTime = false;
        SoundManager.Instance.PlayBGM(BGMName.Hack);
	}

	// Update is called once per frame
	void Update () {
        if (_start_)
        {
            _time += Time.deltaTime;
            if(_time >= 1f)
            {
                _time = 0f;
            }
        }
        if (into_pc._compariClear) //&& patte._lowAnimClear && patte._speedyAnimClear)
        {
            if (!_allClear)
            {
                _allClear = true;
                Common.Instance.clearFlag[Common.Instance.miniNum] = true;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
        }
    }

    /// <summary>
    /// スタート時少し待つ処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartedTimer()
    {
        yield return new WaitForSeconds(5.25f);
        hack_tap.PlaceButton(10);
        yield return new WaitForSeconds(0.2f);
        es.enabled = true;
        Timer();
    }

    /// <summary>
    /// 時間制限の処理
    /// </summary>
    /// <returns></returns>
    private bool Timer()
    {
        if (!patte._PatteringPlay && !hack_boss._choosing)
        {
            _timerActive = true;
            timer -= Time.deltaTime;
            time.text = "Timer: " + timer.ToString("f1");
        }
        else
            _timerActive = false;
        
        if(timer < 0f)
        {
<<<<<<< HEAD
            time.text = "Timer: 0.0";
            //ChangeScene(SceneName.Title);
=======
            if (!_overTime)
            {
                _overTime = true;
                time.text = "Timer: 0.0";
                Common.Instance.clearFlag[Common.Instance.miniNum] = false;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
>>>>>>> master
        }
        return (timer < 0f);
    }

    private void ReadText()
    {
<<<<<<< HEAD
        TextAsset csvfile = Resources.Load("Hacking/Quest") as TextAsset;
        System.IO.StringReader stren = new System.IO.StringReader(csvfile.text);
        Debug.Log("text: " + csvfile.ToString());
        // 表示
        while (stren.Peek() > -1)
        {
            string str = stren.ReadLine();
            str_list.Add(str.Split(',')); // リストに入れる

            line++; // 行数加算
=======
        TextAsset csvfile_quest = Resources.Load("Minigame/Hacking/Quest") as TextAsset;
        TextAsset csvfile_answer = Resources.Load("Minigame/Hacking/Answer") as TextAsset;
        TextAsset csvfile_folder_ans = Resources.Load("Minigame/Hacking/Folder_answer") as TextAsset;
        System.IO.StringReader stren_quest = new System.IO.StringReader(csvfile_quest.text);
        System.IO.StringReader stren_answer = new System.IO.StringReader(csvfile_answer.text);
        System.IO.StringReader stren_folder_ans = new System.IO.StringReader(csvfile_folder_ans.text);

        while (stren_quest.Peek() > -1)
        {
            str_quest = stren_quest.ReadLine();
            str_answer = stren_answer.ReadLine();
            string[] s_a = str_answer.Split(',');
            string[] s_q = str_quest.Split(',');

            for (int i=0; i< s_a.Length; i++)
            {
                Answer_list.Add(s_a[i]);
                Quest_list.Add(s_q[i]);
            }
        }
        while (stren_folder_ans.Peek() > -1)
        {
            str_folder_ans = stren_folder_ans.ReadLine();
            string[] s_f = str_folder_ans.Split(',');
            for (int i = 0; i < s_f.Length; i++)
            {
                Folder_ans_list.Add(s_f[i]);
            }
>>>>>>> master
        }
    }
    /// <summary>
    /// お題の処理
    /// </summary>
    private void Theme()
    {
        int rand_theme = Random.Range(0, _chipped.Length-1);
        theme_obj.GetComponentInChildren<Text>(true).text = _chipped[rand_theme].ToString();
    }

    /// <summary>
    /// 集めた単語を確認するUIの処理
    /// </summary>
    public void CollectWordsOpen()
    {
        switch (_confirmActive)
        {
            case true:
                collectObject.GetComponent<RectTransform>().DOLocalMoveX(215, 0.2f);
                Black_back.GetComponent<RectTransform>().DOLocalMoveX(1175, 0.2f);
                _confirmActive = false;
                break;
            case false:
                collectObject.GetComponent<RectTransform>().DOLocalMoveX(-195, 0.2f);
                Black_back.GetComponent<RectTransform>().DOLocalMoveX(-1154, 0.2f);
                _confirmActive = true;
                break;
        }
    }
}