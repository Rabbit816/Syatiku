using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackMain : MonoBehaviour {


    [SerializeField,Tooltip("時間制限初期値")]
    private float timer = 30.0f;

    [SerializeField,Tooltip("時間オブジェクト")]
    private Text time;

    //お題のアニメーションが終わったかどうか
    bool _themeActive = false;

    [SerializeField,Tooltip("お題の名前")]
    private string[] _chipped;

    [SerializeField,Tooltip("お題のオブジェクト")]
    private GameObject theme_obj;

    private char spliter = ',';
    private string[] res;

    private string current;
    

    // Use this for initialization
    void Start () {
        //ReadText();
        _themeActive = false;
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
    /// お題の処理
    /// </summary>
    public void Theme()
    {
        //お題の内容ランダムで選択し出現（移動）
        int rand_theme = Random.Range(0, _chipped.Length-1);

        theme_obj.GetComponentInChildren<Text>(true).text = _chipped[rand_theme].ToString();
        Animation anim = theme_obj.GetComponent<Animation>();
        anim.Play();
        if (anim.Play())
            _themeActive = false;
        else
            _themeActive = true;

        //出現中何もできないようにする

    }
}
