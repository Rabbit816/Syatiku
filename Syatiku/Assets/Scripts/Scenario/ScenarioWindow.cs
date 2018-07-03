using UnityEngine.UI;
using UnityEngine;

public class ScenarioWindow : MonoBehaviour
{

    public Image bgi;

    public Text name;
    public Text message;
    public Text skipText;
    public Text logText;
    public Text autoText;
    public GameObject log;
    public GameObject recommendIcon;
    public Image[] characters;
    public Image[] icons;

    public CanvasGroup scenarioCanvas;

    public void Init()
    {
        bgi.sprite = null;
        bgi.color = new Color(1f,1f,1f,0);
        recommendIcon.SetActive(false);
        skipText.text = "スキップ";
        autoText.text = "オート";
        logText.text = "";
        for (int i = 0;i < characters.Length; i++)
        {
            characters[i].gameObject.SetActive(false);
            icons[i].gameObject.SetActive(false);
        }
        scenarioCanvas.alpha = 0;
        gameObject.SetActive(false);
    }
}