using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI DialogText;
    [SerializeField] GameObject DialogObject;
    [SerializeField] Button NextButton;
    public List<string> DialogNames = new List<string>();
    public List<string> Dialog = new List<string>();

    [SerializeField] int CurrentDisplay = 0;

    private void Start()
    {
        NameText.text = DialogNames[CurrentDisplay];
        StartCoroutine(AnimateText(Dialog[CurrentDisplay]));
        NextButton.interactable = false;
        CurrentDisplay++;
        print(Dialog.Count);
    }

    public void NextDialog()
    {
        if (CurrentDisplay < Dialog.Count)
        {
            // Stop the current text animation if any
            DOTween.Complete(DialogText);

            // Update text and start the animation
            NameText.text = DialogNames[CurrentDisplay];
            StartCoroutine(AnimateText(Dialog[CurrentDisplay]));
            NextButton.interactable = false;
            CurrentDisplay++;
        }
        else
        {
            // Fade out and hide the dialog object
            DialogText.DOFade(0f, 0.5f).OnComplete(() =>
            {
                DialogObject.SetActive(false);
                CurrentDisplay = 0;
            });
        }
    }

    public void SkipDialog()
    {
        // Hide dialog object immediately
        DialogObject.SetActive(false);
        CurrentDisplay = 0;
    }

    public void ShowDialog()
    {
        // Show dialog object immediately
        DialogObject.SetActive(true);
    }

    IEnumerator AnimateText(string text)
    {
        DialogText.text = "";
        foreach (char c in text)
        {
            DialogText.text += c;
            yield return new WaitForSeconds(0.05f); // Adjust the delay as needed
        }
        NextButton.interactable = true;
    }
}
