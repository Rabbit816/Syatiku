using System.Collections;
using UnityEngine;

public class JoyAnimation : MonoBehaviour {

	void Start () {
       StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        Vector3 turn = new Vector3(-1, 1, 1);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.3f);
            transform.localScale = turn;
            yield return new WaitForSeconds(0.3f);
            transform.localScale = Vector3.one;
        }
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
