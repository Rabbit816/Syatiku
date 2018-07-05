using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {
    [SerializeField]
    private GameObject title;

    [SerializeField]
    private AudioClip aClip;

    void Start()
    {
        if (!Common.Instance)
        {
            var common = Instantiate(Resources.Load("Prefabs/Common/Common"));
            DontDestroyOnLoad(common);
            var sound = Instantiate(Resources.Load("Prefabs/Common/SoundManager"));
            DontDestroyOnLoad(sound);

            //SoundManager.Instance.PlayBGM(BGMName.Title);
        }
        Common.Instance.PlayBGM(aClip);
    }

    //モード選択
    public void ChangeMode(int mode)
    {
        //ScenarioController sc = new ScenarioController();
        //sc.nowScene = 0;

        Common.Instance.gameMode = mode;
        if (mode == 0)
            Common.Instance.actionCount = 1;
        else
            Common.Instance.actionCount = 3;
        Common.Instance.ChangeScene(Common.SceneName.Scenario);
    }
    //タイトルボタンを削除
    public void Select()
    {
        title.SetActive(false);
    }

}
