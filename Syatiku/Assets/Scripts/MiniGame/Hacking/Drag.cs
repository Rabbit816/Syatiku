using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Ray ray;
    private RaycastHit2D hit;
    private GameObject collect;
    Vector3 dragVec;

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
        RaycastResult result = pointer.pointerPressRaycast;
        Debug.Log(gameObject.transform.parent.tag);
        if (gameObject.transform.transform.tag  == "asnwer")
        {
            Debug.Log("おとんみっけ");
            transform.SetParent(collect.transform, false);
        }

        dragVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(dragVec);
    }

    public void OnEndDrag(PointerEventData pointer)
    {
        Debug.Log("とれてるよ");

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        foreach (var hit in raycastResults)
        {
            // もし answer の上なら、その位置に固定する
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