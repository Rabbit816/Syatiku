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

    [SerializeField]
    GameObject hukidashiObj;

    [SerializeField]
    GameObject yakitoriObj;

    [SerializeField]
    GameObject sakeObj;

    [SerializeField]
    GameObject saladObj;

    [SerializeField]
    GameObject sashimiObj;


    //商品を格納する配列
    private int[] foodsBox = new int[4];

    //注文を表示するための配列
    public int[] OrderBox = new int[4];
    public int[] OrderCounter = new int[4];
    public float[] OrderPos = new float[4] {-6.37f, -2.12f, 2.12f, 6.37f};
    System.Random rnd = new System.Random();

    private int[] Num = new int[4];
    private int NumCounter = 0;

    //注文の配列の用意
    public void OrderShuffle()
    {
        //商品を格納する配列を用意&シャッフル
        for(int i = 0; i < foodsBox.Length; i++)
        {
            foodsBox[i] = i;
        }
        for(int i = 0; i < foodsBox.Length; i++)
        {
            int shuffle1 = rnd.Next(foodsBox.Length);
            int shuffle2 = rnd.Next(foodsBox.Length);
            int value = foodsBox[shuffle1];
            foodsBox[shuffle1] = foodsBox[shuffle2];
            foodsBox[shuffle2] = value;
        }
        
        //注文配列・個数配列・Num配列の用意
        for(int i = 0; i < OrderBox.Length; i++)
        {
            OrderBox[i] = foodsBox[i];
            OrderCounter[i] = Random.Range(1, 5);
            Num[i] = i;
        }
    }
    
    //表示する位置をシャッフル
    public void PosShuffle()
    {
        for (int i = 0; i < OrderPos.Length; i++)
        {
            int shuffle1 = rnd.Next(OrderPos.Length);
            int shuffle2 = rnd.Next(OrderPos.Length);
            float value = OrderPos[shuffle1];
            OrderPos[shuffle1] = OrderPos[shuffle2];
            OrderPos[shuffle2] = value;

            //Num配列のシャッフル
            int val = Num[shuffle1];
            Num[shuffle1] = Num[shuffle2];
            Num[shuffle2] = val;
        }
    }

    //注文商品を1個ずつランダムな位置に表示して消すを繰り返す
    private IEnumerator OrderMethod()
    {
        for (int i = 0; i < OrderBox.Length; i++)
        {
            yield return new WaitForSeconds(1.0f);
            //吹き出しを表示
            var hukidashi = Instantiate(hukidashiObj, new Vector2(OrderPos[i], 3.3f), Quaternion.identity);
            hukidashi.transform.localScale = new Vector2(1.25f, 1.25f);
            hukidashi.transform.parent = menuObject.transform;

            //注文数の表示
            OrderNum();

            switch (OrderBox[i])
            {
                //やきとりを表示
                case 0:
                    var yakitori = Instantiate(yakitoriObj, new Vector2(OrderPos[i], 3.6f), Quaternion.identity);
                    yakitori.transform.localScale = new Vector2(0.8f, 0.8f);
                    yakitori.transform.parent = menuObject.transform;
                    break;
                //酒を表示
                case 1:
                    var sake = Instantiate(sakeObj, new Vector2(OrderPos[i], 3.6f), Quaternion.identity);
                    sake.transform.localScale = new Vector2(0.3f, 0.3f);
                    sake.transform.parent = menuObject.transform;
                    break;
                //サラダを表示
                case 2:
                    var salad = Instantiate(saladObj, new Vector2(OrderPos[i], 3.6f), Quaternion.identity);
                    salad.transform.localScale = new Vector2(0.35f, 0.35f);
                    salad.transform.parent = menuObject.transform;
                    break;
                //刺身を表示
                default:
                    var sashimi = Instantiate(sashimiObj, new Vector2(OrderPos[i], 3.6f), Quaternion.identity);
                    sashimi.transform.localScale = new Vector2(0.3f, 0.3f);
                    sashimi.transform.parent = menuObject.transform;
                    break;
            }
            //2秒後に表示された吹き出しと商品を消す
            yield return new WaitForSeconds(2.0f);
            Delete();
            OrderCounterOFF();
        }
        yield return new WaitForSeconds(1.0f);
        ButtonController button = GetComponent<ButtonController>();
        button.DrinkSceneButtonON();
        NumCounter = 0;

    }
  
    public void Order()
    {
        StartCoroutine("OrderMethod");
    }

    public void Delete()
    {
        var Delete = menuObject.transform;
        for (int i = 0; i < Delete.childCount; i++)
        {
            Destroy(Delete.GetChild(i).gameObject);
        }
    }

    //注文の答えの表示
    public void OrderAnswer()
    {
        OrderCounterON();

        for (int i = 0; i < OrderBox.Length; i++)
        {
            //吹き出しを表示
            var hukidashi = Instantiate(hukidashiObj, new Vector2(OrderPos[i], 3.3f), Quaternion.identity);
            hukidashi.transform.localScale = new Vector2(1.25f, 1.25f);
            hukidashi.transform.parent = menuObject.transform;
            switch (OrderBox[i])
            {
                //やきとりを表示
                case 0:
                    var yakitori = Instantiate(yakitoriObj, new Vector2(OrderPos[i], 3.6f), Quaternion.identity);
                    yakitori.transform.localScale = new Vector2(0.8f, 0.8f);
                    yakitori.transform.parent = menuObject.transform;
                    break;
                //酒を表示
                case 1:
                    var sake = Instantiate(sakeObj, new Vector2(OrderPos[i], 3.6f), Quaternion.identity);
                    sake.transform.localScale = new Vector2(0.3f, 0.3f);
                    sake.transform.parent = menuObject.transform;
                    break;
                //サラダを表示
                case 2:
                    var salad = Instantiate(saladObj, new Vector2(OrderPos[i], 3.6f), Quaternion.identity);
                    salad.transform.localScale = new Vector2(0.35f, 0.35f);
                    salad.transform.parent = menuObject.transform;
                    break;
                //刺身を表示
                default:
                    var sashimi = Instantiate(sashimiObj, new Vector2(OrderPos[i], 3.6f), Quaternion.identity);
                    sashimi.transform.localScale = new Vector2(0.3f, 0.3f);
                    sashimi.transform.parent = menuObject.transform;
                    break;
            }
        }
    }
    
    public void OrderNum()
    {
        switch (Num[NumCounter])
        {
            case 0:
                OrderCounter1.gameObject.SetActive(true);
                OrderCounter1.GetComponent<Text>().text = "× " + OrderCounter[NumCounter].ToString();
                break;
            case 1:
                OrderCounter2.gameObject.SetActive(true);
                OrderCounter2.GetComponent<Text>().text = "× " + OrderCounter[NumCounter].ToString();
                break;
            case 2:
                OrderCounter3.gameObject.SetActive(true);
                OrderCounter3.GetComponent<Text>().text = "× " + OrderCounter[NumCounter].ToString();
                break;
            default:
                OrderCounter4.gameObject.SetActive(true);
                OrderCounter4.GetComponent<Text>().text = "× " + OrderCounter[NumCounter].ToString();
                break;
        }
        NumCounter++;
    }

    public void OrderCounterON()
    {
        OrderCounter1.gameObject.SetActive(true);
        OrderCounter2.gameObject.SetActive(true);
        OrderCounter3.gameObject.SetActive(true);
        OrderCounter4.gameObject.SetActive(true);
    }

    public void OrderCounterOFF()
    {
        OrderCounter1.gameObject.SetActive(false);
        OrderCounter2.gameObject.SetActive(false);
        OrderCounter3.gameObject.SetActive(false);
        OrderCounter4.gameObject.SetActive(false);
    }


	void Start () {
        menuObject = GameObject.Find("MenuObject");
        OrderCounter1 = GameObject.Find("DrinkingCounter/OrderCounter1");
        OrderCounter2 = GameObject.Find("DrinkingCounter/OrderCounter2");
        OrderCounter3 = GameObject.Find("DrinkingCounter/OrderCounter3");
        OrderCounter4 = GameObject.Find("DrinkingCounter/OrderCounter4");

        OrderShuffle();
        PosShuffle();
        Order();
        OrderCounterOFF();
    }
	
	void Update () {
		
	}

    
}


