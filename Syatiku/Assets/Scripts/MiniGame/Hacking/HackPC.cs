using UnityEngine;

public class HackPC : MonoBehaviour {

    private float _drag = 0.0f;
    private float _dorp = 0.0f;
    private bool _dragActive = false;
    private Vector3 dragVec;
    private Vector3 screenToWorldPointPosition;
    private Vector3 pointer;
    private GameObject wordbtn;
    private Transform trans;

    // Use this for initialization
    void Start () {
        _dragActive = false;
        wordbtn = GameObject.Find("Prefabs/MiniGame/WordImage");
        //Button btn = wordbtn.GetComponent<Button>().OnClick(OnTouchDrag(trans));
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    /// <summary>
    /// 欠けてる文章の処理
    /// </summary>
    public void ChippedString()
    {
        // ランダムで文章を選択
        // 表示
    }

    /// <summary>
    /// バラバラの文字処理
    /// </summary>
    private void PiecesString()
    {
        // 欠けてる文章によって出す文字変更
        // 場所を設定　ランダムでやるかも

    }

    private bool CheckString()
    {
        // 文字列があっているかどうか処理

        return true;
    }

    private void OnTouchDrag(Transform a)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            dragVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(dragVec);
            a.transform.position = screenToWorldPointPosition;
        }
    }
}
