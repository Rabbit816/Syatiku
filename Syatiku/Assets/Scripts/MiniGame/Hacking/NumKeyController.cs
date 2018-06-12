using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumKeyController : MonoBehaviour {
    [SerializeField, Tooltip("拡大一回目の時の画像")]
    private Image isActiveDesk;
    [SerializeField,Tooltip("拡大二回目の時の画像")]
    private Image isActiveKey;
    [SerializeField,Tooltip("拡大一回目の時のナンバー")]
    private Text[] t_num = new Text[3];
    [SerializeField,Tooltip("拡大二回目の時のナンバー")]
    private Text[] t_num2 = new Text[3];
    [SerializeField,Tooltip("拡大二回目の時の南京錠画像")]
    private Image lockImage;
    private int[] keyNum = new int[3];
    private int[] answerNum = new int[3];
    private bool[] checkFlag = { false, false, false };
    private System.Random rand = new System.Random();

	// Use this for initialization
	void Start () {
        isActiveDesk.gameObject.SetActive(false);
        isActiveKey.gameObject.SetActive(false);
        RandNum();
        Debug.Log("answerNum:" + answerNum[0] + answerNum[1] + answerNum[2]);
        Debug.Log("KeyNum:" + keyNum[0] + keyNum[1] + keyNum[2]);
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

    /// <summary>
    /// 初期値ロックナンバーとロック解除ナンバーのランダム処理
    /// </summary>
    private void RandNum()
    {
        int rand_num = 0;
        int counter = 0;
        while (counter < answerNum.Length)
        {
            rand_num = rand.Next(0, 10);

            answerNum[counter] = rand_num;
            GameObject answerObject = GameObject.Find("Canvas/IntoPC/KeyNumber_" + counter);
            answerObject.GetComponentInChildren<Text>().text = rand_num.ToString();

            rand_num = rand.Next(0, 10);

            keyNum[counter] = rand_num;
            t_num[counter].text = keyNum[counter].ToString();
            t_num2[counter].text = keyNum[counter].ToString();
            //ansewrNumとkeyNumが全部一緒にならないように同じ配列の番号の時被らないようにする処理
            if (answerNum[counter] == keyNum[counter])
                counter--;
            counter++;
        }
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
