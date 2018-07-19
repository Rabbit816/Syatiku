using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField, Tooltip("画面をタップできないように遮るObject")]
    private GameObject Dont_Tap;
    [SerializeField, Tooltip("カメラ")]
    private Camera cam;

    private string str_quest;
    private string str_answer;

    [HideInInspector]
    public List<string> Quest_list = new List<string>();
    [HideInInspector]
    public List<string> Answer_list = new List<string>();

    private IntoPCAction into_pc;
    private PatteringEvent patte;
    private HackMeishi hack_meishi;
    private HackBoss hack_boss;

    private bool _allClear = false;
    [HideInInspector]
    public bool _timerActive = false;

    // Use this for initialization
    void Start () {
        into_pc = GetComponent<IntoPCAction>();
        patte = GetComponent<PatteringEvent>();
        hack_boss = GetComponent<HackBoss>();
        Dont_Tap.SetActive(true);
        ReadText();
        Theme();
        _allClear = false;
        _timerActive = false;
        CameraAction();
	}

	// Update is called once per frame
	void Update () {
        StartCoroutine(StartedTimer());
        if (into_pc._compariClear && patte._lowAnimClear)
        {
            if (!_allClear)
            {
                _allClear = true;
                Common.Instance.clearFlag[Common.Instance.isClear] = true;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
        }
    }

    private IEnumerator StartedTimer()
    {
        yield return new WaitForSeconds(1.8f);
        Timer();
    }

    private void CameraAction()
    {
        RectTransform cam_rect = cam.GetComponent<RectTransform>();
        Sequence seq = DOTween.Sequence();
        cam.orthographicSize = 1;
        cam.DOOrthoSize(1f, 1f);
        //seq.Append(cam.transform.DOLocalMove(cam.transform.forward * -10, 1f));
        //seq.Append(cam.transform.DOLocalMove(cam.transform.forward * 10, 0.8f));
    }
    /// <summary>
    /// time秒数内までどのボタンも押せなくする
    /// </summary>
    /// <param name="time">待ち時間</param>
    /// <returns></returns>
    private IEnumerator Wait_Time(float time)
    {
        yield return new WaitForSeconds(time);
        Dont_Tap.SetActive(false);
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
            time.text = "Timer: 0.0";
            Common.Instance.clearFlag[Common.Instance.isClear] = false;
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
        
        StartCoroutine(Wait_Time(1.8f));
    }
}