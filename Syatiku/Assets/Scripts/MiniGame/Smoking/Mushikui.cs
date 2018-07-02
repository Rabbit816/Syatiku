using System;
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
        private string musikui_select;  // 選択肢
        private int lineNum;            // 行数
        private int rowNum;             // 列数

        private string[] message_puestion;
        private string[] message_answer;
        private string[] message_master;
        private string[] musikuiList;
        private string[][] selectList;

        // 「」の中身取得
        public string Mushikui { get { return musikui; } }
        // 元メッセージ取得
        public string Message_origin { get { return message_origin; } }
        // 変換後のメッセージ取得
        public string Message_after { get { return message_after; } }

        public void LoadMessage(string _message,int j)
        {
            
            message_origin = _message;
            if (!message_origin.Contains("「")) {
                musikui_select = message_origin;
                Debug.Log("select=" + musikui_select);
                selectList[lineNum][rowNum] = musikui_select;
                rowNum++;
                return;
            }
            if (rowNum != 0)
                lineNum++;
            rowNum = 0;
            // mushikui を検出
            var startPos = _message.IndexOf("「");
            var endPos = _message.IndexOf("」");
            musikui = _message.Substring(startPos + 1, endPos - startPos - 1);
            // mMessage を作成
            message_after = message_origin.Replace("「" + musikui + "」", "＿＿＿");

            message_puestion[j] = message_after;
            message_master[j] = message_origin;
            musikuiList[j] = musikui;
            
        }
        public void Log()
        {
            Debug.Log("Mushikui=" + Mushikui);
            Debug.Log("Message_origin=" + Message_origin);
            Debug.Log("Message_after=" + Message_after);
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
                    musi.LoadMessage(i[j],j);
                    musi.Log();
                }
            }
        }
        
    }
}
