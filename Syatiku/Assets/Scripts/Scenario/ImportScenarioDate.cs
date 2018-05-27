using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ScenarioInfo
{

    public string name;
    public string message;

    public ScenarioInfo(string _name = "名前", string _message = "セリフ")
    {
        name = _name;
        message = _message;
    }

}

public class ImportScenarioDate : MonoBehaviour {

    [SerializeField]
    string filePath = "Text/chapter_0_0";

    public List<ScenarioInfo> CreateScenarioInfo()
    {
        List<ScenarioInfo> scenarioInfos = new List<ScenarioInfo>();

        //テキストファイルの読み込み
        TextAsset textAsset = Resources.Load<TextAsset>(filePath);
        //@brでシナリオを区切る
        string[] scenarios = textAsset.text.Split(new string[] { "@br" }, System.StringSplitOptions.None);

        for (int i = 0; i < scenarios.Length; i++)
        {
            scenarioInfos.Add(ScenarioAnalysis(scenarios[i]));
        }

        return scenarioInfos;
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
                //タグの行
                ParseTagKind(text, scenario);
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

    void ParseTagKind(string text, ScenarioInfo scenario)
    {

        if (text.Contains("name"))
        {
            //名前
            scenario.name = TakeInfo(text);
        }
        else if (text.Contains(""))
        {

        }
    }

    string TakeInfo(string text)
    {
        int beginNum = text.IndexOf("{") + 1;
        int lastNum = text.IndexOf("}");
        return text.Substring(beginNum, lastNum - beginNum);
    }
}
