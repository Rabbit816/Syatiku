using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {
    
    private GameObject obj;
    private Vector2 aaa;
	// Use this for initialization
	void Start () {
        obj = GameObject.Find("Image");
        aaa = obj.transform.localScale;
    }

    // Update is called once per frame
    void Update () {
        //while(image.transform.localScale.x > 0) { 
        aaa.x -= 0.5f * Time.deltaTime;
        obj.transform.localScale = aaa;
        //}
    }
}
