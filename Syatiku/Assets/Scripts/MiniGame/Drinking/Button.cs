using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {
    
    [SerializeField]
    private Image learnButton;    
      
    public void OnClick()
    {        
        //Denmokuの移動方法
        transform.parent = GameObject.Find("Denmoku").transform;       
        GameObject.Find("Denmoku").transform.position = new Vector3(476f, 264f);       
        Debug.Log("クリック");
    }   
}
