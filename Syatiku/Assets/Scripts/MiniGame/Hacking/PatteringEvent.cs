using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PatteringEvent : MonoBehaviour {

    [SerializeField, Tooltip("Paper_1")]
    private RectTransform Paper_1;
    [SerializeField, Tooltip("Paper_2")]
    private RectTransform Paper_2;

    private IntoPCAction intopc_action;

    private Sequence seq;
    Sequence se;
    //いいタイミングかどうか
    private bool _success = false;

	// Use this for initialization
	void Start () {
        intopc_action = GetComponent<IntoPCAction>();
        seq = DOTween.Sequence();
         se = DOTween.Sequence();
        _success = false;
        //AnimLoop();
        LowAnim();
    }
	
	// Update is called once per frame
	void Update () {

    }

    /// <summary>
    /// アニメーション中の色変更
    /// </summary>
    /// <param name="num">0.黄色,1.白</param>
    private void ChangeColor(int num)
    {
        switch (num)
        {
            case 0:
                Paper_1.GetComponent<Image>().color = new Color(255, 255, 0);
                Paper_2.GetComponent<Image>().color = new Color(255, 255, 0);
                break;
            case 1:
                Paper_1.GetComponent<Image>().color = new Color(255, 255, 255);
                Paper_2.GetComponent<Image>().color = new Color(255, 255, 255);
                break;
            default:
                Debug.Log("ColorNum :" + num);
                break;
        }
    }

    /// <summary>
    /// アニメーション中のタップの時処理
    /// </summary>
    public void TapResult()
    {
        if (!_success)
        {
            Common.Instance.clearFlag[Common.Instance.isClear] = false;
            Common.Instance.ChangeScene(Common.SceneName.Result);
        }
        else
        {
            Common.Instance.clearFlag[Common.Instance.isClear] = true;
            Common.Instance.ChangeScene(Common.SceneName.Result);
        }
    }

    /// <summary>
    /// 遅めのアニメーション処理
    /// </summary>
    public void LowAnim()
    {
        se.Append(Paper_1.DOLocalRotate(new Vector2(0, Paper_1.localRotation.y + 180), 1.0f).SetDelay(0.5f).SetLoops(70, LoopType.Restart))
           .InsertCallback(3.9f, () => ChangeColor(0))
           .InsertCallback(3.9f, () => _success = true)
           .InsertCallback(4.5f, () => _success = false)
           .InsertCallback(4.5f, () => ChangeColor(1))
           .InsertCallback(14.9f, () => ChangeColor(0))
           .InsertCallback(14.9f, () => _success = true)
           .InsertCallback(15.5f, () => _success = false)
           .InsertCallback(15.5f, () => ChangeColor(1))
           .InsertCallback(29.9f, () => ChangeColor(0))
           .InsertCallback(29.9f, () => _success = true)
           .InsertCallback(30.5f, () => _success = false)
           .InsertCallback(30.5f, () => ChangeColor(1));
    }

    /// <summary>
    /// Animationのイベント処理（Loopバージョン）
    /// </summary>
    public void AnimLoop()
    {
        se.Append(Paper_1.DOLocalRotate(new Vector2(0, Paper_1.localRotation.y + 180), 0.5f).SetDelay(0.3f).SetLoops(70, LoopType.Restart))
           .InsertCallback(3.0f, () => ChangeColor(0))
           .InsertCallback(3.0f, () => _success = true)
           .InsertCallback(3.3f, () => _success = false)
           .InsertCallback(3.3f, () => ChangeColor(1))
           .InsertCallback(12.0f, () => ChangeColor(0))
           .InsertCallback(12.0f, () => _success = true)
           .InsertCallback(12.3f, () => _success = false)
           .InsertCallback(12.3f, () => ChangeColor(1))
           .InsertCallback(18.0f, () => ChangeColor(0))
           .InsertCallback(18.0f, () => _success = true)
           .InsertCallback(18.3f, () => _success = false)
           .InsertCallback(18.3f, () => ChangeColor(1))
           .OnComplete(() => { Common.gameClear = _success; Common.Instance.ChangeScene(Common.SceneName.Result); });
    }
}
