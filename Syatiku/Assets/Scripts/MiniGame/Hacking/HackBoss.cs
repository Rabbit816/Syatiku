using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HackBoss : MonoBehaviour {

    [SerializeField, Tooltip("メーターの上司Object")]
    private GameObject Boss;

    [SerializeField, Tooltip("ドアの上司Object")]
    private GameObject ComeBoss;
    [SerializeField, Tooltip("作業してる風の人Object（RectTransform）")]
    private RectTransform WorkingHuman;
    [SerializeField, Tooltip("作業してる風の人Object（RectTransform）")]
    private GameObject WorkingObject;

    private HackTap hack_tap;
    private float BossTimer = 5.0f;
    private bool _worktap = false;
    private bool _commingboss = false;
    private HackMain hack_main;

	// Use this for initialization
	void Start () {
        WorkingObject.SetActive(false);
        hack_tap = GetComponent<HackTap>();
        hack_main = GetComponent<HackMain>();

        ComeBoss.SetActive(false);
        _worktap = false;
        _commingboss = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (_commingboss)
        {
            BossTimer -= Time.deltaTime;
            if (_worktap)
            {
                Boss.transform.localPosition = new Vector2(-365, -130);
                ComeBoss.SetActive(false);
            }
            else if(BossTimer <= 0.0f)
            {
                Common.gameClear = false;
                Common.Instance.ChangeScene(Common.SceneName.Result);
            }
        }
	}

    /// <summary>
    /// 上司が来て待ってる時の処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator WatchBoss()
    {
        yield return new WaitForSeconds(1.5f);
    }

    /// <summary>
    /// メーターの上司が動く処理 
    /// </summary>
    public void MoveBoss()
    {
        Boss.transform.localPosition = new Vector2(Boss.transform.localPosition.x + 265, -130);
        hack_main.comingCount++;
        if (hack_main.comingCount % 2 == 0)
        {
            hack_tap.PlaceButton(11);
            ComeOnBoss();
        }
        else
            ComeBoss.SetActive(false);
    }

    /// <summary>
    /// 上司が部屋に来た時の処理
    /// </summary>
    private void ComeOnBoss()
    {
        hack_tap.PlaceButton(13);
        ComeBoss.SetActive(true);
        StartCoroutine(WatchBoss());
        _commingboss = true;
    }

    public void WorkButton()
    {
        _worktap = true;
        WorkingObject.SetActive(true);
        //WorkingHuman.DOLocalJump(WorkingHuman.transform.localPosition, 3f, 5, 5f);
        WorkingObject.SetActive(false);
    }
}
