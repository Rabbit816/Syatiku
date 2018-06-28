using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePointerController : MonoBehaviour {

    [SerializeField]
    Image damageGage;

    RectTransform rectTransform;
    float damageGageWidth;
    Vector3 targetPos;

    [SerializeField, Header("最大ダメージ量")]
    int damagePointMax;
    static int damagePoint;

    [SerializeField, Header("移動速度")]
    float moveSpeed = 2f;

    void Start () {
        rectTransform = GetComponent<RectTransform>();
        damageGageWidth = damageGage.rectTransform.sizeDelta.x;
        targetPos = rectTransform.position;
    }
	
	void Update () {
        if (rectTransform.position != targetPos)
        {
            float distance = (targetPos - rectTransform.position).sqrMagnitude;
            if (distance < 1f)
            {
                rectTransform.position = targetPos;
            }
            rectTransform.position = Vector3.Lerp(rectTransform.position, targetPos, Time.deltaTime * moveSpeed);
        }
    }

    public void DamagePointUp()
    {
        if (damagePoint < damagePointMax)
        {
            damagePoint++;
            targetPos.x += damageGageWidth / damagePointMax;
        }
    }

    public void DamagePointDown()
    {
        if (damagePoint > 0)
        {
            damagePoint--;
            targetPos.x -= damageGageWidth / damagePointMax;
        }
    }
}
