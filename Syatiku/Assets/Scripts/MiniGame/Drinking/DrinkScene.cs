using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkScene : MonoBehaviour {

    GameObject menuObject;
    GameObject OrderCounter1;
    GameObject OrderCounter2;
    GameObject OrderCounter3;
    GameObject OrderCounter4;
    GameObject hukidashiObj;
    GameObject yakitoriObj;
    GameObject sakeObj;
    GameObject saladObj;
    GameObject sashimiObj;

    ButtonController button;


    //商品を格納する配列
    private int[] foodsBox = new int[4];

    //注文を表示するための配列
    public int[] OrderBox = new int[4];
    public int[] OrderCounter = new int[4];
    private float[] OrderPos = new float[4] {-6, -2, 2, 6};
    System.Random rnd = new System.Random();

    private int[] Num = new int[4];
    private int NumCounter = 0;

    //注文の配列の用意
    public void OrderShuffle()
    {
        //商品を格納する配列を用意&シャッフル
        for(int i = 0; i < this.foodsBox.Length; i++)
        {
            this.foodsBox[i] = i;
        }
        Common.Instance.Shuffle(foodsBox);
        
        //注文配列・個数配列・Num配列の用意
        for(int i = 0; i < OrderBox.Length; i++)
        {
            this.OrderBox[i] = this.foodsBox[i];
            this.OrderCounter[i] = Random.Range(1, 5);
            this.Num[i] = i;
        }
    }
    
    //表示する位置をシャッフル
    public void PosShuffle()
    {
        for (int i = 0; i < this.OrderPos.Length; i++)
        {
            int shuffle1 = rnd.Next(this.OrderPos.Length);
            int shuffle2 = rnd.Next(this.OrderPos.Length);
            float valueF = this.OrderPos[shuffle1];
            this.OrderPos[shuffle1] = this.OrderPos[shuffle2];
            this.OrderPos[shuffle2] = valueF;

            //Num配列のシャッフル
            int valueI = this.Num[shuffle1];
            this.Num[shuffle1] = this.Num[shuffle2];
            this.Num[shuffle2] = valueI;
        }
    }

    //注文商品を1個ずつランダムな位置に表示して消すを繰り返す
    private IEnumerator OrderMethod()
    {
        for (int i = 0; i < this.OrderBox.Length; i++)
        {
            yield return new WaitForSeconds(1.0f);
            //吹き出しを表示
            var hukidashi = Instantiate(this.hukidashiObj, new Vector2(OrderPos[i], 2.5f), Quaternion.identity);
            hukidashi.transform.localScale = new Vector2(1.12f, 1.12f);
            hukidashi.transform.parent = menuObject.transform;

            //注文数の表示
            OrderNum();

            switch (OrderBox[i])
            {
                //やきとりを表示
                case 0:
                    this.yakitoriObj = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/yakitori");
                    var yakitori = Instantiate(this.yakitoriObj, new Vector2(OrderPos[i], 2.8f), Quaternion.identity);
                    yakitori.transform.localScale = new Vector2(0.7f, 0.7f);
                    yakitori.transform.parent = this.menuObject.transform;
                    break;
                //酒を表示
                case 1:
                    this.sakeObj = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/sake");
                    var sake = Instantiate(this.sakeObj, new Vector2(OrderPos[i], 2.8f), Quaternion.identity);
                    sake.transform.localScale = new Vector2(0.25f, 0.25f);
                    sake.transform.parent = this.menuObject.transform;
                    break;
                //サラダを表示
                case 2:
                    saladObj = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/salad");
                    var salad = Instantiate(saladObj, new Vector2(OrderPos[i], 2.8f), Quaternion.identity);
                    salad.transform.localScale = new Vector2(0.35f, 0.35f);
                    salad.transform.parent = menuObject.transform;
                    break;
                //刺身を表示
                default:
                    sashimiObj = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/sashimi");
                    var sashimi = Instantiate(sashimiObj, new Vector2(OrderPos[i], 2.8f), Quaternion.identity);
                    sashimi.transform.localScale = new Vector2(0.25f, 0.25f);
                    sashimi.transform.parent = menuObject.transform;
                    break;
            }
            //2秒後に表示された吹き出しと商品を消す
            yield return new WaitForSeconds(2.0f);
            Delete();
            OrderCounterOFF();
        }
        yield return new WaitForSeconds(1.0f);
        button.DrinkSceneButtonON();
        NumCounter = 0;

    }
  
    public void Order()
    {
        StartCoroutine("OrderMethod");
    }

    public void Delete()
    {
        var Delete = this.menuObject.transform;
        for (int i = 0; i < Delete.childCount; i++)
        {
            Destroy(Delete.GetChild(i).gameObject);
        }
    }

    //注文の答えの表示
    public void OrderAnswer()
    {
        for (int i = 0; i < OrderBox.Length; i++)
        {
            //吹き出しを表示
            var hukidashi = Instantiate(hukidashiObj, new Vector2(OrderPos[i], 2.5f), Quaternion.identity);
            hukidashi.transform.localScale = new Vector2(1.12f, 1.12f);
            hukidashi.transform.parent = this.menuObject.transform;
            
        }
    }
    
    public void OrderNum()
    {
        switch (Num[NumCounter])
        {
            case 0:
                this.OrderCounter1.gameObject.SetActive(true);
                this.OrderCounter1.GetComponent<Text>().text = "× " + this.OrderCounter[NumCounter].ToString();
                break;
            case 1:
                this.OrderCounter2.gameObject.SetActive(true);
                this.OrderCounter2.GetComponent<Text>().text = "× " + this.OrderCounter[NumCounter].ToString();
                break;
            case 2:
                this.OrderCounter3.gameObject.SetActive(true);
                this.OrderCounter3.GetComponent<Text>().text = "× " + this.OrderCounter[NumCounter].ToString();
                break;
            default:
                this.OrderCounter4.gameObject.SetActive(true);
                this.OrderCounter4.GetComponent<Text>().text = "× " + this.OrderCounter[NumCounter].ToString();
                break;
        }
        NumCounter++;
    }

    public void OrderCounterON()
    {
        this.OrderCounter1.gameObject.SetActive(true);
        this.OrderCounter2.gameObject.SetActive(true);
        this.OrderCounter3.gameObject.SetActive(true);
        this.OrderCounter4.gameObject.SetActive(true);
    }

    public void OrderCounterOFF()
    {
        this.OrderCounter1.gameObject.SetActive(false);
        this.OrderCounter2.gameObject.SetActive(false);
        this.OrderCounter3.gameObject.SetActive(false);
        this.OrderCounter4.gameObject.SetActive(false);
    }

	void Start () {
        this.button = GetComponent<ButtonController>();
        this.menuObject = GameObject.Find("MenuObject");
        this.OrderCounter1 = GameObject.Find("DrinkingCounter/OrderCounter1");
        this.OrderCounter2 = GameObject.Find("DrinkingCounter/OrderCounter2");
        this.OrderCounter3 = GameObject.Find("DrinkingCounter/OrderCounter3");
        this.OrderCounter4 = GameObject.Find("DrinkingCounter/OrderCounter4");
        this.hukidashiObj = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/hukidashi");
        

        this.OrderShuffle();
        this.PosShuffle();
        this.Order();
        this.OrderCounterOFF();
    }
	
	void Update () {

	}

    
}


