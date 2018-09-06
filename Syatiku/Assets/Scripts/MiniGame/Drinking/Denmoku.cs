using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    // スクリプトのインスタンスの取得
    DrinkScene drink;
    ButtonController button;

    // 注文する商品の順番を保存する配列
    [HideInInspector]
    public int[] InputOrderBox = new int[4];

    // 注文する商品の個数を保存する配列
    [HideInInspector]
    public int[] InputOrderCounter = new int[4];

    // 注文リストの個数
    [SerializeField]
    Text[] Counter;

    // 注文リストの個数表示
    [SerializeField]
    GameObject[] CounterButton;

    private int Num;

    private float[] Order_List = new float[4] {4.25f, 2.94f, 1.57f, 0.15f};

    // 注文リストの有効・無効を管理する
    public void OrderListController(bool b)
    {
        if (b)
        {
            this.CounterButton[this.Num].gameObject.SetActive(true);
        }
        else
        {
            this.CounterButton[button.CounterNum].gameObject.SetActive(false);
        }
    }

    // 注文リストを初期化する
    public void MenuListOFF()
    {
        for(int i = 0; i < this.CounterButton.Length; i++)
        {
            this.CounterButton[i].gameObject.SetActive(false);
        }
    }
    // 注文リストに注文した商品を表示する
    public void ListInMenu(int MenuID)
    {
        // 注文リストに表示する場所を決める
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

        // 注文リストに注文した商品を表示する
        var MenuObject = Instantiate(drink.MenuList[MenuID], new Vector2(5.0f, this.Order_List[this.Num]), Quaternion.identity);
        MenuObject.transform.localScale = new Vector2(0.37f, 0.37f);
        MenuObject.transform.parent = drink.menuObject.transform;
    }

    void Start () {
        button = GetComponent<ButtonController>();
        drink = GetComponent<DrinkScene>();
	}
	
	void Update () {
        // 注文リストに注文数を表示
        this.Counter[0].text = "× " + this.InputOrderCounter[0].ToString();
        this.Counter[1].text = "× " + this.InputOrderCounter[1].ToString();
        this.Counter[2].text = "× " + this.InputOrderCounter[2].ToString();
        this.Counter[3].text = "× " + this.InputOrderCounter[3].ToString();
    }
}
