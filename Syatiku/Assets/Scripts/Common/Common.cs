using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Common : MonoBehaviour {

    public enum SceneName
    {
        Title = 0,
        Scenario,
        Select,
        Action,
        Smoking,
        Hacking,
        Drinking,
        Boss,
        Result,
    }

    private static Common instance;
    private bool isFading = false;
    private Color fadeColor = Color.black;
    private float fadeAlpha = 0;
    public static Common Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (Common)FindObjectOfType(typeof(Common));

                if (instance == null)
                {
                    Debug.LogError(typeof(Common) + "is nothing");
                }
            }
            return instance;
        }
    }

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
        if (!this == Instance)
        {
            Destroy(this.gameObject);
            return;
        }
            DontDestroyOnLoad(gameObject);
    }

    public void FadeChangeScene(SceneName name,float interval)
    {
        StartCoroutine(Fade(name , interval));
    }

    public IEnumerator Fade(SceneName name, float interval)
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
}
