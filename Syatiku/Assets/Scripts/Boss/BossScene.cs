using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class BossScene : MonoBehaviour {
    public static BossScene Instance { get; private set; }

    #region スタートアニメーション用

    [SerializeField]
    RectTransform footSound1;
    [SerializeField]
    RectTransform footSound2;
    [SerializeField]
    RectTransform timer;
    [SerializeField]
    RectTransform gage;
    [SerializeField]
    RectTransform bossBigSound;
    [SerializeField]
    RectTransform startText;
    [SerializeField]
    GameObject panel;
    [SerializeField]
    GameObject spotLight;

    #endregion

    [SerializeField]
    FlickPartController flickPart;
    [SerializeField]
    SanctionPartController sanctionPart;
    [SerializeField]
    DamageGageController damageGageController;
    [SerializeField]
    GameObject standingBoss;
    [SerializeField]
    Image background;
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

    #region スタート演出

    IEnumerator StartAnimation()
    {
        RectTransform boss = standingBoss.GetComponent<RectTransform>();
        yield return new WaitForSeconds(1f);

        //1歩目
        Move(boss, new Vector3(20f, 100f, 0f), new Vector3(0.6f, 0.6f, 1f), 0);
        footSound1.gameObject.SetActive(true);
        footSound2.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        //2歩目
        Move(boss, new Vector3(0f, -50f, 0f), new Vector3(0.9f, 0.9f, 1f), 0);
        Move(footSound1, new Vector3(-220f, 300f, 0f), new Vector3(1.2f, 1.2f, 1f), 0);
        Move(footSound2, new Vector3(220f, 350f, 0f), new Vector3(1.2f, 1.2f, 1f), 0);
        yield return new WaitForSeconds(1.5f);

        //3歩目
        Move(boss, new Vector3(0, -200f, 0f), new Vector3(1.2f, 1.2f, 1f), 0);
        Move(footSound1, new Vector3(-300f, 400f, 0f), new Vector3(1.5f, 1.5f, 1f), 0);
        Move(footSound2, new Vector3(300f, 200f, 0f), new Vector3(1.5f, 1.5f, 1f), 0);
        yield return new WaitForSeconds(1.5f);

        //暗転
        footSound1.gameObject.SetActive(false);
        footSound2.gameObject.SetActive(false);
        panel.SetActive(true);
        Move(boss, new Vector3(0, -600f, 0f), new Vector3(2f, 2f, 1f), 0);
        yield return new WaitForSeconds(2f);

        //巨大ボス披露
        panel.SetActive(false);
        bossBigSound.gameObject.SetActive(true);
        Move(bossBigSound, bossBigSound.localPosition, Vector3.one);
        yield return new WaitForSeconds(3f);

        //ゲームサイズに戻す
        bossBigSound.gameObject.SetActive(false);
        Move(boss, new Vector3(0, -200f, 0f), new Vector3(1.2f, 1.2f, 1f));
        yield return new WaitForSeconds(1f);

        //タイマー出現
        spotLight.SetActive(false);
        Move(timer, new Vector3(810f, 450f, 0f), Vector3.one);
        yield return new WaitForSeconds(0.5f);
        //ゲージ出現
        Move(gage, new Vector3(25f, -500f, 0f), Vector3.one);
        yield return new WaitForSeconds(0.5f);
        //背景変更
        background.sprite = backgroundSprites[0];
        background.color = Color.white;
        //スタートテキスト出現
        startText.gameObject.SetActive(true);
        Move(startText, startText.localPosition, Vector3.one);
        yield return new WaitForSeconds(2.0f);
        //スタートテキスト消える
        Move(startText, startText.localPosition, Vector3.zero);
        yield return new WaitForSeconds(1f);

        startText.gameObject.SetActive(false);
        GameStart();
    }

    void Move(RectTransform target, Vector3 targetPos, Vector3 targetScale, float time = 1f)
    {
        DOTween.To(
            () => target.localPosition,
            position => target.localPosition = position,
            targetPos,
            time
        );

        target.DOScale(targetScale, time);
    }

    void GameStart()
    {
        flickPart.gameObject.SetActive(true);
        SoundManager.Instance.PlayBGM(BGMName.Boss);
    }

    #endregion

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
        if (separateValue > 0 && damageGageController.damagePoint >= separateValue)
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
