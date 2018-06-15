using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntoPCAction : MonoBehaviour {

    [SerializeField,Tooltip("PC内のでる場所")]
    private GameObject[] PassWordObject;

    private Transform password_child;

    //配置した結果の判断
    private bool _isResult = false;

	// Use this for initialization
	void Start () {
        _isResult = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckPassWord()
    {
        for(int i=0; i < PassWordObject.Length; i++)
        {
            Text quest_text = PassWordObject[i].GetComponentInChildren<Text>();
            if (PassWordObject[i].gameObject.transform.childCount != 0)
            {
                
            }else {
                _isResult = false;
                break;
            }
                
        }
    }
}
