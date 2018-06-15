using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumKeyController : MonoBehaviour {
    [SerializeField]
    private Image isActiveDesk,isActiveKey;
    [SerializeField]
    private Text[] t_num = new Text[3];
    [SerializeField]
    private Text[] t_num2 = new Text[3];
    [SerializeField]
    private Image lockImage;
    private int[] keyNum = new int[3];
    private int[] answerNum = { 2, 1, 3 };
    private bool[] checkFlag = { false, false, false };
	// Use this for initialization
	void Start () {
        isActiveDesk.gameObject.SetActive(false);
        isActiveKey.gameObject.SetActive(false);
        for(int i = 0; i < keyNum.Length; i++) {
            int rand = Random.Range(0, 9);
            keyNum[i] = rand;
            t_num[i].text = keyNum[i].ToString();
            t_num2[i].text = keyNum[i].ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IsActive(int x) {
        if(x == 0)
        isActiveDesk.gameObject.SetActive(true);
        else
        isActiveKey.gameObject.SetActive(true);
    }

    public void HideImage(int x) {
        if (x == 0)
            isActiveDesk.gameObject.SetActive(false);
        else
            isActiveKey.gameObject.SetActive(false);
    }

    public void NumChange(int num) {
        keyNum[num]++;
        if (keyNum[num] > 9) {
            keyNum[num] = 0;
        }
        t_num[num].text = keyNum[num].ToString();
        t_num2[num].text = keyNum[num].ToString();
        Check();
    }
    public void Check() {
        for(int i = 0; i < answerNum.Length; i++) {
            if (keyNum[i] == answerNum[i])
                checkFlag[i] = true;
            else
                checkFlag[i] = false;
            Debug.Log("flag" + i + "=" + checkFlag[i]);
            if (checkFlag[0])
                if (checkFlag[1])
                    if (checkFlag[2]) {
                        isActiveDesk.gameObject.SetActive(false);
                        isActiveKey.gameObject.SetActive(false);
                        lockImage.gameObject.SetActive(false);
                    }
        }
    }
}
