using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickTextPart : MonoBehaviour {

    [SerializeField]
    GameObject sarcasmTextPrefab;
    [SerializeField]
    RectTransform sarcasmTextBox;
    List<GameObject> sarcasmTextList = new List<GameObject>();

    float spawnTextTimer;
    float spawnTextTime = 3.0f;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (spawnTextTimer > spawnTextTime)
        {
            //テキストの生成
            SpawnSarcasmText();
        }
        spawnTextTimer += Time.deltaTime;
    }

    void SpawnSarcasmText()
    {
        //タイマー初期化
        spawnTextTimer = 0;
        spawnTextTime = Random.Range(1, 5);

        for (int i = 0; i < sarcasmTextList.Count; i++)
        {

            //使われていない（非表示中の）ものがあれば再利用
            if (!sarcasmTextList[i].activeSelf)
            {
                sarcasmTextList[i].SetActive(true);
                return;
            }
        }

        //全て使用中なら新しく生成
        GameObject sarcasmText = Instantiate(sarcasmTextPrefab);
        //sarcasmMessageBoxの子要素に
        sarcasmText.transform.SetParent(sarcasmTextBox, false);
        //リストに追加
        sarcasmTextList.Add(sarcasmText);
    }
}
