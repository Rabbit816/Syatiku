using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottom : MonoBehaviour {
    
    [SerializeField]
    public Image LearnBottom;
    public int count;

    // Use this for initialization
    void Start()
    {
        LearnBottom.gameObject.SetActive(false);        
    }
   
    void Update ()
    {
        
	}
    public void OnClick()
    {
        transform.parent = GameObject.Find("Denmoku").transform;       
        GameObject.Find("Denmoku").transform.position = new Vector3(330f, 186f, 0);       
        Debug.Log("クリック");
    }
}
