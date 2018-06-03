using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {
    
    [SerializeField]
    private Image learnButton;    

    // Use this for initialization
    void Start()
    {
       
    }
   
    void Update ()
    {
        
	}
    public void OnClick()
    {        
        //Denmokuの移動方法
        transform.parent = GameObject.Find("Denmoku").transform;       
        GameObject.Find("Denmoku").transform.position = new Vector3(436f, 246f, 0);       
        Debug.Log("クリック");
    }   
}
