using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PatteringEvent : MonoBehaviour {

    [SerializeField, Tooltip("Paper_0")]
    private RectTransform Paper_0;
    [SerializeField, Tooltip("Paper_1")]
    private RectTransform Paper_1;
    [SerializeField, Tooltip("Paper_2")]
    private RectTransform Paper_2;

    private IntoPCAction intopc_action;

    private Sequence seq;

    private int loopCount = 1;
    private Animator anim;
    private bool _eventResult = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        intopc_action = GetComponent<IntoPCAction>();
        seq = DOTween.Sequence();
        _eventResult = false;
        loopCount = 1;
        AnimationEvent();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Animationのイベント処理
    /// </summary>
    private void AnimationEvent()
    {
        Debug.Log("きてるよ");
        loopCount++;
        seq.Append(Paper_1.DOLocalRotate(new Vector2(0, 180), 0.5f).SetDelay(0.1f));
        seq.Append(Paper_1.DOLocalRotate(new Vector2(0, Paper_1.localRotation.y + 180), 0.5f).SetDelay(0.1f));
        seq.Append(Paper_1.DOLocalRotate(new Vector2(0, Paper_1.localRotation.y + 180), 0.5f).SetDelay(0.1f));
        seq.Append(Paper_1.DOLocalRotate(new Vector2(0, Paper_1.localRotation.y + 180), 0.5f).SetDelay(0.1f));
        //Paper_1.transform.localRotation = Vector2.zero;


        //seq.Append(Paper_0.DORotate(new Vector2(0, 180), 1.0f).SetDelay(0.5f));

        //seq.Append(Paper_2.DORotate(new Vector2(0, 180), 1.0f).SetDelay(0.5f));
    }
}
