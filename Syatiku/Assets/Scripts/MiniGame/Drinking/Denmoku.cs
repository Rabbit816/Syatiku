using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    // スクリプトのインスタンスの取得
    DrinkScene drink;
    ButtonController button;

    // 注文する商品のIDを保存する配列
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

    private float[] Order_List = new float[4] {3.25f, 1.94f, 0.57f, -0.85f};

    GameObject DeleteList_Menu;

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
        this.CounterButton[this.Num].gameObject.SetActive(true);

        // 注文リストに注文した商品を表示する
        var MenuObject = Instantiate(drink.MenuList[MenuID], new Vector2(5.0f, this.Order_List[this.Num]), Quaternion.identity);
        MenuObject.transform.localScale = new Vector2(0.37f, 0.37f);
        MenuObject.transform.parent = drink.menuObject.transform;
    }

    // 注文リストを整理するメソッド
    public void OrderListArrange()
    {
        int LoopLimit = 0;
        int LoopCounter = button.CounterNum;

        for (int i = 1; i < this.Counter.Length; i++)
        {
            if(this.InputOrderBox[i] >= 0)
            {
                LoopLimit++;
            }
            else
            {
                break;
            }
        }
        
        if(LoopCounter < LoopLimit)
        {
            while (LoopCounter < LoopLimit)
            {
                this.DeleteMenu(this.InputOrderBox[LoopCounter]);
                this.InputOrderBox[LoopCounter] = this.InputOrderBox[LoopCounter + 1];
                this.InputOrderCounter[LoopCounter] = this.InputOrderCounter[LoopCounter + 1];
                var MenuObject = Instantiate(drink.MenuList[this.InputOrderBox[LoopCounter + 1]], new Vector2(5.0f, this.Order_List[LoopCounter]), Quaternion.identity);
                MenuObject.transform.localScale = new Vector2(0.37f, 0.37f);
                MenuObject.transform.parent = drink.menuObject.transform;
                LoopCounter++;
            }
        }
        this.DeleteMenu(this.InputOrderBox[LoopCounter]);
        this.CounterButton[LoopCounter].gameObject.SetActive(false);
        this.InputOrderBox[LoopCounter] = -1;
        this.InputOrderCounter[LoopCounter] = 0;
    }

    // 注文リストに表示された商品を消すメソッド
    public void DeleteMenu(int i)
    {
        switch (i)
        {
            // 枝豆を削除
            case 0:
                this.DeleteList_Menu = GameObject.Find("MenuObject/EdamamePrefab(Clone)");
                break;

            // 卵焼きを削除
            case 1:
                this.DeleteList_Menu = GameObject.Find("MenuObject/TamagoyakiPrefab(Clone)");
                break;

            // から揚げを削除
            case 2:
                this.DeleteList_Menu = GameObject.Find("MenuObject/KaraagePrefab(Clone)");
                break;

            // サラダを削除
            case 3:
                this.DeleteList_Menu = GameObject.Find("MenuObject/SaladPrefab(Clone)");
                break;

            default:
                Debug.Log("DeleteMenu : エラー");
                break;
        }
        Destroy(this.DeleteList_Menu);
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
