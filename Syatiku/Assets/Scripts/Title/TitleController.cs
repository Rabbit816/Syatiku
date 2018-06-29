using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {
    [SerializeField]
    private GameObject title;

    //モード選択
    public void ChangeMode(int mode)
    {
        Common.Instance.gameMode = mode;
        Common.Instance.ChangeScene(Common.SceneName.Scenario);
    }
    //タイトルボタンを削除
    public void Select()
    {
        title.SetActive(false);
    }
}
