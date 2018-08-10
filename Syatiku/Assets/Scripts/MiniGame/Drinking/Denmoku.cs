using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    DrinkScene drink;
    ButtonController button;

    //デンモク内で使う配列・変数
    [HideInInspector] public int[] InputOrderBox = new int[4];
    [HideInInspector] public int[] InputOrderCounter = new int[4];
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
        drink.LoadPrefab(this.InputOrderBox[this.OrderCount]);
        var Yakitori = Instantiate(drink.yakitoriObj, new Vector2(-6.4f, OrderListPos[this.OrderCount]), Quaternion.identity);
        Yakitori.transform.localScale = new Vector2(0.6f, 0.6f);
        Yakitori.transform.parent = drink.menuObject.transform;
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
        drink.LoadPrefab(this.InputOrderBox[this.OrderCount]);
        var Sake = Instantiate(drink.sakeObj, new Vector2(-6.4f, OrderListPos[this.OrderCount]), Quaternion.identity);
        Sake.transform.localScale = new Vector2(0.23f, 0.23f);
        Sake.transform.parent = drink.menuObject.transform;
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
        drink.LoadPrefab(this.InputOrderBox[this.OrderCount]);
        var Salad = Instantiate(drink.saladObj, new Vector2(-6.4f, OrderListPos[this.OrderCount]), Quaternion.identity);
        Salad.transform.localScale = new Vector2(0.3f, 0.3f);
        Salad.transform.parent = drink.menuObject.transform;
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
        drink.LoadPrefab(this.InputOrderBox[this.OrderCount]);
        var Sashimi = Instantiate(drink.sashimiObj, new Vector2(-6.4f, OrderListPos[this.OrderCount]), Quaternion.identity);
        Sashimi.transform.localScale = new Vector2(0.23f, 0.23f);
        Sashimi.transform.parent = drink.menuObject.transform;
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
        drink = GetComponent<DrinkScene>();
        button = GetComponent<ButtonController>();
	}
	
	void Update () {
        this.Counter1.GetComponent<Text>().text = "× " + this.InputOrderCounter[0].ToString();
        this.Counter2.GetComponent<Text>().text = "× " + this.InputOrderCounter[1].ToString();
        this.Counter3.GetComponent<Text>().text = "× " + this.InputOrderCounter[2].ToString();
        this.Counter4.GetComponent<Text>().text = "× " + this.InputOrderCounter[3].ToString();
    }
}
