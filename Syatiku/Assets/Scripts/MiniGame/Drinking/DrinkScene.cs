using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkScene : MonoBehaviour {

    // スクリプトのインスタンスの取得
    ButtonController button;
    Denmoku denmoku;
    DenmokuMeter meter;

    // 表示された注文商品が格納されるオブジェクト
    public GameObject menuObject;

    // 商品のPrefabを格納する配列
    [SerializeField]
    GameObject[] MenuList;

    // 注文数を表示する
    [SerializeField]
    Text[] OrderCounterList;

    // 吹き出しを表示する
    [SerializeField]
    GameObject[] HukidashiList;

    // 注文の正誤結果を表示する
    [SerializeField]
    Text[] AnswerList;

    [SerializeField]
    Text TapText;

    // ゲームの残り回数を表示
    [SerializeField]
    Text LimitText;
    
    // 商品IDを保存する配列
    private int[] foodsBox = new int[4];

    // オーダーが入った商品のIDを保存する配列
    private int[] OrderBox = new int[4];

    // オーダーが入った商品の個数を保存する配列
    private int[] OrderCounter = new int[4];

    // オーダーが入った商品画像を表示する場所の配列
    private float[] OrderPos = new float[4] {-5.85f, -1.9f, 2.05f, 6.0f};

    private int[] Num = new int[4];

    private int NumCounter = 0;

    private bool NextGameFlg;

    [SerializeField, Range(0, 2), Tooltip("注文の表示時間(秒)")]
    private float Timer;

    [SerializeField, Range(2, 10), Tooltip("回数制限")]
    private int Limit;

    private float OriginPos1;
    private float OriginPos2;
    private float OriginPos3;

    private int ClearQuota;
    private int ClearCount;
    private int ClearScore;

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
            if(this.OrderPos[i] == this.OriginPos1)
            {
                this.Num[i] = 0;
            }
            else if(this.OrderPos[i] == this.OriginPos2)
            {
                this.Num[i] = 1;
            }
            else if(this.OrderPos[i] == this.OriginPos3)
            {
                this.Num[i] = 2;
            }
            else
            {
                this.Num[i] = 3;
            }
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

            var Menu_Order = Instantiate(this.MenuList[this.OrderBox[i]], new Vector2(OrderPos[i], 2.6f), Quaternion.identity);
            Menu_Order.transform.localScale = new Vector2(0.9f, 0.9f);
            Menu_Order.transform.parent = this.menuObject.transform;

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
        //吹き出しを表示
        this.Hukidashi(true);

        // 注文入力が時間切れの場合
        if (meter.TimeOverFlg)
        {
            for(int i = 0; i < this.AnswerList.Length; i++)
            {
                Vector2 pos = this.AnswerList[i].transform.localPosition;
                pos.y = 175;
                this.AnswerList[i].transform.localPosition = pos;
                this.AnswerList[i].transform.localScale = new Vector2(0.75f, 0.75f);
                this.AnswerList[i].color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);
                this.AnswerList[i].gameObject.SetActive(true);
            }
            this.AnswerList[0].text = "時";
            this.AnswerList[1].text = "間";
            this.AnswerList[2].text = "切";
            this.AnswerList[3].text = "れ";
        }
        
        // 時間切れになる前に注文入力が終了した場合
        else
        {
            for (int i = 0; i < this.AnswerList.Length; i++)
            {
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
        }
        if(this.ClearCount == 4)
        {
            this.ClearScore++;
        }
        this.NextGameFlg = true;
        this.TapText.gameObject.SetActive(true);
        Limit--;
    }
    
    //注文を表示する際の、吹き出しと個数を表示させるメソッド
    public void OrderHukidashi()
    {
        this.HukidashiList[this.Num[this.NumCounter]].gameObject.SetActive(true);
        this.OrderCounterList[this.Num[this.NumCounter]].gameObject.SetActive(true);
        this.OrderCounterList[this.Num[this.NumCounter]].text = "× " + this.OrderCounter[this.NumCounter].ToString();
        this.NumCounter++;
    }

    public void OrderCounterOFF()
    {
        for(int i = 0; i < this.OrderCounterList.Length; i++)
        {
            this.OrderCounterList[i].gameObject.SetActive(false);
        }
    }

    public void AnswerResultOFF()
    {
        for(int i = 0; i < AnswerList.Length; i++)
        {
            this.AnswerList[i].gameObject.SetActive(false);
        }
    }
    
    public void Hukidashi(bool b)
    {
        for(int i = 0; i < this.HukidashiList.Length; i++)
        {
            this.HukidashiList[i].gameObject.SetActive(b);
        }
    }
    
    //注文の正誤判定の表示を管理するメソッド
    public void OutputAnswer(int i, bool b)
    {
        Vector2 pos = this.AnswerList[i].transform.localPosition;
        this.AnswerList[i].gameObject.SetActive(true);
        this.AnswerList[i].transform.localScale = new Vector2(1.5f, 1.5f);
        if (b)
        {
            pos.y = 200;
            this.AnswerList[i].transform.localPosition = pos;
            this.AnswerList[i].text = "○";
            this.AnswerList[i].color = new Color(255f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
            this.ClearCount++;
        }
        else
        {
            pos.y = 175;
            this.AnswerList[i].transform.localPosition = pos;
            this.AnswerList[i].text = "×";
            this.AnswerList[i].color = new Color(40f / 255f, 0f / 255f, 255f / 255f, 255f / 255f);
        }
    }

    // 注文入力の時間切れの処理
    public void TimeOver()
    {
        for(int i = 0; i < this.AnswerList.Length; i++)
        {
            Vector2 pos = this.AnswerList[i].transform.localPosition;
            pos.y = 175;
            this.AnswerList[i].transform.localPosition = pos;
            this.AnswerList[i].transform.localScale = new Vector2(1.0f, 1.0f);
            this.AnswerList[i].color = new Color(50f / 255f, 50f / 255f, 50f / 255f, 255f / 255f);
            switch (i)
            {
                case 0:
                    this.AnswerList[i].text = "時";
                    break;
                case 1:
                    this.AnswerList[i].text = "間";
                    break;
                case 2:
                    this.AnswerList[i].text = "切";
                    break;
                case 3:
                    this.AnswerList[i].text = "れ";
                    break;
                default:
                    Debug.Log("TimeOver : エラー");
                    break;
            }
            this.AnswerList[i].gameObject.SetActive(true);
        }
        this.Hukidashi(true);
        this.NextGameFlg = true;
        this.TapText.gameObject.SetActive(true);
        Limit--;
    }

    public void GameStart()
    {
        this.OrderShuffle();
        this.PosShuffle();
        this.Order();
        for(int i = 0; i < denmoku.InputOrderBox.Length; i++)
        {
            denmoku.InputOrderBox[i] = -1;
        }
        this.TapText.text = "画面をタップ！";
        meter.TimeOverFlg = false;
        this.ClearCount = 0;
    }
   
    void Start () {
        //ゲームの初期状態を用意する処理
        button = GetComponent<ButtonController>();
        denmoku = GetComponent<Denmoku>();
        meter = GetComponent<DenmokuMeter>();
        this.OriginPos1 = this.OrderPos[0];
        this.OriginPos2 = this.OrderPos[1];
        this.OriginPos3 = this.OrderPos[2];
        this.OrderCounterOFF();
        this.AnswerResultOFF();
        this.Hukidashi(false);
        this.NextGameFlg = true;
        this.TapText.text = "タップしてスタート！";

        // クリア条件の設定
        this.ClearQuota = (int)(this.Limit * 0.8f);
        this.ClearScore = 0;
    }

    // 飲み会のゲームクリア判定
    public void GameResult()
    {
        if(this.ClearScore >= this.ClearQuota)
        {
            Common.Instance.clearFlag[Common.Instance.miniNum] = true;
        }
        else
        {
            Common.Instance.clearFlag[Common.Instance.miniNum] = false;
        }
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
            // ゲーム終了時の処理
            else
            {
                this.NextGameFlg = false;
                this.GameResult();
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
            
        }
        this.LimitText.text = "あと " + this.Limit.ToString() + " 回";
    }
}


