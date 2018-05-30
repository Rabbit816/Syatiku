using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour {

    [SerializeField]
    private Sprite[] menu2;
   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick() {
        Debug.Log("選んだな⁉");
    }
}
