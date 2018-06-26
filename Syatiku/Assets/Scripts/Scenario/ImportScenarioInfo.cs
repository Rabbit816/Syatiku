using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImportScenarioInfo : MonoBehaviour {

    ScenarioWindow window;

    public ImportScenarioInfo(string filePath, ref List<ScenarioInfo> scenarioList, ScenarioWindow window)
    {
        this.window = window;

        List<ScenarioInfo> scenarioInfos = new List<ScenarioInfo>();

        //テキストファイルの読み込み
        TextAsset textAsset = Resources.Load<TextAsset>(filePath);
        //@brでシナリオを区切る
        string[] scenarios = textAsset.text.Split(new string[] { "@br" }, System.StringSplitOptions.None);

        for (int i = 0; i < scenarios.Length; i++)
        {
            scenarioInfos.Add(ScenarioAnalysis(scenarios[i]));
        }

        scenarioList = scenarioInfos;
    }

    /// <summary>
    /// 区切ったシナリオを細かく解析
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    ScenarioInfo ScenarioAnalysis(string line)
    {
        ScenarioInfo scenario = new ScenarioInfo();

        StringReader sr = new StringReader(line);
        var message = new System.Text.StringBuilder();

        string text;
        while ((text = sr.ReadLine()) != null)
        {

            if (text.StartsWith("//") || string.IsNullOrEmpty(text))
            {
                //不必要な行
                continue;
            }
            else if (text.StartsWith("@"))
            {
                //コマンドの行
                ParseCommandInfo(text, scenario);
            }
            else
            {
                //セリフの行
                message.Append(text + "\n");
            }
        }

        if (message.Length != 0)
        {
            scenario.message = message.ToString();
        }

        return scenario;
    }

    #region ParseCommand

    /// <summary>
    /// コマンドによって処理を分ける
    /// </summary>
    void ParseCommandInfo(string text, ScenarioInfo scenario)
    {
        if (text.Contains("name"))
        {
            scenario.commandActionList.Add(() =>
            {
                //名前
                window.name.text = TakeTextInfo(text) ?? "";
            });
        }
        else if (text.Contains("bgi"))
        {
            //背景画像
            scenario.commandActionList.Add(() =>
            {
                string imagePath = "Scenario/" + TakeTextInfo(text);
                SetSprite(window.bgi, imagePath);
            });
        }
        else if (text.Contains("charaOn") || text.Contains("emo"))
        {
            //キャラクター画像表示
            scenario.commandActionList.Add(() =>
            {
                string imagePath = "Scenario/" + TakeTextInfo(text);
                Image target = GetTargetImage(text);
                target.gameObject.SetActive(true);
                SetSprite(target, imagePath);
            });
        }
        else if (text.Contains("charaOff"))
        {
            //キャラクター画像非表示
            scenario.commandActionList.Add(() =>
            {
                Image target = GetTargetImage(text);
                target.gameObject.SetActive(false);
            });
        }
        else if (text.Contains("fadeIn"))
        {
            scenario.commandActionList.Add(() =>
            {
                FadeImage(text, 0f, 1f);
            });
        }
        else if (text.Contains("fadeOut"))
        {
            scenario.commandActionList.Add(() =>
            {
                FadeImage(text, 1f, 0f);
            });
        }
        else if (text.Contains("se"))
        {
            //SE

        }
        else if (text.Contains("bgm"))
        {
            //BGM

        }
        else if (text.Contains("end"))
        {
            scenario.commandActionList.Add(() =>
                FadeManager.Instance.Fade(window.scenarioCanvas, 1f, 0f)
            );
        }
    }

    /// <summary>
    /// テキストの情報を抜き取る ({ }の中身)
    /// </summary>
    string TakeTextInfo(string text)
    {
        int beginNum = text.IndexOf("{") + 1;
        int lastNum = text.IndexOf("}");
        return text.Substring(beginNum, lastNum - beginNum);
    }

    /// <summary>
    /// 画像をセット
    /// </summary>
    void SetSprite(Image image, string path)
    {
        image.sprite = Resources.Load<Sprite>(path);
    }

    /// <summary>
    /// 対象画像を取得
    /// </summary>
    Image GetTargetImage(string text)
    {
        Image target = null;
        if (text.LastIndexOf("left") >= 0)
        {
            if (text.IndexOf('c') == 1) target = window.charaLeft;
            else target = window.iconLeft;
        }
        else if (text.LastIndexOf("center") >= 0)
        {
            if (text.IndexOf('c') == 1) target = window.charaCenter;
            else target = window.iconCenter;
        }
        else if (text.LastIndexOf("right") >= 0)
        {
            if (text.IndexOf('c') == 1) target = window.charaRight;
            else target = window.iconRight;
        }

        return target;
    }

    /// <summary>
    /// フェード
    /// </summary>
    void FadeImage(string text, float startAlpha, float targetAlpha)
    {
        string targetName = TakeTextInfo(text);
        Image target = null;
        float waitTime = GetTime(text);

        switch (targetName)
        {
            case "character":
                target = GetTargetImage(text);
                break;
            case "background":
                target = window.bgi;
                break;
        }
        target.color = new Color(1f, 1f, 1f, startAlpha);
        FadeManager.Instance.Fade(target, waitTime, targetAlpha);
    }

    /// <summary>
    /// 時間を取得
    /// </summary>
    float GetTime(string text)
    {
        int beginNum = text.IndexOf("[") + 1;
        int lastNum = text.IndexOf("]");
        float time = float.Parse(text.Substring(beginNum, lastNum - beginNum));

        return time;
    }
    
    #endregion
}
