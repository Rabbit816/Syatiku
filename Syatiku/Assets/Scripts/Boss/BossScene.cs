using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BossScene : MonoBehaviour {
    public static BossScene Instance { get; private set; }

    [SerializeField]
    FlickPartController flickPart;
    [SerializeField]
    SanctionPartController sanctionPart;
    [SerializeField]
    DamageGageController damageGageController;
    [SerializeField]
    GameObject standingBoss;

    [SerializeField, Header("ハリセンモード移行への区切り値")]
    int[] SeparateValues;
    //区切り値への到達状態
    bool[] isReachStates;

    int missCount;
    int successCount;

    private Vector3 touchStartPos;

    void Awake () {
        Instance = this;
        isReachStates = new bool[SeparateValues.Length];

        flickPart.gameObject.SetActive(true);
        sanctionPart.gameObject.SetActive(false);
	}

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //フリックの開始
            touchStartPos = Input.mousePosition;
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
        damageGageController.DamagePointDown();
    }

    public void SuccessCountUP()
    {
        successCount++;
        damageGageController.DamagePointUp();

        int separateValue = -1;
        int i;
        //到達していない区切り値を探す
        for (i = 0; i < isReachStates.Length; i++)
        {
            if (!isReachStates[i]) {
                separateValue = SeparateValues[i];
                break;
            }
        }

        //区切り値へ到達
        if (damageGageController.damagePoint == separateValue)
        {
            if(i >= 0) isReachStates[i] = true;
            ChangePart();
        }
        else
        {
            flickPart.FlickSuccess();
        }
    }

    public void ChangePart()
    {
        flickPart.gameObject.SetActive(!flickPart.gameObject.activeSelf);
        sanctionPart.gameObject.SetActive(!sanctionPart.gameObject.activeSelf);
        standingBoss.SetActive(true);
    }

    public void ChangeBossState(GameObject slappedBoss, float duration = 0, bool re = false)
    {
        standingBoss.SetActive(false);
        slappedBoss.SetActive(true);
        if(re) StartCoroutine(ReturnBossState(slappedBoss, duration));
    }

    IEnumerator ReturnBossState(GameObject slappedBoss, float duration)
    {
        if (duration == 0) yield return null;
        yield return new WaitForSeconds(duration);
        standingBoss.SetActive(true);
        slappedBoss.SetActive(false);
    }

    public void Result()
    {
        if (isReachStates[isReachStates.Length - 1])
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GoodEnd");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("BadEnd");
        }
        Debug.Log("ゲーム終了：結果発表");
    }
}
