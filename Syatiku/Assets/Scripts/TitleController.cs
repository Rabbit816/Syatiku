using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {
    [SerializeField]
    private GameObject title;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void ChangeMode(int mode)
    {
        switch (mode)
        {
            case 0:
                Common.Instance.ChangeScene(Common.SceneName.Action);
                break;
            case 1:
                Common.Instance.ChangeScene(Common.SceneName.Action);
                break;
            default:
                break;
        }
        
    }

    public void Select()
    {
        title.SetActive(false);
    }
}
