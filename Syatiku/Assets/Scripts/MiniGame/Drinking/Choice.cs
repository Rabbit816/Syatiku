using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour {    
    
    [SerializeField]
    private Sprite menu;
    [SerializeField]
    private Image[] menu2;
    [SerializeField]
    private GameObject returnbutton;
           
    void Start () {
        Invisible();        
    }
    
    //ボタンを押してAnswerに表示
    public void OnClick() {            
        if (menu2[0].enabled == false)
        {
            menu2[0].sprite = menu;
            menu2[0].enabled = true;            
        }
        else if (menu2[1].enabled == false)
        {
            menu2[1].sprite = menu;
            menu2[1].enabled = true;
        }
        else if (menu2[2].enabled == false)
        {
            menu2[2].sprite = menu;
            menu2[2].enabled = true;
        }
        else  if(menu2[3].enabled == false)
        {
            menu2[3].sprite = menu;
            menu2[3].enabled = true;            
            StartCoroutine(Bo());            
        }           
    }
    //吹き出しの表示が終わったらボタンを表示する
    private IEnumerator Bo()
    {        
        yield return new WaitForSeconds(1f);
        returnbutton.gameObject.SetActive(true);
    }    
    
    public void Invisible()
    {
        //画像を見えなくする
        for (int i = 0; i < menu2.Length; ++i)
        {   
            menu2[i].enabled = false;                   
        }
        //注文するボタンを見えなくする
        returnbutton.gameObject.SetActive(false);
    }
}