using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class QuestPoint : MonoBehaviour
{
    [Header("Dialogue")]
    //[SerializeField] private DialogueInfoSO dialogueInfoSO;
    //[SerializeField] private DialogueInfoSO dialogueInfoSO;
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;
    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;
    public bool playerIsNear { get; private set; } = false;
    private string questId;
    private QuestState currentQuestState;
    private QuestIcon questIcon;
    private Interactable target;
    public GameObject canvas;
    //private UIController uIController;
    private DialogueInfoSO[] questDialog;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
        target = GetComponent<Interactable>();
        //uIController = GetComponentInChildren<UIController>();
        canvas.SetActive(false);
        //Debug.Log("Success QuestPoint");
    }
    private void OnEnable()
    {
        EventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        EventManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }
    private void OnDisable()
    {
        EventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        EventManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }

    private void SubmitPressed()
    {
        if (!playerIsNear)
        {
            //Debug.Log("ผู้เล่นอยู่ห่างจาก NPC");
            return;
        }

        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            EventManager.instance.questEvents.StartQuest(questId);
            //Debug.Log("Quest ID from QuestPoint: " + questId);
            //uIController.SetQuestToUI(questId);
            canvas.SetActive(true);
            EventManager.instance.dialogueEvents.DialogueStart(questInfoForPoint.DialogueInfoSOStart);
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            EventManager.instance.questEvents.FinishQuest(questId);
            canvas.SetActive(false);
            EventManager.instance.dialogueEvents.DialogueFinish(questInfoForPoint.DialogueInfoSOFinish);
        }
    }
    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
            //Debug.Log("Quest With Id: " + questId + " Update to State: " + currentQuestState);
        }
    }

    public void PlayerIsNear(bool isNear)
    {
        playerIsNear = isNear;
        //Debug.Log("PlayerIsNear: " + isNear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            //Debug.Log("PlayerIsNear: " + playerIsNear + " from QuestPoint");

            target = FindFirstObjectByType<PlayerController>().GetComponent<Interactable>();
            //Debug.Log("Target : " + target + "Type " + target.interactionType);
            target.myPlayer.InteractableChange(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            //Debug.Log("PlayerIsNear: " + playerIsNear + " from QuestPoint");
        }
    }
}
