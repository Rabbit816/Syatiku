using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Common : MonoBehaviour {
    private Image fadePanel;
    private GameObject fadePanelPrefab;
    private static Common instance;
    public static Common Instance
    {
        get
        {
            if(instance == null)
            {

            }
            return instance;
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
    void Start () {
        fadePanel = (Image)Resources.Load("Prefab/Common/FadePanel");
        Instantiate(fadePanel, GameObject.Find("Canvas").transform);
    }

    public IEnumerator Fade(float alpha,float interval)
    {
        float time = 0;
        while(time <= interval)
        {
            alpha = Mathf.Lerp(0f, 1f, time / interval);
            fadePanel.GetComponent<Image>().color += new Color(0, 0, 0, alpha);
            time += Time.deltaTime;
            yield return 0;
        }
    }
}
