using UnityEngine;
using DG.Tweening;

public class HackMeishi : MonoBehaviour {

    [SerializeField, Tooltip("PaperPrefab")]
    private GameObject paper_prefab;
    private RectTransform Meishi_rect;
    private GameObject MeishiObject;
    private HackTap hack_tap;
    private GameObject Damy_meishi;

    private GameObject GetWord;
    [HideInInspector]
    public bool _document = false;

    // Use this for initialization
    void Start () {
        GetWord = GameObject.Find("Canvas/Check/GetWord");
        Meishi_rect = GameObject.Find("Canvas/Zoom/Meishi/meishi_move").GetComponent<RectTransform>();
        MeishiObject = GameObject.Find("Canvas/Zoom/Meishi/Image");
        Damy_meishi = GameObject.Find("Canvas/Desk/Meishi");
        MeishiObject.SetActive(false);
        hack_tap = GameObject.Find("controll").GetComponent<HackTap>();
        _document = false;
    }

    public void MeishiPrefab()
    {
        MeishiObject.SetActive(true);
        hack_tap.ZoomActive(5);
    }

    /// <summary>
    /// 名刺タップの処理
    /// </summary>
    public void MeishiTap()
    {
        if (Meishi_rect.transform.childCount == 2)
            return;
        GameObject _get_doc = Instantiate(paper_prefab, GetWord.transform);
        _get_doc.transform.SetAsLastSibling();
        GameObject obj = new GameObject();
        obj.transform.SetParent(Meishi_rect.transform, false);
        Damy_meishi.SetActive(false);
        _document = true;

        Sequence s = DOTween.Sequence();
        s.Append(Meishi_rect.DOLocalMove(new Vector3(374, 221, 0), 0.7f))
            .Join(Meishi_rect.DOScale(0.2f, 0.7f))
            .OnComplete(() => Meishi_rect.gameObject.SetActive(false));
    }
}
