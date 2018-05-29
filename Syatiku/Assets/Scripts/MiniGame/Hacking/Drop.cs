using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler,IPointerEnterHandler
{

    private Transform target;
    private LayerMask layerMask = 8;

    private void Start()
    {

    }

    public void OnDrop(PointerEventData pointer)
    {
        //var raycastResults = new List<RaycastResult>();
        //EventSystem.current.RaycastAll(pointer, raycastResults);

        //foreach (var hit in raycastResults)
        //{
        //    // もし DroppableField の上なら、その位置に固定する
        //    if (hit.gameObject.CompareTag("DroppableField"))
        //    {
        //        transform.position = hit.gameObject.transform.position;
        //        this.enabled = false;
        //    }
        //}
    }

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    public void OnPointerEnter(PointerEventData pointer)
    {
        //Debug.Log("はいってるよん");
        //if (Input.GetMouseButtonUp(0) ||  layerMask == 8)
        //{
        //    Debug.Log("きてるぅ");
        //    //pointer.pointerDrag.transform.position = this.gameObject.transform.position;
        //}
        //if (pointer.pointerDrag == null)
        //{
        //    return;
        //}
    }

    private Transform SearchTransform(Transform trans)
    {
        if (trans.transform.CompareTag("answer"))
        {
            return trans;
        }
        if (trans == trans.root)
            return null;
        return trans;
    }
}
