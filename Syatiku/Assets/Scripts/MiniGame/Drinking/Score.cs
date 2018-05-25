using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private int score = 0;
    public Text scoreText = null;

    // Use this for initialization
    void Start () {
        scoreText.text = score.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClickpluc()
    {
        score = score + 1;
        Debug.Log("クリ");
    }
    public void OnClickminus()
    {
        score = score - 1;
        Debug.Log("ック");
    }
}
