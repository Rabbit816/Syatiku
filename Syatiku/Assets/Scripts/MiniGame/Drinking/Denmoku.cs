using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    ButtonController button;

    //メニューリスト
    [SerializeField] Image Yakitori;
    [SerializeField] Image Sake;
    [SerializeField] Image Salad;
    [SerializeField] Image Sashimi;

    //デンモク内で使う配列・変数
    public int[] InputOrderBox = new int[4];
    public int[] InputOrderCounter = new int[4];
    private float[] OrderListPos = new float[4] { 360, 275, 190, 105 };
    private int OrderCount = 0;


    public void ListInYakitori()
    {
        this.InputOrderBox[OrderCount] = 0;
        this.InputOrderCounter[OrderCount] = 1;
        this.Yakitori.GetComponent<RectTransform>().localPosition = new Vector2(0, OrderListPos[OrderCount]);
        this.OrderCount++;
        if(OrderCount == 4)
        {
            this.OrderCount = 0;
            button.OrderButton.interactable = true;
        }
    }

    public void ListInSake()
    {
        this.InputOrderBox[OrderCount] = 1;
        this.InputOrderCounter[OrderCount] = 1;
        this.Sake.GetComponent<RectTransform>().localPosition = new Vector2(0, OrderListPos[OrderCount]);
        this.OrderCount++;
        if (OrderCount == 4)
        {
            this.OrderCount = 0;
            button.OrderButton.interactable = true;
        }
    }

    public void ListInSalad()
    {
        this.InputOrderBox[OrderCount] = 2;
        this.InputOrderCounter[OrderCount] = 1;
        this.Salad.GetComponent<RectTransform>().localPosition = new Vector2(0, OrderListPos[OrderCount]);
        this.OrderCount++;
        if (OrderCount == 4)
        {
            this.OrderCount = 0;
            button.OrderButton.interactable = true;
        }
    }

    public void ListInSashimi()
    {
        this.InputOrderBox[OrderCount] = 3;
        this.InputOrderCounter[OrderCount] = 1;
        this.Sashimi.GetComponent<RectTransform>().localPosition = new Vector2(0, OrderListPos[OrderCount]);
        this.OrderCount++;
        if (OrderCount == 4)
        {
            this.OrderCount = 0;
            button.OrderButton.interactable = true;
        }
    }

    public void ResetList()
    {
        for(int i = 0; i < InputOrderBox.Length; i++)
        {
            switch (this.InputOrderBox[i])
            {
                case 0:
                    this.Yakitori.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    break;
                case 1:
                    this.Sake.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    break;
                case 2:
                    this.Salad.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    break;
                case 3:
                    this.Sashimi.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    break;
            }
        }
    }

    void Start () {
        button = GetComponent<ButtonController>();
	}
	
	void Update () {
		
	}
}
