using UnityEngine.UI;
using UnityEngine;

public class ScenarioWindow : MonoBehaviour
{

    public Vector3 closeMenuPos;
    public Vector3 opneMenuPos;

    public Text name;
    public Text message;
    public Text logText;
    public GameObject log;
    public GameObject recommendLight;
    public Image[] characters;
    public Image[] icons;
    public Sprite[] menuSprites;
    public RectTransform menu;
    public Image menuButton;

    public CanvasGroup scenarioCanvas;

    void Start()
    {
        closeMenuPos = menu.localPosition;
    }

    public void Init()
    {
        //メニュー
        menu.localPosition = closeMenuPos;
        menu.gameObject.SetActive(true);
        menuButton.sprite = menuSprites[0];
        //キャラ、アイコン
        for (int i = 0;i < characters.Length; i++)
        {
            characters[i].gameObject.SetActive(false);
            icons[i].gameObject.SetActive(false);
        }
        recommendLight.SetActive(false);
        //テキスト
        logText.text = "";
        name.text = "";
        message.text = "";
        //キャンバス
        scenarioCanvas.alpha = 0.01f;
        gameObject.SetActive(false);
    }
}