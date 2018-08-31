using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    // スクリプトのインスタンスの取得
    ButtonController button;

    // 注文する商品の順番を保存する配列
    [HideInInspector]
    public int[] InputOrderBox = new int[4];

    // 注文する商品の個数を保存する配列
    [HideInInspector]
    public int[] InputOrderCounter = new int[4];

    // 注文リストに注文商品を表示する
    [SerializeField]
    Text[] OrderMenuList;

    // 注文リストに注文する個数を表示する
    [SerializeField]
    Text[] CounterList;

    private int Num;

    // 注文リストの有効・無効を管理する
    public void OrderListController(bool b)
    {
        if (b)
        {
            this.OrderMenuList[this.Num].gameObject.SetActive(true);
            this.CounterList[this.Num].gameObject.SetActive(true);
        }
        else
        {
            this.OrderMenuList[button.CounterNum].gameObject.SetActive(false);
            this.CounterList[button.CounterNum].gameObject.SetActive(false);
        }
    }

    // 注文リストを初期化する
    public void MenuListOFF()
    {
        for(int i = 0; i < this.CounterList.Length; i++)
        {
            this.OrderMenuList[i].gameObject.SetActive(false);
            this.CounterList[i].gameObject.SetActive(false);
        }
    }

    // 注文リストに注文した商品を表示する
    public void ListInMenu(int MenuID)
    {
        // 注文リストに表示する場所を決める
        for(int i = 0; i < this.InputOrderBox.Length; i++)
        {
            if(this.InputOrderBox[i] < 0)
            {
                this.Num = i;
                break;
            }
        }
        this.InputOrderBox[this.Num] = MenuID;
        this.InputOrderCounter[this.Num] = 1;
        
        // 注文リストに注文した商品名を表示する
        switch (MenuID)
        {
            case 0:
                this.OrderMenuList[this.Num].text = "枝豆";
                break;
            case 1:
                this.OrderMenuList[this.Num].text = "卵焼き";
                break;
            case 2:
                this.OrderMenuList[this.Num].text = "からあげ";
                break;
            case 3:
                this.OrderMenuList[this.Num].text = "サラダ";
                break;
            default:
                Debug.Log("ListCheck : エラー");
                break;
        }
    }

    void Start () {
        button = GetComponent<ButtonController>();
	}
	
	void Update () {
        // 注文リストに注文数を表示
        this.CounterList[0].text = "× " + this.InputOrderCounter[0].ToString();
        this.CounterList[1].text = "× " + this.InputOrderCounter[1].ToString();
        this.CounterList[2].text = "× " + this.InputOrderCounter[2].ToString();
        this.CounterList[3].text = "× " + this.InputOrderCounter[3].ToString();
    }
}
