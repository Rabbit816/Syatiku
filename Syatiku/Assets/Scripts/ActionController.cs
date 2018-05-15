using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

<<<<<<< HEAD
    //public void ChangeMinigame(int num)
    //{
    //    switch (num)
    //    {
    //        case 0:
    //            ChangeScene(SceneName.Smoking);
    //            break;
    //        case 1:
    //            ChangeScene(SceneName.Hacking);
    //            break;
    //        case 2:
    //            ChangeScene(SceneName.Drinking);
    //            break;
    //        default:
    //            break;
    //    }
    //}
=======
    public void ChangeMinigame(int num)
    {
        switch (num)
        {
            case 0:
                Common.Instance.FadeChangeScene(Common.SceneName.Smoking, 1.0f);
                break;
            case 1:
                Common.Instance.FadeChangeScene(Common.SceneName.Hacking, 1.0f);
                break;
            case 2:
                Common.Instance.FadeChangeScene(Common.SceneName.Drinking, 1.0f);
                break;
            default:
                break;
        }
    }
>>>>>>> da770ab9bb907ffa1ade7b243a51836978e4330f
}
