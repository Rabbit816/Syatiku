using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
   
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickpluc()
    {
        //+ボタンが押されたらNumberを呼び出す
        FindObjectOfType<Number>().Addpoint(1);
        Debug.Log("クリ");
    }
    public void OnClickminus()
    {
        FindObjectOfType<Number>().Addpoint(-1);
        Debug.Log("ック");
    }   
}
