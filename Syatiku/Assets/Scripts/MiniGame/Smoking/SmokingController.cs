using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmokingController : MonoBehaviour {
    [SerializeField]
    private Image tabaco;
	// Use this for initialization
	void Start () {
        StartCoroutine(TimeDown());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator TimeDown()
    {
        while (tabaco.rectTransform.sizeDelta.x > 0)
        {
            tabaco.rectTransform.sizeDelta -= new Vector2(10,0);
            yield return null;
        }
    }
}
