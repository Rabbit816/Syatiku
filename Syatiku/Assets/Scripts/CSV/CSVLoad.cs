using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVLoad : MonoBehaviour {
    private string filePath = "CSV/CSVTest.csv";
    private string[] textList;
    private TextAsset fileData;
	// Use this for initialization
	void Start () {
        
        fileData = Resources.Load("CSV/CSVTest") as TextAsset;
        //foreach(var i in )
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
