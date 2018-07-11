using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Denmoku : MonoBehaviour {

    GameObject InfoMeter;

    //メニューリスト
    [SerializeField]
    Image Yakitori;
    [SerializeField]
    Image Sake;
    [SerializeField]
    Image Salad;
    [SerializeField]
    Image Sashimi;

    //デンモク内で使う配列・変数
    private int[] InputOrderBox = new int[4];
    private int[] InputOrderCounter = new int[4];
    private float[] OrderListPos = new float[4] {427, 332, 237, 142};
    private int DenmokuCounter = 0;
    

	void Start () {
        InfoMeter = GameObject.Find("DrinkMain/InfoMeter");
        InfoMeter.GetComponent<Slider>().value = 50;
	}
	
	void Update () {
		
	}
}
