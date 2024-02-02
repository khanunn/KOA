using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;


public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    [SerializeField] TMP_Text NameText;
    [SerializeField] TMP_Text DialogText;
    [SerializeField] GameObject DialogObject;
    [SerializeField] Button NextButton;
    public List<string> DialogNames = new List<string>();
    public List<string> Dialog = new List<string>();

    [SerializeField] int CurrentDisplay = 0;

    [Header("New Dialog Finish")]
    [SerializeField] private GameObject questDialogObj;
    [SerializeField] private TMP_Text questTitle;
    [SerializeField] private TMP_Text questDescription;
    [SerializeField] private TMP_Text currentAmount;
    [SerializeField] private TMP_Text RequiredAmount;
    private void OnEnable()
    {
        EventManager.instance.dialogueEvents.onDialogueStart += StartDialogue;
        EventManager.instance.dialogueEvents.onDialogueFinish += FinishDialogue;
        EventManager.instance.dialogueEvents.onAddQuestStep += AddQuestStepDialogue;
    }

    private void OnDisable()
    {
        EventManager.instance.dialogueEvents.onDialogueStart -= StartDialogue;
        EventManager.instance.dialogueEvents.onDialogueFinish -= FinishDialogue;
        EventManager.instance.dialogueEvents.onAddQuestStep -= AddQuestStepDialogue;
    }

    private void Start()
    {
        if (instance != null)
        {
            Debug.LogError("DialogueManager > 1");
        }
        instance = this;
        /* NameText.text = DialogNames[CurrentDisplay];
        StartCoroutine(AnimateText(Dialog[CurrentDisplay]));
        NextButton.interactable = false;
        CurrentDisplay++;
        print(Dialog.Count); */
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
            yield return new WaitForSeconds(0.005f); // Adjust the delay as needed
        }
        NextButton.interactable = true;
    }

    private void StartDialogue(DialogueInfoSO info)
    {
        ShowDialog();
        Dialog.AddRange(info.messageText);
        DialogNames.AddRange(info.nameText);

        //Dialog = info.messageText;
        NameText.text = DialogNames[CurrentDisplay];
        StartCoroutine(AnimateText(Dialog[CurrentDisplay]));
        NextButton.interactable = false;
        CurrentDisplay++;
        print(Dialog.Count);
    }
    private void FinishDialogue(QuestInfoSO info)
    {
        questDialogObj.SetActive(true);
        Dialog.AddRange(info.DialogueInfoSOFinish.messageText);
        DialogNames.AddRange(info.DialogueInfoSOFinish.nameText);

        questTitle.text = DialogNames[CurrentDisplay];
        questDescription.text = Dialog[CurrentDisplay];

    }

    private void AddQuestStepDialogue(QuestStep step)
    {
        currentAmount.text = step.questStepCurrent.ToString();
        RequiredAmount.text = step.questStepToComplete.ToString();
    }
}

