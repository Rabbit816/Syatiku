using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour {

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

    [HideInInspector]
    public float s;
    [HideInInspector]
    public float alfa = 0.01f;

    public void ChangeScene(SceneName name)
    {
        SceneManager.LoadScene((int)name);
    }

    public IEnumerator FadeIn(Image panel)
    {
        panel.gameObject.SetActive(true);
        s = 0;
        Debug.Log("In");
        while (s < 1)
        {
            panel.GetComponent<Image>().color += new Color(0, 0, 0, alfa);
            s += alfa;
            yield return null;
        }
    }

    public IEnumerator FadeOut(Image panel)
    {
        s = 1;
        Debug.Log("Out");
        while (s > 0)
        {
            panel.GetComponent<Image>().color -= new Color(0, 0, 0, alfa);
            s -= alfa;
            yield return null;
        }
        //yield return new WaitForSeconds(1.0f);
        //panel.gameObject.SetActive(false);
    }
}
