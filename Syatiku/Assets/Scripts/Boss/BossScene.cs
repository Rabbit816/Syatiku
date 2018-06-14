using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : MonoBehaviour {
    public static BossScene Instance { get; private set; }

    [SerializeField]
    GameObject sanctionPartCanvas;
    [SerializeField]
    GameObject flickPartCanvas;
    [SerializeField]
    UnityEngine.UI.Text timerText;

    [SerializeField]
    int attackValue = 3;

    [SerializeField]
    int damageGageMax;
    int damageGage;

    int missCount;
    int successCount;

    //ボスシーンのゲーム時間
    [SerializeField]
    float gameTime;

    private static Vector3 touchStartPos;

    void Awake () {
        Instance = this.GetComponent<BossScene>();
	}

    public void ChangePart()
    {
        sanctionPartCanvas.SetActive(!sanctionPartCanvas.activeSelf);
        flickPartCanvas.SetActive(!flickPartCanvas.activeSelf);
    }
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            //フリックの開始
            touchStartPos = Input.mousePosition;
        }

        UpdateTimer();
	}

    /// <summary>
    /// タイマーの更新
    /// </summary>
    void UpdateTimer()
    {
        if (gameTime < 0)
        {
            Result();
        }
        else
        {
            gameTime -= Time.deltaTime;
            timerText.text = gameTime.ToString("F0");
        }
    }

    /// <summary>
    /// テキストの移動速度、方向を更新
    /// </summary>
    public void SetMoveForce(ref Vector3 moveForce)
    {
        //離した位置
        Vector3 touchEndPos = Input.mousePosition;
        //フリックの長さ
        Vector3 flickLength = touchEndPos - touchStartPos;
        Vector3 newMoveForce = flickLength.normalized * flickLength.sqrMagnitude / 10000;

        //フリックが小さすぎなければ
        if (newMoveForce.x > 0.1f || newMoveForce.x < -0.1f ||
            newMoveForce.y > 0.1f || newMoveForce.y < -0.1f)
        {
            //値を変更
            moveForce = newMoveForce;
        }

    }

    public void MissCountUP()
    {
        missCount++;
    }

    public void SuccessCountUP()
    {
        successCount++;

        if (successCount % attackValue == 0)
        {
            ChangePart();
        }
    }

    void Result()
    {
        Debug.Log("ゲーム終了：結果発表");
    }
    
}
