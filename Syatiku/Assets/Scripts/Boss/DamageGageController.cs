using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageGageController : MonoBehaviour {

    Image damageGage;

    [SerializeField, Header("最大ダメージ量")]
    float damagePointMax;
    [HideInInspector]
    public float damagePoint;
    [SerializeField, Header("ダメージ上昇値")]
    float damageUpNum = 1f;
    [SerializeField, Header("ダメージ減少値")]
    float damageDownNum = 0.5f;

    [SerializeField, Header("ゲージの変化時間")]
    float changeTime = 1f;

    void Start () {
        damageGage = GetComponent<Image>();
        damageGage.fillAmount = 0;
        damagePoint = 0;
	}

    public void DamagePointUp()
    {
        if (damagePoint < damagePointMax)
        {
            damagePoint += damageUpNum;
            ChangeDamagePoint();
        }
    }

    public void DamagePointDown()
    {
        if (damagePoint > 0)
        {
            damagePoint -= damageDownNum;
            ChangeDamagePoint();
        }
    }

    void ChangeDamagePoint()
    {
        float targetNum = damagePoint / damagePointMax;
        DOTween.To(
            () => damageGage.fillAmount,
            num => damageGage.fillAmount = num,
            targetNum,
            changeTime
            );
    }
}
