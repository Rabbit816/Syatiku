using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BGMName
{
    Boss,
    Hacking,
    Title,
    DrinkingParty,
    Smoking,
    Action,
    BadEnd,
    NormalEnd,
    GoodEnd,
}

public enum SEName
{
    DenmokuTap,     //飲み会デンモクタップ
    Cancel,         //戻るボタン系
    CorrectChoice,  //喫煙で正解選択
    EnemyCome,      //ハッキングで敵(社長、課長)が来たとき
    FrameFell,      //ハッキングで額縁が落ちた時
    FindInfo,       //ハッキングで資料、パスワードが見つかったとき
    Flick,          //テキストフリック
    Harisen,        //ハリセン
    CorrectHit,     //正解のテキスト衝突
    WrongChoice,    //喫煙で不正解選択
    Locker,         //ハッキングの引き出しあけたとき
    Message,        //
    Failed ,        //リザルト失敗
    Success,        //リザルト成功
    WrongHit,       //不正解テキスト衝突
    PasswordMiss,   //パスワードミスった
    Page,           //ハッキングのペラペラ
    SetPassword,    //パスワードの文字をはめるとき
    SeVondnidle,    //使わないもの（気が向いたら消す）
    TapAction,      //ミニゲーム選択音
    Timer,          //喫煙所の選択肢表示中のタイマー音
    Hukidashi,      //リザルトの吹き出し音
    Spot,           //リザルトスポットライト音
}

public enum SmokingVoiceName
{
    Start_man1,     //お疲れ様です
    Start_man2,     //じつは...
    Miss_man1,      //なにいってるんだ?
    Clear_shirota,  //やった!
    Failed_shirota, //そんな...
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

    public bool IsSeEndOrStop()
    {
        return (seSource.status == CriAtomSource.Status.PlayEnd || seSource.status == CriAtomSource.Status.Stop);
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
