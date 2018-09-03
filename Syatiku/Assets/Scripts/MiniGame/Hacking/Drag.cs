using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Drag : MonoBehaviour, IDragHandler, IEndDragHandler
{
<<<<<<< HEAD
<<<<<<< HEAD
    private Ray ray;
    private RaycastHit2D hit;
    private GameObject collect;
    Vector3 dragVec;
=======

=======
>>>>>>> master
    private Vector3 dragVec;
    private GameObject collect;
>>>>>>> 28e16f5bd3286929b5a04a3c81772ffbe166d95a

    void Start()
    {
<<<<<<< HEAD
        collect = GameObject.Find("Canvas/IntoPC/CollectedWord");
    }

    private void Update()
    {

    }

    public void OnBeginDrag(PointerEventData pointer)
    {
        
=======
        //親の設定
        if(gameObject.tag == "string")
        {
            collect = GameObject.Find("Canvas/PC/PassWordFase/Collect");
        }else if (gameObject.tag == "windows")
        {
            collect = GameObject.Find("Canvas/PC/WindowFase/Window");
        }else if(gameObject.tag == "folder")
        {
            collect = GameObject.Find("Canvas/Zoom/AdminStrator/AdminPage/Collect");
        }
>>>>>>> master
    }

    /// <summary>
    /// ドラッグした時、ドラッグ中の処理
    /// </summary>
    /// <param name="pointer"></param>
    public void OnDrag(PointerEventData pointer)
    {
<<<<<<< HEAD
        RaycastResult result = pointer.pointerPressRaycast;
        Debug.Log(gameObject.transform.parent.tag);
        if (gameObject.transform.transform.tag  == "asnwer")
        {
            Debug.Log("おとんみっけ");
            transform.SetParent(collect.transform, false);
        }

=======
        transform.SetParent(collect.transform, false);
<<<<<<< HEAD
        if (gameObject.transform.tag == "asnwer")
        {
            Debug.Log("おとんみっけ");
            
        }
>>>>>>> 28e16f5bd3286929b5a04a3c81772ffbe166d95a
=======
>>>>>>> master
        dragVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(dragVec);
    }

    /// <summary>
    /// ドラッグし終わった時の処理
    /// </summary>
    /// <param name="pointer"></param>
    public void OnEndDrag(PointerEventData pointer)
    {
<<<<<<< HEAD
        Debug.Log("とれてるよ");

=======
>>>>>>> 28e16f5bd3286929b5a04a3c81772ffbe166d95a
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        foreach (var hit in raycastResults)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            // もし answer の上なら、その位置に固定する
=======
=======
            
            Debug.Log("foreachの中");
>>>>>>> parent of 3ed5bff... Hacking 細かい調節
            // もし answer の上なら、その位置のVector2(0,0)に固定する
>>>>>>> 28e16f5bd3286929b5a04a3c81772ffbe166d95a
            if (hit.gameObject.CompareTag("answer"))
            {
                if (hit.gameObject.transform.childCount == 0)
                {
                    transform.SetParent(hit.gameObject.transform, false);
                    transform.localPosition = Vector2.zero;
                }
                else
                    return;
<<<<<<< HEAD
                }
<<<<<<< HEAD
                
            }
        }
    }
}
=======
=======
>>>>>>> master
            }
        }
    }
}
>>>>>>> 28e16f5bd3286929b5a04a3c81772ffbe166d95a
