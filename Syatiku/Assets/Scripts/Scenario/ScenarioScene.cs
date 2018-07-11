using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioScene : MonoBehaviour {

    [SerializeField]
    string filePath;

    void Start () {
        ScenarioController.Instance.BeginScenario(filePath);
    }
}
