using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackMain : MonoBehaviour {


    [SerializeField,Tooltip("時間制限初期値")]
    private float timer = 30.0f;

    [SerializeField,Tooltip("時間オブジェクト")]
    private Text time;

    [SerializeField,Tooltip("お題の名前")]
    private string[] _chipped;

    [SerializeField,Tooltip("お題のオブジェクト")]
    private GameObject theme_obj;

    private string current;
    private int Maxline = 0;

    private string str_quest;
    private string str_answer;

    [HideInInspector]
    public List<string> Quest_list = new List<string>();
    [HideInInspector]
    public List<string> Answer_list = new List<string>();


    // Use this for initialization
    void Start () {
        ReadText();
        Maxline = 0;
        Theme();
        timeout = true;
	}

    private bool timeout = false;
	// Update is called once per frame
	void Update () {
        //if(_themeActive)
        //    if (Timer())
        //        Timer();

        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = 0;
            if (timeout)
            {
                Common.gameClear = false;
                Common.Instance.ChangeScene(Common.SceneName.Result);
                timeout = false;
            }
        }
        time.text = "Timer: " + timer.ToString("f1");
    }

    /// <summary>
    /// 時間制限の処理
    /// </summary>
    /// <returns></returns>
    private bool Timer()
    {
        timer -= Time.deltaTime;
        time.text = "Timer: " + timer.ToString("f1");
        if(timer < 0f)
        {
            time.text = "Timer: 0.0";
            Common.Instance.ChangeScene(Common.SceneName.Result);
        }
        return (timer < 0f);
    }

    /// <summary>
    /// 文章、単語読み込み
    /// </summary>
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
            string[] s_a = str_answer.Split(',');
            string[] s_q = str_quest.Split(',');
            
            for (int i=0; i< s_a.Length; i++)
            {
                Answer_list.Add(s_a[i]);
            }
            for (int i = 0; i < s_q.Length; i++)
            {
                Quest_list.Add(s_q[i]);
            }

            //Answer_list.Add(s_a[Maxline]);
            //Quest_list.Add(s_q[Maxline]);
            Debug.Log("str_answer:" + Quest_list[1].ToString());

            Maxline++; // 行数加算
        }
    }

    /// <summary>
    /// お題の処理
    /// </summary>
    public void Theme()
    {
        //お題の内容ランダムで選択し出現（移動）
        int rand_theme = Random.Range(0, _chipped.Length-1);

        theme_obj.GetComponentInChildren<Text>(true).text = _chipped[rand_theme].ToString();
        Animator anim = theme_obj.GetComponent<Animator>();
        anim.Play("ThemeAnimation");

        //出現中何もできないようにする

    }
}
