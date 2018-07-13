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

    private int OrderCount = 0;
    private int AgainCounter = 0;
    public int[] OrderBox = new int[4];

    //飲み会シーンのボタンを表示
    public void DrinkSceneButtonON()
    {
        //もう一度注文を聞くボタンが押されたかの判定
        if(AgainCounter == 0)
        {
            Remember.gameObject.SetActive(true);
            Again.gameObject.SetActive(true);
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
        AgainCounter = 0;
        DrinkSceneButtonOFF();

        //仮の処理
        DenmokuImage.GetComponent<RectTransform>().localPosition = new Vector2(-400, -250);

        OtsumamiButton();
        OrderButton.interactable = false;
    }

    //もう一度注文を聞くボタン
    public void AgainButton()
    {
        AgainCounter = 1;
        DrinkSceneButtonOFF();
        drink.Order();
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
            OrderCount++;
        }
    }
    /// <summary>
    /// 注文
    /// </summary>
    public void Order()
    {
        ButtonReset();
        OrderCount = 0;
        denmoku.ResetList();
        DenmokuImage.GetComponent<RectTransform>().localPosition = new Vector2(-400, -800);
        drink.OrderAnswer();
    }

    public void ButtonReset()
    {
        for(int i = 0; i < OrderBox.Length; i++)
        {
            switch (OrderBox[i])
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
                default:
                    Sashimi.interactable = true;
                    break;
            }

        }
    }

    void Start () {
        drink = GetComponent<DrinkScene>();
        denmoku = GetComponent<Denmoku>();
        DenmokuImage = GameObject.Find("Denmoku");
        Menu_Otsumami = GameObject.Find("ScrollContent/Otsumami");
        Menu_Drink = GameObject.Find("ScrollContent/Drink");
        Menu_Dessert = GameObject.Find("ScrollContent/Dessert");
        MenuScrollbar = GameObject.Find("Scrollbar");
        DrinkSceneButtonOFF();
	}
	
	
	void Update () {
		
	}
}
