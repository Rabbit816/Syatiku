using UnityEngine;

public class SanctionPartController : MonoBehaviour {

    [SerializeField]
    DamageGageController damageGageController;
    [SerializeField]
    Sprite[] slappedBossSprites;
    [SerializeField]
    RectTransform slappedBoss;
    GameObject slappedBossGO;

    [SerializeField]
    float endTime;
    float timer = 0;

    public void Initialize(int gameMode)
    {
        slappedBoss.GetComponent<UnityEngine.UI.Image>().sprite = slappedBossSprites[gameMode - 1];
        slappedBossGO = slappedBoss.gameObject;
    }

    public void UpdateSanctionPart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!slappedBossGO.activeSelf)
            {
                BossScene.Instance.ChangeBossState(slappedBossGO);
            }
            damageGageController.ChangeDamagePoint();
            Vector3 bossScale = slappedBoss.localScale;
            bossScale.x *= -1;
            //向きの変更
            slappedBoss.localScale = bossScale;
        }

        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        if (timer > endTime)
        {
            timer = 0;
            slappedBoss.gameObject.SetActive(false);
            BossScene.Instance.ChangePart();
        }
    }

    public float Result()
    {
        return damageGageController.Result();
    }
}
