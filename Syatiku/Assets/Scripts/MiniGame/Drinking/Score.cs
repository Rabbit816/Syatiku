using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {      

    public void OnClickPluc()
    {
        //+ボタンが押されたらNumberを呼び出す
        FindObjectOfType<Number>().Addpoint(1);
        Debug.Log("クリ");
    }
    public void OnClickMinus()
    {        
        //numberのカウントが0以上なら
        if (FindObjectOfType<Number>().number > 0)
        {
            //-ボタンが押されたらNumberを呼び出す
            FindObjectOfType<Number>().Addpoint(-1);
            Debug.Log("ック");
        }
    }   
}
