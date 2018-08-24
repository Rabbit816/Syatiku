using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioScene : MonoBehaviour {

    [SerializeField]
    string filePath;
    [SerializeField]
    CriAtomSource voiceSource;

    void Start () {
        SoundManager.Instance.SetVoiceSource(voiceSource);
        ScenarioController.Instance.BeginScenario(filePath);
        SoundManager.Instance.StopBGM();
    }

}
