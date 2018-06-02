using UnityEngine;
using UnityEngine.UI;

public class Choice : MonoBehaviour {

    [SerializeField]
    private Sprite[] menu;

    [SerializeField]
    private Image[] food;

   
    // Use this for initialization
    void Start () {
        for (int i = 0; i < food.Length; ++i)
        {
            food[i].enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick() {
        Debug.Log("選んだな⁉");
    }
}
