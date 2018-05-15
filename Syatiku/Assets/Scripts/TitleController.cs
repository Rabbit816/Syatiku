using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : BaseController {
    [SerializeField]
    private Image fadePanel;

    private void Start()
    {
        StartCoroutine(FadeOut(fadePanel));
    }
    public void ChangeSelect()
    {
        //ChangeScene(SceneName.Select);
        ChangeScene(SceneName.Action);
    }
}
