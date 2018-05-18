using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;//シャッフルする時に必要
using System;

public class DrinkMain : MonoBehaviour {

    [SerializeField]
    public Image[] Balloon;
    [SerializeField]
    private Sprite[] Balloon2;
    private int[] num;
    private int[] num2;
    private float timer;
    public int count;
    public int index;
    private bool open;
    private ryouri ryouri = new ryouri();

    // Use this for initialization
    void Start()
    {
        open = false;

        //numの初期化       
        num = new int[4] { 0, 1, 2, 3 };

        //ランダムに吹き出しを表示する
        RandomBalloon();

        //tnmの初期化
        for (int i = 0; i < Balloon.Length; ++i)
        {
            Balloon[i].enabled = false;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 4)
        {
            GetComponent<Bottom>();          
            return;
        }

        //一秒毎にする
        timer += Time.deltaTime;
        if (timer > 1)
        {
            if (open)
            {
                open = false;
                Balloon[index].enabled = false;
                Balloon[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                count += 1;
                timer = 0;
                Debug.Log("2秒経過");
            }
            else
            {
                timer = 0;
                open = true;
                index = Array.IndexOf(num, count);
                Balloon[index].transform.GetChild(0).GetComponent<Image>().sprite = Balloon2[ryouri.GetSprite()];
                Balloon[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Balloon[index].enabled = true;
                Debug.Log("Index" + index);
            }  
        }
    }  

    //ランダムに表示させる
    public void RandomBalloon()
    {
        num2 = num.OrderBy(i => Guid.NewGuid()).ToArray();
        num = num2;
    }
}

