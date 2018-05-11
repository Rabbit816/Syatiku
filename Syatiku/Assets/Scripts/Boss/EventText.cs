using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventText : MonoBehaviour
{
    RectTransform rectTransform;
    Vector3 moveDir;
    float moveSpeed;
    bool isFlick;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        moveDir = Vector3.zero;
        moveSpeed = 1.0f;
        isFlick = false;
    }

    void Update()
    {

        //座標移動
        if (isFlick)
        {
            rectTransform.localPosition += moveDir * moveSpeed;
        }

    }

    public void MyPointerDownUI()
    {
        BossScene.SetMoveForce(out moveDir, out moveSpeed);
        isFlick = true;
    }

    public void MyDragUI()
    {
        transform.position = Input.mousePosition;
    }


}
