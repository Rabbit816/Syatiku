using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        //bgmSource.Play("futta-rainbow");
        DontDestroyOnLoad(this);
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

}
