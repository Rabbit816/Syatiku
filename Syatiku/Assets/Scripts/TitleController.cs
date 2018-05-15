using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

    void Update()
    {
        
    }

    public void ChangeSelect()
    {
        //ChangeScene(SceneName.Select);
        Common.Instance.FadeChangeScene(Common.SceneName.Action,1.0f);
    }
}
