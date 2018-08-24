using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    DrinkScene drink;
    ButtonController button;

    //デンモク内で使う配列・変数
    [HideInInspector]
    public int[] InputOrderBox = new int[4];

    [HideInInspector]
    public int[] InputOrderCounter = new int[4];

    [SerializeField]
    Text[] OrderMenuList;

    [SerializeField]
    Text[] CounterList;

    private int Num;

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

    public void MenuListOFF()
    {
        for(int i = 0; i < this.CounterList.Length; i++)
        {
            this.OrderMenuList[i].gameObject.SetActive(false);
            this.CounterList[i].gameObject.SetActive(false);
        }
    }

    public void ListCheck(int MenuID)
    {
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
        drink = GetComponent<DrinkScene>();
        button = GetComponent<ButtonController>();
	}
	
	void Update () {
        this.CounterList[0].text = "× " + this.InputOrderCounter[0].ToString();
        this.CounterList[1].text = "× " + this.InputOrderCounter[1].ToString();
        this.CounterList[2].text = "× " + this.InputOrderCounter[2].ToString();
        this.CounterList[3].text = "× " + this.InputOrderCounter[3].ToString();
    }
}
