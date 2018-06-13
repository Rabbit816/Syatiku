using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour {    
    
    [SerializeField]
    private Sprite menu;
    [SerializeField]
    private Image[] menu2;
    private bool flg = true;
    void Start () {
        //画像を見えなくする
        for (int i = 0; i < menu2.Length; ++i)
        {            
            menu2[i].enabled = false;                   
        }        
    }		
    //ボタンを押してAnswerに表示
    public void OnClick() {            
        if (menu2[0].enabled == false)
        {
            menu2[0].sprite = menu;
            menu2[0].enabled = true;
            Debug.Log("肉");
        }
        else if (menu2[1].enabled == false)
        {
            menu2[1].sprite = menu;
            menu2[1].enabled = true;
            Debug.Log("酒");
        }
        else if (menu2[2].enabled == false)
        {
            menu2[2].sprite = menu;
            menu2[2].enabled = true;
            Debug.Log("刺身");
        }
        else  if(menu2[3].enabled == false)
        {
            menu2[3].sprite = menu;
            menu2[3].enabled = true;
            Debug.Log("サラダ");
        }            
    }
}