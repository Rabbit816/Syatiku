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
    private float[] OrderListPos = new float[4] { 3.9f, 1.75f, -0.45f, -2.55f };
    private int OrderCount = 0;

    GameObject Counter1, Counter2, Counter3, Counter4;

    public void OrderListCounterON()
    {
        switch (button.OrderCount)
        {
            case 0:
                this.Counter1.gameObject.SetActive(true);
                break;
            case 1:
                this.Counter2.gameObject.SetActive(true);
                break;
            case 2:
                this.Counter3.gameObject.SetActive(true);
                break;
            case 3:
                this.Counter4.gameObject.SetActive(true);
                break;
            default:
                Debug.Log("エラー");
                break;
        }
    }

    public void CounterOFF()
    {
        this.Counter1.gameObject.SetActive(false);
        this.Counter2.gameObject.SetActive(false);
        this.Counter3.gameObject.SetActive(false);
        this.Counter4.gameObject.SetActive(false);
    }


    public void ListInYakitori()
    {
        this.InputOrderBox[this.OrderCount] = 0;
        this.InputOrderCounter[this.OrderCount] = 1;
        this.YakitoriPre = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/yakitori");
        var Yakitori = Instantiate(this.YakitoriPre, new Vector2(-6.4f, OrderListPos[this.OrderCount]), Quaternion.identity);
        Yakitori.transform.localScale = new Vector2(0.6f, 0.6f);
        Yakitori.transform.parent = this.OrderList.transform;
        this.OrderCount++;
        if(this.OrderCount == 4)
        {
            this.OrderCount = 0;
            button.OrderButton.interactable = true;
        }
    }

    public void ListInSake()
    {
        this.InputOrderBox[this.OrderCount] = 1;
        this.InputOrderCounter[this.OrderCount] = 1;
        this.SakePre = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/sake");
        var Sake = Instantiate(this.SakePre, new Vector2(-6.4f, OrderListPos[this.OrderCount]), Quaternion.identity);
        Sake.transform.localScale = new Vector2(0.23f, 0.23f);
        Sake.transform.parent = this.OrderList.transform;
        this.OrderCount++;
        if (this.OrderCount == 4)
        {
            this.OrderCount = 0;
            button.OrderButton.interactable = true;
        }
    }

    public void ListInSalad()
    {
        this.InputOrderBox[this.OrderCount] = 2;
        this.InputOrderCounter[this.OrderCount] = 1;
        this.SaladPre = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/salad");
        var Salad = Instantiate(this.SaladPre, new Vector2(-6.4f, OrderListPos[this.OrderCount]), Quaternion.identity);
        Salad.transform.localScale = new Vector2(0.3f, 0.3f);
        Salad.transform.parent = this.OrderList.transform;
        this.OrderCount++;
        if (this.OrderCount == 4)
        {
            this.OrderCount = 0;
            button.OrderButton.interactable = true;
        }
    }

    public void ListInSashimi()
    {
        this.InputOrderBox[this.OrderCount] = 3;
        this.InputOrderCounter[this.OrderCount] = 1;
        this.SashimiPre = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/sashimi");
        var Sashimi = Instantiate(this.SashimiPre, new Vector2(-6.4f, OrderListPos[this.OrderCount]), Quaternion.identity);
        Sashimi.transform.localScale = new Vector2(0.23f, 0.23f);
        Sashimi.transform.parent = this.OrderList.transform;
        this.OrderCount++;
        if (this.OrderCount == 4)
        {
            this.OrderCount = 0;
            button.OrderButton.interactable = true;
        }
    }

    void Start () {
        this.Counter1 = GameObject.Find("Order1/Counter1");
        this.Counter2 = GameObject.Find("Order2/Counter2");
        this.Counter3 = GameObject.Find("Order3/Counter3");
        this.Counter4 = GameObject.Find("Order4/Counter4");
        this.OrderList = GameObject.Find("MenuObject");
        button = GetComponent<ButtonController>();
	}
	
	void Update () {
        this.Counter1.GetComponent<Text>().text = "× " + this.InputOrderCounter[0].ToString();
        this.Counter2.GetComponent<Text>().text = "× " + this.InputOrderCounter[1].ToString();
        this.Counter3.GetComponent<Text>().text = "× " + this.InputOrderCounter[2].ToString();
        this.Counter4.GetComponent<Text>().text = "× " + this.InputOrderCounter[3].ToString();
    }
}
