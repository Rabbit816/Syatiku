using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageGageController : MonoBehaviour {

    Image damageGage;
    [SerializeField, Header("最大ダメージ量")]
    float damagePointMax;
    [SerializeField, Header("ゲージの変化時間")]
    float changeTime = 0.5f;
    int damage = 0;

    void Start () {
        damageGage = GetComponent<Image>();
        damageGage.fillAmount = 0;
	}

    public void ChangeDamagePoint()
    {
        if (damage >= damagePointMax) return;

        damage++;
        float targetNum = damage / damagePointMax;
        DOTween.To(
            () => damageGage.fillAmount,
            num => damageGage.fillAmount = num,
            targetNum,
            changeTime
        );
    }

    public float Result()
    {
        return damage / damagePointMax;
    }
}
