using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

    void Start()
    {
        Common.Instance.gameScore["Smoking"] += 0;
        Debug.Log(Common.Instance.gameScore["Smoking"]);
    }
    void Update()
    {
        
    }

    public void ChangeSelect()
    {
        //ChangeScene(SceneName.Select);
        Common.Instance.ChangeScene(Common.SceneName.Action);
    }
}
