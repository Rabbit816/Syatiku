using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    //インスタンスの取得
    DrinkScene drink;
    Denmoku denmoku;
    Merter meter;

    public Button Remember;
    public Button Again;

    //メニュータブのボタン
    public Button Otsumami;
    public Button Drink;
    public Button Dessert;

    //メニューのボタン
    public Button OrderButton;
    public Button Yakitori;
    public Button Sake;
    public Button Salad;
    public Button Sashimi;


    GameObject DenmokuImage;
    GameObject Menu_Otsumami, Menu_Drink, Menu_Dessert;
    GameObject MenuScrollbar;
    GameObject Counter1, Counter2, Counter3, Counter4;

    private int OrderCount = 0;
    private bool AgainFlg = true;

    //飲み会シーンのボタンを表示
    public void DrinkSceneButton(bool SwitchFlg)
    {
        if (SwitchFlg)
        {
            //もう一度注文を聞くボタンが押されたかの判定
            if (this.AgainFlg)
            {
                this.Remember.gameObject.SetActive(true);
                this.Again.gameObject.SetActive(true);
                this.Remember.GetComponent<RectTransform>().localPosition = new Vector2(0, 50);
            }
            else
            {
                this.Remember.gameObject.SetActive(true);
                this.Remember.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            }
        }
        else
        {
            this.Remember.gameObject.SetActive(false);
            this.Again.gameObject.SetActive(false);
        }
    }

    //覚えたボタン
    public void RememberButton()
    {
        this.AgainFlg = true;
        this.DrinkSceneButton(false);
        this.OtsumamiButton();
        this.OrderButton.interactable = false;
        this.Counter1.gameObject.SetActive(false);
        this.Counter2.gameObject.SetActive(false);
        this.Counter3.gameObject.SetActive(false);
        this.Counter4.gameObject.SetActive(false);
        meter.MeterON = true;
    }

    //もう一度注文を聞くボタン
    public void AgainButton()
    {
        this.AgainFlg = false;
        this.DrinkSceneButton(false);
        drink.Order();
    }

    //メニュータブのおつまみボタン
    public void OtsumamiButton()
    {
        this.Otsumami.interactable = false;
        this.Drink.interactable = true;
        this.Dessert.interactable = true;
        this.MenuScrollbar.GetComponent<Scrollbar>().value = 1;
        this.Menu_Otsumami.gameObject.SetActive(true);
        this.Menu_Drink.gameObject.SetActive(false);
        this.Menu_Dessert.gameObject.SetActive(false);
    }

    //メニュータブの飲み物ボタン
    public void DrinkButton()
    {
        this.Otsumami.interactable = true;
        this.Drink.interactable = false;
        this.Dessert.interactable = true;
        this.MenuScrollbar.GetComponent<Scrollbar>().value = 1;
        this.Menu_Otsumami.gameObject.SetActive(false);
        this.Menu_Drink.gameObject.SetActive(true);
        this.Menu_Dessert.gameObject.SetActive(false);
    }

    //メニュータブのデザートボタン
    public void DessertButton()
    {
        this.Otsumami.interactable = true;
        this.Drink.interactable = true;
        this.Dessert.interactable = false;
        this.MenuScrollbar.GetComponent<Scrollbar>().value = 1;
        this.Menu_Otsumami.gameObject.SetActive(false);
        this.Menu_Drink.gameObject.SetActive(false);
        this.Menu_Dessert.gameObject.SetActive(true);
    }

    //メニューのやきとりボタン
    public void YakitoriButton()
    {
        if(this.OrderCount != 4)
        {
            this.Yakitori.interactable = false;
            denmoku.ListInYakitori();
            this.OrderListCounterON();
            this.OrderCount++;
        }
    }

    //メニューの酒ボタン
    public void SakeButton()
    {
        if (this.OrderCount != 4)
        {
            this.Sake.interactable = false;
            denmoku.ListInSake();
            this.OrderListCounterON();
            this.OrderCount++;
        }
    }
    
    //メニューのサラダボタン
    public void SaladButton()
    {
        if (this.OrderCount != 4)
        {
            this.Salad.interactable = false;
            denmoku.ListInSalad();
            this.OrderListCounterON();
            this.OrderCount++;
        }
    }

    //メニューの刺身ボタン
    public void SashimiButton()
    {
        if (this.OrderCount != 4)
        {
            this.Sashimi.interactable = false;
            denmoku.ListInSashimi();
            this.OrderListCounterON();
            this.OrderCount++;
        }
    }
    
    //注文ボタン
    public void Order()
    {
        this.ButtonReset();
        this.OrderCount = 0;
        drink.Delete();
        this.DenmokuImage.GetComponent<RectTransform>().localPosition = new Vector2(-400, -850);
        drink.Answer();
        meter.MeterON = false;
        meter.value = 1f;
    }
    
    public void ButtonReset()
    {
        for(int i = 0; i < denmoku.InputOrderBox.Length; i++)
        {
            switch (denmoku.InputOrderBox[i])
            {
                case 0:
                    this.Yakitori.interactable = true;
                    break;
                case 1:
                    this.Sake.interactable = true;
                    break;
                case 2:
                    this.Salad.interactable = true;
                    break;
                case 3:
                    this.Sashimi.interactable = true;
                    break;
            }
        }
    }

    public void OrderListCounterON()
    {
        switch (this.OrderCount)
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
        }
    }


    // 注文欄の増減ボタン
    public void OrderListPlus1()
    {
        if(denmoku.InputOrderCounter[0] < 4)
        {
            denmoku.InputOrderCounter[0]++;
        }
    }

    public void OrderListMinus1()
    {
        if(denmoku.InputOrderCounter[0] > 1)
        {
            denmoku.InputOrderCounter[0]--;
        }
    }

    public void OrderListPlus2()
    {
        if (denmoku.InputOrderCounter[1] < 4)
        {
            denmoku.InputOrderCounter[1]++;
        }
    }

    public void OrderListMinus2()
    {
        if (denmoku.InputOrderCounter[1] > 1)
        {
            denmoku.InputOrderCounter[1]--;
        }
    }

    public void OrderListPlus3()
    {
        if (denmoku.InputOrderCounter[2] < 4)
        {
            denmoku.InputOrderCounter[2]++;
        }
    }

    public void OrderListMinus3()
    {
        if (denmoku.InputOrderCounter[2] > 1)
        {
            denmoku.InputOrderCounter[2]--;
        }
    }

    public void OrderListPlus4()
    {
        if (denmoku.InputOrderCounter[3] < 4)
        {
            denmoku.InputOrderCounter[3]++;
        }
    }

    public void OrderListMinus4()
    {
        if (denmoku.InputOrderCounter[3] > 1)
        {
            denmoku.InputOrderCounter[3]--;
        }
    }

    void Start () {
        drink = GetComponent<DrinkScene>();
        denmoku = GetComponent<Denmoku>();
        meter = GetComponent<Merter>();
        this.DenmokuImage = GameObject.Find("Denmoku");
        this.Menu_Otsumami = GameObject.Find("ScrollContent/Otsumami");
        this.Menu_Drink = GameObject.Find("ScrollContent/Drink");
        this.Menu_Dessert = GameObject.Find("ScrollContent/Dessert");
        this.MenuScrollbar = GameObject.Find("Scrollbar");
        this.Counter1 = GameObject.Find("Order1/Counter1");
        this.Counter2 = GameObject.Find("Order2/Counter2");
        this.Counter3 = GameObject.Find("Order3/Counter3");
        this.Counter4 = GameObject.Find("Order4/Counter4");
        this.DrinkSceneButton(false);
        this.DenmokuImage.GetComponent<RectTransform>().localPosition = new Vector2(-400, -850);
    }
	
	
	void Update () {
        this.Counter1.GetComponent<Text>().text = "× " + denmoku.InputOrderCounter[0].ToString();
        this.Counter2.GetComponent<Text>().text = "× " + denmoku.InputOrderCounter[1].ToString();
        this.Counter3.GetComponent<Text>().text = "× " + denmoku.InputOrderCounter[2].ToString();
        this.Counter4.GetComponent<Text>().text = "× " + denmoku.InputOrderCounter[3].ToString();
    }
}
