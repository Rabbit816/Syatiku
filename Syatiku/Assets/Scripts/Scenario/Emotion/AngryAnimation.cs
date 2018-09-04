using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AngryAnimation : MonoBehaviour {

    bool isAngry;

    void Start()
    {
        isAngry = gameObject.name.Contains("Angry");
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        float time = 1f;
        RectTransform r = GetComponent<RectTransform>();
        Vector3 pos = r.localPosition;
        Vector3 targetPos = Vector3.zero;
        if (isAngry)
        {
            targetPos = new Vector3(pos.x + 50, pos.y + 80, 0);
        }
        else
        {
            if (gameObject.name.Contains("Sigh"))
            {
                targetPos = new Vector3(pos.x - 60, pos.y - 60, 0);
            }
            else
            {
                targetPos = new Vector3(pos.x + 40, pos.y + 80, 0);
                time = 2f;
            }
        }
        UnityEngine.UI.Image i = GetComponent<UnityEngine.UI.Image>();

        Move(r, i, targetPos, time);
        yield return new WaitForSeconds(time);

        if (isAngry)
        {
            r.localPosition = pos;
            i.color = Color.white;
            Move(r, i, targetPos, time);
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }

    protected void Move(RectTransform r, UnityEngine.UI.Image i, Vector3 targetPos, float time)
    {
        DOTween.To(
        () => r.localPosition,
        p => r.localPosition = p,
        targetPos,
        time
        );

        DOTween.ToAlpha(
           () => i.color,
           c => i.color = c,
           0f,
           time
       );
    }
}
