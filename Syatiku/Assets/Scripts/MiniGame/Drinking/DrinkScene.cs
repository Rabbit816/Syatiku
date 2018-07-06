using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkScene : MonoBehaviour {

    public GameObject menuObject;
    public GameObject hukidashiObj;
    public GameObject yakitoriObj;
    public GameObject sakeObj;
    public GameObject saladObj;
    public GameObject sashimiObj;

    //注文を表示するための配列
    public int[] foodsBox = new int[4];
    public float[] orderPos = new float[4] {-6.5f, -2, 2.3f, 6.5f};

    //配列の中身をシャッフルする
    public void OrderShuffle()
    {
        //配列の中身を用意
        for (int i = 0; i < foodsBox.Length; i++)
        {
            foodsBox[i] = Random.Range(0, foodsBox.Length + 1);
        }

        //配列をシャッフル
        System.Random rnd = new System.Random();
        for (int i = 0; i < foodsBox.Length; i++)
        {
            int shuffle1 = rnd.Next(foodsBox.Length);
            int shuffle2 = rnd.Next(foodsBox.Length);
            int value = foodsBox[shuffle1];
            foodsBox[shuffle1] = foodsBox[shuffle2];
            foodsBox[shuffle2] = value;
        }

        for (int i = 0; i < orderPos.Length; i++)
        {
            int shuffle1 = rnd.Next(orderPos.Length);
            int shuffle2 = rnd.Next(orderPos.Length);
            float value = orderPos[shuffle1];
            orderPos[shuffle1] = orderPos[shuffle2];
            orderPos[shuffle2] = value;
        }


    }
    
    //注文商品を1個ずつランダムに表示して消すを繰り返す
    private IEnumerator OrderMethod()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < foodsBox.Length; i++)
        {
            switch (foodsBox[i])
            {
                case 0:
                    var hukidashi1 = Instantiate(hukidashiObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    hukidashi1.transform.localScale = new Vector2(1.25f, 1.25f);
                    hukidashi1.transform.parent = menuObject.transform;
                    var yakitori = Instantiate(yakitoriObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    yakitori.transform.parent = menuObject.transform;
                    yield return new WaitForSeconds(2.0f);
                    Delete();
                    yield return new WaitForSeconds(1.0f);
                    break;
                case 1:
                    var hukidashi2 = Instantiate(hukidashiObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    hukidashi2.transform.localScale = new Vector2(1.25f, 1.25f);
                    hukidashi2.transform.parent = menuObject.transform;
                    var sake = Instantiate(sakeObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    sake.transform.localScale = new Vector2(0.35f, 0.35f);
                    sake.transform.parent = menuObject.transform;
                    yield return new WaitForSeconds(2.0f);
                    Delete();
                    yield return new WaitForSeconds(1.0f);
                    break;
                case 2:
                    var hukidashi3 = Instantiate(hukidashiObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    hukidashi3.transform.localScale = new Vector2(1.25f, 1.25f);
                    hukidashi3.transform.parent = menuObject.transform;
                    var salad = Instantiate(saladObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    salad.transform.localScale = new Vector2(0.45f, 0.45f);
                    salad.transform.parent = menuObject.transform;
                    yield return new WaitForSeconds(2.0f);
                    Delete();
                    yield return new WaitForSeconds(1.0f);
                    break;
                default:
                    var hukidashi4 = Instantiate(hukidashiObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    hukidashi4.transform.localScale = new Vector2(1.25f, 1.25f);
                    hukidashi4.transform.parent = menuObject.transform;
                    var sashimi = Instantiate(sashimiObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    sashimi.transform.localScale = new Vector2(0.35f, 0.35f);
                    sashimi.transform.parent = menuObject.transform;
                    yield return new WaitForSeconds(2.0f);
                    Delete();
                    yield return new WaitForSeconds(1.0f);
                    break;
            }
        }

        //正解を表示
        OrderAnswer();

    }
  
    //コルーチン処理の実行
    public void Order()
    {
        StartCoroutine("OrderMethod");
    }

    //注文の表示を消す
    public void Delete()
    {
        var Delete = menuObject.transform;
        for (int i = 0; i < Delete.childCount; i++)
        {
            Destroy(Delete.GetChild(i).gameObject);
        }
    }

    //注文の答えを表示
    public void OrderAnswer()
    {
        for (int i = 0; i < foodsBox.Length; i++)
        {
            switch (foodsBox[i])
            {
                case 0:
                    var hukidashi1 = Instantiate(hukidashiObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    hukidashi1.transform.localScale = new Vector2(1.25f, 1.25f);
                    hukidashi1.transform.parent = menuObject.transform;
                    var yakitori = Instantiate(yakitoriObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    yakitori.transform.parent = menuObject.transform;
                    break;
                case 1:
                    var hukidashi2 = Instantiate(hukidashiObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    hukidashi2.transform.localScale = new Vector2(1.25f, 1.25f);
                    hukidashi2.transform.parent = menuObject.transform;
                    var sake = Instantiate(sakeObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    sake.transform.localScale = new Vector2(0.35f, 0.35f);
                    sake.transform.parent = menuObject.transform;
                    break;
                case 2:
                    var hukidashi3 = Instantiate(hukidashiObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    hukidashi3.transform.localScale = new Vector2(1.25f, 1.25f);
                    hukidashi3.transform.parent = menuObject.transform;
                    var salad = Instantiate(saladObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    salad.transform.localScale = new Vector2(0.45f, 0.45f);
                    salad.transform.parent = menuObject.transform;
                    break;
                default:
                    var hukidashi4 = Instantiate(hukidashiObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    hukidashi4.transform.localScale = new Vector2(1.25f, 1.25f);
                    hukidashi4.transform.parent = menuObject.transform;
                    var sashimi = Instantiate(sashimiObj, new Vector2(orderPos[i], 3.3f), Quaternion.identity);
                    sashimi.transform.localScale = new Vector2(0.35f, 0.35f);
                    sashimi.transform.parent = menuObject.transform;
                    break;
            }
        }
    }

	void Start () {
        OrderShuffle();
        Order();
	}
	
	void Update () {
		
	}

    
}


