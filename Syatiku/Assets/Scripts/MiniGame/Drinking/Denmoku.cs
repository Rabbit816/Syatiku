using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    DrinkScene drink;
    ButtonController button;

    //メニューリスト
    [SerializeField] Image Yakitori;
    [SerializeField] Image Sake;
    [SerializeField] Image Salad;
    [SerializeField] Image Sashimi;

    //デンモク内で使う配列・変数
    public int[] InputOrderBox = new int[4];
    public int[] InputOrderCounter = new int[4];
    private float[] OrderListPos = new float[4] { 440, 340, 240, 140 };
    private int OrderCount = 0;


    public void ListInYakitori()
    {
        InputOrderBox[OrderCount] = 0;
        InputOrderCounter[OrderCount] = 1;
        Yakitori.GetComponent<RectTransform>().localPosition = new Vector2(0, OrderListPos[OrderCount]);
        OrderCount++;
        if(OrderCount == 4)
        {
            OrderCount = 0;
            button.OrderButton.interactable = true;
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
            button.OrderButton.interactable = true;
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
            button.OrderButton.interactable = true;
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
            button.OrderButton.interactable = true;
        }
    }

    public void ResetList()
    {
        for(int i = 0; i < InputOrderBox.Length; i++)
        {
            switch (InputOrderBox[i])
            {
                case 0:
                    Yakitori.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    break;
                case 1:
                    Sake.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    break;
                case 2:
                    Salad.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    break;
                default:
                    Sashimi.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    break;
            }
        }
    }

    void Start () {
        drink = GetComponent<DrinkScene>();
        button = GetComponent<ButtonController>();
	}
	
	void Update () {
		
	}
}
