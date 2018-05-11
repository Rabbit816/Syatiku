using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Common : MonoBehaviour {

	public enum SceneName
    {
        Title = 0,
        Scenario,
        Select,
        Action,
        Smoking,
        Hacking,
        Drinking,
        Boss,
        Result,
    }

    public void ChangeScene(SceneName name)
    {
        SceneManager.LoadScene((int)name);
    }

}
