using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BGMName
{
    Title,
    Hack,
    Boss,
}

public class SoundManager : MonoBehaviour {

    static SoundManager instance;
    public static SoundManager Instance {
        get
        {
            if (instance != null) return instance;
            instance = FindObjectOfType<SoundManager>();
            return instance;
        }
    }

    void Awake()
    {
        CheckInstance();
        DontDestroyOnLoad(this);
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        if (instance == this)
        {
            return;
        }

        Destroy(gameObject);
    }

    readonly List<string> bgmNameList = new List<string>
    {
        "TopBGM",
        "HackBGM",
        "BossBGM",
    };

    [SerializeField]
    CriAtomSource bgmSource;
    [SerializeField]
    CriAtomSource seSource;
    [SerializeField]
    CriAtomSource voiceSource;

    private void Start()
    {
        //bgmSource.Play("TopBGM");
    }

    public void PlayBGM(BGMName cueName)
    {
        bgmSource.Play(bgmNameList[(int)cueName]);
    }

    public void PlayBGM(string cueName)
    {
        bgmSource.Play(cueName);
    }

    public void PlaySE(string cueName)
    {
        seSource.Play(cueName);
    }

    public void PlayVoice(string cueName)
    {
        voiceSource.Play(cueName);
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void StopSE()
    {
        seSource.Stop();
    }

    public void StopVoice()
    {
        voiceSource.Stop();
    }

}
