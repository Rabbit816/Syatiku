﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    //インスタンスの取得
    DrinkScene drink;
    Denmoku denmoku;
    DenmokuMeter meter;

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

    [SerializeField]
    GameObject DenmokuImage;

    [SerializeField]
    GameObject Menu_Otsumami, Menu_Drink, Menu_Dessert;

    [SerializeField]
    GameObject MenuScrollbar;

    private int OrderCount = 0;
    private bool AgainFlg = true;
    [HideInInspector]
    public int CounterNum;

    //飲み会シーンのボタンを表示
    public void DrinkSceneButton(bool b)
    {
        if (b)
        {
            this.Remember.gameObject.SetActive(b);
            
            //もう一度注文を聞くボタンが押されたかの判定
            if (this.AgainFlg)
            {
                this.Again.gameObject.SetActive(b);
                this.Remember.GetComponent<RectTransform>().localPosition = new Vector2(0, 50);
            }
            else
            {
                this.Remember.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            }
        }
        else
        {
            this.Remember.gameObject.SetActive(b);
            this.Again.gameObject.SetActive(b);
        }
    }

    //覚えたボタン
    public void RememberButton()
    {
        this.AgainFlg = true;
        this.DrinkSceneButton(false);
        this.OtsumamiButton();
        this.OrderButton.interactable = false;
        denmoku.CounterOFF();
        meter.TimeMeterFlg = true;
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
        this.MenuTabControll(false, true, true, true, false, false);
    }

    //メニュータブの飲み物ボタン
    public void DrinkButton()
    {
        this.MenuTabControll(true, false, true, false, true, false);
    }

    //メニュータブのデザートボタン
    public void DessertButton()
    {
        this.MenuTabControll(true, true, false, false, false, true);
    }

    public void MenuTabControll(bool b1, bool b2, bool b3, bool b4, bool b5, bool b6)
    {
        this.MenuScrollbar.GetComponent<Scrollbar>().value = 1;
        this.Otsumami.interactable = b1;
        this.Drink.interactable = b2;
        this.Dessert.interactable = b3;
        this.Menu_Otsumami.gameObject.SetActive(b4);
        this.Menu_Drink.gameObject.SetActive(b5);
        this.Menu_Dessert.gameObject.SetActive(b6);
    }

    //メニューのやきとりボタン
    public void YakitoriButton()
    {
        if(this.OrderCount != 4)
        {
            this.Yakitori.interactable = false;
            denmoku.ListInYakitori();
            denmoku.OrderListCounter(true);
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
            denmoku.OrderListCounter(true);
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
            denmoku.OrderListCounter(true);
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
            denmoku.OrderListCounter(true);
            this.OrderCount++;
        }
    }
    
    //注文ボタン
    public void Order()
    {
        this.ButtonReset();
        this.OrderCount = 0;
        drink.Delete();
        this.DenmokuImage.GetComponent<RectTransform>().localPosition = new Vector2(-970, -2000);
        drink.Answer();
        meter.TimeMeterFlg = false;
        meter.TimeMeter.value = meter.Timer;
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
                default:
                    Debug.Log("ButtonReset : エラー");
                    break;
            }
        }
    }

    public void CounterController(bool b)
    {
        if (b)
        {
            if (denmoku.InputOrderCounter[CounterNum] < 4)
            {
                denmoku.InputOrderCounter[CounterNum]++;
            }
        }
        else
        {
            if (denmoku.InputOrderCounter[CounterNum] > 1)
            {
                denmoku.InputOrderCounter[CounterNum]--;
            }
            else
            {
                switch (denmoku.InputOrderBox[this.CounterNum])
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
                    default:
                        Debug.Log("CounterController : エラー");
                        break;
                }
                denmoku.OrderListCounter(false);
                denmoku.ListOutMenu();
                denmoku.InputOrderBox[this.CounterNum] = -1;
                denmoku.InputOrderCounter[this.CounterNum] = 0;
                if(this.OrderCount > 0)
                {
                    this.OrderCount--;
                }
            }
        }
    }

    public void CounterButton(int i)
    {
        if(CounterNum != i)
        {
            CounterNum = i;
        }
    }

    void Start () {
        drink = GetComponent<DrinkScene>();
        denmoku = GetComponent<Denmoku>();
        meter = GetComponent<DenmokuMeter>();
        this.DrinkSceneButton(false);
        this.DenmokuImage.GetComponent<RectTransform>().localPosition = new Vector2(-970, -2000);
    }
	
	
	void Update () {

        if (this.OrderCount == 4)
        {
            this.OrderButton.interactable = true;
        }
        else
        {
            this.OrderButton.interactable = false;
        }
    }
}
