using System;

public class DialogueEvents
{
    public event Action<DialogueInfoSO> onDialogueStart;
    public void DialogueStart(DialogueInfoSO dialogue)
    {
        if (onDialogueStart != null)
        {
            onDialogueStart(dialogue);
        }
    }

    public event Action<QuestInfoSO> onDialogueFinish;
    public void DialogueFinish(QuestInfoSO quest)
    {
        if (onDialogueFinish != null)
        {
            onDialogueFinish(quest);
        }
    }

    public event Action<QuestStep> onAddQuestStep;
    public void AddQuestStep(QuestStep quest)
    {
        if (onAddQuestStep != null)
        {
            onAddQuestStep(quest);
        }
    }
}