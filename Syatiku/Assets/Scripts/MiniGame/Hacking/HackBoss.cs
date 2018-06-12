using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackBoss : MonoBehaviour {

    [SerializeField, Tooltip("横から出てくる上司の画像")]
    private Image Boss_img;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AppearBoss()
    {
        Boss_img.GetComponent<Animator>().Play(0);
    }
}
