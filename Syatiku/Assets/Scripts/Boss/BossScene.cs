using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DG.Tweening;

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
    [SerializeField]
    UnityEngine.UI.Image background;
    [SerializeField]
    Sprite[] backgroundSprites; //0: FlickPart 1: SanctionPart

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
        flickPart.gameObject.SetActive(false);
        sanctionPart.gameObject.SetActive(false);

        StartCoroutine(StartAnimation());
	}

    IEnumerator StartAnimation()
    {
        RectTransform boss = standingBoss.GetComponent<RectTransform>();
        yield return new WaitForSeconds(1f);

        MoveBoss(boss, new Vector3(20f, 100f, 0f), new Vector3(0.6f, 0.6f, 1f));
        yield return new WaitForSeconds(2f);

        MoveBoss(boss, new Vector3(-20f, 0f, 0f), new Vector3(0.8f, 0.8f, 1f));
        yield return new WaitForSeconds(2f);

        MoveBoss(boss, new Vector3(0, -100f, 0f), new Vector3(1f, 1f, 1f));
        yield return new WaitForSeconds(2f);

        //yield return new WaitForSeconds(1f);
        GameStart();
    }

    void MoveBoss(RectTransform target, Vector3 targetPos, Vector3 targetScale)
    {
        DOTween.To(
            () => target.localPosition,
            position => target.localPosition = position,
            targetPos,
            1f
        );

        target.DOScale(targetScale, 1f);
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //フリックの開始
            touchStartPos = Input.mousePosition;
        }
	}

    void GameStart()
    {
        flickPart.gameObject.SetActive(true);
        SoundManager.Instance.PlayBGM(BGMName.Boss);
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
        if (separateValue > 0 && damageGageController.damagePoint > separateValue)
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
        //背景の変更
        if (flickPart.gameObject.activeSelf) background.sprite = backgroundSprites[0];
        else background.sprite = backgroundSprites[1];

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
