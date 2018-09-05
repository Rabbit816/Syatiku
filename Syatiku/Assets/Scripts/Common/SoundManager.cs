using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BGMName
{
    Boss,
    Hack,
    Title,
}

public enum SEName
{
    BadSe,
    BossHit,
    GoodSe,
    tapSe,
}

public enum SmokingVoiceName
{
    Failed_shirota, //そんな...
    Clear_shirota,  //やった!
    Start_man1,     //お疲れ様です
    Start_man2,     //じつは...
    Miss_man1,      //なにいってるんだ?
    Miss_man2,      //そんな...
    Start_woman,    //お疲れ様です
    Miss_woman,     //なにいってるの?
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

    [SerializeField]
    CriAtomSource bgmSource;
    [SerializeField]
    CriAtomSource seSource;
    [SerializeField]
    CriAtomSource voiceSource;

    public void PlayBGM(BGMName cueName)
    {
        bgmSource.Stop();
        bgmSource.Play((int)cueName);
    }

    public void PlayBGM(string cueName)
    {
        bgmSource.Stop();
        bgmSource.Play(cueName);
    }

    public void PlaySE(SEName cueName)
    {
        seSource.Play((int)cueName);
    }

    public void PlaySE(string cueName)
    {
        seSource.Play(cueName);
    }

    public void PlayVoice(int cueId)
    {
        voiceSource.Play(cueId);
    }

    public void PlayVoice(string cueName)
    {
        voiceSource.Play(cueName);
    }

    public void SetVoiceSource(CriAtomSource source)
    {
        voiceSource = source;
    }

    public bool IsVoiceEndOrStop()
    {
        return (voiceSource.status == CriAtomSource.Status.PlayEnd || voiceSource.status == CriAtomSource.Status.Stop);
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
        if(voiceSource != null) voiceSource.Stop();
    }

}
