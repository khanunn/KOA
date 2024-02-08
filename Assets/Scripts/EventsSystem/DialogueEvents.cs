using System;
using UnityEngine;

public class DialogueEvents
{
    /* public event Action<DialogueInfoSO> onDialogueStart;
    public void DialogueStart(DialogueInfoSO dialogue)
    {
        if (onDialogueStart != null)
        {
            onDialogueStart(dialogue);
        }
    } */
    public event Action<QuestInfoSO, QuestState> onDialogueStart;
    public void DialogueStart(QuestInfoSO quest, QuestState questState)
    {
        if (onDialogueStart != null)
        {
            onDialogueStart(quest, questState);
        }
    }

    public event Action<QuestInfoSO, QuestState> onDialogueFinish;
    public void DialogueFinish(QuestInfoSO quest, QuestState questState)
    {
        if (onDialogueFinish != null)
        {
            onDialogueFinish(quest, questState);
        }
    }

    public event Action<GameObject> onAddQuestStep;
    public void AddQuestStep(GameObject obj)
    {
        if (onAddQuestStep != null)
        {
            onAddQuestStep(obj);
        }
    }

    public event Action<int, int> onUpdateAmount;
    public void UpdateAmount(int current, int require)
    {
        if (onUpdateAmount != null)
        {
            onUpdateAmount(current, require);
        }
    }

    public event Action onDialogueCancle;
    public void DialogueCancle()
    {
        if (onDialogueCancle != null)
        {
            onDialogueCancle();
        }
    }
}