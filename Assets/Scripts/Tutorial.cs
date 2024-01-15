using TMPro;
using UnityEngine;
using System;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance { get; private set; }
    public TMP_Text tutorial;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Tutorial > 1 in scene");
        }
        instance = this;

        SetTextTutorial("Welcome to The test\n 1.Pick all item and check inventory");
    }

    public void SetTextTutorial(string text)
    {
        tutorial.text = text;
    }
}