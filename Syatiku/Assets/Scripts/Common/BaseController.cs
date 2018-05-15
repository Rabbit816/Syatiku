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
    public float alfa;
    [HideInInspector]
    public float s;

    public virtual void Start()
    {
        alfa = 0.01f;
    }
    public void ChangeScene(SceneName name)
    {
        SceneManager.LoadScene((int)name);
    }

    //public IEnumerator FadeIn(Image panel)
    //{
    //    s = 0;
    //    panel.gameObject.SetActive(true);
    //    Debug.Log("In");
    //    while (s < 1)
    //    {
    //        Debug.Log(s);
    //        panel.GetComponent<Image>().color += new Color(0, 0, 0, alfa);
    //        s += alfa;
    //        yield return null;
    //    }
    //}

    //public IEnumerator FadeOut(Image panel)
    //{
    //    s = 1;
    //    Debug.Log("Out");
    //    Debug.Log(s);
    //    while (s > 0)
    //    {
            
    //        panel.GetComponent<Image>().color -= new Color(0, 0, 0, alfa);
    //        s -= alfa;
    //        Debug.Log(s);
    //        yield return null;
    //    }
    //    yield return new WaitForSeconds(1.0f);
    //    panel.gameObject.SetActive(false);
    //}
}
