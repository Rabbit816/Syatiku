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
                message.Append(text);
            }
        }

        scenario.message = message.ToString();

        return scenario;
    }

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
                window.name.text = TakeCommandInfo(text);
            });
        }
        else if (text.Contains("bgi"))
        {
            //背景画像
            scenario.commandActionList.Add(() =>
            {
                string imagePath = "Scenario/" + TakeCommandInfo(text);
                SetSprite(window.bgi, imagePath);
            });
        }
        else if (text.Contains("bgm"))
        {
            //BGM

        }
        else if (text.Contains("charaOn"))
        {
            //キャラクター画像表示
            scenario.commandActionList.Add(() =>
            {
                string imagePath = "Scenario/" + TakeCommandInfo(text);
                Image target = GetCharaPos(text);
                SetSprite(target, imagePath);
            });
        }
        else if (text.Contains("charaOff"))
        {
            //キャラクター画像非表示
            scenario.commandActionList.Add(() =>
            {
                Image target = GetCharaPos(text);
                target.sprite = null;
            });
        }
        else if (text.Contains("se"))
        {
            //SE

        }
    }

    /// <summary>
    /// コマンドの情報を取得
    /// </summary>
    string TakeCommandInfo(string text)
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
    /// 変えるキャラクター画像の位置を取得
    /// </summary>
    Image GetCharaPos(string text)
    {
        Image target = null;
        if(text.LastIndexOf("left") >= 0)
        {
            target = window.charaLeft;
        }
        else if(text.LastIndexOf("center") >= 0)
        {
            target = window.charaCenter;
        }
        else if (text.LastIndexOf("right") >= 0)
        {
            target = window.charaRight;
        }
        return target;
    }
    
}
