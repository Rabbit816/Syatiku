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
    [SerializeField, Tooltip("画面をタップできないように遮るObject")]
    private GameObject Dont_Tap;

    private string current;
    private int Maxline = 0;

    [HideInInspector]
    public int comingCount = 0;

    private string str_quest;
    private string str_answer;

    [HideInInspector]
    public List<string> Quest_list = new List<string>();
    [HideInInspector]
    public List<string> Answer_list = new List<string>();

    private bool timeout = false;

    // Use this for initialization
    void Start () {
        Dont_Tap.SetActive(true);
        ReadText();
        Maxline = 0;
        comingCount = 0;
        Theme();
        timeout = true;
	}

	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = 0;
            if (timeout)
            {
                Common.Instance.clearFlag[Common.Instance.isClear] = true;
                Common.Instance.ChangeScene(Common.SceneName.Result);
                timeout = false;
            }
        }
        time.text = "Timer: " + timer.ToString("f1");
    }

    private IEnumerator Wait_Time(float time)
    {

        yield return new WaitForSeconds(time);
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
            Maxline++; // 行数加算
        }
    }

    /// <summary>
    /// お題の処理
    /// </summary>
    private void Theme()
    {
        int rand_theme = Random.Range(0, _chipped.Length-1);

        theme_obj.GetComponentInChildren<Text>(true).text = _chipped[rand_theme].ToString();
        Animator anim = theme_obj.GetComponent<Animator>();
        anim.Play("ThemeAnimation");
        
        StartCoroutine(Wait_Time(10f));
        Dont_Tap.SetActive(false);
    }
}
