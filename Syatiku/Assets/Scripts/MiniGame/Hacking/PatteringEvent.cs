using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PatteringEvent : MonoBehaviour {

    [SerializeField, Tooltip("Paper_1")]
    private RectTransform Paper_1;

    private IntoPCAction intopc_action;

    private Sequence seq;
    private bool _success = false;
    [SerializeField, Tooltip("もう一回ボタン")]
    private GameObject Onemore;

	// Use this for initialization
	void Start () {
        Onemore.SetActive(false);
        intopc_action = GetComponent<IntoPCAction>();
        seq = DOTween.Sequence();
        _success = false;
        AnimationEvent();
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
                break;
            case 1:
                Paper_1.GetComponent<Image>().color = new Color(255,255,255);
                break;
            default:
                Debug.Log("ColorNum :" + num);
                break;
        }
    }

    /// <summary>
    /// もう一回ボタンの処理
    /// </summary>
    public void OneMore()
    {
        Onemore.SetActive(false);
        AnimationEvent();
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
    /// Animationのイベント処理
    /// </summary>
    public void AnimationEvent()
    {
        Button btn = Paper_1.GetComponent<Button>();
        seq.Append(Paper_1.DOLocalRotate(new Vector2(0, Paper_1.localRotation.y + 180), 0.8f).SetDelay(0.5f).SetLoops(10, LoopType.Restart))
            .InsertCallback(0f, () => _success = false)
            .InsertCallback(4.49f, () => ChangeColor(0))
            .InsertCallback(4.49f, () => _success = true)
            .InsertCallback(5.2f, () => _success = false)
            .InsertCallback(5.2f, () => ChangeColor(1))
            .Play();
        Onemore.SetActive(true);
    }
}
