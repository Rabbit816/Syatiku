﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectController : MonoBehaviour,
     IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    private Image blackMan, whiteMan;
    [SerializeField]
    private float zoomScale;

    public void OnPointerEnter(PointerEventData eventData) {
        switch (gameObject.name) {
            case "Black":
                blackMan.transform.localScale = new Vector2(zoomScale, zoomScale);
                break;
            case "White":
                whiteMan.transform.localScale = new Vector2(zoomScale, zoomScale);
                break;
            default:
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        switch (gameObject.name) {
            case "Black":
                blackMan.transform.localScale = new Vector2(1f, 1f);
                break;
            case "White":
                whiteMan.transform.localScale = new Vector2(1, 1f);
                break;
            default:
                break;
        }
    }
}
