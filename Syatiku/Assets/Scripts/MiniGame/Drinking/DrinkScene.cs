using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkScene : MonoBehaviour {

    //メニューのオブジェクト
    [HideInInspector]
    public GameObject
        menuObject,
        yakitoriObj,
        sakeObj,
        saladObj,
        sashimiObj;

    GameObject OrderCounter1, OrderCounter2, OrderCounter3, OrderCounter4;
    GameObject Hukidashi1, Hukidashi2, Hukidashi3, Hukidashi4;
    GameObject Answer1, Answer2, Answer3, Answer4;
    GameObject TapText;
    GameObject LimitText;
    
    ButtonController button;
    Denmoku denmoku;

    //商品を格納する配列
    private int[] foodsBox = new int[4];

    //注文を表示するための配列
    private int[] OrderBox = new int[4];
    private int[] OrderCounter = new int[4];
    private float[] OrderPos = new float[4] {-5.7f, -1.85f, 2.0f, 5.85f};

    private int[] Num = new int[4];
    private int NumCounter = 0;
    private float[] AnswerPos = new float[4] { -680.0f, -220.0f, 240.0f, 700.0f };
    private bool NextGameFlg;
    [SerializeField, Range(0, 2), Tooltip("注文の表示時間(秒)")] private float Timer;
    [SerializeField, Range(1, 9), Tooltip("回数制限")] private int Limit;
    private float OriginPos1;
    private float OriginPos2;
    private float OriginPos3;

    //注文の配列の用意
    public void OrderShuffle()
    {
        //商品を格納する配列を用意&シャッフル
        for(int i = 0; i < this.foodsBox.Length; i++)
        {
            this.foodsBox[i] = i;
        }
        Common.Instance.Shuffle(this.foodsBox);
        
        //注文配列・個数配列
        for(int i = 0; i < this.OrderBox.Length; i++)
        {
            this.OrderBox[i] = this.foodsBox[i];
            this.OrderCounter[i] = Random.Range(1, 5);
        }
    }
    
    //表示する位置をシャッフル
    public void PosShuffle()
    {
        Common.Instance.Shuffle(this.OrderPos);

        //Num配列の用意
        for (int i = 0; i < this.OrderPos.Length; i++)
        {
            if(this.OrderPos[i] == OriginPos1)
            {
                this.Num[i] = 0;
            }
            else if(this.OrderPos[i] == OriginPos2)
            {
                this.Num[i] = 1;
            }
            else if(this.OrderPos[i] == OriginPos3)
            {
                this.Num[i] = 2;
            }
            else
            {
                this.Num[i] = 3;
            }
        }
    }

    //メニューのプレファブを読み込む
    public void LoadPrefab(int i)
    {
        switch (i)
        {
            case 0:
                this.yakitoriObj = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/yakitori");
                break;
            case 1:
                this.sakeObj = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/sake");
                break;
            case 2:
                this.saladObj = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/salad");
                break;
            case 3:
                this.sashimiObj = Resources.Load<GameObject>("Prefabs/MiniGame/Drinking/sashimi");
                break;
            default:
                Debug.Log("エラー");
                break;
        }
    }

    //注文商品を1個ずつランダムな位置に表示して消すを繰り返す
    private IEnumerator OrderMethod()
    {
        for (int i = 0; i < this.OrderBox.Length; i++)
        {
            yield return new WaitForSeconds(1.0f);
            
            //注文の表示
            this.OrderHukidashi();
            
            switch (this.OrderBox[i])
            {
                //やきとりを表示
                case 0:
                    this.LoadPrefab(this.OrderBox[i]);
                    var yakitori = Instantiate(this.yakitoriObj, new Vector2(OrderPos[i], 2.7f), Quaternion.identity);
                    yakitori.transform.localScale = new Vector2(0.7f, 0.7f);
                    yakitori.transform.parent = this.menuObject.transform;
                    break;
                //酒を表示
                case 1:
                    this.LoadPrefab(this.OrderBox[i]);
                    var sake = Instantiate(this.sakeObj, new Vector2(OrderPos[i], 2.7f), Quaternion.identity);
                    sake.transform.localScale = new Vector2(0.25f, 0.25f);
                    sake.transform.parent = this.menuObject.transform;
                    break;
                //サラダを表示
                case 2:
                    this.LoadPrefab(this.OrderBox[i]);
                    var salad = Instantiate(saladObj, new Vector2(OrderPos[i], 2.7f), Quaternion.identity);
                    salad.transform.localScale = new Vector2(0.35f, 0.35f);
                    salad.transform.parent = menuObject.transform;
                    break;
                //刺身を表示
                case 3:
                    this.LoadPrefab(this.OrderBox[i]);
                    var sashimi = Instantiate(sashimiObj, new Vector2(OrderPos[i], 2.7f), Quaternion.identity);
                    sashimi.transform.localScale = new Vector2(0.25f, 0.25f);
                    sashimi.transform.parent = menuObject.transform;
                    break;
                default:
                    Debug.Log("エラー");
                    break;
            }
            //数秒後に表示された吹き出しと商品を消す
            yield return new WaitForSeconds(this.Timer);
            this.Delete();
            this.OrderCounterOFF();
            this.Hukidashi(false);
        }
        yield return new WaitForSeconds(1.0f);
        button.DrinkSceneButton(true);
        this.NumCounter = 0;
    }
  
    public void Order()
    {
        StartCoroutine(this.OrderMethod());
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
    public void Answer()
    {
        for (int i = 0; i < this.OrderBox.Length; i++)
        {
            //吹き出しを表示
            this.Hukidashi(true);

            if (this.OrderBox[i] == denmoku.InputOrderBox[i])
            {
                if (this.OrderCounter[i] == denmoku.InputOrderCounter[i])
                {
                    this.OutputAnswer(Num[i], true);
                }
                else
                {
                    this.OutputAnswer(Num[i], false);
                }
            }
            else
            {
                this.OutputAnswer(Num[i], false);
            }
        }
        this.NextGameFlg = true;
        this.TapText.gameObject.SetActive(true);
        Limit--;
    }
    
    //注文を表示する際の、吹き出しと個数を表示させるメソッド
    public void OrderHukidashi()
    {
        switch (Num[NumCounter])
        {
            case 0:
                this.Hukidashi1.gameObject.SetActive(true);
                this.OrderCounter1.gameObject.SetActive(true);
                this.OrderCounter1.GetComponent<Text>().text = "× " + this.OrderCounter[NumCounter].ToString();
                break;
            case 1:
                this.Hukidashi2.gameObject.SetActive(true);
                this.OrderCounter2.gameObject.SetActive(true);
                this.OrderCounter2.GetComponent<Text>().text = "× " + this.OrderCounter[NumCounter].ToString();
                break;
            case 2:
                this.Hukidashi3.gameObject.SetActive(true);
                this.OrderCounter3.gameObject.SetActive(true);
                this.OrderCounter3.GetComponent<Text>().text = "× " + this.OrderCounter[NumCounter].ToString();
                break;
            case 3:
                this.Hukidashi4.gameObject.SetActive(true);
                this.OrderCounter4.gameObject.SetActive(true);
                this.OrderCounter4.GetComponent<Text>().text = "× " + this.OrderCounter[NumCounter].ToString();
                break;
            default:
                Debug.Log("エラー");
                break;
        }
        NumCounter++;
    }

    public void OrderCounterOFF()
    {
        this.OrderCounter1.gameObject.SetActive(false);
        this.OrderCounter2.gameObject.SetActive(false);
        this.OrderCounter3.gameObject.SetActive(false);
        this.OrderCounter4.gameObject.SetActive(false);
    }

    public void AnswerResultOFF()
    {
        this.Answer1.gameObject.SetActive(false);
        this.Answer2.gameObject.SetActive(false);
        this.Answer3.gameObject.SetActive(false);
        this.Answer4.gameObject.SetActive(false);
    }

    public void Hukidashi(bool b)
    {
        this.Hukidashi1.gameObject.SetActive(b);
        this.Hukidashi2.gameObject.SetActive(b);
        this.Hukidashi3.gameObject.SetActive(b);
        this.Hukidashi4.gameObject.SetActive(b);
    }

    //注文の正誤判定の表示を管理するメソッド
    public void OutputAnswer(int i, bool b)
    {
        switch (i)
        {
            case 0:
                this.Answer1.gameObject.SetActive(true);
                if (b)
                {
                    this.Answer1.GetComponent<RectTransform>().localPosition = new Vector2(this.AnswerPos[i], 200);
                    this.Answer1.GetComponent<Text>().text = "○";
                    this.Answer1.GetComponent<Text>().color = new Color(255f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                }
                else
                {
                    this.Answer1.GetComponent<RectTransform>().localPosition = new Vector2(this.AnswerPos[i], 175);
                    this.Answer1.GetComponent<Text>().text = "×";
                    this.Answer1.GetComponent<Text>().color = new Color(40f / 255f, 0f / 255f, 255f / 255f, 255f / 255f);
                }
                break;
            case 1:
                this.Answer2.gameObject.SetActive(true);
                if (b)
                {
                    this.Answer2.GetComponent<RectTransform>().localPosition = new Vector2(this.AnswerPos[i], 200);
                    this.Answer2.GetComponent<Text>().text = "○";
                    this.Answer2.GetComponent<Text>().color = new Color(255f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                }
                else
                {
                    this.Answer2.GetComponent<RectTransform>().localPosition = new Vector2(this.AnswerPos[i], 175);
                    this.Answer2.GetComponent<Text>().text = "×";
                    this.Answer2.GetComponent<Text>().color = new Color(40f / 255f, 0f / 255f, 255f / 255f, 255f / 255f);
                }
                break;
            case 2:
                this.Answer3.gameObject.SetActive(true);
                if (b)
                {
                    this.Answer3.GetComponent<RectTransform>().localPosition = new Vector2(this.AnswerPos[i], 200);
                    this.Answer3.GetComponent<Text>().text = "○";
                    this.Answer3.GetComponent<Text>().color = new Color(255f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                }
                else
                {
                    this.Answer3.GetComponent<RectTransform>().localPosition = new Vector2(this.AnswerPos[i],175);
                    this.Answer3.GetComponent<Text>().text = "×";
                    this.Answer3.GetComponent<Text>().color = new Color(40f / 255f, 0f / 255f, 255f / 255f, 255f / 255f);
                }
                break;
            case 3:
                this.Answer4.gameObject.SetActive(true);
                if (b)
                {
                    this.Answer4.GetComponent<RectTransform>().localPosition = new Vector2(this.AnswerPos[i], 200);
                    this.Answer4.GetComponent<Text>().text = "○";
                    this.Answer4.GetComponent<Text>().color = new Color(255f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                }
                else
                {
                    this.Answer4.GetComponent<RectTransform>().localPosition = new Vector2(this.AnswerPos[i], 175);
                    this.Answer4.GetComponent<Text>().text = "×";
                    this.Answer4.GetComponent<Text>().color = new Color(40f / 255f, 0f / 255f, 255f / 255f, 255f / 255f);
                }
                break;
            default:
                Debug.Log("エラー");
                break;
        }
    }

    public void GameStart()
    {
        this.OrderShuffle();
        this.PosShuffle();
        this.Order();
        this.TapText.GetComponent<Text>().text = "画面をタップ！";
    }
   
    void Start () {
        //ゲームの初期状態を用意する処理
        button = GetComponent<ButtonController>();
        denmoku = GetComponent<Denmoku>();
        this.menuObject = GameObject.Find("MenuObject");
        this.OrderCounter1 = GameObject.Find("DrinkingCounter/OrderCounter1");
        this.OrderCounter2 = GameObject.Find("DrinkingCounter/OrderCounter2");
        this.OrderCounter3 = GameObject.Find("DrinkingCounter/OrderCounter3");
        this.OrderCounter4 = GameObject.Find("DrinkingCounter/OrderCounter4");
        this.Hukidashi1 = GameObject.Find("Hukidashi/Hukidashi1");
        this.Hukidashi2 = GameObject.Find("Hukidashi/Hukidashi2");
        this.Hukidashi3 = GameObject.Find("Hukidashi/Hukidashi3");
        this.Hukidashi4 = GameObject.Find("Hukidashi/Hukidashi4");
        this.Answer1 = GameObject.Find("OrderAnswer/Answer1");
        this.Answer2 = GameObject.Find("OrderAnswer/Answer2");
        this.Answer3 = GameObject.Find("OrderAnswer/Answer3");
        this.Answer4 = GameObject.Find("OrderAnswer/Answer4");
        this.TapText = GameObject.Find("DrinkMain/TapText");
        this.LimitText = GameObject.Find("DrinkMain/Limit");
        this.OriginPos1 = this.OrderPos[0];
        this.OriginPos2 = this.OrderPos[1];
        this.OriginPos3 = this.OrderPos[2];
        this.OrderCounterOFF();
        this.AnswerResultOFF();
        this.Hukidashi(false);
        this.NextGameFlg = true;
        this.TapText.GetComponent<Text>().text = "タップしてスタート！";
    }
	void Update () {
        if(Input.GetMouseButtonDown(0) && this.NextGameFlg)
        {
            if(Limit > 0)
            {
                this.TapText.gameObject.SetActive(false);
                this.NextGameFlg = false;
                this.AnswerResultOFF();
                Hukidashi(false);
                this.GameStart();
            }
            else
            {
                this.NextGameFlg = false;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
            
        }
        this.LimitText.GetComponent<Text>().text = "あと " + this.Limit.ToString() + " 回";
    }

    
}


