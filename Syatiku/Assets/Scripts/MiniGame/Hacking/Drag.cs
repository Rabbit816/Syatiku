using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections.Generic;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler,IPointerEnterHandler
{
    private Vector3 startR;
    private RectTransform rect;

    private Ray ray;
    private RaycastHit2D hit;
    int rayLayer = 0;

    Vector3 dragVec;
    Vector3 old_vec;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        startR = rect.localPosition;
        rayLayer =  8;
    }

    private void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData pointer)
    {
        
    }

    public void OnDrag(PointerEventData pointer)
    {
        dragVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(dragVec);
    }

    public void OnEndDrag(PointerEventData pointer)
    {
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("EndDrag");
        //hit = new RaycastHit2D();
        //if (Physics2D.Raycast(ray.origin, new Vector3(0,0,-30), Mathf.Infinity))
        //{
        //    if(hit.collider.tag == "answer")
        //    {
        //        Debug.Log("とれてるよ");
        //    }
        //    Debug.Log(hit.collider.gameObject.name);
        //    gameObject.transform.SetParent(hit.transform.transform);
        //}

        //if (hit)
        //{
        //    Debug.Log("OnEndDrag");
        //    if (hit.collider.tag == "answer")
        //    {
        //        gameObject.transform.parent = hit.collider.transform.transform;
        //        //gameObject.transform.position = hit.collider.GetComponent<Transform>().position;
        //    }
        //    else
        //    {
        //        rect.localPosition = startR;
        //    }
        //}
        //old_vec = vec;
        if (hit.collider)
        {
            Debug.Log("きるよ～" + hit.collider.tag.ToString());
        }
    }

    public void OnDrop(PointerEventData pointer)
    {
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        Debug.Log("はいってまっせ");
        foreach (var hit in raycastResults)
        {
            Debug.Log("foreachの中");
            // もし answer の上なら、その位置に固定する
            if (hit.gameObject.CompareTag("answer"))
            {
                Debug.Log("answer発見");
                transform.SetParent(hit.gameObject.transform, false);
                this.enabled = false;
            }
            else
            {
                rect.localPosition = startR;
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointer)
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("きてるぅ");
            //gameObject.transform.parent = hit.collider.transform.transform;
        }

        if (pointer.pointerDrag == null)
        {
            return;
        }
    }
}
