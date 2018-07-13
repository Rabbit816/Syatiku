using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    GameObject InfoMeter;
    DrinkScene drink;

    //メニューリスト
    [SerializeField] Image Yakitori;
    [SerializeField] Image Sake;
    [SerializeField] Image Salad;
    [SerializeField] Image Sashimi;

    //デンモク内で使う配列・変数
    private int[] InputOrderBox = new int[4];
    private int[] InputOrderCounter = new int[4];
    private int[] OrderListPos = new int[4] { 427, 332, 237, 142 };
    private int OrderCount = 0;
    private int testCount = 0;

    //注文の答え合わせ
    public void Answer()
    {
        for(int i = 0; i < InputOrderBox.Length; i++)
        {
            if(InputOrderBox[i] == drink.OrderBox[i])
            {
                if(InputOrderCounter[i] == drink.OrderCounter[i])
                {
                    Success();
                }
                else
                {
                    Failed();
                }
            }
            else
            {
                Failed();
            }
        }
    }

    public void Success()
    {
        testCount++;
        Debug.Log(testCount + "人目: 正解");
    }

    public void Failed()
    {
        testCount++;
        Debug.Log(testCount + "人目: 不正解");
    }

    public void ListInYakitori()
    {
        InputOrderBox[OrderCount] = 0;
        InputOrderCounter[OrderCount] = 1;
        Yakitori.GetComponent<RectTransform>().localPosition = new Vector2(0, OrderListPos[OrderCount]);
        OrderCount++;
        if(OrderCount == 4)
        {
            OrderCount = 0;
        }
    }

    public void ListInSake()
    {
        InputOrderBox[OrderCount] = 1;
        InputOrderCounter[OrderCount] = 1;
        Sake.GetComponent<RectTransform>().localPosition = new Vector2(0, OrderListPos[OrderCount]);
        OrderCount++;
        if (OrderCount == 4)
        {
            OrderCount = 0;
        }
    }

    public void ListInSalad()
    {
        InputOrderBox[OrderCount] = 2;
        InputOrderCounter[OrderCount] = 1;
        Salad.GetComponent<RectTransform>().localPosition = new Vector2(0, OrderListPos[OrderCount]);
        OrderCount++;
        if (OrderCount == 4)
        {
            OrderCount = 0;
        }
    }

    public void ListInSashimi()
    {
        InputOrderBox[OrderCount] = 3;
        InputOrderCounter[OrderCount] = 1;
        Sashimi.GetComponent<RectTransform>().localPosition = new Vector2(0, OrderListPos[OrderCount]);
        OrderCount++;
        if (OrderCount == 4)
        {
            OrderCount = 0;
        }
    }

    void Start () {
        drink = GetComponent<DrinkScene>();
        InfoMeter = GameObject.Find("DrinkMain/InfoMeter");
        InfoMeter.GetComponent<Slider>().value = 50;
	}
	
	void Update () {
		
	}
}
