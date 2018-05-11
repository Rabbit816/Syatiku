using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;//シャッフルする時に必要
using System;

public class DrinkMain : MonoBehaviour {
    public Image[] tnm;
    public int[] num;
    private float timer;
    private int count;
    private int index;
    private bool open;


    // Use this for initialization
    void Start () {
        open = false;
        //numの初期化       
        num = new int[4] { 0, 1, 2, 3 };
        //tnmの初期化
        for(int i = 0; i < tnm.Length; ++i)
        {
            tnm[i].enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {      
        //一秒毎にする
        timer += Time.deltaTime;
        if (timer > 1)
        {
            if (open)
            {
                open = false;
                tnm[index].enabled = false;
                count += 1;
                timer = 0;
                Debug.Log("2秒経過");
            }
            else
            {
                timer = 0;
                open = true;
                index = Array.IndexOf(num, count);
                tnm[index].enabled = true;
                Debug.Log("1秒経過");
            }
        }
        if (count == 4)
        {
            count = 0;
        }

    }
}
