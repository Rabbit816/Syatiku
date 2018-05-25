using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour {

    private Transform target;

    public void OnDrop(PointerEventData pointer)
    {
        target = SearchTransform(pointer.pointerPressRaycast.gameObject.transform);
        gameObject.transform.position = target.position;
        Debug.Log("きてるよ");
    }

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    public void OnPointerEnter(PointerEventData pointer)
    {
        if (pointer.pointerDrag == null) return;


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
