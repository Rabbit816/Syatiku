using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WorningController : MonoBehaviour {
    [SerializeField]
    private GameObject WLabel; // LebelUI

    [SerializeField]
    private GameObject worning; // worningUI

	// Use this for initialization
	void Start () {
        worning.transform.DOScale(1.5f, 1f).SetLoops(-1);
        Sequence seq = DOTween.Sequence();
        seq.Append(
            worning.transform.DOScale(1.3f, 1f).SetEase(Ease.Linear)
        );
        seq.Append(
            worning.transform.DOScale(1.0f, 1f).SetEase(Ease.Linear)
        );
        seq.SetLoops(-1);

        WLabel.transform.DOLocalMoveX(-800, 2f).SetEase(Ease.Linear).SetLoops(-1);
    }
}
