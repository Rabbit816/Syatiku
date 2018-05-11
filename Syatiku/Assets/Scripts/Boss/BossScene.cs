using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : MonoBehaviour {

    static Vector3 touchStartPos;

    void Start () {

	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }
	}

    public static void SetMoveForce(out Vector3 moveDir, out float moveSpeed)
    {
        //離した位置
        Vector3 touchEndPos = Input.mousePosition;
        //フリックの長さ
        Vector3 flickLength = touchEndPos - touchStartPos;

        moveDir = flickLength.normalized;
        moveSpeed = flickLength.magnitude / 100;
    }
}
