using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HackBoss : MonoBehaviour {

    [SerializeField, Tooltip("メーターの上司Object")]
    private GameObject Boss;
    [SerializeField, Tooltip("ドアの上司Object")]
    private GameObject ComeBoss;
    [SerializeField, Tooltip("EventSystem")]
    private EventSystem event_system;
    [SerializeField, Tooltip("Zoom object")]
    private GameObject Zoom;
    [SerializeField, Header("ボスの質問に答える時の選択Object")]
    private GameObject ChooseObject;
    [SerializeField, Header("ボスの質問に答える時のText")]
    private Text chose_text;
    [HideInInspector]
    public int comingCount = 0;

    private HackTap hack_tap;
    private HackMain hack_main;
    private PatteringEvent patte;
    [Header("上司が待機してる時間")]
    public float BossTimer = 5.0f;
    private float Bosswait;
    private bool _chooseTap = false;
    private bool _commingboss = false;
    private bool _gameover = false;
    [HideInInspector]
    public bool _choosing = false;
    private int rand = 0;
    private float req = 3f;

    // Use this for initialization
    void Start () {
        hack_tap = GetComponent<HackTap>();
        hack_main = GetComponent<HackMain>();
        patte = GetComponent<PatteringEvent>();

        ChooseObject.SetActive(false);
        ComeBoss.SetActive(false);
        _commingboss = false;
        _gameover = false;
        _choosing = false;
        comingCount = 0;
        Bosswait = BossTimer + 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
        //ボスランダム処理
        if (hack_main._timerActive)
        {
            req -= Time.deltaTime;
            if (req <= 0f)
            {
                Debug.Log("Active_in");
                rand = Random.Range(0, 2);
                Debug.Log("Random:" + rand);
                if (rand == 1 && !patte._PatteringPlay)
                {
                    MoveBoss();
                }
                req = 3f;
            }
        }
       //ボスが来た時のタイマー処理
        if (_commingboss)
        {
            Bosswait -= Time.deltaTime;
            chose_text.text = Bosswait.ToString("f1");
            if (_chooseTap)
            {
                Boss.transform.localPosition = new Vector2(-365, -130);
                ComeBoss.SetActive(false);
                hack_tap.PlaceButton(12);
                _chooseTap = false;
                _commingboss = false;
                Bosswait = BossTimer + 0.1f;
            }
            else if(Bosswait <= 0.0f)
            {
                Bosswait = 0.0f;
                if (!_gameover)
                {
                    _gameover = true;
                    Common.Instance.clearFlag[Common.Instance.isClear] = false;
                    Common.Instance.ChangeScene(Common.SceneName.Result);
                }
            }
        }
	}

    /// <summary>
    /// 上司が来て待ってる時の処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator WatchBoss(float time)
    {
        yield return new WaitForSeconds(time);
        event_system.enabled = true;
        _commingboss = true;
        ChooseObject.SetActive(true);
    }

    /// <summary>
    /// メーターの上司が動く処理 
    /// </summary>
    public void MoveBoss()
    {
        RectTransform boss_rect = Boss.GetComponent<RectTransform>();
        boss_rect.transform.DOMoveX(boss_rect.transform.position.x + 2.6f, 0.5f);
        comingCount++;
        if (comingCount%4 == 0)
        {
            hack_tap.PlaceButton(11);
            Zoom.transform.GetChild(3).gameObject.SetActive(false);
            Zoom.transform.GetChild(4).gameObject.SetActive(false);
            event_system.enabled = false;
            ComeOnBoss();
        }
    }

    /// <summary>
    /// 上司が部屋に来た時の処理
    /// </summary>
    public void ComeOnBoss()
    {
        hack_tap.PlaceButton(13);
        _choosing = true;
        if (!ComeBoss.activeSelf)
        {
            ComeBoss.SetActive(true);
            StartCoroutine(WatchBoss(1.5f));
        }
    }

    /// <summary>
    /// 選択ボタン処理
    /// </summary>
    public void ChooseButton()
    {
        _chooseTap = true;
        ChooseObject.SetActive(false);
        _choosing = false;
    }
}