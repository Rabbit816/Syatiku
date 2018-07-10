using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 汎用スクリプト(Common.Instance.関数、変数名)
/// </summary>
public class Common : MonoBehaviour {

    /// <summary>
    /// シーン名
    /// </summary>
    public enum SceneName
    {
        Title = 0,
        Scenario,
        Action,
        Smoking,
        Hacking,
        Drinking,
        Boss,
        Result,
    }

    /// <summary>
    /// ミニゲームで手に入る資料
    /// </summary>
    [System.NonSerialized]
    public bool[] dataFlag =
    {
        false,
        false,
        false,
        false,
        false,
    };

    // ミニゲームクリアフラグ
    [System.NonSerialized]
    public bool[] clearFlag = 
    {
        false, // hack
        false, // drink
        false  // smoke
    };

    /// <summary>
    /// 何のミニゲームやったか(0:hack,1:drink,2:smoke)
    /// </summary>
    [System.NonSerialized]
    public int isClear; 

    [System.NonSerialized]
    public int gameMode; // シナリオがどちらか

    [SerializeField,Header("シーン遷移時の時間")]
    private float interval; // シーン遷移時の時間
    private bool isFading = false;
    private Color fadeColor = Color.black;
    private float fadeAlpha = 0;
    private static Common instance;

    [System.NonSerialized]
    public int actionCount; // 行動回数

    // 同じオブジェクト(Common)があるか判定
    public static Common Instance
    {
        get
        {
            if (instance == null) {
                instance = FindObjectOfType<Common>();
            }
            return instance;
        }
    }

    // フェードのUIを描画
    public void OnGUI()
    {
        if (this.isFading)
        {
            this.fadeColor.a = this.fadeAlpha;
            GUI.color = this.fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // BGM再生(AudioListener)
    public void PlayBGM(AudioClip bgm) {
        var audio = gameObject.GetComponent<AudioSource>();
        audio.clip = bgm;
        audio.Play();
    }

    /// <summary>
    /// シーン遷移処理(Common.Instance.ChangeScene(Common.SceneName.シーン名))
    /// </summary>
    /// <param name="name"></param>
    public void ChangeScene(SceneName name)
    {
        StartCoroutine(Fade(name));
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerator Fade(SceneName name)
    {
        this.isFading = true;
        float time = 0;
        while(time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        SceneManager.LoadScene((int)name);

        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }

    /// <summary>
    /// シャッフル変数(Common.Instance.Shuffle(配列))
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="param"></param>
    /// <returns></returns>
    public T[] Shuffle<T>(T[] param)
    {
        //T[] randList = new T[param.Length];
        for (int i = 0; i < param.Length; i++)
        {
            T temp = param[i];
            int rand = Random.Range(0, param.Length - 1);
            param[i] = param[rand];
            param[rand] = temp;
        }
        return param;
    }

    public void OnClickOnce(Button button)
    {
        button.interactable = false;
    }
}
