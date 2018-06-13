using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectController : MonoBehaviour,
     IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    private Image blackMan, whiteMan;
    [SerializeField]
    private float zoomScale;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    int a;
    public void OnPointerEnter(PointerEventData eventData) {
        switch (gameObject.name) {
            case "Black":
                blackMan.transform.localScale = new Vector2(zoomScale, zoomScale);
                Debug.Log("Black");
                break;
            case "White":
                whiteMan.transform.localScale = new Vector2(zoomScale, zoomScale);
                Debug.Log("White");
                break;
            default:
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        switch (gameObject.name) {
            case "Black":
                blackMan.transform.localScale = new Vector2(1f, 1f);
                Debug.Log("Black");
                break;
            case "White":
                whiteMan.transform.localScale = new Vector2(1, 1f);
                Debug.Log("White");
                break;
            default:
                break;
        }
    }
}
