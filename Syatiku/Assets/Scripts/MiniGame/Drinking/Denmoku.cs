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

    private float[] OrderListPos = new float[4] { 3.9f, 1.75f, -0.45f, -2.55f };

    [SerializeField]
    GameObject[] CounterList;

    private int Num;
    private int MenuID;

    public void OrderListCounter(bool b)
    {
        if (b)
        {
            this.CounterList[this.Num].gameObject.SetActive(true);
        }
        else
        {
            this.CounterList[button.CounterNum].gameObject.SetActive(false);
        }
    }

    public void CounterOFF()
    {
        for(int i = 0; i < this.CounterList.Length; i++)
        {
            this.CounterList[i].gameObject.SetActive(false);
        }
    }


    public void ListInYakitori()
    {
        MenuID = 0;
        this.ListCheck();
        var Yakitori = Instantiate(drink.MenuList[this.MenuID], new Vector2(-6.4f, OrderListPos[this.Num]), Quaternion.identity);
        Yakitori.transform.localScale = new Vector2(0.6f, 0.6f);
        Yakitori.transform.parent = drink.menuObject.transform;
    }

    public void ListInSake()
    {
        MenuID = 1;
        this.ListCheck();
        var Sake = Instantiate(drink.MenuList[this.MenuID], new Vector2(-6.4f, OrderListPos[this.Num]), Quaternion.identity);
        Sake.transform.localScale = new Vector2(0.23f, 0.23f);
        Sake.transform.parent = drink.menuObject.transform;
    }

    public void ListInSalad()
    {
        MenuID = 2;
        this.ListCheck();
        var Salad = Instantiate(drink.MenuList[this.MenuID], new Vector2(-6.4f, OrderListPos[this.Num]), Quaternion.identity);
        Salad.transform.localScale = new Vector2(0.3f, 0.3f);
        Salad.transform.parent = drink.menuObject.transform;
    }

    public void ListInSashimi()
    {
        MenuID = 3;
        this.ListCheck();
        var Sashimi = Instantiate(drink.MenuList[this.MenuID], new Vector2(-6.4f, OrderListPos[this.Num]), Quaternion.identity);
        Sashimi.transform.localScale = new Vector2(0.23f, 0.23f);
        Sashimi.transform.parent = drink.menuObject.transform;
    }

    public void ListOutMenu()
    {
        switch (this.InputOrderBox[button.CounterNum])
        {
            case 0:
                GameObject obj1 = GameObject.Find("MenuObject/yakitori(Clone)");
                Destroy(obj1);
                break;
            case 1:
                GameObject obj2 = GameObject.Find("MenuObject/sake(Clone)");
                Destroy(obj2);
                break;
            case 2:
                GameObject obj3 = GameObject.Find("MenuObject/salad(Clone)");
                Destroy(obj3);
                break;
            case 3:
                GameObject obj4 = GameObject.Find("MenuObject/sashimi(Clone)");
                Destroy(obj4);
                break;
            default:
                Debug.Log("ListoutMenu : エラー");
                break;
        }
    }

    public void ListCheck()
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
    }

    void Start () {
        drink = GetComponent<DrinkScene>();
        button = GetComponent<ButtonController>();
	}
	
	void Update () {
        this.CounterList[0].GetComponent<Text>().text = "× " + this.InputOrderCounter[0].ToString();
        this.CounterList[1].GetComponent<Text>().text = "× " + this.InputOrderCounter[1].ToString();
        this.CounterList[2].GetComponent<Text>().text = "× " + this.InputOrderCounter[2].ToString();
        this.CounterList[3].GetComponent<Text>().text = "× " + this.InputOrderCounter[3].ToString();
    }
}
