using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAction : MonoBehaviour {

	// Use this for initialization
	public void OnClick()
    {
        Common.Instance.ChangeScene(Common.SceneName.Action);
    }
}
