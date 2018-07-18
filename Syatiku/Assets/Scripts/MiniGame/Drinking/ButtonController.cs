using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    //インスタンスの取得
    DrinkScene drink;
    Denmoku denmoku;

    public Button Remember;
    public Button Again;
    public Button NextGame;
    public Button SlideinButton;

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
    public void DrinkSceneButtonON()
    {
        //もう一度注文を聞くボタンが押されたかの判定
        if(AgainFlg)
        {
            Remember.gameObject.SetActive(true);
            Again.gameObject.SetActive(true);
            Remember.GetComponent<RectTransform>().localPosition = new Vector2(0, 50);
        }
        else
        {
            Remember.gameObject.SetActive(true);
            Remember.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        }
    }

    //飲み会シーンのボタンを非表示
    public void DrinkSceneButtonOFF()
    {
        Remember.gameObject.SetActive(false);
        Again.gameObject.SetActive(false);
    }

    //覚えたボタン
    public void RememberButton()
    {
        AgainFlg = true;
        DrinkSceneButtonOFF();

        SlideinButton.enabled = true;
    }

    //もう一度注文を聞くボタン
    public void AgainButton()
    {
        AgainFlg = false;
        DrinkSceneButtonOFF();
        drink.Order();
    }

    //スライドイン
    public void Slidin()
    {
        
        OtsumamiButton();
        OrderButton.interactable = false;
        Counter1.gameObject.SetActive(false);
        Counter2.gameObject.SetActive(false);
        Counter3.gameObject.SetActive(false);
        Counter4.gameObject.SetActive(false);
    }

    //メニュータブのおつまみボタン
    public void OtsumamiButton()
    {
        Otsumami.interactable = false;
        Drink.interactable = true;
        Dessert.interactable = true;
        MenuScrollbar.GetComponent<Scrollbar>().value = 1;
        Menu_Otsumami.gameObject.SetActive(true);
        Menu_Drink.gameObject.SetActive(false);
        Menu_Dessert.gameObject.SetActive(false);
    }

    //メニュータブの飲み物ボタン
    public void DrinkButton()
    {
        Otsumami.interactable = true;
        Drink.interactable = false;
        Dessert.interactable = true;
        MenuScrollbar.GetComponent<Scrollbar>().value = 1;
        Menu_Otsumami.gameObject.SetActive(false);
        Menu_Drink.gameObject.SetActive(true);
        Menu_Dessert.gameObject.SetActive(false);
    }

    //メニュータブのデザートボタン
    public void DessertButton()
    {
        Otsumami.interactable = true;
        Drink.interactable = true;
        Dessert.interactable = false;
        MenuScrollbar.GetComponent<Scrollbar>().value = 1;
        Menu_Otsumami.gameObject.SetActive(false);
        Menu_Drink.gameObject.SetActive(false);
        Menu_Dessert.gameObject.SetActive(true);
    }

    //メニューのやきとりボタン
    public void YakitoriButton()
    {
        if(OrderCount != 4)
        {
            Yakitori.interactable = false;
            denmoku.ListInYakitori();
            OrderListCounterON();
            OrderCount++;
        }
    }

    //メニューの酒ボタン
    public void SakeButton()
    {
        if (OrderCount != 4)
        {
            Sake.interactable = false;
            denmoku.ListInSake();
            OrderListCounterON();
            OrderCount++;
        }
    }
    
    //メニューのサラダボタン
    public void SaladButton()
    {
        if (OrderCount != 4)
        {
            Salad.interactable = false;
            denmoku.ListInSalad();
            OrderListCounterON();
            OrderCount++;
        }
    }

    //メニューの刺身ボタン
    public void SashimiButton()
    {
        if (OrderCount != 4)
        {
            Sashimi.interactable = false;
            denmoku.ListInSashimi();
            OrderListCounterON();
            OrderCount++;
        }
    }
    
    //注文ボタン
    public void Order()
    {
        ButtonReset();
        OrderCount = 0;
        denmoku.ResetList();
        DenmokuImage.GetComponent<RectTransform>().localPosition = new Vector2(-400, -850);
        drink.OrderAnswer();
        SlideinButton.enabled = false;
    }

    public void ButtonReset()
    {
        for(int i = 0; i < denmoku.InputOrderBox.Length; i++)
        {
            switch (denmoku.InputOrderBox[i])
            {
                case 0:
                    Yakitori.interactable = true;
                    break;
                case 1:
                    Sake.interactable = true;
                    break;
                case 2:
                    Salad.interactable = true;
                    break;
                case 3:
                    Sashimi.interactable = true;
                    break;
            }

        }
    }

    public void OrderListCounterON()
    {
        switch (OrderCount)
        {
            case 0:
                Counter1.gameObject.SetActive(true);
                break;
            case 1:
                Counter2.gameObject.SetActive(true);
                break;
            case 2:
                Counter3.gameObject.SetActive(true);
                break;
            case 3:
                Counter4.gameObject.SetActive(true);
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

    //次のゲーム(注文)を開始するボタン
    public void NextOrder()
    {
        NextGame.gameObject.SetActive(false);
        drink.OrderShuffle();
        drink.PosShuffle();
        drink.Order();
    }

    void Start () {
        drink = GetComponent<DrinkScene>();
        denmoku = GetComponent<Denmoku>();
        DenmokuImage = GameObject.Find("Denmoku");
        Menu_Otsumami = GameObject.Find("ScrollContent/Otsumami");
        Menu_Drink = GameObject.Find("ScrollContent/Drink");
        Menu_Dessert = GameObject.Find("ScrollContent/Dessert");
        MenuScrollbar = GameObject.Find("Scrollbar");
        Counter1 = GameObject.Find("Order1/Counter1");
        Counter2 = GameObject.Find("Order2/Counter2");
        Counter3 = GameObject.Find("Order3/Counter3");
        Counter4 = GameObject.Find("Order4/Counter4");
        DrinkSceneButtonOFF();
        SlideinButton.enabled = false;
	}
	
	
	void Update () {
        Counter1.GetComponent<Text>().text = "× " + denmoku.InputOrderCounter[0].ToString();
        Counter2.GetComponent<Text>().text = "× " + denmoku.InputOrderCounter[1].ToString();
        Counter3.GetComponent<Text>().text = "× " + denmoku.InputOrderCounter[2].ToString();
        Counter4.GetComponent<Text>().text = "× " + denmoku.InputOrderCounter[3].ToString();
    }
}
