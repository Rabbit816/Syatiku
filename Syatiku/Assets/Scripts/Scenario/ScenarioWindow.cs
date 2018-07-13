using UnityEngine.UI;
using UnityEngine;

public class ScenarioWindow : MonoBehaviour
{
    Vector3 startMenuPos;
    [SerializeField]
    Vector3 targetMenuPos;
    public Image bgi;

    public Text name;
    public Text message;
    public Text skipText;
    public Text logText;
    public Text autoText;
    public GameObject log;
    public GameObject recommendLight;
    public Image[] characters;
    public Image[] icons;
    public Sprite[] menuSprites;
    public RectTransform menu;
    public Image menuButton;

    public CanvasGroup scenarioCanvas;

    private void Start()
    {
        startMenuPos = menu.localPosition;
    }

    public void Init()
    {
        //背景
        bgi.sprite = null;
        //メニュー
        menu.localPosition = startMenuPos;
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
        skipText.text = "スキップ";
        autoText.text = "オート";
        logText.text = "";
        name.text = "";
        message.text = "";
        //キャンバス
        scenarioCanvas.alpha = 0.01f;
        gameObject.SetActive(false);
    }
}