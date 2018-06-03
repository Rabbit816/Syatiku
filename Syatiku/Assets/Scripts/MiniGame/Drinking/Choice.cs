using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour {    

    [SerializeField]
    private Sprite[] menu;
    [SerializeField]
    private Image[] niku;
    [SerializeField]
    private Image[] sake;
    [SerializeField]
    private Image[] sashimi;
    [SerializeField]
    private Image[] salad;    

    private int i;
    
    void Start () {
        //画像を見えなくする
        for (int i = 0; i < sake.Length; ++i)
        {
            niku[i].enabled = false;
            sake[i].enabled = false;
            sashimi[i].enabled = false;
            salad[i].enabled = false;            
        }       
    }
	
	void Update () {
		
	}

    public void OnClick() {    
        //ボタンを押してAnswerに表示
        if (niku[i].enabled == false)
        {
            
            Debug.Log("肉");
        }
        else if (sake[i].enabled == false)
        {
            
            Debug.Log("酒");
        }
        else if (sashimi[i].enabled == false)
        {
            
            Debug.Log("刺身");
        }
        else if (salad[i].enabled == false)
        {
            
            Debug.Log("サラダ");
        }            
    }
}