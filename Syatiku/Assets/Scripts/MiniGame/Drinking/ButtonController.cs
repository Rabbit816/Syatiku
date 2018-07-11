using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    //インスタンスの取得
    public DrinkScene drink;
    public Denmoku denmoku;

    public Button Remember;
    public Button Again;
    public Image DenmokuImage;

    private int AgainCounter = 0;

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
        DenmokuImage.GetComponent<RectTransform>().localPosition = new Vector2(-400, -250);
    }

    //もう一度注文を聞くボタン
    public void AgainButton()
    {
        AgainCounter = 1;
        DrinkSceneButtonOFF();
        drink.Order();
    }
	
	void Start () {
        DrinkSceneButtonOFF();
	}
	
	
	void Update () {
		
	}
}
