using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTest : MonoBehaviour {
    private float alfa;
    private float s;
    [SerializeField]
    private GameObject panel;
	// Use this for initialization
	void Start () {
        alfa = 0.01f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(MoveFade());
        }	
	}

    public IEnumerator MoveFade()
    {
        Debug.Log("Move");
        while (s < 1)
        {
            panel.GetComponent<Image>().color += new Color(0, 0, 0, alfa);
            s += alfa;
            yield return null;
        }
    }
}
