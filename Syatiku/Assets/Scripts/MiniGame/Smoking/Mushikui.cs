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
        private string[] select;

        public List<string> message_puestion;
        public List<string> message_answer;
        public List<string> message_master;
        public List<string> musikuiList;
        public List<string[]> selectList;


        // 「」の中身取得
        public string Musikui { get { return musikui; } }
        // 元メッセージ取得
        public string Message_origin { get { return message_origin; } }
        // 変換後のメッセージ取得
        public string Message_after { get { return message_after; } }

        public void LoadMessage(string message)
        {
            message_origin = message;

            if (!message_origin.Contains("「"))
            {   // originが選択肢の場合の処理
                musikui_select = message_origin;
                Debug.Log("select=" + musikui_select);
                rowNum++;
                if (rowNum <= 4)
                {   // 選択肢を配列に入れる
                    select[rowNum] = musikui_select;
                }
                if(rowNum == 4)
                {
                    // 選択肢が終わったら選択肢配列をListに入れる
                    selectList.Add(select);
                    select = null;
                    lineNum++;
                }
                return;
            }
            
            rowNum = 0;
            // mushikui を検出
            var startPos = message.IndexOf("「");
            var endPos = message.IndexOf("」");
            musikui = message.Substring(startPos + 1, endPos - startPos - 1);
            // mMessage を作成
            message_after = message_origin.Replace("「" + musikui + "」", "＿＿＿");

            // 問題、虫食い問題、虫食い部分をListに入れる
            message_puestion.Add(Message_after);
            message_master.Add(Message_origin);
            musikuiList.Add(Musikui);
            
        }
        public void Log()
        {
            Debug.Log("Mushikui=" + Musikui);
            Debug.Log("Message_origin=" + Message_origin);
            Debug.Log("Message_after=" + Message_after);
        }

    }

    //private string musikui;
    //private string motoMessage;
    //private string mMessage;
    //private string answerMessage;
    
    public Mushikui(string filepath)
    {
        Load(filepath);
    }

    private string filePath = "CSV/MushikuiTest";
    private string[] textList;
    private TextAsset CSVFile;
    public static List<string[]> csvData = new List<string[]>();

    // Use this for initialization
    public void Load(string filepath)
    {
        // 外部からのファイルパスを使用
        CSVFile = Resources.Load(filepath) as TextAsset;
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
