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
            //ChangeScene(SceneName.Title);
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
        Debug.Log("お題 : " + rand_theme + ":" + _chipped[rand_theme]);

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
