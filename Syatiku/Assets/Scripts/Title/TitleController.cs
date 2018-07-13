using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {
    [SerializeField]
    private GameObject title;

    void Awake()
    {
        if (!Common.Instance)
        {
            var common = Instantiate(Resources.Load("Prefabs/Common/Common"));
            DontDestroyOnLoad(common);
        }
    }

    //モード選択
    public void ChangeMode(int mode)
    {
        Common.Instance.gameMode = mode;
        if (mode == 0)
            Common.Instance.actionCount = 1;
        else
            Common.Instance.actionCount = 1;

        Common.Instance.ChangeScene(Common.SceneName.Scenario);
    }
    //タイトルボタンを削除
    public void Select()
    {
        title.SetActive(false);
    }

}
