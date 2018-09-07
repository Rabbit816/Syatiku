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
    GameObject bossBigSound;
    [SerializeField]
    RectTransform startText;
    [SerializeField]
    GameObject panel;
    [SerializeField]
    GameObject spotLight;

    #endregion
    [SerializeField]
    Sprite[] standingBossSprites;
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

    enum GameState
    {
        StartAnimation,
        FlickPart,
        SanctionPart,
        End,
    }
    GameState state;
    int missCount;
    int successCount;

    private Vector3 touchStartPos;

    void Awake () {
        Initialize();
	}

    void Initialize()
    {
        Instance = this;
        isReachStates = new bool[SeparateValues.Length];
        int gameMode = Common.Instance.gameMode < 1 ? 2: Common.Instance.gameMode;
        flickPart.Initialize(gameMode);
        sanctionPart.Initialize(gameMode);
        standingBoss.GetComponent<Image>().sprite = standingBossSprites[gameMode - 1];
        state = 0;
        
        StartCoroutine(StartAnimation());
    }

    #region スタート演出

    IEnumerator StartAnimation()
    {
        RectTransform bossRectTransform = standingBoss.GetComponent<RectTransform>();
        RectTransform spotRectTransform = spotLight.GetComponent<RectTransform>();
        yield return new WaitForSeconds(1f);

        //1歩目
        Move(bossRectTransform, new Vector3(20f, 100f, 0f), new Vector3(0.6f, 0.6f, 1f), 0);
        spotRectTransform.offsetMin = new Vector2(730f, 300f);
        spotRectTransform.offsetMax = new Vector2(-700f, 0);
        footSound1.gameObject.SetActive(true);
        footSound2.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        //2歩目
        Move(bossRectTransform, new Vector3(-100f, -50f, 0f), new Vector3(0.9f, 0.9f, 1f), 0);
        spotRectTransform.offsetMin = new Vector2(500f, 10f);
        spotRectTransform.offsetMax = new Vector2(-700f, 0);
        Move(footSound1, new Vector3(-300f, 300f, 0f), new Vector3(1.2f, 1.2f, 1f), 0);
        Move(footSound2, new Vector3(140f, 350f, 0f), new Vector3(1.2f, 1.2f, 1f), 0);
        yield return new WaitForSeconds(1.5f);

        //3歩目
        Move(bossRectTransform, new Vector3(0, -200f, 0f), new Vector3(1.2f, 1.2f, 1f), 0);
        spotRectTransform.offsetMin = new Vector2(500f, 0f);
        spotRectTransform.offsetMax = new Vector2(-500f, 0);
        Move(footSound1, new Vector3(-300f, 400f, 0f), new Vector3(1.5f, 1.5f, 1f), 0);
        Move(footSound2, new Vector3(300f, 200f, 0f), new Vector3(1.5f, 1.5f, 1f), 0);
        yield return new WaitForSeconds(1.5f);

        //暗転
        footSound1.gameObject.SetActive(false);
        footSound2.gameObject.SetActive(false);
        panel.SetActive(true);
        Move(bossRectTransform, new Vector3(0, -600f, 0f), new Vector3(2f, 2f, 1f), 0);
        yield return new WaitForSeconds(2f);

        //巨大ボス披露
        panel.SetActive(false);
        bossBigSound.SetActive(true);
        spotRectTransform.offsetMin = new Vector2(300f, 0f);
        spotRectTransform.offsetMax = new Vector2(-300f, 0);
        yield return new WaitForSeconds(3f);

        //ゲームサイズに戻す
        bossBigSound.SetActive(false);
        Move(bossRectTransform, new Vector3(0, -200f, 0f), new Vector3(1.2f, 1.2f, 1f));
        yield return new WaitForSeconds(1f);

        //背景変更
        background.sprite = backgroundSprites[0];
        ChangeColor(background, Color.white, 2f);
        //タイマー出現
        spotLight.SetActive(false);
        Move(timer, new Vector3(810f, 450f, 0f), Vector3.one);
        yield return new WaitForSeconds(0.5f);
        //ゲージ出現
        Move(gage, new Vector3(25f, -500f, 0f), Vector3.one);
        yield return new WaitForSeconds(0.5f);
        //スタートテキスト出現
        startText.gameObject.SetActive(true);
        Move(startText, startText.localPosition, Vector3.one);
        yield return new WaitForSeconds(1.5f);
        //スタートテキスト消える
        Move(startText, startText.localPosition, Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.5f);

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

    void ChangeColor(Image image, Color color, float time)
    {
        DOTween.To(
            () => image.color,
            c => image.color = c,
            color,
            time
        );
    }

    void GameStart()
    {
        flickPart.gameObject.SetActive(true);
        SoundManager.Instance.PlayBGM(BGMName.Boss);
        state = GameState.FlickPart;
    }

    #endregion

    void Update () {
        if (state == GameState.FlickPart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //フリックの開始
                touchStartPos = Input.mousePosition;
            }

            flickPart.UpdateFlickPart();
        }
        else if(state == GameState.SanctionPart)
        {
            sanctionPart.UpdateSanctionPart();
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
        bool isFlickPart = (state == GameState.FlickPart);
        flickPart.gameObject.SetActive(!isFlickPart);
        sanctionPart.gameObject.SetActive(isFlickPart);
        state = isFlickPart ? GameState.SanctionPart : GameState.FlickPart;
        //背景の変更
        if (!isFlickPart) background.sprite = backgroundSprites[0];
        else background.sprite = backgroundSprites[1];

        standingBoss.SetActive(true);
    }

    public void ChangeBossState(GameObject slappedBoss, float duration = 0, bool re = false)
    {
        standingBoss.SetActive(false);
        slappedBoss.SetActive(true);
        if(re && state == GameState.FlickPart) StartCoroutine(ReturnBossState(slappedBoss, duration));
    }

    IEnumerator ReturnBossState(GameObject slappedBoss, float duration)
    {
        // バグの原因はここ
        if (duration == 0) yield return null;
        yield return new WaitForSeconds(duration);
        standingBoss.SetActive(true);
        slappedBoss.SetActive(false);
    }

    public void Result()
    {
        state = GameState.End;
        Common.SceneName scene = Common.SceneName.MainNormalEnd;
        int gameMode = Common.Instance.gameMode;
        // good
        if (isReachStates[isReachStates.Length - 1])
        {
            scene = gameMode > 1 ? Common.SceneName.AnotherGoodEnd : Common.SceneName.MainGoodEnd;
        }
        // notmal
        else if(isReachStates[isReachStates.Length - 2])
        {
            scene = gameMode > 1 ? Common.SceneName.AnotherNormalEnd : Common.SceneName.MainNormalEnd;
        }
        // bad
        else
        {
            scene = gameMode > 1 ? Common.SceneName.AnotherBadEnd : Common.SceneName.MainBadEnd;
        }
        Common.Instance.ChangeScene(scene);
    }
}
