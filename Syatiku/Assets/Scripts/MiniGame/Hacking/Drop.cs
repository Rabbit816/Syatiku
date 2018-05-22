using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour {

    private Text dropText;

    public void OnDrop(PointerEventData pointer)
    {
        string text = Drag.dragText;
    }

    public void OnPointerEnter(PointerEventData pointer)
    {
        if (pointer.pointerDrag == null) return;
    }
}
