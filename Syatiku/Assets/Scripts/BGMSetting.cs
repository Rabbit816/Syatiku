using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSetting : MonoBehaviour {
    [SerializeField]
    private AudioClip aClip;
	// Use this for initialization
	void Start () {
        Common.Instance.PlayBGM(aClip);
	}
}
