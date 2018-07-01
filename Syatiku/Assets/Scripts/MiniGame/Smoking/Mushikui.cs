using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class Mushikui {

    struct MushikuiData
    {
        private string musikui;         // 「」の中の文字
        private string message_origin;  //  元のメッセージ
        private string message_after;   // 虫食いを抜いたメッセージ

        // 「」の中身取得
        public string Mushikui { get { return musikui; } }
        // 元メッセージ取得
        public string Message_origin { get { return message_origin; } }
        // 変換後のメッセージ取得
        public string Message_after { get { return message_after; } }

        public void LoadMessage(string _message)
        {
            message_origin = _message;
            // mushikui を検出
            var startPos = _message.IndexOf("「");
            var endPos = _message.IndexOf("」");
            musikui = _message.Substring(startPos + 1, endPos - startPos - 1);
            // mMessage を作成
            message_after = message_origin.Replace("「" + musikui + "」", "＿＿＿");
        }
        public void Log()
        {
            Debug.Log(Mushikui);
            Debug.Log(Message_origin);
            Debug.Log(Message_after);
        }

    }

    private string musikui;
    private string motoMessage;
    private string mMessage;
    private string answerMessage;
    
    public Mushikui(string _filepath)
    {
        Load(_filepath);
    }

    private string filePath = "CSV/MushikuiTest";
    private string[] textList;
    private TextAsset CSVFile;
    public static List<string[]> csvData = new List<string[]>();

    // Use this for initialization
    public void Load(string _filepath)
    {
        // TODO:ここで外部からのファイルパスを使用すること！！
        CSVFile = Resources.Load(_filepath) as TextAsset;
        StringReader render = new StringReader(CSVFile.text);
        while (render.Peek() > -1)
        {
            string line = render.ReadLine();
            csvData.Add(line.Split(','));
            foreach (var i in csvData)
            {
                for (int j = 0; j < i.Length; j++)
                {
                    var musi = new MushikuiData();
                    musi.LoadMessage(i[j]);
                    musi.Log();
                }
            }
        }
        
    }
}
