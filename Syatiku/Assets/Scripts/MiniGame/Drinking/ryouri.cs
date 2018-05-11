using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;


public class ryouri : MonoBehaviour {
    [SerializeField]
    private Image[] tnm;    
    private int index;
    private bool open;
    private int[] num;
    private int[] num2;
    private float timer;

    // Use this for initialization
    void Start () {
        num = new int[4] { 0, 1, 2, 3 };
        num2 = num.OrderBy(i => Guid.NewGuid()).ToArray();
        for (int i = 0; i < tnm.Length; ++i)
        {
            tnm[i].enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(open == true)
        {
            if (timer > 1)
            {
                open = false;
                tnm[index].enabled = false;          
                Debug.Log("料理表示");
            }
            else
            {
                open = true;
                index = Array.IndexOf(num2, 0);
                tnm[index].enabled = true;
                Debug.Log("料理非表示");
            }
        }
        
	}
}
