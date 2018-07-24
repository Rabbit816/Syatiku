using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    ButtonController button;

    //メニューリスト
    GameObject OrderList;
    GameObject YakitoriPre;
    GameObject SakePre;
    GameObject SaladPre;
    GameObject SashimiPre;

    //デンモク内で使う配列・変数
    public int[] InputOrderBox = new int[4];
    public int[] InputOrderCounter = new int[4];
    private float[] OrderListPos = new float[4] { 4, 1.85f, -0.3f, -2.45f };
    private int OrderCount = 0;


    public void ListInYakitori()
    {
        this.InputOrderBox[OrderCount] = 0;
        this.InputOrderCounter[OrderCount] = 1;
        this.YakitoriPre = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/yakitori");
        var Yakitori = Instantiate(this.YakitoriPre, new Vector2(-6.5f, OrderListPos[OrderCount]), Quaternion.identity);
        Yakitori.transform.localScale = new Vector2(0.6f, 0.6f);
        Yakitori.transform.parent = this.OrderList.transform;
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
        this.SakePre = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/sake");
        var Sake = Instantiate(this.SakePre, new Vector2(-6.5f, OrderListPos[OrderCount]), Quaternion.identity);
        Sake.transform.localScale = new Vector2(0.23f, 0.23f);
        Sake.transform.parent = this.OrderList.transform;
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
        this.SaladPre = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/salad");
        var Salad = Instantiate(this.SaladPre, new Vector2(-6.5f, OrderListPos[OrderCount]), Quaternion.identity);
        Salad.transform.localScale = new Vector2(0.3f, 0.3f);
        Salad.transform.parent = this.OrderList.transform;
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
        this.SashimiPre = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/sashimi");
        var Sashimi = Instantiate(this.SashimiPre, new Vector2(-6.5f, OrderListPos[OrderCount]), Quaternion.identity);
        Sashimi.transform.localScale = new Vector2(0.23f, 0.23f);
        Sashimi.transform.parent = this.OrderList.transform;
        this.OrderCount++;
        if (OrderCount == 4)
        {
            this.OrderCount = 0;
            button.OrderButton.interactable = true;
        }
    }

    void Start () {
        this.OrderList = GameObject.Find("MenuObject");
        button = GetComponent<ButtonController>();
	}
	
	void Update () {
		
	}
}
