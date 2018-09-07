using UnityEngine;

public class SanctionPartController : MonoBehaviour {

    [SerializeField]
    RectTransform slappedBoss;

    [SerializeField]
    float endTime;
    float timer = 0;
    //叩いた回数
    int slappedCount = 0;

    public void UpdateSanctionPart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (slappedCount == 0)
            {
                BossScene.Instance.ChangeBossState(slappedBoss.gameObject);
            }
            slappedCount++;
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
            slappedCount = 0;
            slappedBoss.gameObject.SetActive(false);
            BossScene.Instance.ChangePart();
        }
    }
}
