using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections.Generic;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Vector3 dragVec;
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
        transform.SetParent(collect.transform, false);
        if (gameObject.transform.tag == "asnwer")
        {
            Debug.Log("おとんみっけ");
            
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
