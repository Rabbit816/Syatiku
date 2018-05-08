using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackMain : MonoBehaviour {

    [SerializeField,Tooltip("時間制限初期値")]
    private float timer = 30.0f;

    [SerializeField]
    private Text time;

    bool _themeActive = false;

    [SerializeField]
    private string[] _chipped;

    [SerializeField]
    private GameObject theme_obj;

    [HideInInspector]       //publicで宣言してもInspectorからは見えない
    public int i = 10;

	// Use this for initialization
	void Start () {
        _themeActive = false;
        Theme();
	}
	
	// Update is called once per frame
	void Update () {
        if(_themeActive)
            if (Timer())
                Timer();
            else
                Scene();
    }

    /// <summary>
    /// 時間制限の処理
    /// </summary>
    /// <returns></returns>
    private bool Timer()
    {
        timer -= Time.deltaTime;
        time.text = "Timer: " + timer.ToString("f1");
        bool _time = timer <= 0 ? true : false;
        return _time;
    }

    /// <summary>
    /// お題の処理
    /// </summary>
    public void Theme()
    {
        //お題の内容ランダムで選択し出現（移動）
        int rand_theme = Random.Range(0, _chipped.Length-1);
        Debug.Log("お題 : " + rand_theme + ":" + _chipped[rand_theme]);
        theme_obj.GetComponentInChildren<Text>(true).text = _chipped[rand_theme].ToString();
        Animation anim = theme_obj.GetComponent<Animation>();
        anim.Play();
        if (!anim.isPlaying)
            _themeActive = true;

        //出現中何もできないようにする

    }

    /// <summary>
    /// あとでわっしーがシーン移動関数を作るのでそれを使う
    /// </summary>
    void Scene() { }
}
