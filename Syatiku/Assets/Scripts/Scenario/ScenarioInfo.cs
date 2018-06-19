using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenarioInfo
{
    public string message;
    public List<System.Action> commandActionList;

    public ScenarioInfo(string message = "")
    {
        this.message = message;
        commandActionList = new List<System.Action>();
    }

}
