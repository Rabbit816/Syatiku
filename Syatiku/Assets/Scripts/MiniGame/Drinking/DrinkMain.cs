using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;//シャッフルする時に必要
using System;

public class DrinkMain : MonoBehaviour {

    [SerializeField]
    public Image[] tnm;
    [SerializeField]
    private Sprite[] tnm2;
    private int[] num;
    private int[] num2;
    private float timer;
    private int count;
    public int index;
    private bool open;
    private ryouri ryouri = new ryouri();

    // Use this for initialization
    void Start()
    {
        open = false;
        //numの初期化       
        num = new int[4] { 0, 1, 2, 3 };
        Randomtnm();
        //tnmの初期化
        for (int i = 0; i < tnm.Length; ++i)
        {
            tnm[i].enabled = false;
        }
        //transform.parent = GameObject.Find("tnm").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 4)
        {
            count = 0;
            Randomtnm();
            ryouri.Randomtnm();
        }

        //一秒毎にする
        timer += Time.deltaTime;
        if (timer > 1)
        {
            if (open)
            {
                open = false;
                tnm[index].enabled = false;
                tnm[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                count += 1;
                timer = 0;
                Debug.Log("2秒経過");
            }
            else
            {
                timer = 0;
                open = true;
                index = Array.IndexOf(num, count);
                tnm[index].transform.GetChild(0).GetComponent<Image>().sprite = tnm2[ryouri.GetSprite()];
                tnm[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                tnm[index].enabled = true;
                Debug.Log("Index" + index);
            }  
        }
        

    }      

    //ランダムに表示させる
    public void Randomtnm()
    {
        num2 = num.OrderBy(i => Guid.NewGuid()).ToArray();
        num = num2;
    }
}

