using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WorningController : MonoBehaviour {
    [SerializeField]
    private GameObject[] WLabel = new GameObject[2]; // LebelUI

    [SerializeField]
    private GameObject worning; // worningUI

	// Use this for initialization
	void Start () {
        worning.transform.DOScale(1.5f, 1f).SetLoops(-1);
        //while (true) {
            
        //}
	}
}
