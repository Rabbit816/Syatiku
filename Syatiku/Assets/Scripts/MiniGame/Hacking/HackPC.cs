using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HackPC : MonoBehaviour {
    
    private Text prefabText;
    [HideInInspector]
    public int counter = 0;

    // Use this for initialization
    void Start () {
        ChippedString();
    }

    // Update is called once per frame
    void Update () {
        
	}

    /// <summary>
    /// 欠けてる文章の処理
    /// </summary>
    private void ChippedString()
    {
        
    }

    /// <summary>
    /// バラバラの文字処理
    /// </summary>
    private void PiecesString()
    {
        // 欠けてる文章によって出す文字変更
        // 場所を設定　ランダムでやるかも

    }

    private bool CheckString()
    {
        // 文字列があっているかどうか処理
        counter++;
        return true;
    }
    
}
