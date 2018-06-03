using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;//シャッフルする時に必要
using System;

public class DrinkMain : MonoBehaviour {

    [SerializeField]
    private Image[] balloon;
    [SerializeField]
    private Sprite[] balloon2;
    [SerializeField]
    private GameObject button;

    private int[] num;
    private int[] num2;
    private float timer;
    private int count;
    private int index;
    private bool open = false;
    private Order order = new Order(); 

    // Use this for initialization
    void Start()
    {       
        //numの初期化       
        num = new int[4] { 0, 1, 2, 3 };

        //ランダムに吹き出しを表示する
        Randomballoon();

        //balloonの初期化
        for (int i = 0; i < balloon.Length; ++i)
        {
            balloon[i].enabled = false;
        }
        button.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (count == 4)
        {
            StartCoroutine(Bo());         
            return;
        }

        //一秒毎にする
        timer += Time.deltaTime;
        if (timer > 1)
        {
            //吹き出しの中の表示方法
            if (open)
            {
                open = false;
                balloon[index].enabled = false;
                balloon[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                count += 1;
                timer = 0;
                Debug.Log("2秒経過");
            }
            else
            {
                timer = 0;
                open = true;
                index = Array.IndexOf(num, count);
                balloon[index].transform.GetChild(0).GetComponent<Image>().sprite = balloon2[order.GetSprite()];
                balloon[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                balloon[index].enabled = true;
                Debug.Log("Index" + index);
            }
        }
    }

    //ランダムに表示させる
    public void Randomballoon()
    {        
        num2 = num.OrderBy(i => Guid.NewGuid()).ToArray();
        num = num2;
    }

    /*public void Randomballoon()
    {       
        num2 = (Common.Instance.Shuffle(num));
        num = num2;
    }*/
    private IEnumerator Bo()
    {
        yield return new WaitForSeconds(1f);
        button.gameObject.SetActive(true);
    }
}

