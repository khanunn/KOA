using System.Drawing;
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
    [SerializeField] private TMP_Text requiredAmount;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject rewardItem;
    [SerializeField] private Transform rewardContent;
    private QuestState currentQuestState;
    private QuestStep questStep;
    private string questId;
    private List<GameObject> allReward = new List<GameObject>();
    private List<GameObject> allQuestStep = new List<GameObject>();
    private void OnEnable()
    {
        EventManager.instance.dialogueEvents.onDialogueStart += StartDialogue;
        EventManager.instance.dialogueEvents.onDialogueFinish += FinishDialogue;
        EventManager.instance.dialogueEvents.onAddQuestStep += AddQuestStepDialogue;
        EventManager.instance.dialogueEvents.onUpdateAmount += UpdateAmount;
        EventManager.instance.dialogueEvents.onDialogueCancle += CancleDialogue;
    }

    private void OnDisable()
    {
        EventManager.instance.dialogueEvents.onDialogueStart -= StartDialogue;
        EventManager.instance.dialogueEvents.onDialogueFinish -= FinishDialogue;
        EventManager.instance.dialogueEvents.onAddQuestStep -= AddQuestStepDialogue;
        EventManager.instance.dialogueEvents.onUpdateAmount -= UpdateAmount;
        EventManager.instance.dialogueEvents.onDialogueCancle -= CancleDialogue;
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

    /* private void StartDialogue(DialogueInfoSO info)
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
    } */
    private void StartDialogue(QuestInfoSO info, QuestState questState)
    {
        currentQuestState = questState;
        questId = info.id;
        Debug.Log("QuestState: " + currentQuestState);
        questDialogObj.SetActive(true);
        /* Dialog.AddRange(info.DialogueInfoSOFinish.messageText);
        DialogNames.AddRange(info.DialogueInfoSOFinish.nameText); */

        questTitle.text = info.DialogTitle;
        questDescription.text = info.DialogDescription[0];
        currentAmount.text = questStep.questStepCurrent.ToString();
        requiredAmount.text = questStep.questStepToComplete.ToString();
        objectiveText.text = info.displayName;
        buttonText.text = "Accept";

        InstantiateReward(info);
    }
    private void FinishDialogue(QuestInfoSO info, QuestState questState)
    {
        currentQuestState = questState;
        questId = info.id;
        Debug.Log("QuestState: " + currentQuestState);
        questDialogObj.SetActive(true);
        /* Dialog.AddRange(info.DialogueInfoSOFinish.messageText);
        DialogNames.AddRange(info.DialogueInfoSOFinish.nameText); */

        questTitle.text = info.DialogTitle;
        questDescription.text = info.DialogDescription[1];
        /* currentAmount.text = "";
        requiredAmount.text = ""; */
        currentAmount.text = questStep.questStepToComplete.ToString();
        requiredAmount.text = questStep.questStepToComplete.ToString();
        objectiveText.text = info.displayName;
        buttonText.text = "Finish";

        InstantiateReward(info);
    }
    private void CancleDialogue()
    {
        questDialogObj.SetActive(false);
        foreach (var item in allReward)
        {
            Destroy(item);
        }
        questTitle.text = "";
        questDescription.text = "";
        currentAmount.text = "";
        requiredAmount.text = "";
        objectiveText.text = "";
        buttonText.text = "";
    }

    private void AddQuestStepDialogue(GameObject obj)
    {
        Debug.Log("Dialog OBJ: " + obj);
        allQuestStep.Add(obj);
        questStep = obj.GetComponent<QuestStep>();
    }

    public void ButtonQuestAccept()
    {
        if (currentQuestState.Equals(QuestState.CAN_START))
        {
            Debug.Log("Accept Quest");
            EventManager.instance.questEvents.StartQuest(questId);

        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH))
        {
            Debug.Log("Finished Quest");
            EventManager.instance.questEvents.FinishQuest(questId);
        }

        CancleDialogue();
    }
    public void ButtonQuestCloseWindow()
    {
        CancleDialogue();
    }

    private void InstantiateReward(QuestInfoSO questInfoSO)
    {
        /* GameObject obj = Instantiate(rewardItem, rewardContent);
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>(); */
        foreach (ItemInfoSO item in questInfoSO.Items)
        {
            GameObject obj = Instantiate(rewardItem, rewardContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcon.sprite = item.Icon;
            allReward.Add(obj);
        }
    }

    private void UpdateAmount(int current, int require)
    {
        currentAmount.text = current.ToString();
        requiredAmount.text = require.ToString();
    }
}

