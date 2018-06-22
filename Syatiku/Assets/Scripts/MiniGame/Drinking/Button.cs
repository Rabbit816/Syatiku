using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {

    private GameObject obj;
    private Vector2 me;
    private bool bbb = false;
    [SerializeField]
    private Image[] Meter;

    void Start()
    {
        obj = GameObject.Find("Image");
        me = obj.transform.localScale;
    }

    void Update()
    {
        Timer();
    }

    //Denmokuの上への移動方法
    public void OnClick()
    {
        bbb = true;
        transform.parent = GameObject.Find("Denmoku").transform;
        GameObject.Find("Denmoku").transform.position = new Vector2(0, 0);
    }

    //Denmokuの下への移動方法
    public void OnClickReturn()
    {
        bbb = false;
        Debug.Log(bbb);
        transform.parent = GameObject.Find("Denmoku").transform;
        GameObject.Find("Denmoku").transform.position = new Vector2(0, -550f);
        FindObjectOfType<DrinkMain>().button.gameObject.SetActive(false);
    }

    //メーターを減少させる
    private void Timer()
    {
        if (bbb)
        {
            me.x -= 0.1f * Time.deltaTime;
            obj.transform.localScale = me;
            if (me.x < 0)
            {
                bbb = false;
                //シーンの移動
                Common.Instance.ChangeScene(Common.SceneName.Drinking);
            }
        }
    }
}