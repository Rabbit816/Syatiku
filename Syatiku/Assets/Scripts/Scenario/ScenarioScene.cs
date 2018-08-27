using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioScene : MonoBehaviour {

    [SerializeField]
    string filePath;
    [SerializeField]
    CriAtomSource voiceSource;

    void Start () {
        //現在のシーン名を取得
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.SetVoiceSource(voiceSource);

        if (sceneName == "BeforeBattle")
        {
            BeginScenarioBeforeBattle();
        }
        else
        {
            ScenarioController.Instance.BeginScenario(filePath);
        }
    }

    /// <summary>
    /// ボス決戦前のシナリオ再生(シナリオを始める番号:最初のボイス番号)
    /// vs社長(bad 0:0 normal 6:1 good 11:2)
    /// vs課長
    /// </summary>
    private void BeginScenarioBeforeBattle()
    {
        int startInfoIndex = 0;
        int startVoiceIndex = 0;
        bool[] clearFlag = Common.Instance.clearFlag;
        int clearCount = 0;
        //クリアしたミニゲームの数
        for (int i = 0; i < clearFlag.Length; i++)
        {
            clearCount += clearFlag[i] ? 1 : 0;
        }
        switch (clearCount)
        {
            //Bad
            case 0:
            case 1:
                break;
            //Normal
            case 2:
                startInfoIndex = 6;
                startVoiceIndex = 1;
                break;
            //Good
            case 3:
                startInfoIndex = 11;
                startVoiceIndex = 2;
                break;
        }

        ScenarioController.Instance.BeginScenario(filePath, startInfoIndex, startVoiceIndex);
    }

}
