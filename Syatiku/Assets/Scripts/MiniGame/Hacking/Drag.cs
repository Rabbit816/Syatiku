using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections.Generic;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startR;
    private RectTransform rect;

    private Ray ray;
    private RaycastHit2D hit;
    int rayLayer = 0;

    Vector3 dragVec;
    Vector3 old_vec;

    private GameObject collect;

    void Start()
    {
        collect = GameObject.Find("Canvas/IntoPC/CollectedWord");
    }

    private void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData pointer)
    {
        
    }

    public void OnDrag(PointerEventData pointer)
    {
        if (gameObject.transform.transform.tag == "asnwer")
        {
            Debug.Log("おとんみっけ");
            transform.SetParent(collect.transform, false);
        }
        dragVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(dragVec);
    }

    public void OnEndDrag(PointerEventData pointer)
    {
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        foreach (var hit in raycastResults)
        {
            
            Debug.Log("foreachの中");
            // もし answer の上なら、その位置のVector2(0,0)に固定する
            if (hit.gameObject.CompareTag("answer"))
            {
                if (hit.gameObject.transform.childCount == 0)
                {
                    transform.SetParent(hit.gameObject.transform, false);
                    transform.localPosition = Vector2.zero;
                }
                else
                {
                    return;
                }
            }
        }
            
    }
}
