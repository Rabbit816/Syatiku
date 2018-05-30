using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public Text numberText;
    private int number;

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        numberText.text = number.ToString();
    }

    private void Initialize()
    {
        number = 0;
    }

    public void Addpoint(int point)
    {
        number = number + point;
    }

    public void Save()
    {
        Initialize();
    }
}

