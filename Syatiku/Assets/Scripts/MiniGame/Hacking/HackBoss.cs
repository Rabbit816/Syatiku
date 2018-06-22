using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackBoss : MonoBehaviour {

    //ボスが動いた回数
    private int moveCout = 0;

    [SerializeField, Tooltip("メーターの上司Object")]
    private GameObject Boss;

    [SerializeField, Tooltip("ドアの上司Object")]
    private GameObject ComeBoss;

    private HackTap hack_tap;

	// Use this for initialization
	void Start () {
        hack_tap = GetComponent<HackTap>();
        moveCout = 0;
        ComeBoss.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 上司が来て待ってる時の処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator WatchBoss()
    {
        hack_tap.PlaceButton(10);
        yield return new WaitForSeconds(1.5f);
        Boss.transform.localPosition = new Vector2(-365, -130);
    }

    /// <summary>
    /// メーターの上司が動く処理 
    /// </summary>
    public void MoveBoss()
    {
        Boss.transform.localPosition = new Vector2(Boss.transform.localPosition.x + 265, -130);
        moveCout++;
        if (moveCout % 2 == 0)
            ComeOnBoss();
        else
            ComeBoss.SetActive(false);
    }

    /// <summary>
    /// 上司が部屋に来た時の処理
    /// </summary>
    private void ComeOnBoss()
    {
        ComeBoss.SetActive(true);
        Debug.Log("上司がきた:" + moveCout);
    }
}
