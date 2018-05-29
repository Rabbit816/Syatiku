using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour {
    [SerializeField]
    private Text scoreText;
	// Use this for initialization
	void Start () {
        if (Common.gameClear)
            scoreText.text = Common.Instance.data[0] + "を手に入れた！\n";
        else
            scoreText.text = "何も手に入らなかった...";

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
