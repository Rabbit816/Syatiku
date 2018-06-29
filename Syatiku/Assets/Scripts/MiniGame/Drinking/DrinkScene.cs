using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkScene : MonoBehaviour {

    //料理を表示するための配列
    int[] foodsBox = new int[4];

    public void foodsDeck()
    {
        
        for (int i = 0; i < foodsBox.Length; i++)
        {
            foodsBox[i] = i;
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
    }

	void Start () {
        foodsDeck();

        //注文を表示
        var menuObject = GameObject.Find("Menu");
        for(int i = 0; i < 4; i++)
        {
            int foodsId = foodsBox[i];
            switch (foodsId)
            {
                case 0:
                    var yakitoriPre = Resources.Load<GameObject>("MiniGame/Drinking/Prefabs/niku");
                    var yakitori = Instantiate(yakitoriPre, transform.position, Quaternion.identity);
                    yakitori.transform.position = new Vector2(0, 0);
                    yakitori.transform.parent = menuObject.transform;
                    Debug.Log("やきとり");
                    break;
                case 1:
                    Debug.Log("酒");
                    break;
                case 2:
                    Debug.Log("サラダ");
                    break;
                default:
                    Debug.Log("刺身");
                    break;
            }

        }


	}
	
	void Update () {
		
	}
}
