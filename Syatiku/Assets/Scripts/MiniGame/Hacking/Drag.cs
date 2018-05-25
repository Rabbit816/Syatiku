using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 startR;
    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        startR = rect.localPosition;
    }

    public void OnBeginDrag(PointerEventData pointer)
    {
        
    }
    public void OnDrag(PointerEventData pointer)
    {
        Vector3 dragVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(dragVec);
    }
    public void OnEndDrag(PointerEventData pointer)
    {
        rect.localPosition = startR;
    }

}
