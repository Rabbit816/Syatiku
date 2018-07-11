using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    public Slider InfoMeter;

    //メニューリスト
    public Image Yakitori;
    public Image Sake;
    public Image Salad;
    public Image Sashimi;

    //デンモク内で使う配列・変数
    private int[] InputOrderBox = new int[4];
    private int[] InputOrderCounter = new int[4];
    private float[] OrderListPos = new float[4] {-170, -265, -360, -455};
    private int DenmokuCounter = 0;
    

	void Start () {
        
	}
	
	void Update () {
		
	}
}
