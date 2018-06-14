using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter: MonoBehaviour {

    private GameObject obj;
    private Vector2 me;
    bool aaa = false;

    void Start () {
        obj = GameObject.Find("Image");
        me = obj.transform.localScale;
    }
	
	void Update () {
        Timer();        
    }

    private void Timer()
    {
        if (aaa == false)
        {
            me.x -= 0.5f * Time.deltaTime;
            obj.transform.localScale = me;
            if (me.x < 0)
            {
                
                aaa = true;
            }
        }
    }
}
